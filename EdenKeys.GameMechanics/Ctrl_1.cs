using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Ctrl_1 : ExecutionBase
{
    /// <summary>
    /// Target
    /// </summary>
    /// <param name="simulator"></param>
    public Ctrl_1(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F1, KeystrokeModifiers.Control);

        this.TaskRunner = (simulator) =>
        {
            // Ctrl + 1
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftControl);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            // Release Ctrl + 1
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftControl);
            Thread.Sleep(50);
        };
    }
}