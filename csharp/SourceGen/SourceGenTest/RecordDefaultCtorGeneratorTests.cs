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
    public class Program { public static void Main(string[] args) => Console.WriteLine(); }

    public partial record TestRecord(TestRecord1 Foo);

    public partial record TestRecord1(string Foo, int Bar);

    [AttributeUsage(AttributeTargets.Parameter)]
    public class MyAttribute:Attribute{}

    public partial record TestRecord2([MyAttribute]string Foo, int Bar)
    {
    }

    public record NotPartialRecord(string Foo);
}";
            var comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new RecordDefaultCtorGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
            Assert.AreEqual(4, newComp.SyntaxTrees.Count());
        }

        [Test]
        public void RecordWithDefaultArgumentsTest()
        {
            var cases = new[]
                {
                    "(int I = default)",
                    "(int I = default(int))",
                    "(int I = 3)",
                    "(string I = Program.Constant)"
                }
                .Select((s, i) => $"public partial record Record{i}{s};")
                .ToList();

            var userSource = $@"
using System;
namespace MyCode.Top.Child
{{
#pragma warning disable CS8019
    using System.Collections.Generic;
#pragma warning restore CS8019
    public class Program 
    {{
        public const string Constant = ""Test"";
        public static void Main(string[] args) => Console.WriteLine(); 
    }}

    {string.Join(Environment.NewLine, cases)}
}}";
            var comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new RecordDefaultCtorGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
            Assert.AreEqual(cases.Count + 1, newComp.SyntaxTrees.Count());
        }

        [Test]
        public void RecordWithTypeParamsGeneratorTest()
        {
            var cases = new[]
                {
                    "<T>(string Foo)",
                    "<T>(int I, T Foo)",
                    "<T>(string Foo)",
                    "<T>(int I, T Foo)",
                    "<T, R>(int I, T Foo, T Foo1, R Bar)",
                    "(List<int> Ints)",
                    "<T>(List<T> Ts)",
                    "<T>(Dictionary<int,T> Ts)",
                    "<T,R>(Dictionary<T,R> Rs)"
                }
                .Select((s, i) => $"public partial record Record{i}{s};")
                .ToList();

            var userSource = $@"
namespace MyCode.Top.Child
{{
    using System;
    using System.Collections.Generic;
    public class Program {{ public static void Main(string[] args) => Console.WriteLine(); }}

    {string.Join(Environment.NewLine, cases)}
}}";
            var comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new RecordDefaultCtorGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
            Assert.AreEqual(cases.Count + 1, newComp.SyntaxTrees.Count());
        }

        [Test]
        public void RecordWithDefaultCtor_Skipped()
        {
            var userSource = @"
namespace MyCode.Top.Child
{
    using System;
    public class Program { public static void Main(string[] args) => Console.WriteLine(); }

    public partial record Record(int i)
    {
        public Record() : this(1){}
    }
}";
            var comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new RecordDefaultCtorGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
            Assert.AreEqual(1, newComp.SyntaxTrees.Count());
        }

        [Test]
        public void RecordNonPartial_Skipped()
        {
            var userSource = @"
namespace MyCode.Top.Child
{
    using System;
    public class Program { public static void Main(string[] args) => Console.WriteLine(); }

    public record Record(int i)
    {
    }
}";
            var comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new RecordDefaultCtorGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
            Assert.AreEqual(1, newComp.SyntaxTrees.Count());
        }

        // - create analyzer for required fields
        // - multiples files
        // - global namespace
        // - nested partials (nested classes)
        // - namespace collision ?? 
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