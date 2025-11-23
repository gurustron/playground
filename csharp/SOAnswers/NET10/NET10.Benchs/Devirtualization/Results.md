#### ReadOnlyCollectionEnumeratorDevirtualizationBench

> devirtualization for array’s interface implementation

.NET 9.0.10

| Method                | Runtime |       Mean | 
|-----------------------|---------|-----------:|
| SumEnumerableViaArray | 9       |   741.1 ns |                                                                                                  
| SumForLoopViaArray    | 9       | 2,398.6 ns | 
| SumEnumerableViaList  | 9       | 1,095.0 ns | 
| SumForLoopViaList     | 9       |   821.9 ns |
| SumEnumerableViaArray | 10      |   641.2 ns |                                                                                                    
| SumForLoopViaArray    | 10      |   585.8 ns | 
| SumEnumerableViaList  | 10      |   931.9 ns | 
| SumForLoopViaList     | 10      |   819.9 ns | 


#### GDVBenchs

.NET 10.0.0

| Method          |     Mean |
|-----------------|---------:|
| ViaInterface    | 18.02 us |                                                                                                          
| ViaAbstractBase | 13.29 us |
| Concrete        | 13.33 us |

.NET 9.0.10

| Method          |     Mean | 
|-----------------|---------:|
| ViaInterface    | 18.60 us |                                                                                                          
| ViaAbstractBase | 13.28 us | 
| Concrete        | 13.34 us |