using EdenKeys.Shared;
using EdenKeys.Shared.KeyboardListener;
using SharpHook;

namespace EdenKeys.GameMechanics;

public class Buffs_F12 : ExecutionBase
{
    /// <summary>
    /// Full buff sequence
    /// </summary>
    /// <param name="simulator"></param>
    public Buffs_F12(EventSimulator simulator) : base(simulator, false)
    {
        this.Toggle = new KeystrokeEvent(Keycode.VK_F12, null);

        this.TaskRunner = (simulator) =>
        {
            // Alt
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.VcLeftAlt);
            Thread.Sleep(50);

            // 0
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc0);
            Thread.Sleep(50);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc0);
            Thread.Sleep(3_250);

            // 9
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc9);
            Thread.Sleep(50);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc9);
            Thread.Sleep(3_250);

            // 8
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc8);
            Thread.Sleep(50);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc8);
            Thread.Sleep(3_250);

            // 7
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc7);
            Thread.Sleep(50);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc7);
            Thread.Sleep(3_250);

            // 6
            simulator.SimulateKeyPress(SharpHook.Native.KeyCode.Vc6);
            Thread.Sleep(50);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.Vc6);
            Thread.Sleep(50);
            simulator.SimulateKeyRelease(SharpHook.Native.KeyCode.VcLeftAlt);
            Thread.Sleep(10_250);

        };
    }
}