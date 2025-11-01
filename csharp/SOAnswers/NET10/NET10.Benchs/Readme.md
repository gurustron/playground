IterateArraySpanBench

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores                                                                                                                                                             
.NET SDK 10.0.100-rc.2.25502.107                                                                                                                                                                                                    
[Host]     : .NET 9.0.10 (9.0.1025.47515), X64 RyuJIT AVX2                                                                                                                                                                        
DefaultJob : .NET 9.0.10 (9.0.1025.47515), X64 RyuJIT AVX2


| Method | Mean      | Error     | StdDev    | Median    |
|------- |----------:|----------:|----------:|----------:|
| Span   |  9.228 ms | 0.2396 ms | 0.6873 ms |  8.957 ms |                                                                                                                                                                          
| Arr    | 10.603 ms | 0.4499 ms | 1.2908 ms | 10.857 ms |

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores                                                                                                                                                             
.NET SDK 10.0.100-rc.2.25502.107                                                                                                                                                                                                    
[Host]     : .NET 10.0.0 (10.0.25.50307), X64 RyuJIT AVX2                                                                                                                                                                         
DefaultJob : .NET 10.0.0 (10.0.25.50307), X64 RyuJIT AVX2


| Method | Mean     | Error     | StdDev    |
|------- |---------:|----------:|----------:|
| Span   | 9.816 ms | 0.4721 ms | 1.3697 ms |                                                                                                                                                                                       
| Arr    | 9.167 ms | 0.3098 ms | 0.8987 ms |