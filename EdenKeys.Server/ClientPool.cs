using EdenKeys.Shared;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace EdenKeys.Server;

public class ClientPool : IDisposable
{
    private readonly List<TcpClient> clients;
    private readonly BackgroundWorker bw;
    private readonly TcpListener tcpListener;

    public bool IsDisposed = false;

    public ClientPool(TcpListener tcpListener)
    {
        this.clients = [];
        this.tcpListener = tcpListener;

        this.bw = new();
        this.bw.DoWork += new DoWorkEventHandler(HandleClientConnectionEvents);
        this.bw.WorkerSupportsCancellation = true;

        this.bw.RunWorkerAsync();
    }

    public void Dispose()
    {
        if (bw.IsBusy)
        {
            bw.CancelAsync();
        }

        tcpListener.Dispose();

        GC.SuppressFinalize(this);

        IsDisposed = true;
    }

    private List<TcpClient> GetClients()
    {
        var badClients = new List<TcpClient>();
        var goodClients = new List<TcpClient>();

        foreach (var client in clients)
        {
            if (!client.Connected)
            {
                badClients.Add(client);
            }

            goodClients.Add(client);
        }

        clients.RemoveAll(badClients.Contains);

        return goodClients;
    }

    public bool HasClients()
    {
        return GetClients().Count > 0;
    }

    private void HandleClientConnectionEvents(object? sender, DoWorkEventArgs e)
    {
        while (true)
        {
            var tcpClient = this.tcpListener.AcceptTcpClient();

            clients.Add(tcpClient);

            if (bw.CancellationPending)
            {
                break;
            };

            Thread.Sleep(50);
        }
    }

    public void SendToAllClients(KeystrokeEvent keyStrokeEvent)
    {
        if (!HasClients())
        {
            return;
        }

        var message = JsonSerializer
            .Serialize(keyStrokeEvent);

        var badClients = new List<TcpClient>();

        foreach (var client in GetClients())
        {
            if (!client.Connected)
            {
                badClients.Add(client);
                continue;
            }

            try
            {
                var networkStream = client.GetStream();

                Console.WriteLine($"Sending to {client.Client.RemoteEndPoint}: {message}");

                var messageBytes = Encoding.ASCII.GetBytes(message);

                networkStream.Write(messageBytes);
            }
            catch
            {
                badClients.Add(client);
            }
        }

        if (badClients.Count > 0)
        {
            clients.RemoveAll(badClients.Contains);
        }
    }
}