```c#
public class FieldKeyword
{
  private string _msg;
  [CompilerGenerated]
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string <MessageWithFieldKeyword>k__BackingField;

  public string Message
  {
    get => this._msg;
    set => this._msg = value ?? throw new ArgumentNullException(nameof (value));
  }

  public string MessageWithFieldKeyword
  {
    get => this.<MessageWithFieldKeyword>k__BackingField;
    set
    {
      this.<MessageWithFieldKeyword>k__BackingField = value ?? throw new ArgumentNullException(nameof (value));
    }
  }
}
```

So you can just call methods on `ExtensionMembers` class - `ExtensionMembers.DoSomething()`