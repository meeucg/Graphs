using System.Diagnostics;
namespace GraphByList;

public class BenchmarkTools
{
    public delegate void Method();

    private long startMemory = 0;
    private long startTime = 0;

    public long MeasureMemoryUsage(Method method)
    {
        long memoryBefore = GC.GetTotalMemory(true);
        method?.Invoke();
        long memoryAfter = GC.GetTotalMemory(true);

        return memoryAfter - memoryBefore;
    }

    public long MeasureTime(Method method)
    {
        long ticksBefore = Stopwatch.GetTimestamp();
        method?.Invoke();
        long ticksAfter = Stopwatch.GetTimestamp();

        return ticksAfter - ticksBefore;
    }

    public void MemoryStart()
    {
        startMemory = GC.GetTotalMemory(true);
    }
    public long MemoryEnd() 
    { 
        return GC.GetTotalMemory(true) - startMemory;
    }

    public void TimeStart()
    {
        startTime = Stopwatch.GetTimestamp();
    }
    public long TimeEnd()
    {
        return Stopwatch.GetTimestamp() - startTime;
    }
}

