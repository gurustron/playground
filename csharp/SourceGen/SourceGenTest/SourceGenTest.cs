using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using SourceGen;

namespace SourceGenTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleGeneratorTest()
        {
            var userSource = @"
    using System;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GeneratedNamespace{
    partial class GeneratedClass
    {
         [DisplayName(nameof(GeneratedPropName))]
         [DisplayName(""name"")]
        public string VerySpecificPropName { get; set; }
    }
}

namespace MyCode
{

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }
}
";
            Compilation comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new CustomGenerator());

            Assert.IsEmpty(generatorDiags);
            var immutableArray = newComp.GetDiagnostics();
            Assert.IsEmpty(immutableArray);
        }

        private static Compilation CreateCompilation(string source) => CSharpCompilation.Create(
            assemblyName: "compilation",
            syntaxTrees: new[] {CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview))},
            references: GetGlobalReferences(),
            options: new CSharpCompilationOptions(OutputKind.ConsoleApplication)
        );

        private static GeneratorDriver CreateDriver(Compilation compilation, params ISourceGenerator[] generators) => CSharpGeneratorDriver.Create(
            generators: ImmutableArray.Create(generators),
            additionalTexts: ImmutableArray<AdditionalText>.Empty,
            parseOptions: (CSharpParseOptions)compilation.SyntaxTrees.First().Options,
            optionsProvider: null
        );

        private static Compilation RunGenerators(Compilation compilation, out ImmutableArray<Diagnostic> diagnostics, params ISourceGenerator[] generators)
        {
            CreateDriver(compilation, generators).RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out diagnostics);
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