```

BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
Intel Core i9-14900HX 0.80GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.300
  [Host]     : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3


```
| Method                    | ThreadCount | TotalSize | Mean             | Error           | StdDev          | Median           | Gen0     | Gen1     | Gen2     | Allocated  |
|-------------------------- |------------ |---------- |-----------------:|----------------:|----------------:|-----------------:|---------:|---------:|---------:|-----------:|
| **ProcessViaConcurrentBag**   | **1**           | **1**         |       **1,956.0 ns** |        **26.07 ns** |        **24.38 ns** |       **1,962.0 ns** |   **0.0381** |   **0.0362** |        **-** |    **1.98 KB** |
| ProcessViaConcurrentQueue | 1           | 1         |       1,593.4 ns |        30.87 ns |        36.74 ns |       1,594.3 ns |   0.0286 |        - |        - |    2.23 KB |
| ProcessViaConcurrentStack | 1           | 1         |       1,345.5 ns |        88.61 ns |       261.27 ns |       1,492.2 ns |   0.0210 |        - |        - |    1.64 KB |
| ProcessViaPreAllocated    | 1           | 1         |         908.8 ns |        18.02 ns |        25.84 ns |         909.1 ns |   0.0210 |        - |        - |    1.63 KB |
| **ProcessViaConcurrentBag**   | **1**           | **100**       |      **63,756.6 ns** |     **1,262.67 ns** |     **3,259.36 ns** |      **63,590.6 ns** |        **-** |        **-** |        **-** |    **3.21 KB** |
| ProcessViaConcurrentQueue | 1           | 100       |      66,808.7 ns |        47.84 ns |        42.41 ns |      66,812.3 ns |   0.3662 |        - |        - |    4.66 KB |
| ProcessViaConcurrentStack | 1           | 100       |      64,237.2 ns |     1,263.52 ns |     2,278.39 ns |      65,096.7 ns |        - |        - |        - |    7.11 KB |
| ProcessViaPreAllocated    | 1           | 100       |      63,207.0 ns |     1,254.22 ns |     2,881.77 ns |      64,478.8 ns |        - |        - |        - |    2.07 KB |
| **ProcessViaConcurrentBag**   | **1**           | **100000**    | **125,472,792.8 ns** | **2,400,504.21 ns** | **2,857,629.76 ns** | **126,950,150.6 ns** |        **-** |        **-** |        **-** | **1416.68 KB** |
| ProcessViaConcurrentQueue | 1           | 100000    | 127,104,848.1 ns | 1,159,992.37 ns | 1,085,057.58 ns | 127,477,077.2 ns | 250.0000 | 250.0000 | 250.0000 | 1419.31 KB |
| ProcessViaConcurrentStack | 1           | 100000    |  63,457,238.4 ns | 1,268,929.90 ns | 2,383,358.13 ns |  64,021,021.1 ns |        - |        - |        - | 5470.39 KB |
| ProcessViaPreAllocated    | 1           | 100000    |  64,191,991.3 ns |   450,813.65 ns |   421,691.37 ns |  64,408,074.8 ns | 111.1111 | 111.1111 | 111.1111 |  392.38 KB |
| **ProcessViaConcurrentBag**   | **4**           | **1**         |       **2,767.3 ns** |        **54.79 ns** |        **63.10 ns** |       **2,762.8 ns** |   **0.0343** |   **0.0305** |        **-** |    **2.31 KB** |
| ProcessViaConcurrentQueue | 4           | 1         |       2,531.5 ns |        50.25 ns |        59.82 ns |       2,532.4 ns |   0.0343 |        - |        - |    2.56 KB |
| ProcessViaConcurrentStack | 4           | 1         |       2,466.4 ns |        49.17 ns |        87.41 ns |       2,485.2 ns |   0.0267 |        - |        - |    1.97 KB |
| ProcessViaPreAllocated    | 4           | 1         |       2,505.4 ns |        48.15 ns |        45.04 ns |       2,507.2 ns |   0.0229 |        - |        - |    1.95 KB |
| **ProcessViaConcurrentBag**   | **4**           | **100**       |      **35,912.1 ns** |     **1,453.11 ns** |     **4,284.54 ns** |      **37,045.2 ns** |   **0.0305** |        **-** |        **-** |    **3.79 KB** |
| ProcessViaConcurrentQueue | 4           | 100       |      39,141.5 ns |       552.53 ns |       516.84 ns |      39,284.0 ns |   0.0610 |        - |        - |    5.24 KB |
| ProcessViaConcurrentStack | 4           | 100       |      19,217.2 ns |       307.21 ns |       287.36 ns |      19,275.6 ns |   0.0916 |        - |        - |    7.69 KB |
| ProcessViaPreAllocated    | 4           | 100       |      35,740.0 ns |       402.10 ns |       376.12 ns |      35,883.4 ns |   0.1831 |        - |        - |    2.77 KB |
| **ProcessViaConcurrentBag**   | **4**           | **100000**    |  **31,878,457.4 ns** |   **378,064.70 ns** |   **353,641.96 ns** |  **31,952,855.5 ns** |        **-** |        **-** |        **-** | **1433.94 KB** |
| ProcessViaConcurrentQueue | 4           | 100000    |  36,297,946.9 ns |   292,023.03 ns |   273,158.52 ns |  36,281,275.4 ns |        - |        - |        - | 1419.73 KB |
| ProcessViaConcurrentStack | 4           | 100000    |  22,890,032.5 ns | 2,521,284.87 ns | 7,434,065.87 ns |  28,226,491.5 ns |  93.7500 |        - |        - | 5470.97 KB |
| ProcessViaPreAllocated    | 4           | 100000    |  15,822,735.2 ns |   237,949.73 ns |   222,578.32 ns |  15,869,692.0 ns |  93.7500 |  93.7500 |  93.7500 |  393.09 KB |
| **ProcessViaConcurrentBag**   | **8**           | **1**         |       **2,895.5 ns** |        **56.18 ns** |        **57.70 ns** |       **2,873.8 ns** |   **0.0305** |   **0.0229** |        **-** |     **2.3 KB** |
| ProcessViaConcurrentQueue | 8           | 1         |       2,547.7 ns |        39.43 ns |        36.88 ns |       2,549.7 ns |   0.0343 |        - |        - |    2.56 KB |
| ProcessViaConcurrentStack | 8           | 1         |       2,447.9 ns |        48.83 ns |        94.07 ns |       2,482.1 ns |   0.0229 |        - |        - |    1.95 KB |
| ProcessViaPreAllocated    | 8           | 1         |       2,424.9 ns |        43.52 ns |        40.71 ns |       2,428.3 ns |   0.0229 |        - |        - |    1.96 KB |
| **ProcessViaConcurrentBag**   | **8**           | **100**       |      **17,504.8 ns** |       **164.09 ns** |       **145.46 ns** |      **17,507.2 ns** |   **0.0610** |   **0.0305** |        **-** |    **5.69 KB** |
| ProcessViaConcurrentQueue | 8           | 100       |      30,152.9 ns |       503.22 ns |       446.09 ns |      30,037.4 ns |   0.0610 |        - |        - |    6.14 KB |
| ProcessViaConcurrentStack | 8           | 100       |      13,897.1 ns |       162.88 ns |       152.36 ns |      13,915.3 ns |   0.1068 |        - |        - |    8.59 KB |
| ProcessViaPreAllocated    | 8           | 100       |      13,564.4 ns |       133.51 ns |       124.89 ns |      13,553.9 ns |   0.0458 |        - |        - |    3.83 KB |
| **ProcessViaConcurrentBag**   | **8**           | **100000**    |   **8,152,702.5 ns** |   **160,486.16 ns** |   **254,548.08 ns** |   **8,187,032.1 ns** |        **-** |        **-** |        **-** | **1463.57 KB** |
| ProcessViaConcurrentQueue | 8           | 100000    |  21,290,541.0 ns |   377,901.64 ns |   353,489.43 ns |  21,223,608.4 ns |        - |        - |        - | 1420.57 KB |
| ProcessViaConcurrentStack | 8           | 100000    |   8,444,513.5 ns | 1,054,085.60 ns | 3,107,995.40 ns |   6,763,177.8 ns |  62.5000 |        - |        - | 5471.81 KB |
| ProcessViaPreAllocated    | 8           | 100000    |  15,071,373.7 ns |   297,685.46 ns |   472,160.73 ns |  15,172,403.4 ns | 109.3750 | 109.3750 | 109.3750 |   394.1 KB |
