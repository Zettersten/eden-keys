using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class SpamAOE_F11 : ExecutionBase
{
    /// <summary>
    /// Spam AOE Attach & Group Heal
    /// </summary>
    /// <param name="simulator"></param>
    public SpamAOE_F11(EventSimulator simulator) : base(simulator, true)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F11, null);
        this.DelayBetweenLoop = TimeSpan.FromMilliseconds(2_000);

        this.TaskRunner = (simulator) =>
        {
            // Alt + 3
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftAlt);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc3);
            Thread.Sleep(50);

            // Release Alt + 3
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc3);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftAlt);
            Thread.Sleep(50);
        };
    }
}