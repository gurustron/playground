using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using SourceGen.RecordDefaultCtor;

namespace SourceGenTest
{
    public class RecordDefaultCtorGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleGeneratorTest()
        {
            var userSource = @"
namespace MyCode.Top.Child
{
    using System;
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }

    public partial record TestRecord(string Foo);

    public partial record TestRecord1(string Foo, int Bar);

    public partial record TestRecord2(string Foo, int Bar)
    {

    }

    public record NotPartialRecord(string Foo);
}
";
            var comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new RecordDefaultCtorGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
        }
        
        // - multiples files
        // - global namespace
        // - nested partials 
        // - namespace collision
        // - custom ctor with same number of parameters but  

        private static Compilation CreateCompilation(string source)
        {
            return CSharpCompilation.Create(
                "compilation",
                new[] {CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview))},
                GetGlobalReferences(),
                new CSharpCompilationOptions(OutputKind.ConsoleApplication)
            );
        }

        private static GeneratorDriver CreateDriver(Compilation compilation, params ISourceGenerator[] generators)
        {
            return CSharpGeneratorDriver.Create(
                ImmutableArray.Create(generators),
                ImmutableArray<AdditionalText>.Empty,
                (CSharpParseOptions) compilation.SyntaxTrees.First().Options
            );
        }

        private static Compilation RunGenerators(Compilation compilation, out ImmutableArray<Diagnostic> diagnostics,
            params ISourceGenerator[] generators)
        {
            CreateDriver(compilation, generators)
                .RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out diagnostics);
            return updatedCompilation;
        }

        private static MetadataReference[] GetGlobalReferences()
        {
            var assemblies = new[]
            {
                typeof(object).Assembly,
                typeof(Console).Assembly
            };

            var returnList = assemblies
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            //The location of the .NET assemblies
            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            /* 
                * Adding some necessary .NET assemblies
                * These assemblies couldn't be loaded correctly via the same construction as above,
                * in specific the System.Runtime.
                */
            returnList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "mscorlib.dll")));
            returnList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.dll")));
            returnList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Core.dll")));
            returnList.Add(MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll")));

            return returnList.ToArray();
        }
    }
}