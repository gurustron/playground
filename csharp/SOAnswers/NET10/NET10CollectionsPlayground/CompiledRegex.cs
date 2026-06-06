using System.Text.RegularExpressions;

namespace NET10CollectionsPlayground;

public static partial class CompiledRegex
{
    [GeneratedRegex("abc|def", RegexOptions.IgnoreCase, "en-US")]
    public static partial Regex AbcOrDefGeneratedRegex();

    [GeneratedRegex("(?i)hello|world", RegexOptions.IgnoreCase, "en-US")]
    public static partial Regex HelloWorldIGeneratedRegex();

    [GeneratedRegex("a|z", RegexOptions.IgnoreCase, "en-US")]
    public static partial Regex AOrZGeneratedRegex();

    [GeneratedRegex("[az]{2}", RegexOptions.IgnoreCase, "en-US")]
    public static partial Regex AOrZTwiceGeneratedRegex();
}