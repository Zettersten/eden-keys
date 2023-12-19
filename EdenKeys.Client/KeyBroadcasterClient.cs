using EdenKeys.Shared;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace EdenKeys.Client;

public class KeyBroadcasterClient
{
    private readonly TcpClient tcpClient;
    private readonly string serverIp;
    private readonly int serverPort;

    public KeyBroadcasterClient(string ip, int port)
    {
        this.serverIp = ip;
        this.serverPort = port;
        this.tcpClient = new TcpClient();
    }

    public void ConnectToServer()
    {
        try
        {
            Thread.Sleep(1000);

            this.tcpClient.Connect(serverIp, serverPort);

            Console.WriteLine("Connected to server.");

            ListenForMessages();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ListenForMessages()
    {
        var stream = tcpClient.GetStream();
        var buffer = new byte[1024];
        var executor = new KeyExecutor();

        try
        {
            while (true)
            {
                var bytesRead = stream.Read(buffer);

                if (bytesRead == 0)
                {
                    continue;
                }

                var message = Encoding.ASCII.GetString(buffer, 0, bytesRead);


                try
                {
                    if (message.Contains("}{"))
                    {
                        foreach (var item in message.Split("}{"))
                        {
                            var messageItem = item;

                            if (!messageItem.StartsWith('{'))
                            {
                                messageItem = "{" + messageItem;
                            }

                            if (!messageItem.EndsWith('}'))
                            {
                                messageItem += "}";
                            }

                            var keystrokeEvent = JsonSerializer
                                .Deserialize<KeystrokeEvent>(messageItem);

                            Console.WriteLine("Received: " + messageItem);

                            executor.EnqueueKey(keystrokeEvent);
                        }
                    } 
                    else
                    {
                        var keystrokeEvent = JsonSerializer
                            .Deserialize<KeystrokeEvent>(message);

                        Console.WriteLine("Received: " + message);

                        executor.EnqueueKey(keystrokeEvent);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bad JSON: " + ex.Message);
                }

                Thread.Sleep(50);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            tcpClient.Close();
        }
    }
}