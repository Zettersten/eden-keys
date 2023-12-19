using EdenKeys.Shared;

namespace EdenKeys.GameMechanics;

public sealed class ExecutionRegistry
{
    private readonly List<ExecutionBase> executions;

    public ExecutionRegistry()
    {
        this.executions = [];
    }

    public void Register(ExecutionBase execution)
    {
        if (this.executions.Any(x => x.Toggle == execution.Toggle))
        {
            return;
        }

        this.executions.Add(execution);
    }

    public void RunIfExists(KeystrokeEvent message, CancellationToken token)
    {
        var runners = this.executions
            .Where(x => x.Toggle == message)
            .ToList();

        if (runners is null or { Count: 0 })
        {
            Console.WriteLine("NO Runners.");
            return;
        }

        _ = Task.Run(() =>
        {
            foreach (var runner in runners)
            {
                if (!runner.ShouldLoop)
                {
                    runner.Run();

                    continue;
                }

                runner.RunLoop(token);
            }
        }, token);
    }

    public void Unregister(ExecutionBase execution)
    {
        this.executions
            .RemoveAll(x => x.Toggle == execution.Toggle);
    }

    public void UnregisterAll()
    {
        this.executions.Clear();
    }
}