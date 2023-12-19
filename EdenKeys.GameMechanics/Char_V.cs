using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Char_V : ExecutionBase
{
    /// <summary>
    /// Targeted Attack 1
    /// </summary>
    /// <param name="simulator"></param>
    public Char_V(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_V, null);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 3
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcV);
            Thread.Sleep(50);

            // Release Alt + 3
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcV);
            Thread.Sleep(50);
        };
    }
}