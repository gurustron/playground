using System.Collections;

namespace WhatsNewCSharp14.Features;

public static class ExtensionMembers
{
    // only static, e.g. invoked on type - int.DoSomething()
    extension(int /*receiver parameter*/) 
    {
        public static int DoSomething() => 42;

        // will not compile:
        // public int DoSomething() => 42;
    }

    extension(int name) 
    {
        // Type 'MyExtensionMembers' already defines a member called 'DoSomething' with the same parameter types
        // public int DoSomething() => name;
    }

    // static and instance
    extension(long name /*named receiver parameter*/) 
    {
        // Type 'MyExtensionMembers' already defines a member called 'DoSomething' with the same parameter types
        // public static long DoSomething() => 42;
        public static long DoSomethingLong() => 42;
        public long DoSomethingInstance() => name; // 42L.DoSomethingInstance(), no 
    }

    public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source) => throw new Exception();
}

public static class E
{
    extension<T>(T[] ts)
    {
        // It is an error for members to declare type parameters or parameters
        // (as well as local variables and local functions directly within the member body)
        // with the same name as a type parameter or receiver parameter of the extension declaration.
        // public void M3(int T, string ts) { }          // Error: Cannot reuse names `T` and `ts`
        // public void M4<T, ts>(string s) { }           // Error: Cannot reuse names `T` and `ts`
        
        // Though can be used as names
        public void T() { M(ts); } // Generated static method M<T>(T[]) is found
        public void ts() { }

        public void M()
        {
            // ts(); // Error: Method, delegate, or event is expected
            // T(ts); // Error: T is a type parameter
        } 
    }
}