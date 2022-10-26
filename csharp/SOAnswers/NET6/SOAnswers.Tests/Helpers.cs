namespace SOAnswers.Tests;
using OneOf;

public static class Helpers
{
    public static string BasicLog(OneOf<int, short, long> logData)
        => logData.ToString();//.Match(i => i.ToString(), i => i.ToString(), i => i.ToString());

    internal static void BasicLog1(
        this long logData)
    {
    }
}