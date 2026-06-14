using N;

Console.WriteLine("Hello, World!");

var d = new Dictionary<int,int>();


//System.Collections.Generic.Dictionary`2<int32, int32>::set_Item(!0, !1)
d[1]=1;


namespace N
{
    class BannedType
    {
        public BannedType() {}

        public int BannedMethod() => 1;

        public void BannedMethod(int i) {}

        public void BannedMethod<T>(T t) {}

        public void BannedMethod<T>(Func<T> f) {}

        public string BannedField = null!;

        public string BannedProperty { get; }= null!;

        public event EventHandler BannedEvent;
    }

    class BannedType<T>
    {
    }
}

// T:N.BannedType;Don't use BannedType
// T:N.BannedType`1;Don't use BannedType<T>
// M:N.BannedType.#ctor
// M:N.BannedType.BannedMethod
// M:N.BannedType.BannedMethod(System.Int32);Don't use BannedMethod
// M:N.BannedType.BannedMethod`1(``0)
// M:N.BannedType.BannedMethod`1(System.Func{``0})
// F:N.BannedType.BannedField
// P:N.BannedType.BannedProperty
// E:N.BannedType.BannedEvent
// N:N



