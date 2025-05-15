using BenchmarkDotNet.Attributes;

namespace NET9.Benchs;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class TaskWhenEachBenchMark
{
    private IEnumerable<Task<(int Index, int Delay)>> GetTasks()
    {
        int[] delays = [11, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2, 1];

        var tasksToRun = delays
            .Select(async (delay, index) =>
            {
                await Task.Delay(delay);
                return (index, delay);
            });
        
        return tasksToRun;
    }

    [Benchmark]
    public async Task<int> TaskWhenAll()
    {
        var tasks = GetTasks();

        var valueTuples = await Task.WhenAll(tasks);

        int sum = 0;
        foreach (var (index, delay) in valueTuples)
        {
            if (delay > 5)
            {
                sum += delay;
            }
        }

        return sum;
    }
    
    [Benchmark]
    public async Task<int> TaskWhenEach()
    {
        var tasks = GetTasks();
        
        int sum = 0;
        await foreach (var completed in Task.WhenEach(tasks))
        {
            var (index, delay) = completed.Result;
            if (delay > 5)
            {
                sum += delay;
            }
        }

        return sum;
    }
}