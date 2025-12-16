```c#
public static class ExtensionMembers
{
  public static int DoSomething() => 42;

  public static long DoSomethingLong() => 42;

  public static long DoSomethingInstance(this long name) => name;

  public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
  {
    throw new Exception();
  }

  [SpecialName]
  public sealed class <G>$BA41CFE2B5EDAEB8C1B9062F59ED4D69
  {
    [ExtensionMarker("<M>$BA41CFE2B5EDAEB8C1B9062F59ED4D69")]
    public static int DoSomething() => throw null;

    [SpecialName]
    public static class <M>$BA41CFE2B5EDAEB8C1B9062F59ED4D69
    {
      [CompilerGenerated]
      [SpecialName]
      public static void <Extension>$([In] int obj0)
      {
      }
    }

    [SpecialName]
    public static class <M>$B3FEBCDB8BB81580ACA2A6723EF75B1C
    {
    }
  }

  [SpecialName]
  public sealed class <G>$E8CA98ACBCAEE63BB261A3FD4AF31675
  {
    [ExtensionMarker("<M>$0CEEE8F2559F317FC8DF13D55C0019D1")]
    public static long DoSomethingLong() => throw null;

    [ExtensionMarker("<M>$0CEEE8F2559F317FC8DF13D55C0019D1")]
    public long DoSomethingInstance() => throw null;

    [SpecialName]
    public static class <M>$0CEEE8F2559F317FC8DF13D55C0019D1
    {
      [CompilerGenerated]
      [SpecialName]
      public static void <Extension>$(long name)
      {
      }
    }
  }
}
```

So you can just call methods on `ExtensionMembers` class - `int i = ExtensionMembers.DoSomething()`