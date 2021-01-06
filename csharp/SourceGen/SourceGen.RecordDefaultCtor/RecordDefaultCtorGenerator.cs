using System;
using Microsoft.CodeAnalysis;

namespace SourceGen.RecordDefaultCtor
{
    [Generator]
    public class RecordDefaultCtorGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new RecordSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not RecordSyntaxReceiver receiver)
            {
                throw new Exception();
            }

            Console.WriteLine(receiver.Records.Count);
        }
    }
}
