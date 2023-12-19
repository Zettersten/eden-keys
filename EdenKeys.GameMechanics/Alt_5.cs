using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Alt_5 : ExecutionBase
{
    /// <summary>
    /// Targetted Heal
    /// </summary>
    /// <param name="simulator"></param>
    public Alt_5(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F5, KeystrokeModifiers.Alt);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 5
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftAlt);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc5);
            Thread.Sleep(50);

            // Release Alt + 5
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc5);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftAlt);
            Thread.Sleep(50);
        };
    }
}