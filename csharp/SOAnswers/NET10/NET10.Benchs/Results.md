#### StackAllocBench

| Method                  | Mean     | Allocated |
|------------------------ |---------:|----------:|
| WithStartNew            | 33.20 ns |         - |                                                                                                                   
| WithStartNewReturn      | 37.46 ns |      40 B |
| WithStartNewPass        | 40.73 ns |      40 B |
| WithStartNewPassInlined | 33.65 ns |         - |
| WithStartNewPassByRef   | 41.38 ns |      40 B |