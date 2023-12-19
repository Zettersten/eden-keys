using EdenKeys.GameMechanics;
using EdenKeys.Shared;
using SharpHook;
using System.ComponentModel;

namespace EdenKeys.Client;

public class KeyExecutor
{
    private readonly EventSimulator simulator;
    private readonly BackgroundWorker bw;
    private readonly Queue<KeystrokeEvent> messageQueue;
    private readonly ExecutionRegistry executionRegistry;

    public KeyExecutor()
    {
        this.simulator = new EventSimulator();
        this.bw = new BackgroundWorker();
        this.bw.DoWork += new DoWorkEventHandler(HandleKeyExecution);
        this.bw.WorkerSupportsCancellation = true;
        this.messageQueue = new Queue<KeystrokeEvent>();

        this.executionRegistry = new ExecutionRegistry();

        this.executionRegistry.Register(new TargetAndFollow_F10(this.simulator));
        this.executionRegistry.Register(new SpamAOE_F11(this.simulator));
        this.executionRegistry.Register(new Buffs_F12(this.simulator)); 
        this.executionRegistry.Register(new TargetAndAttack_F9(this.simulator));

        this.executionRegistry.Register(new Char_V(this.simulator));

        this.executionRegistry.Register(new Ctrl_1(this.simulator));
        this.executionRegistry.Register(new Ctrl_2(this.simulator));
        this.executionRegistry.Register(new Ctrl_3(this.simulator));
        this.executionRegistry.Register(new Ctrl_4(this.simulator));
        this.executionRegistry.Register(new Ctrl_5(this.simulator));

        this.executionRegistry.Register(new Num_1(this.simulator));
        this.executionRegistry.Register(new Num_2(this.simulator));
        this.executionRegistry.Register(new Num_3(this.simulator));
        this.executionRegistry.Register(new Num_4(this.simulator));
        this.executionRegistry.Register(new Num_5(this.simulator));

        this.executionRegistry.Register(new Alt_1(this.simulator));
        this.executionRegistry.Register(new Alt_2(this.simulator));
        this.executionRegistry.Register(new Alt_3(this.simulator));
        this.executionRegistry.Register(new Alt_4(this.simulator));
        this.executionRegistry.Register(new Alt_5(this.simulator));

        this.bw.RunWorkerAsync();
    }

    public void EnqueueKey(KeystrokeEvent message)
    {
        messageQueue.Enqueue(message);
    }

    private void HandleKeyExecution(object? sender, DoWorkEventArgs e)
    {
        var cancellationSource = new CancellationTokenSource();

        while (true)
        {
            if (messageQueue.Count == 0)
            {
                Thread.Sleep(50);
                continue;
            }

            var message = messageQueue
                .Dequeue();

            if (message.KeyCode == Shared.KeyboardListener.Keycode.VK_ESCAPE)
            {
                cancellationSource.Cancel();

                Thread.Sleep(50);

                cancellationSource = new CancellationTokenSource();

                continue;
            }

            executionRegistry
                .RunIfExists(message, cancellationSource.Token);

            if (bw.CancellationPending)
            {
                break;
            };

            Thread.Sleep(50);
        }
    }

    public void Dispose()
    {
        if (bw.IsBusy)
        {
            bw.CancelAsync();
        }

        messageQueue.Clear();
        GC.SuppressFinalize(this);
    }
}