WaitAsync

Without unobserved handler:

| Method               | IsThrowing | Mean     | Error    | StdDev   | Allocated |
|--------------------- |----------- |---------:|---------:|---------:|----------:|
| WhenAllDelay         | False      | 50.35 ms | 0.127 ms | 0.119 ms |     822 B |
| WhenAllDelayReversed | False      | 50.37 ms | 0.116 ms | 0.097 ms |     822 B |
| WaitAsyncTimeout     | False      | 50.41 ms | 0.119 ms | 0.099 ms |    1502 B |
| WhenAllDelay         | True       | 50.38 ms | 0.132 ms | 0.103 ms |    1286 B |
| WhenAllDelayReversed | True       | 50.33 ms | 0.090 ms | 0.080 ms |    1286 B |
| WaitAsyncTimeout     | True       | 50.34 ms | 0.070 ms | 0.062 ms |    1966 B |


With unobserved handler

| Method               | IsThrowing | Mean     | Error    | StdDev   | Allocated |
|--------------------- |----------- |---------:|---------:|---------:|----------:|
| WhenAllDelay         | False      | 50.32 ms | 0.118 ms | 0.111 ms |     794 B |
| WhenAllDelayReversed | False      | 50.33 ms | 0.066 ms | 0.051 ms |     822 B |
| WaitAsyncTimeout     | False      | 50.40 ms | 0.108 ms | 0.101 ms |    1502 B |
| WhenAllDelay         | True       | 50.30 ms | 0.109 ms | 0.096 ms |    1286 B |
| WhenAllDelayReversed | True       | 50.34 ms | 0.095 ms | 0.084 ms |    1286 B |
| WaitAsyncTimeout     | True       | 50.33 ms | 0.123 ms | 0.109 ms |    1966 B |
