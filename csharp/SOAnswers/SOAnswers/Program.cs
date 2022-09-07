// See https://aka.ms/new-console-template for more information

using System.Globalization;
using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using Moq;

Console.WriteLine(UInt32.MaxValue);

Console.WriteLine(Convert.ToUInt32("111",2));
Console.WriteLine();
public class BaseValidator
{
    public BaseValidator(Context context) {}
}

public class Context:IDisposable
{
    public void Dispose()
    {
    }
}
public interface IContextProvider 
{
    Context Create();
}



public class EntityValidator: BaseValidator , IDisposable
{
    private readonly Context _context;
    public EntityValidator(IContextProvider provider) : this(new CaptureContextProvider(provider)) 
    {
    }
    
    private EntityValidator(CaptureContextProvider provider) : base(provider.Create()) 
    {
        _context = provider.Create();
    }
    class CaptureContextProvider : IContextProvider
    {
        private readonly IContextProvider _contextProvider;
        private Context? _capture;

        public CaptureContextProvider(IContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public Context Create()
        {
            _capture ??= _contextProvider.Create();
            return _capture;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
