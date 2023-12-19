using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using System.Net;
using System.Net.Sockets;

namespace EdenKeys.Server;

public class KeyBroadcasterServer
{
    public void StartServer()
    {
        var tcpListener = new TcpListener(IPAddress.Any, 3000);

        tcpListener.Start();

        var clientPool = new ClientPool(tcpListener);

        var keyboardListener = new Listener
        {
            AllKeys = true
        };

        // Throttling Logic
        var keyBuffer = new List<Keycode>();

        var timer = new System.Threading.Timer(_ =>
        {
            ProcessKeyBuffer(keyBuffer, clientPool);
            keyBuffer.Clear();
        }, null, Timeout.Infinite, Timeout.Infinite);

        int throttleInterval = 350; // Time in milliseconds

        keyboardListener.KeyPressed += (Keycode keycodes) =>
        {
            keyBuffer.Add(keycodes);
            timer.Change(throttleInterval, Timeout.Infinite);
        };

        keyboardListener.Listen();

        Console.WriteLine("Server started on port 3000.");

        try
        {
            while (true)
            {
                if (clientPool.IsDisposed)
                {
                    break;
                }

                Thread.Sleep(50);
            }
        }
        finally
        {
            keyboardListener.StopListening();
            clientPool.Dispose();
        }
    }

    private static void ProcessKeyBuffer(List<Keycode> keyBuffer, ClientPool clientPool)
    {
        if (keyBuffer.Count == 0)
        {
            return;
        }

        var distinctKeys = keyBuffer
            .Distinct();

        Keycode? keycode = null;
        KeystrokeModifiers? keyStrokeModifier = null;

        foreach (var key in distinctKeys)
        {
            if (IsModifierKey(key, out var modifier))
            {
                keyStrokeModifier = modifier;
                continue;
            }

            keycode = key;
        }

        if (keycode is null)
        {
            return;
        }

        switch (keycode)
        {
            case Keycode.VK_LBUTTON:
            case Keycode.VK_MBUTTON:
            case Keycode.VK_RBUTTON:
            case Keycode.VK_TAB:
                return;
        }

        var keyStrokeEvent = new KeystrokeEvent(keycode.Value, keyStrokeModifier);

        clientPool.SendToAllClients(keyStrokeEvent);
    }

    private static bool IsModifierKey(Keycode keycode, out KeystrokeModifiers? modifier)
    {
        switch (keycode)
        {
            case Keycode.VK_LMENU:
            case Keycode.VK_RMENU:
            case Keycode.VK_MENU:
                modifier = KeystrokeModifiers.Alt;
                return true;

            case Keycode.VK_LSHIFT:
            case Keycode.VK_RSHIFT:
            case Keycode.VK_SHIFT:
                modifier = KeystrokeModifiers.Shift;
                return true;

            case Keycode.VK_LCONTROL:
            case Keycode.VK_RCONTROL:
            case Keycode.VK_CONTROL:
                modifier = KeystrokeModifiers.Control;
                return true;

            case Keycode.VK_LWIN:
            case Keycode.VK_RWIN:
                modifier = KeystrokeModifiers.Windows;
                return true;

            default:
                modifier = null;
                return false;
        }
    }
}