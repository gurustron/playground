```

BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
Intel Core i9-14900HX 0.80GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.300
  [Host]     : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3


```
| Method                    | ThreadCount | SizePerThread | Mean             | Error           | StdDev          | Median           | Gen0      | Gen1     | Gen2     | Allocated  |
|-------------------------- |------------ |-------------- |-----------------:|----------------:|----------------:|-----------------:|----------:|---------:|---------:|-----------:|
| **ProcessViaConcurrentBag**   | **1**           | **1**             |         **971.9 ns** |        **19.28 ns** |        **33.25 ns** |         **968.3 ns** |    **0.0248** |   **0.0238** |        **-** |     **1904 B** |
| ProcessViaConcurrentQueue | 1           | 1             |         605.5 ns |        12.79 ns |        37.70 ns |         608.1 ns |    0.0277 |        - |        - |     2160 B |
| ProcessViaConcurrentStack | 1           | 1             |         443.8 ns |         8.88 ns |        15.56 ns |         443.9 ns |    0.0200 |        - |        - |     1560 B |
| ProcessViaPreAllocated    | 1           | 1             |         438.4 ns |         8.75 ns |        20.29 ns |         435.8 ns |    0.0196 |        - |        - |     1512 B |
| **ProcessViaConcurrentBag**   | **1**           | **4**             |         **981.0 ns** |        **18.22 ns** |        **17.04 ns** |         **977.0 ns** |    **0.0248** |   **0.0238** |        **-** |     **1912 B** |
| ProcessViaConcurrentQueue | 1           | 4             |         652.2 ns |        13.05 ns |        29.20 ns |         653.2 ns |    0.0277 |        - |        - |     2168 B |
| ProcessViaConcurrentStack | 1           | 4             |         492.1 ns |         9.89 ns |        27.56 ns |         492.3 ns |    0.0219 |        - |        - |     1728 B |
| ProcessViaPreAllocated    | 1           | 4             |         431.1 ns |         8.37 ns |        20.99 ns |         428.8 ns |    0.0196 |        - |        - |     1520 B |
| **ProcessViaConcurrentBag**   | **1**           | **25**            |       **1,153.6 ns** |        **22.96 ns** |        **37.72 ns** |       **1,151.4 ns** |    **0.0248** |   **0.0229** |        **-** |     **2000 B** |
| ProcessViaConcurrentQueue | 1           | 25            |         894.2 ns |        16.94 ns |        18.12 ns |         891.6 ns |    0.0315 |   0.0019 |   0.0019 |     2256 B |
| ProcessViaConcurrentStack | 1           | 25            |         819.6 ns |        16.34 ns |        32.63 ns |         817.5 ns |    0.0372 |        - |        - |     2904 B |
| ProcessViaPreAllocated    | 1           | 25            |         447.4 ns |         9.00 ns |        17.33 ns |         449.4 ns |    0.0210 |        - |        - |     1608 B |
| **ProcessViaConcurrentBag**   | **1**           | **100**           |       **1,991.0 ns** |        **39.09 ns** |        **54.80 ns** |       **1,990.9 ns** |    **0.0687** |   **0.0648** |        **-** |     **3112 B** |
| ProcessViaConcurrentQueue | 1           | 100           |       2,445.4 ns |        47.16 ns |        50.46 ns |       2,437.2 ns |    0.0572 |        - |        - |     4600 B |
| ProcessViaConcurrentStack | 1           | 100           |       1,895.5 ns |        52.27 ns |       154.11 ns |       1,912.2 ns |    0.0916 |        - |        - |     7104 B |
| ProcessViaPreAllocated    | 1           | 100           |         549.6 ns |        11.13 ns |        22.73 ns |         546.7 ns |    0.0248 |        - |        - |     1904 B |
| **ProcessViaConcurrentBag**   | **1**           | **100000**        |   **1,306,341.8 ns** |    **26,021.34 ns** |    **39,737.34 ns** |   **1,304,661.2 ns** |   **19.5313** |  **17.5781** |  **17.5781** |  **1450538 B** |
| ProcessViaConcurrentQueue | 1           | 100000        |   1,795,102.0 ns |     7,623.85 ns |     6,758.35 ns |   1,795,734.1 ns |  398.4375 | 398.4375 | 398.4375 |  1453300 B |
| ProcessViaConcurrentStack | 1           | 100000        |   1,376,015.7 ns |    27,300.95 ns |    68,492.75 ns |   1,378,641.8 ns |   72.2656 |        - |        - |  5601504 B |
| ProcessViaPreAllocated    | 1           | 100000        |     177,236.5 ns |     3,498.38 ns |     5,649.23 ns |     176,872.5 ns |  124.7559 | 124.7559 | 124.7559 |   401588 B |
| **ProcessViaConcurrentBag**   | **2**           | **1**             |       **1,998.0 ns** |        **25.28 ns** |        **23.65 ns** |       **2,005.2 ns** |    **0.0916** |   **0.0877** |        **-** |     **2138 B** |
| ProcessViaConcurrentQueue | 2           | 1             |       1,730.8 ns |        34.36 ns |        91.72 ns |       1,766.4 ns |    0.0305 |        - |        - |     2376 B |
| ProcessViaConcurrentStack | 2           | 1             |       1,656.7 ns |        30.65 ns |        28.67 ns |       1,658.4 ns |    0.0229 |        - |        - |     1832 B |
| ProcessViaPreAllocated    | 2           | 1             |       1,725.4 ns |        31.47 ns |        29.43 ns |       1,718.4 ns |    0.0229 |        - |        - |     1728 B |
| **ProcessViaConcurrentBag**   | **2**           | **4**             |       **1,999.2 ns** |        **39.21 ns** |        **57.48 ns** |       **2,001.0 ns** |    **0.0267** |   **0.0229** |        **-** |     **2176 B** |
| ProcessViaConcurrentQueue | 2           | 4             |       1,819.0 ns |        35.59 ns |        43.70 ns |       1,829.4 ns |    0.0305 |        - |        - |     2400 B |
| ProcessViaConcurrentStack | 2           | 4             |       1,591.7 ns |        31.53 ns |        41.00 ns |       1,603.6 ns |    0.0286 |        - |        - |     2168 B |
| ProcessViaPreAllocated    | 2           | 4             |       1,621.7 ns |        23.81 ns |        22.27 ns |       1,620.8 ns |    0.0229 |        - |        - |     1752 B |
| **ProcessViaConcurrentBag**   | **2**           | **25**            |       **2,598.5 ns** |        **36.58 ns** |        **32.43 ns** |       **2,608.1 ns** |    **0.0305** |   **0.0267** |        **-** |     **2584 B** |
| ProcessViaConcurrentQueue | 2           | 25            |       2,526.3 ns |        47.13 ns |        48.40 ns |       2,512.9 ns |    0.0420 |        - |        - |     3342 B |
| ProcessViaConcurrentStack | 2           | 25            |       1,798.9 ns |        28.13 ns |        24.94 ns |       1,798.3 ns |    0.0572 |        - |        - |     4522 B |
| ProcessViaPreAllocated    | 2           | 25            |       1,617.8 ns |        32.42 ns |        83.10 ns |       1,642.2 ns |    0.0305 |   0.0019 |   0.0019 |          - |
| **ProcessViaConcurrentBag**   | **2**           | **100**           |       **4,505.0 ns** |        **82.59 ns** |        **73.21 ns** |       **4,495.9 ns** |    **0.0610** |   **0.0534** |        **-** |     **4808 B** |
| ProcessViaConcurrentQueue | 2           | 100           |      15,641.2 ns |       306.80 ns |       409.57 ns |      15,807.5 ns |    0.0610 |        - |        - |     5216 B |
| ProcessViaConcurrentStack | 2           | 100           |       3,148.6 ns |        44.98 ns |        39.87 ns |       3,152.3 ns |    0.1907 |   0.0038 |   0.0038 |          - |
| ProcessViaPreAllocated    | 2           | 100           |       1,703.7 ns |        32.38 ns |        38.54 ns |       1,712.5 ns |    0.0324 |        - |        - |     2520 B |
| **ProcessViaConcurrentBag**   | **2**           | **100000**        |   **2,015,863.2 ns** |    **39,638.93 ns** |    **71,477.18 ns** |   **2,014,026.9 ns** |   **41.0156** |  **37.1094** |  **37.1094** |  **2899958 B** |
| ProcessViaConcurrentQueue | 2           | 100000        |  15,361,866.7 ns |   305,289.87 ns |   713,605.29 ns |  15,366,635.5 ns |   31.2500 |  31.2500 |  31.2500 |  2902542 B |
| ProcessViaConcurrentStack | 2           | 100000        |   1,625,278.9 ns |    96,499.62 ns |   284,531.33 ns |   1,743,364.8 ns |  216.7969 |        - |        - | 11201746 B |
| ProcessViaPreAllocated    | 2           | 100000        |     249,406.8 ns |     8,314.58 ns |    24,515.73 ns |     254,900.9 ns |  249.7559 | 249.7559 | 249.7559 |   801982 B |
| **ProcessViaConcurrentBag**   | **4**           | **1**             |       **2,547.3 ns** |        **38.89 ns** |        **36.38 ns** |       **2,533.7 ns** |    **0.0343** |   **0.0305** |        **-** |     **2369 B** |
| ProcessViaConcurrentQueue | 4           | 1             |       1,970.5 ns |        38.92 ns |        47.79 ns |       1,976.2 ns |    0.0305 |        - |        - |     2529 B |
| ProcessViaConcurrentStack | 4           | 1             |       1,687.0 ns |        32.83 ns |        47.09 ns |       1,699.3 ns |    0.0267 |        - |        - |     2102 B |
| ProcessViaPreAllocated    | 4           | 1             |       1,674.6 ns |        31.80 ns |        34.03 ns |       1,675.0 ns |    0.0343 |        - |        - |     1856 B |
| **ProcessViaConcurrentBag**   | **4**           | **4**             |       **3,000.9 ns** |        **59.70 ns** |       **149.76 ns** |       **2,970.9 ns** |    **0.0305** |   **0.0267** |        **-** |     **2532 B** |
| ProcessViaConcurrentQueue | 4           | 4             |       2,357.6 ns |        46.61 ns |       109.87 ns |       2,370.4 ns |    0.0343 |        - |        - |     2646 B |
| ProcessViaConcurrentStack | 4           | 4             |       1,825.1 ns |        35.59 ns |        40.99 ns |       1,827.9 ns |    0.0362 |        - |        - |     2755 B |
| ProcessViaPreAllocated    | 4           | 4             |       1,772.4 ns |        31.13 ns |        27.59 ns |       1,776.0 ns |    0.0324 |        - |        - |     1900 B |
| **ProcessViaConcurrentBag**   | **4**           | **25**            |       **4,311.2 ns** |        **83.71 ns** |       **169.10 ns** |       **4,276.6 ns** |    **0.0458** |   **0.0381** |        **-** |     **3854 B** |
| ProcessViaConcurrentQueue | 4           | 25            |       8,283.2 ns |       163.84 ns |       218.72 ns |       8,286.3 ns |    0.0610 |        - |        - |     5247 B |
| ProcessViaConcurrentStack | 4           | 25            |       3,203.3 ns |        63.19 ns |        77.60 ns |       3,182.3 ns |    0.1030 |        - |        - |     7712 B |
| ProcessViaPreAllocated    | 4           | 25            |       2,079.8 ns |        40.95 ns |        54.66 ns |       2,075.0 ns |    0.0305 |        - |        - |     2363 B |
| **ProcessViaConcurrentBag**   | **4**           | **100**           |       **7,052.2 ns** |       **104.08 ns** |        **97.36 ns** |       **7,066.9 ns** |    **0.1068** |   **0.0992** |        **-** |     **8197 B** |
| ProcessViaConcurrentQueue | 4           | 100           |      48,615.5 ns |       641.35 ns |       599.92 ns |      48,618.5 ns |    0.0610 |        - |        - |     8767 B |
| ProcessViaConcurrentStack | 4           | 100           |       5,355.4 ns |        61.68 ns |        57.70 ns |       5,342.2 ns |    0.4654 |   0.0076 |   0.0076 |          - |
| ProcessViaPreAllocated    | 4           | 100           |       2,497.3 ns |        49.91 ns |        83.39 ns |       2,509.2 ns |    0.1335 |        - |        - |     3578 B |
| **ProcessViaConcurrentBag**   | **4**           | **100000**        |   **3,312,547.1 ns** |    **62,345.39 ns** |   **107,542.75 ns** |   **3,306,994.9 ns** |   **74.2188** |  **66.4063** |  **66.4063** |  **5798974 B** |
| ProcessViaConcurrentQueue | 4           | 100000        |  51,744,837.7 ns |   880,794.00 ns |   823,895.26 ns |  51,777,116.2 ns |         - |        - |        - |  5799971 B |
| ProcessViaConcurrentStack | 4           | 100000        |   1,788,770.0 ns |    51,673.47 ns |   152,360.39 ns |   1,795,027.7 ns |  292.9688 |        - |        - | 22402201 B |
| ProcessViaPreAllocated    | 4           | 100000        |     343,429.0 ns |    14,230.90 ns |    40,370.69 ns |     329,369.2 ns |  499.0234 | 499.0234 | 499.0234 |  1602698 B |
| **ProcessViaConcurrentBag**   | **8**           | **1**             |       **3,944.8 ns** |        **86.40 ns** |       **254.75 ns** |       **3,899.6 ns** |    **0.0381** |   **0.0305** |        **-** |     **2941 B** |
| ProcessViaConcurrentQueue | 8           | 1             |       3,065.7 ns |        58.92 ns |        70.14 ns |       3,053.7 ns |    0.0381 |        - |        - |     2995 B |
| ProcessViaConcurrentStack | 8           | 1             |       2,298.3 ns |        64.77 ns |       190.97 ns |       2,303.8 ns |    0.0343 |        - |        - |     2646 B |
| ProcessViaPreAllocated    | 8           | 1             |       2,343.9 ns |        46.34 ns |       101.71 ns |       2,350.9 ns |    0.0267 |        - |        - |     2227 B |
| **ProcessViaConcurrentBag**   | **8**           | **4**             |       **4,522.8 ns** |        **89.88 ns** |       **239.90 ns** |       **4,504.3 ns** |    **0.0381** |   **0.0305** |        **-** |     **3332 B** |
| ProcessViaConcurrentQueue | 8           | 4             |       3,766.6 ns |        74.83 ns |       173.44 ns |       3,727.2 ns |    0.0420 |        - |        - |     3160 B |
| ProcessViaConcurrentStack | 8           | 4             |       3,149.8 ns |        61.91 ns |       113.20 ns |       3,179.0 ns |    0.0534 |        - |        - |     4105 B |
| ProcessViaPreAllocated    | 8           | 4             |       2,411.3 ns |        45.62 ns |        54.31 ns |       2,413.6 ns |    0.0305 |        - |        - |     2328 B |
| **ProcessViaConcurrentBag**   | **8**           | **25**            |       **7,258.0 ns** |       **143.55 ns** |       **309.01 ns** |       **7,302.6 ns** |    **0.0763** |   **0.0610** |        **-** |     **5987 B** |
| ProcessViaConcurrentQueue | 8           | 25            |      30,262.2 ns |       578.17 ns |       593.73 ns |      30,184.3 ns |    0.0610 |        - |        - |     6502 B |
| ProcessViaConcurrentStack | 8           | 25            |       4,880.4 ns |        96.58 ns |       107.35 ns |       4,913.6 ns |    0.2365 |        - |        - |    13825 B |
| ProcessViaPreAllocated    | 8           | 25            |       3,141.5 ns |        62.64 ns |       142.66 ns |       3,116.1 ns |    0.0420 |        - |        - |     3125 B |
| **ProcessViaConcurrentBag**   | **8**           | **100**           |      **11,929.0 ns** |       **164.62 ns** |       **153.99 ns** |      **11,984.3 ns** |    **0.1984** |   **0.1831** |        **-** |    **15097 B** |
| ProcessViaConcurrentQueue | 8           | 100           |     152,268.0 ns |     2,286.92 ns |     2,139.19 ns |     152,531.1 ns |         - |        - |        - |    15599 B |
| ProcessViaConcurrentStack | 8           | 100           |       7,162.4 ns |       131.01 ns |       122.54 ns |       7,157.2 ns |    0.6256 |        - |        - |    47638 B |
| ProcessViaPreAllocated    | 8           | 100           |       4,002.5 ns |        79.31 ns |       174.08 ns |       4,001.8 ns |    0.0763 |        - |        - |     5689 B |
| **ProcessViaConcurrentBag**   | **8**           | **100000**        |   **5,636,706.2 ns** |   **112,651.27 ns** |   **258,835.04 ns** |   **5,643,433.4 ns** |  **132.8125** | **117.1875** | **117.1875** | **11598135 B** |
| ProcessViaConcurrentQueue | 8           | 100000        | 161,978,962.3 ns | 3,198,053.87 ns | 4,044,514.52 ns | 162,848,665.5 ns |         - |        - |        - | 11595440 B |
| ProcessViaConcurrentStack | 8           | 100000        |   2,973,386.7 ns |    55,143.78 ns |    48,883.53 ns |   2,964,454.4 ns | 1011.7188 |        - |        - | 44803205 B |
| ProcessViaPreAllocated    | 8           | 100000        |     595,965.4 ns |    38,783.51 ns |   114,354.07 ns |     564,794.2 ns |  999.0234 | 999.0234 | 999.0234 |  3203921 B |
