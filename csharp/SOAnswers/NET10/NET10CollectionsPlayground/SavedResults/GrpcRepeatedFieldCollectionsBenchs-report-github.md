```

BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
Intel Core i9-14900HX 0.80GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.300
  [Host]     : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3


```
| Method               | CollectionSize | Mean        | Error     | StdDev     | Gen0   | Code Size | Allocated |
|--------------------- |--------------- |------------:|----------:|-----------:|-------:|----------:|----------:|
| **ViaDirectCopy**        | **1**              |    **67.00 ns** |  **1.416 ns** |   **3.281 ns** | **0.0024** |   **1,718 B** |     **192 B** |
| ViaDirectCopyHashSet | 1              |    46.52 ns |  1.026 ns |   2.379 ns | 0.0020 |   1,876 B |     152 B |
| PreInitSize          | 1              |    35.79 ns |  0.801 ns |   1.918 ns | 0.0013 |   2,219 B |      96 B |
| **ViaDirectCopy**        | **8**              |    **77.42 ns** |  **1.612 ns** |   **3.503 ns** | **0.0025** |   **1,725 B** |     **192 B** |
| ViaDirectCopyHashSet | 8              |    69.78 ns |  1.452 ns |   2.504 ns | 0.0019 |   1,887 B |     152 B |
| PreInitSize          | 8              |    57.95 ns |  1.263 ns |   3.099 ns | 0.0019 |   2,219 B |     152 B |
| **ViaDirectCopy**        | **9**              |    **76.14 ns** |  **1.586 ns** |   **3.239 ns** | **0.0025** |   **1,745 B** |     **200 B** |
| ViaDirectCopyHashSet | 9              |   116.46 ns |  2.445 ns |   4.592 ns | 0.0039 |   1,873 B |     304 B |
| PreInitSize          | 9              |    62.06 ns |  1.300 ns |   1.497 ns | 0.0020 |   2,219 B |     160 B |
| **ViaDirectCopy**        | **25**             |   **148.71 ns** |  **3.069 ns** |   **6.671 ns** | **0.0050** |   **1,722 B** |     **328 B** |
| ViaDirectCopyHashSet | 25             |   213.68 ns |  4.278 ns |   5.092 ns | 0.0074 |   1,885 B |     584 B |
| PreInitSize          | 25             |   123.85 ns |  2.530 ns |   5.392 ns | 0.0036 |   2,219 B |     288 B |
| **ViaDirectCopy**        | **299**            |   **984.63 ns** | **19.807 ns** |  **48.958 ns** | **0.0324** |   **1,722 B** |    **2520 B** |
| ViaDirectCopyHashSet | 299            | 2,364.44 ns | 47.139 ns | 120.835 ns | 0.1068 |   1,885 B |    8360 B |
| PreInitSize          | 299            | 1,094.55 ns | 21.597 ns |  46.025 ns | 0.0324 |   2,219 B |    2480 B |
