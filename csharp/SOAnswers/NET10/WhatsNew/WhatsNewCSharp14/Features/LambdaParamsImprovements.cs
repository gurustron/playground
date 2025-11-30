namespace WhatsNewCSharp14.Features;

public class LambdaParamsImprovements
{
    public void Do()
    {
        // Simple lambda parameters with modifiers
        // You can add parameter modifiers,
        // such as scoped, ref, in, out, or ref readonly to lambda expression parameters
        // without specifying the parameter type:

        // C# 14 
        TryParse<int> parse1 = (text, out result) => Int32.TryParse(text, out result);
        // Before
        TryParse<int> parse2 = (string text, out int result) => Int32.TryParse(text, out result);
    }


    delegate bool TryParse<T>(string text, out T result);
}