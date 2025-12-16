using System.Reflection;
using System.Runtime.CompilerServices;

namespace WhatsNewCSharp14.Features;

public class FieldKeyword
{
    private string? _msg;
    public string? Message
    {
        get => _msg;
        set => _msg = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public string? MessageWithFieldKeyword
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static void Do()
    {
        var fieldInfos = typeof(FieldKeyword)
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        // prints
        // _msg
        // <MessageWithFieldKeyword>k__BackingField

        foreach (var fieldInfo in fieldInfos)
        {
            Console.WriteLine(fieldInfo.Name);
        }
    }
}