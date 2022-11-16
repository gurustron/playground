using System.Threading.Tasks.Dataflow;

namespace ASPNET7Test;

public static class Exts
{
    public static TransformBlock<TIn, TOut> ToTransformBlock<TIn, TOut>(this Func<TIn, TOut> f )
    {
        return new TransformBlock<TIn, TOut>( f );
    }
}