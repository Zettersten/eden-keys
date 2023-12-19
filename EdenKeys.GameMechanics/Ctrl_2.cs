using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Ctrl_2 : ExecutionBase
{
    /// <summary>
    /// Assist
    /// </summary>
    /// <param name="simulator"></param>
    public Ctrl_2(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F2, KeystrokeModifiers.Control);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 3
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftControl);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc2);
            Thread.Sleep(50);

            // Release Alt + 3
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc2);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftControl);
            Thread.Sleep(50);
        };
    }
}