```

BenchmarkDotNet v0.15.8, Linux Ubuntu 24.04.4 LTS (Noble Numbat)
Intel Core i9-14900HX 0.80GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.2 (10.0.2, 10.0.225.61305), X64 RyuJIT x86-64-v3


```
| Method                         | Values            | TestedString         | Mean        | Error     | StdDev    | Median      | Code Size | Allocated |
|------------------------------- |------------------ |--------------------- |------------:|----------:|----------:|------------:|----------:|----------:|
| **ViaHashSet**                     | **a**                 | **1**                    |   **2.4500 ns** | **0.0725 ns** | **0.0678 ns** |   **2.4151 ns** |     **488 B** |         **-** |
| ViaFrozenSet                   | a                 | 1                    |   0.7946 ns | 0.0049 ns | 0.0038 ns |   0.7931 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | 1                    |   0.2627 ns | 0.0004 ns | 0.0003 ns |   0.2626 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | 1                    |   1.3197 ns | 0.0012 ns | 0.0011 ns |   1.3194 ns |     497 B |         - |
| **ViaHashSet**                     | **a**                 | **11**                   |   **4.4780 ns** | **0.0038 ns** | **0.0030 ns** |   **4.4770 ns** |     **501 B** |         **-** |
| ViaFrozenSet                   | a                 | 11                   |   1.7396 ns | 0.0058 ns | 0.0054 ns |   1.7416 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | 11                   |   1.0248 ns | 0.0008 ns | 0.0007 ns |   1.0246 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | 11                   |   1.5864 ns | 0.0038 ns | 0.0030 ns |   1.5860 ns |     497 B |         - |
| **ViaHashSet**                     | **a**                 | **111**                  |   **6.6224 ns** | **0.0198 ns** | **0.0186 ns** |   **6.6198 ns** |     **493 B** |         **-** |
| ViaFrozenSet                   | a                 | 111                  |   2.7753 ns | 0.0059 ns | 0.0055 ns |   2.7767 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | 111                  |   1.8821 ns | 0.0534 ns | 0.0500 ns |   1.8737 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | 111                  |   2.1187 ns | 0.0034 ns | 0.0027 ns |   2.1194 ns |     501 B |         - |
| **ViaHashSet**                     | **a**                 | **11a**                  |   **6.3015 ns** | **0.0082 ns** | **0.0068 ns** |   **6.2999 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | a                 | 11a                  |   2.6065 ns | 0.0192 ns | 0.0160 ns |   2.5996 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | 11a                  |   2.1200 ns | 0.0715 ns | 0.0669 ns |   2.0979 ns |     122 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | 11a                  |   2.6319 ns | 0.0204 ns | 0.0181 ns |   2.6389 ns |     499 B |         - |
| **ViaHashSet**                     | **a**                 | **a**                    |   **1.8597 ns** | **0.0252 ns** | **0.0224 ns** |   **1.8532 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | a                 | a                    |   0.5273 ns | 0.0007 ns | 0.0006 ns |   0.5272 ns |     180 B |         - |
| ViaCharsSearchValues           | a                 | a                    |   0.2850 ns | 0.0189 ns | 0.0167 ns |   0.2845 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | a                    |   1.3358 ns | 0.0005 ns | 0.0004 ns |   1.3357 ns |     497 B |         - |
| **ViaHashSet**                     | **a**                 | **a11**                  |   **1.8580 ns** | **0.0167 ns** | **0.0139 ns** |   **1.8504 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | a                 | a11                  |   0.5286 ns | 0.0006 ns | 0.0005 ns |   0.5286 ns |     180 B |         - |
| ViaCharsSearchValues           | a                 | a11                  |   0.2679 ns | 0.0072 ns | 0.0064 ns |   0.2644 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | a11                  |   1.3228 ns | 0.0025 ns | 0.0021 ns |   1.3218 ns |     497 B |         - |
| **ViaHashSet**                     | **a**                 | **ab**                   |   **1.8556 ns** | **0.0102 ns** | **0.0085 ns** |   **1.8536 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | a                 | ab                   |   0.5311 ns | 0.0036 ns | 0.0032 ns |   0.5295 ns |     180 B |         - |
| ViaCharsSearchValues           | a                 | ab                   |   0.2780 ns | 0.0160 ns | 0.0150 ns |   0.2728 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | ab                   |   1.3339 ns | 0.0140 ns | 0.0124 ns |   1.3389 ns |     497 B |         - |
| **ViaHashSet**                     | **a**                 | **ba**                   |   **3.2956 ns** | **0.0185 ns** | **0.0173 ns** |   **3.2957 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | a                 | ba                   |   1.0628 ns | 0.0020 ns | 0.0018 ns |   1.0633 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | ba                   |   1.4634 ns | 0.0209 ns | 0.0185 ns |   1.4656 ns |     122 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | ba                   |   1.8561 ns | 0.0321 ns | 0.0284 ns |   1.8396 ns |     499 B |         - |
| **ViaHashSet**                     | **a**                 | **LONGS(...)TaINS [49]** |  **92.5513 ns** | **0.1394 ns** | **0.1304 ns** |  **92.5970 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | a                 | LONGS(...)TaINS [49] |  51.6247 ns | 0.3767 ns | 0.3524 ns |  51.6466 ns |     183 B |         - |
| ViaCharsSearchValues           | a                 | LONGS(...)TaINS [49] |  36.9830 ns | 0.0876 ns | 0.0777 ns |  37.0030 ns |     122 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | LONGS(...)TaINS [49] |   2.3826 ns | 0.0186 ns | 0.0156 ns |   2.3716 ns |     527 B |         - |
| **ViaHashSet**                     | **a**                 | **LONGS(...)11111 [48]** |  **28.6235 ns** | **0.2046 ns** | **0.1813 ns** |  **28.6039 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | a                 | LONGS(...)11111 [48] |  16.3528 ns | 0.0030 ns | 0.0028 ns |  16.3520 ns |     196 B |         - |
| ViaCharsSearchValues           | a                 | LONGS(...)11111 [48] |  13.3682 ns | 0.0489 ns | 0.0458 ns |  13.3746 ns |     122 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | LONGS(...)11111 [48] |   1.8452 ns | 0.0011 ns | 0.0009 ns |   1.8453 ns |     518 B |         - |
| **ViaHashSet**                     | **a**                 | **LONGS(...)11111 [42]** |  **86.2708 ns** | **0.1788 ns** | **0.1493 ns** |  **86.2823 ns** |     **497 B** |         **-** |
| ViaFrozenSet                   | a                 | LONGS(...)11111 [42] |  56.3389 ns | 0.1014 ns | 0.0847 ns |  56.3479 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | LONGS(...)11111 [42] |  34.7700 ns | 0.5602 ns | 0.5753 ns |  34.5230 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | LONGS(...)11111 [42] |   2.1447 ns | 0.0155 ns | 0.0138 ns |   2.1429 ns |     545 B |         - |
| **ViaHashSet**                     | **a**                 | **MEDSC_a**              |  **11.7780 ns** | **0.0456 ns** | **0.0427 ns** |  **11.7561 ns** |     **495 B** |         **-** |
| ViaFrozenSet                   | a                 | MEDSC_a              |   7.4778 ns | 0.0036 ns | 0.0030 ns |   7.4776 ns |     156 B |         - |
| ViaCharsSearchValues           | a                 | MEDSC_a              |   5.3333 ns | 0.0034 ns | 0.0030 ns |   5.3333 ns |     122 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | MEDSC_a              |   2.8851 ns | 0.0260 ns | 0.0243 ns |   2.8936 ns |     504 B |         - |
| **ViaHashSet**                     | **a**                 | **MEDSN_1**              |  **11.4582 ns** | **0.0019 ns** | **0.0015 ns** |  **11.4579 ns** |     **489 B** |         **-** |
| ViaFrozenSet                   | a                 | MEDSN_1              |   7.4119 ns | 0.0078 ns | 0.0061 ns |   7.4114 ns |     160 B |         - |
| ViaCharsSearchValues           | a                 | MEDSN_1              |   6.0105 ns | 0.0162 ns | 0.0152 ns |   6.0028 ns |     118 B |         - |
| ViaCharsSearchValuesIndexOfAny | a                 | MEDSN_1              |   2.3920 ns | 0.0176 ns | 0.0147 ns |   2.3868 ns |     503 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **1**                    |   **2.4036 ns** | **0.0158 ns** | **0.0147 ns** |   **2.4057 ns** |     **488 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | 1                    |   1.0649 ns | 0.0136 ns | 0.0128 ns |   1.0562 ns |     233 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | 1                    |   0.5254 ns | 0.0047 ns | 0.0044 ns |   0.5239 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | 1                    |   1.0425 ns | 0.0067 ns | 0.0063 ns |   1.0417 ns |     580 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **11**                   |   **4.4640 ns** | **0.0133 ns** | **0.0124 ns** |   **4.4589 ns** |     **493 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | 11                   |   2.9527 ns | 0.0145 ns | 0.0121 ns |   2.9472 ns |     233 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | 11                   |   1.1585 ns | 0.0029 ns | 0.0024 ns |   1.1584 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | 11                   |   1.5768 ns | 0.0065 ns | 0.0061 ns |   1.5784 ns |     580 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **111**                  |   **6.5701 ns** | **0.0640 ns** | **0.0568 ns** |   **6.5399 ns** |     **493 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | 111                  |   4.6390 ns | 0.0096 ns | 0.0085 ns |   4.6402 ns |     249 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | 111                  |   2.0803 ns | 0.0033 ns | 0.0030 ns |   2.0799 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | 111                  |   1.9992 ns | 0.0237 ns | 0.0210 ns |   1.9990 ns |     580 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **11a**                  |   **6.3363 ns** | **0.0111 ns** | **0.0093 ns** |   **6.3362 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | 11a                  |   5.3543 ns | 0.0012 ns | 0.0010 ns |   5.3540 ns |     257 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | 11a                  |   1.9665 ns | 0.0297 ns | 0.0264 ns |   1.9654 ns |     147 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | 11a                  |   2.1832 ns | 0.0243 ns | 0.0227 ns |   2.1817 ns |     599 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **a**                    |   **1.8055 ns** | **0.0092 ns** | **0.0086 ns** |   **1.8034 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | a                    |   1.4560 ns | 0.0008 ns | 0.0007 ns |   1.4561 ns |     259 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | a                    |   0.5287 ns | 0.0012 ns | 0.0010 ns |   0.5284 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | a                    |   1.0564 ns | 0.0004 ns | 0.0004 ns |   1.0565 ns |     584 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **a11**                  |   **1.8095 ns** | **0.0087 ns** | **0.0077 ns** |   **1.8079 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | a11                  |   1.4586 ns | 0.0068 ns | 0.0056 ns |   1.4564 ns |     259 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | a11                  |   0.5372 ns | 0.0131 ns | 0.0116 ns |   0.5344 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | a11                  |   1.0775 ns | 0.0161 ns | 0.0143 ns |   1.0738 ns |     584 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **ab**                   |   **1.8087 ns** | **0.0116 ns** | **0.0103 ns** |   **1.8097 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | ab                   |   1.4718 ns | 0.0162 ns | 0.0135 ns |   1.4657 ns |     259 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | ab                   |   0.5510 ns | 0.0141 ns | 0.0125 ns |   0.5505 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | ab                   |   1.0537 ns | 0.0031 ns | 0.0024 ns |   1.0543 ns |     584 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **ba**                   |   **1.8153 ns** | **0.0093 ns** | **0.0087 ns** |   **1.8142 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | ba                   |   1.4677 ns | 0.0041 ns | 0.0034 ns |   1.4663 ns |     259 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | ba                   |   0.5285 ns | 0.0004 ns | 0.0003 ns |   0.5284 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | ba                   |   1.0554 ns | 0.0029 ns | 0.0024 ns |   1.0563 ns |     584 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **LONGS(...)TaINS [49]** |  **97.9139 ns** | **0.1557 ns** | **0.1300 ns** |  **97.9017 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | LONGS(...)TaINS [49] |  81.4018 ns | 0.7524 ns | 0.7038 ns |  81.4190 ns |     270 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | LONGS(...)TaINS [49] |  45.9172 ns | 0.4196 ns | 0.3925 ns |  45.9018 ns |     147 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | LONGS(...)TaINS [49] |   2.6651 ns | 0.0209 ns | 0.0185 ns |   2.6586 ns |     598 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **LONGS(...)11111 [48]** |  **34.0001 ns** | **0.4847 ns** | **0.4534 ns** |  **33.8980 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | LONGS(...)11111 [48] |  26.0296 ns | 0.0184 ns | 0.0163 ns |  26.0242 ns |     279 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | LONGS(...)11111 [48] |  13.2331 ns | 0.0121 ns | 0.0101 ns |  13.2286 ns |     147 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | LONGS(...)11111 [48] |   1.5835 ns | 0.0020 ns | 0.0016 ns |   1.5838 ns |     610 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **LONGS(...)11111 [42]** |  **89.4148 ns** | **0.1595 ns** | **0.1414 ns** |  **89.3810 ns** |     **497 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | LONGS(...)11111 [42] |  80.6535 ns | 0.5397 ns | 0.5048 ns |  80.4337 ns |     251 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | LONGS(...)11111 [42] |  41.2927 ns | 0.8306 ns | 0.8158 ns |  41.2895 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | LONGS(...)11111 [42] |   1.9604 ns | 0.0092 ns | 0.0082 ns |   1.9583 ns |     585 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **MEDSC_a**              |  **16.4957 ns** | **0.0474 ns** | **0.0396 ns** |  **16.4787 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | MEDSC_a              |  11.5742 ns | 0.0179 ns | 0.0149 ns |  11.5699 ns |     251 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | MEDSC_a              |   5.3778 ns | 0.0226 ns | 0.0211 ns |   5.3674 ns |     147 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | MEDSC_a              |   3.4995 ns | 0.0309 ns | 0.0274 ns |   3.5022 ns |     599 B |         - |
| **ViaHashSet**                     | **abcdefghz02345679** | **MEDSN_1**              |  **15.1154 ns** | **0.0244 ns** | **0.0204 ns** |  **15.1143 ns** |     **489 B** |         **-** |
| ViaFrozenSet                   | abcdefghz02345679 | MEDSN_1              |  11.5090 ns | 0.0522 ns | 0.0463 ns |  11.4894 ns |     245 B |         - |
| ViaCharsSearchValues           | abcdefghz02345679 | MEDSN_1              |   6.3550 ns | 0.0037 ns | 0.0029 ns |   6.3550 ns |     145 B |         - |
| ViaCharsSearchValuesIndexOfAny | abcdefghz02345679 | MEDSN_1              |   4.6011 ns | 0.0082 ns | 0.0073 ns |   4.5993 ns |     584 B |         - |
| **ViaHashSet**                     | **az**                | **1**                    |   **2.4731 ns** | **0.0880 ns** | **0.0979 ns** |   **2.3933 ns** |     **488 B** |         **-** |
| ViaFrozenSet                   | az                | 1                    |   0.8411 ns | 0.0027 ns | 0.0022 ns |   0.8408 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | 1                    |   0.2641 ns | 0.0005 ns | 0.0004 ns |   0.2640 ns |     128 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | 1                    |   1.8668 ns | 0.0232 ns | 0.0206 ns |   1.8626 ns |     657 B |         - |
| **ViaHashSet**                     | **az**                | **11**                   |   **4.7163 ns** | **0.0470 ns** | **0.0440 ns** |   **4.7191 ns** |     **493 B** |         **-** |
| ViaFrozenSet                   | az                | 11                   |   1.7427 ns | 0.0081 ns | 0.0072 ns |   1.7405 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | 11                   |   1.1801 ns | 0.0008 ns | 0.0007 ns |   1.1799 ns |     128 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | 11                   |   2.6411 ns | 0.0011 ns | 0.0010 ns |   2.6412 ns |     657 B |         - |
| **ViaHashSet**                     | **az**                | **111**                  |   **6.6075 ns** | **0.0156 ns** | **0.0146 ns** |   **6.6060 ns** |     **493 B** |         **-** |
| ViaFrozenSet                   | az                | 111                  |   2.7720 ns | 0.0053 ns | 0.0049 ns |   2.7725 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | 111                  |   2.2869 ns | 0.0206 ns | 0.0193 ns |   2.2758 ns |     128 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | 111                  |   3.5153 ns | 0.0328 ns | 0.0307 ns |   3.5238 ns |     657 B |         - |
| **ViaHashSet**                     | **az**                | **11a**                  |   **6.2938 ns** | **0.0107 ns** | **0.0090 ns** |   **6.2901 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | az                | 11a                  |   2.6028 ns | 0.0095 ns | 0.0183 ns |   2.5967 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | 11a                  |   2.4474 ns | 0.0528 ns | 0.0494 ns |   2.4547 ns |     130 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | 11a                  |   3.1609 ns | 0.0291 ns | 0.0258 ns |   3.1629 ns |     658 B |         - |
| **ViaHashSet**                     | **az**                | **a**                    |   **1.8500 ns** | **0.0006 ns** | **0.0004 ns** |   **1.8499 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | az                | a                    |   0.2582 ns | 0.0012 ns | 0.0011 ns |   0.2584 ns |     154 B |         - |
| ViaCharsSearchValues           | az                | a                    |   0.2646 ns | 0.0004 ns | 0.0003 ns |   0.2645 ns |     153 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | a                    |   1.5999 ns | 0.0206 ns | 0.0192 ns |   1.5945 ns |     665 B |         - |
| **ViaHashSet**                     | **az**                | **a11**                  |   **1.8491 ns** | **0.0016 ns** | **0.0012 ns** |   **1.8486 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | az                | a11                  |   0.5281 ns | 0.0002 ns | 0.0002 ns |   0.5281 ns |     180 B |         - |
| ViaCharsSearchValues           | az                | a11                  |   0.2639 ns | 0.0002 ns | 0.0002 ns |   0.2639 ns |     153 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | a11                  |   1.5862 ns | 0.0025 ns | 0.0021 ns |   1.5864 ns |     665 B |         - |
| **ViaHashSet**                     | **az**                | **ab**                   |   **1.9128 ns** | **0.0453 ns** | **0.0401 ns** |   **1.9129 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | az                | ab                   |   0.5276 ns | 0.0003 ns | 0.0002 ns |   0.5276 ns |     180 B |         - |
| ViaCharsSearchValues           | az                | ab                   |   0.2653 ns | 0.0037 ns | 0.0031 ns |   0.2641 ns |     153 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | ab                   |   1.5851 ns | 0.0007 ns | 0.0006 ns |   1.5850 ns |     665 B |         - |
| **ViaHashSet**                     | **az**                | **ba**                   |   **4.3728 ns** | **0.0150 ns** | **0.0125 ns** |   **4.3711 ns** |     **499 B** |         **-** |
| ViaFrozenSet                   | az                | ba                   |   2.1536 ns | 0.0105 ns | 0.0087 ns |   2.1557 ns |     154 B |         - |
| ViaCharsSearchValues           | az                | ba                   |   2.1605 ns | 0.0344 ns | 0.0287 ns |   2.1666 ns |     130 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | ba                   |   2.3776 ns | 0.0006 ns | 0.0005 ns |   2.3776 ns |     658 B |         - |
| **ViaHashSet**                     | **az**                | **LONGS(...)TaINS [49]** | **113.8970 ns** | **0.1914 ns** | **0.1696 ns** | **113.8314 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | az                | LONGS(...)TaINS [49] |  51.2855 ns | 0.9250 ns | 0.8652 ns |  51.4220 ns |     183 B |         - |
| ViaCharsSearchValues           | az                | LONGS(...)TaINS [49] |  47.4031 ns | 0.2631 ns | 0.2461 ns |  47.4787 ns |     130 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | LONGS(...)TaINS [49] |   2.3902 ns | 0.0003 ns | 0.0002 ns |   2.3902 ns |     671 B |         - |
| **ViaHashSet**                     | **az**                | **LONGS(...)11111 [48]** |  **29.8960 ns** | **0.0614 ns** | **0.0574 ns** |  **29.8978 ns** |     **495 B** |         **-** |
| ViaFrozenSet                   | az                | LONGS(...)11111 [48] |  16.3637 ns | 0.0102 ns | 0.0085 ns |  16.3597 ns |     196 B |         - |
| ViaCharsSearchValues           | az                | LONGS(...)11111 [48] |  14.1903 ns | 0.0047 ns | 0.0041 ns |  14.1893 ns |     130 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | LONGS(...)11111 [48] |   1.8492 ns | 0.0004 ns | 0.0003 ns |   1.8492 ns |     675 B |         - |
| **ViaHashSet**                     | **az**                | **LONGS(...)11111 [42]** | **100.4560 ns** | **0.1787 ns** | **0.1492 ns** | **100.4467 ns** |     **489 B** |         **-** |
| ViaFrozenSet                   | az                | LONGS(...)11111 [42] |  56.2357 ns | 0.1682 ns | 0.1405 ns |  56.2332 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | LONGS(...)11111 [42] |  45.2257 ns | 0.2948 ns | 0.2757 ns |  45.1353 ns |     128 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | LONGS(...)11111 [42] |   2.0893 ns | 0.0131 ns | 0.0103 ns |   2.0853 ns |     675 B |         - |
| **ViaHashSet**                     | **az**                | **MEDSC_a**              |  **14.1692 ns** | **0.0160 ns** | **0.0150 ns** |  **14.1627 ns** |     **487 B** |         **-** |
| ViaFrozenSet                   | az                | MEDSC_a              |   7.4778 ns | 0.0047 ns | 0.0044 ns |   7.4770 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | MEDSC_a              |   7.7869 ns | 0.0044 ns | 0.0039 ns |   7.7848 ns |     130 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | MEDSC_a              |   3.3110 ns | 0.0547 ns | 0.0511 ns |   3.3216 ns |     589 B |         - |
| **ViaHashSet**                     | **az**                | **MEDSN_1**              |  **14.4589 ns** | **0.0313 ns** | **0.0292 ns** |  **14.4589 ns** |     **497 B** |         **-** |
| ViaFrozenSet                   | az                | MEDSN_1              |   7.5644 ns | 0.0075 ns | 0.0067 ns |   7.5664 ns |     156 B |         - |
| ViaCharsSearchValues           | az                | MEDSN_1              |   8.0801 ns | 0.0147 ns | 0.0123 ns |   8.0740 ns |     128 B |         - |
| ViaCharsSearchValuesIndexOfAny | az                | MEDSN_1              |   2.9192 ns | 0.0114 ns | 0.0096 ns |   2.9145 ns |     594 B |         - |
