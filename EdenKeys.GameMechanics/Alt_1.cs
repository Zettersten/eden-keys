using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Alt_1 : ExecutionBase
{
    /// <summary>
    /// Horse Mount
    /// </summary>
    /// <param name="simulator"></param>
    public Alt_1(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F1, KeystrokeModifiers.Alt);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 4
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftAlt);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            // Release Alt + 4
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftAlt);
            Thread.Sleep(50);
        };
    }
}