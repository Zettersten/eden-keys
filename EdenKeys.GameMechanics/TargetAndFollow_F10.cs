using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class TargetAndFollow_F10 : ExecutionBase
{
    /// <summary>
    /// Target & Stick
    /// </summary>
    /// <param name="simulator"></param>
    public TargetAndFollow_F10(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F10, null);

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

            // Alt + 1
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftAlt);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            // Release Alt + 1
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftAlt);
            Thread.Sleep(50);
        };
    }
}