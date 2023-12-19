using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Num_3 : ExecutionBase
{
    /// <summary>
    /// Targeted Attack 3
    /// </summary>
    /// <param name="simulator"></param>
    public Num_3(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F3, null);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 3
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc3);
            Thread.Sleep(50);

            // Release Alt + 3
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc3);
            Thread.Sleep(50);
        };
    }
}