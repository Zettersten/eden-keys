using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Num_1 : ExecutionBase
{
    /// <summary>
    /// Targeted Attack 1
    /// </summary>
    /// <param name="simulator"></param>
    public Num_1(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F1, null);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 3
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            // Release Alt + 3
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);
        };
    }
}