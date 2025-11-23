#### SwStackAllocBench

| Method                  | Mean     | Allocated |
|------------------------ |---------:|----------:|
| WithNew                 | 33.30 ns |         - |                                                                                      
| WithStartNew            | 33.50 ns |         - |
| WithStartNewReturn      | 37.07 ns |      40 B |
| WithStartNewPass        | 39.97 ns |      40 B |
| WithStartNewPassInlined | 33.06 ns |         - |
| WithStartNewPassByRef   | 39.74 ns |      40 B |

#### ArrayStackAllocBench

| Method          | Runtime |         Mean | Allocated |
|---------------- |--------:|-------------:|----------:|
| ArrayStackAlloc |      10 |     3.478 ns |         - |
| ArrayStackAlloc |       9 |     12.64 ns |      48 B |