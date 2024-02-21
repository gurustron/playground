namespace WhatsNewCSharp12.Interceptors;

using System.Runtime.CompilerServices;

static class ManualInterceptor
{
    // use actual path on your machine since this is usually source generated
    [InterceptsLocation(
        "/home/gurustron/Projects/playground/csharp/SOAnswers/NET8/WhatsNew/WhatsNewCSharp12.Interceptors/Program.cs",
        line: 4, character: 3)] // refers to the call at (L1, C1)
    public static void InterceptorMethod(this C c, int param)
    {
        Console.WriteLine($"interceptor {param}");
    }

    [InterceptsLocation(
        "/home/gurustron/Projects/playground/csharp/SOAnswers/NET8/WhatsNew/WhatsNewCSharp12.Interceptors/Program.cs",
        line: 5, character: 3)] // refers to the call at (L2, C2)
    [InterceptsLocation(
        "/home/gurustron/Projects/playground/csharp/SOAnswers/NET8/WhatsNew/WhatsNewCSharp12.Interceptors/Program.cs",
        line: 6, character: 3)] // refers to the call at (L3, C3)
    public static void OtherInterceptorMethod(this C c, int param)
    {
        Console.WriteLine($"other interceptor {param}");
    }
}