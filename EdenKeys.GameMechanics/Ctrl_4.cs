using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Ctrl_4 : ExecutionBase
{
    /// <summary>
    /// Assist
    /// </summary>
    /// <param name="simulator"></param>
    public Ctrl_4(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F4, KeystrokeModifiers.Control);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 3
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftControl);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc4);
            Thread.Sleep(50);

            // Release Alt + 3
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc4);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftControl);
            Thread.Sleep(50);
        };
    }
}