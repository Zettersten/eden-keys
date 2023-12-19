using EdenKeys.Shared;
using SharpHook;
using System.ComponentModel;

namespace EdenKeys.GameMechanics;

public abstract class ExecutionBase
{
    protected readonly EventSimulator simulator;
    public readonly bool ShouldLoop;
    private readonly BackgroundWorker bw;
    private readonly Queue<Action> tasksToRun;

    public ExecutionBase(EventSimulator simulator, bool shouldLoop)
    {
        this.tasksToRun = new Queue<Action>();

        this.bw = new BackgroundWorker();
        this.bw.DoWork += new DoWorkEventHandler(ExecuteRunner);
        this.bw.WorkerSupportsCancellation = true;
        this.bw.RunWorkerAsync();

        this.simulator = simulator;
        this.ShouldLoop = shouldLoop;
    }

    private void ExecuteRunner(object? sender, DoWorkEventArgs e)
    {
        while (true)
        {
            if (bw.CancellationPending)
            {
                Console.WriteLine("Worked cancelled.");
                Task.Delay(50);
                break;
            }

            if (tasksToRun.Count == 0)
            {
                Console.WriteLine("NO Tasks.");
                Task.Delay(50);
                continue;
            }

            var task = tasksToRun
                .Dequeue();

            task();

            Task.Delay(50);
        }
    }

    public KeystrokeEvent Toggle { get; set; }

    public Action<EventSimulator>? TaskRunner { get; set; }

    public virtual TimeSpan? DelayBetweenLoop { get; set; }

    public virtual void Run()
    {
        tasksToRun.Enqueue(() =>
        {
            if (TaskRunner is null)
            {
                return;
            }

            this.TaskRunner(this.simulator);
        });
    }

    public virtual void RunLoop(CancellationToken cancellationToken)
    {
        tasksToRun.Enqueue(() =>
        {
            if (TaskRunner is null)
            {
                return;
            }

            var delayBetweenLoop = DelayBetweenLoop ?? TimeSpan.FromMilliseconds(250);

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                this.TaskRunner(this.simulator);

                Thread.Sleep(delayBetweenLoop);
            }

        });
    }
}