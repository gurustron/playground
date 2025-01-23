using Guid = System.Guid;

namespace NET9Console.Tests;
[SetUpFixture]
public class RootSetup
{
    public static int I;
    
    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        Interlocked.Increment(ref I);
        // File.Create($"/home/gurustron/Projects/playground/csharp/SOAnswers/NET9Console.Tests/Test_{I}_{Guid.NewGuid()}.txt");
    }

}