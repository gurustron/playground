using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10.Benchs.Devirtualization;

[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
[DisassemblyDiagnoser]
public class GDVBenchs
{
    private readonly IInterface _interface = new InterfaceImpl();
    private readonly AbstractBase _abstract = new Child();
    private readonly NotInterfaceImpl _notInterfaceImpl = new ();
    
    [Benchmark]
    public int ViaInterface()
    {
        int i = 0;
        // for (int j = 0; j < 10_000; j++)
        {
            i += _interface.Do();
        }

        return i;
    }
    
    [Benchmark]
    public int ViaAbstractBase()
    {
        int i = 0;
        // for (int j = 0; j < 10_000; j++)
        {
            i += _abstract.Do();
        }

        return i;
    }
    
    [Benchmark]
    public int Concrete()
    {
        int i = 0;
        // for (int j = 0; j < 10_000; j++)
        {
            i += _notInterfaceImpl.Do();
        }

        return i;
    }
    
    
    interface IInterface
    {
        int Do();
    }

    class InterfaceImpl : IInterface
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Do() => 1;
    }

    abstract class AbstractBase
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual int Do() => 0;
    }

    class Child : AbstractBase
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public override int Do() => 1;
    }

    class NotInterfaceImpl
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Do() => 1;
    }
}
