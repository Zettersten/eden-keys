using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EdenKeys.Shared.KeyboardListener;

public class Listener : IDisposable
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    private const int KEYEVENTF_KEYUP = 0x02;
    private const int KEYDOWN = -32767;
    private readonly List<Keycode> watchCodes;
    private readonly BackgroundWorker bw;

    /// <summary>
    /// Delegate for the event that is raised whenever a keypress that is being watched occurs.
    /// </summary>
    /// <param name="keycodes">Keycode that fired the event</param>
    public delegate void KeypressedEventHandler(Keycode keycodes);

    /// <summary>
    /// Event that is raised whenever a keypress that is being watched occurs.
    /// </summary>
    public event KeypressedEventHandler? KeyPressed;

    /// <summary>
    /// Listen for all keypresses
    /// </summary>
    public bool AllKeys = false;

    /// <summary>
    /// Adds a keycode that the listener should listen for.
    /// </summary>
    /// <param name="keycode">Keycode to be added to the watch list.</param>
    public void AddKeycode(Keycode keycode)
    {
        watchCodes.Add(keycode);
    }

    /// <summary>
    /// Removes a single keycode from the list of keycodes to listen for.
    /// </summary>
    /// <param name="keycode">Keycode to be removed from the watch list.</param>
    public void RemoveKeycode(Keycode keycode)
    {
        watchCodes.Remove(keycode);
    }

    /// <summary>
    /// Clears all keycodes that the listener is currently looking for.
    /// </summary>
    public void ClearKeycodes()
    {
        watchCodes.Clear();
    }

    /// <summary>
    /// Start listening for the selected keys
    /// </summary>
    public void Listen()
    {
        bw.RunWorkerAsync();
    }

    public Listener()
    {
        bw = new BackgroundWorker();
        bw.DoWork += new DoWorkEventHandler(HandleKeyboardEvents);
        bw.WorkerSupportsCancellation = true;

        watchCodes = [];
    }

    private void HandleKeyboardEvents(object? sender, DoWorkEventArgs e)
    {
        while (true)
        {
            if (AllKeys)
            {
                for (var i = 0; i < 255; i++)
                {
                    var ret = GetAsyncKeyState(i);

                    if (ret == KEYDOWN)
                    {
                        this.KeyPressed?.Invoke((Keycode)i);
                    }
                }
            }
            else if (watchCodes.Count > 0)
            {
                foreach (var iKey in watchCodes.Select(v => (int)v))
                {
                    var ret = GetAsyncKeyState(iKey);

                    if (ret != 0)
                    {
                        keybd_event((byte)iKey, 0x45, KEYEVENTF_KEYUP, 0);

                        this.KeyPressed?.Invoke((Keycode)iKey);
                    }
                }
            }

            if (bw.CancellationPending) break;

            Thread.Sleep(50);
        }
    }

    /// <summary>
    /// Stop listening for keystrokes
    /// </summary>
    public void StopListening()
    {
        bw.CancelAsync();
    }

    public void Dispose()
    {
        if (bw.IsBusy)
        {
            bw.CancelAsync();
        }

        GC.SuppressFinalize(this);
    }
}