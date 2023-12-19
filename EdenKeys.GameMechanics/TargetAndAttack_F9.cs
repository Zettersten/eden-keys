using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class TargetAndAttack_F9 : ExecutionBase
{
    /// <summary>
    /// Target & Attack
    /// </summary>
    /// <param name="simulator"></param>
    public TargetAndAttack_F9(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F9, null);

        this.TaskRunner = (simulator) =>
        {
            // Ctrl + 1
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftControl);
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            // Release Ctrl + 1
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            // Alt + 1
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc2);
            Thread.Sleep(50);

            // Release Alt + 1
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc2);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftControl);
            Thread.Sleep(50);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(1_500);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(1_500);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc1);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc1);
            Thread.Sleep(50);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc2);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc2);
            Thread.Sleep(50);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc3);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc3);
            Thread.Sleep(50);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc4);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc4);
            Thread.Sleep(50);

            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc5);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc5);
            Thread.Sleep(50);
        };
    }
}