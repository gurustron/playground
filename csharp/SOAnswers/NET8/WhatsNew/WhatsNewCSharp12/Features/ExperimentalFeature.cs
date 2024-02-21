using System.Diagnostics.CodeAnalysis;

namespace WhatsNewCSharp12.Features;

public class ExperimentalFeature
{
    public static void Do()
    {
        ExperimentalDo();
    }

    [Experimental("TEST_EXP_101010")]
    public static void ExperimentalDo() => Console.WriteLine("Experiment");
}