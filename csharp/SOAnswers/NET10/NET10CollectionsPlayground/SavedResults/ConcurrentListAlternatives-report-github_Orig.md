```

BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
Intel Core i9-14900HX 0.80GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.300
  [Host]     : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3


```
| Method                    | ThreadCount | SizePerThread | Mean     | Error    | StdDev   | Allocated |
|-------------------------- |------------ |-------------- |---------:|---------:|---------:|----------:|
| **ProcessViaConcurrentBag**   | **1**           | **1**             | **15.16 ms** | **0.040 ms** | **0.037 ms** |   **1.86 KB** |
| ProcessViaConcurrentQueue | 1           | 1             | 15.16 ms | 0.042 ms | 0.040 ms |   2.11 KB |
| ProcessViaPreAllocated    | 1           | 1             | 15.15 ms | 0.046 ms | 0.043 ms |   1.51 KB |
| **ProcessViaConcurrentBag**   | **1**           | **4**             | **15.14 ms** | **0.037 ms** | **0.034 ms** |   **1.87 KB** |
| ProcessViaConcurrentQueue | 1           | 4             | 15.17 ms | 0.025 ms | 0.023 ms |   2.12 KB |
| ProcessViaPreAllocated    | 1           | 4             | 15.15 ms | 0.044 ms | 0.041 ms |   1.52 KB |
| **ProcessViaConcurrentBag**   | **1**           | **25**            | **15.16 ms** | **0.029 ms** | **0.027 ms** |   **1.95 KB** |
| ProcessViaConcurrentQueue | 1           | 25            | 15.15 ms | 0.038 ms | 0.036 ms |    2.2 KB |
| ProcessViaPreAllocated    | 1           | 25            | 15.16 ms | 0.038 ms | 0.036 ms |    1.7 KB |
| **ProcessViaConcurrentBag**   | **1**           | **100**           | **15.16 ms** | **0.017 ms** | **0.016 ms** |   **3.04 KB** |
| ProcessViaConcurrentQueue | 1           | 100           | 15.15 ms | 0.029 ms | 0.027 ms |   4.49 KB |
| ProcessViaPreAllocated    | 1           | 100           | 15.15 ms | 0.032 ms | 0.030 ms |   2.27 KB |
| **ProcessViaConcurrentBag**   | **2**           | **1**             | **15.23 ms** | **0.040 ms** | **0.038 ms** |   **2.34 KB** |
| ProcessViaConcurrentQueue | 2           | 1             | 15.21 ms | 0.024 ms | 0.022 ms |   2.33 KB |
| ProcessViaPreAllocated    | 2           | 1             | 15.21 ms | 0.031 ms | 0.028 ms |   1.74 KB |
| **ProcessViaConcurrentBag**   | **2**           | **4**             | **15.20 ms** | **0.012 ms** | **0.010 ms** |   **2.38 KB** |
| ProcessViaConcurrentQueue | 2           | 4             | 15.21 ms | 0.020 ms | 0.019 ms |   2.35 KB |
| ProcessViaPreAllocated    | 2           | 4             | 15.22 ms | 0.025 ms | 0.024 ms |   1.78 KB |
| **ProcessViaConcurrentBag**   | **2**           | **25**            | **15.21 ms** | **0.023 ms** | **0.021 ms** |   **2.54 KB** |
| ProcessViaConcurrentQueue | 2           | 25            | 15.20 ms | 0.019 ms | 0.016 ms |   3.27 KB |
| ProcessViaPreAllocated    | 2           | 25            | 15.20 ms | 0.019 ms | 0.017 ms |    2.1 KB |
| **ProcessViaConcurrentBag**   | **2**           | **100**           | **15.24 ms** | **0.040 ms** | **0.038 ms** |   **4.71 KB** |
| ProcessViaConcurrentQueue | 2           | 100           | 15.24 ms | 0.026 ms | 0.024 ms |   5.11 KB |
| ProcessViaPreAllocated    | 2           | 100           | 15.20 ms | 0.026 ms | 0.025 ms |   3.29 KB |
| **ProcessViaConcurrentBag**   | **4**           | **1**             | **15.32 ms** | **0.035 ms** | **0.032 ms** |   **3.35 KB** |
| ProcessViaConcurrentQueue | 4           | 1             | 15.27 ms | 0.038 ms | 0.036 ms |   2.78 KB |
| ProcessViaPreAllocated    | 4           | 1             | 15.35 ms | 0.042 ms | 0.039 ms |   2.22 KB |
| **ProcessViaConcurrentBag**   | **4**           | **4**             | **15.31 ms** | **0.045 ms** | **0.042 ms** |   **3.37 KB** |
| ProcessViaConcurrentQueue | 4           | 4             | 15.31 ms | 0.026 ms | 0.021 ms |   2.87 KB |
| ProcessViaPreAllocated    | 4           | 4             | 15.26 ms | 0.035 ms | 0.033 ms |   2.29 KB |
| **ProcessViaConcurrentBag**   | **4**           | **25**            | **15.36 ms** | **0.050 ms** | **0.047 ms** |   **3.71 KB** |
| ProcessViaConcurrentQueue | 4           | 25            | 15.35 ms | 0.031 ms | 0.028 ms |   5.18 KB |
| ProcessViaPreAllocated    | 4           | 25            | 15.31 ms | 0.050 ms | 0.047 ms |   2.98 KB |
| **ProcessViaConcurrentBag**   | **4**           | **100**           | **15.33 ms** | **0.025 ms** | **0.024 ms** |   **8.06 KB** |
| ProcessViaConcurrentQueue | 4           | 100           | 15.33 ms | 0.039 ms | 0.036 ms |   8.63 KB |
| ProcessViaPreAllocated    | 4           | 100           | 15.29 ms | 0.022 ms | 0.020 ms |    5.3 KB |
| **ProcessViaConcurrentBag**   | **8**           | **1**             | **15.76 ms** | **0.072 ms** | **0.068 ms** |   **5.41 KB** |
| ProcessViaConcurrentQueue | 8           | 1             | 15.69 ms | 0.088 ms | 0.083 ms |   3.82 KB |
| ProcessViaPreAllocated    | 8           | 1             | 15.70 ms | 0.066 ms | 0.062 ms |   3.21 KB |
| **ProcessViaConcurrentBag**   | **8**           | **4**             | **15.69 ms** | **0.067 ms** | **0.062 ms** |   **5.46 KB** |
| ProcessViaConcurrentQueue | 8           | 4             | 15.67 ms | 0.067 ms | 0.062 ms |   3.89 KB |
| ProcessViaPreAllocated    | 8           | 4             | 15.75 ms | 0.050 ms | 0.047 ms |   3.48 KB |
| **ProcessViaConcurrentBag**   | **8**           | **25**            | **15.71 ms** | **0.055 ms** | **0.048 ms** |   **6.09 KB** |
| ProcessViaConcurrentQueue | 8           | 25            | 15.65 ms | 0.072 ms | 0.067 ms |   6.56 KB |
| ProcessViaPreAllocated    | 8           | 25            | 15.67 ms | 0.084 ms | 0.079 ms |   4.74 KB |
| **ProcessViaConcurrentBag**   | **8**           | **100**           | **15.73 ms** | **0.077 ms** | **0.072 ms** |  **14.86 KB** |
| ProcessViaConcurrentQueue | 8           | 100           | 15.68 ms | 0.072 ms | 0.068 ms |  15.41 KB |
| ProcessViaPreAllocated    | 8           | 100           | 15.69 ms | 0.065 ms | 0.061 ms |   9.42 KB |
