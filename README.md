## BenchmarkAsyncMemory
# Points
* Async functions has some memory cost.
* Nesting async functions increases this cost linearly.
* Calling an async method with no state-machine (e.g. just returning a task) doesn't introduce this cost (free).
* My streaming application that fetch details from DB suffered from those costs.
* Async in hot paths might be costly. Can be avoided be using callbacks.

# Results
| Method                          | Mean     | Error    | StdDev   | Allocated |
|-------------------------------- |---------:|---------:|---------:|----------:|
| AsyncWithStateMachine           | 15.60 ms | 0.057 ms | 0.044 ms |     384 B |
| Async2LevelsWithStateMachine    | 15.56 ms | 0.068 ms | 0.057 ms |     512 B |
| AsyncWithoutStateMachine        | 15.59 ms | 0.038 ms | 0.031 ms |     256 B |
| Async2LevelsWithoutStateMachine | 15.52 ms | 0.118 ms | 0.092 ms |         - |
| Async3LevelWithStateMachine     | 15.59 ms | 0.041 ms | 0.032 ms |     640 B |
