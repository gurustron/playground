namespace HighPerfPlayground;

using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using CommunityToolkit.HighPerformance.Helpers;

public class Tada
{
    public static MemoryOwner<int> Do()
    {
        MemoryOwner<int> buffer = MemoryOwner<int>.Allocate(42);

        var maxIndex = Random.Shared.Next(41);

        for(var i = 0; i <= maxIndex; i++)
        {
            buffer.Span[i] = i;
        }

        return buffer.Slice(0, maxIndex + 1);
    }
}