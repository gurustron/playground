using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace NET10.Benchs.Devirtualization;

[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
[DisassemblyDiagnoser]
public class GDVBenchsV2
{
    private readonly InterfaceWrapper _interface = new(new InterfaceImpl());
    private readonly AbstractClassWrapper _abstract = new(new Child());
    private readonly Wrapper _notInterfaceImpl = new(new NotInterfaceImpl());
    
    [Benchmark]
    public int ViaInterface()
    {
        int i = 0;
        // for (int j = 0; j < 10_000; j++)
        {
            i += _interface.Do();
            i += _interface.Do();
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
            i += _abstract.Do();
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
            i += _notInterfaceImpl.Do();
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
        public int Do() => 1;
    }

    abstract class AbstractBase
    {
        public virtual int Do() => 0;
    }

    class Child : AbstractBase
    {
        public override int Do() => 1;
    }

    class NotInterfaceImpl
    {
        public int Do() => 1;
    }

    class InterfaceWrapper
    {
        private readonly IInterface _instance;

        public InterfaceWrapper(IInterface instance) => _instance = instance;
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Do() => _instance.Do();
    }
    
    class AbstractClassWrapper
    {
        private readonly AbstractBase _instance;

        public AbstractClassWrapper(AbstractBase instance) => _instance = instance;
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Do() => _instance.Do();
    }
    
    class Wrapper
    {
        private readonly NotInterfaceImpl _instance;

        public Wrapper(NotInterfaceImpl instance) => _instance = instance;
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Do() => _instance.Do();
    }
}
