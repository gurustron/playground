using System.Diagnostics.CodeAnalysis;

namespace ASP6MVCtest.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    void D()
    {
    var v1 = GetValue(2, false);
    Console.WriteLine(v1!.GetHashCode());
    var v2 = GetValue(1, true);
    Console.WriteLine(v2.GetHashCode());
    Console.WriteLine(v1.GetHashCode());
    }
    

    string? GetValue(int key)
    {
        return null;
    }

    string? GetValue(int key,[DoesNotReturnIf(true)] bool errorIfInvalidKey)
    {
        string? result = default;
        if (result == null && errorIfInvalidKey) {
            throw new InvalidOperationException("Bad key");
        } else {
            return result;
        }
    }
}


