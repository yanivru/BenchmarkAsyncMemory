using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkAsyncMemoryDotnetFramework
{
    internal class Program
    {
        private static int _i = 0;

        static void Main(string[] args)
        {
            BenchmarkRunner.Run<AsyncMemory>();

            var taskCompletionSource = new TaskCompletionSource<int>();

            for (int i = 0; i < 1_000_000; i++)
            {
                SomeAsyncMethod3(taskCompletionSource);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static async Task SomeAsyncMethod3(TaskCompletionSource<int> taskCompletionSource)
        {
            var i = await taskCompletionSource.Task;
        }
        private static async Task SomeAsyncMethod2(TaskCompletionSource<int> taskCompletionSource)
        {
            await Task.Yield();
            var i = await taskCompletionSource.Task;
            if(i == 0)
                await Task.Yield();
        }

        private static async Task SomeAsyncMethod(TaskCompletionSource<int> taskCompletionSource)
        {
            await Task.Yield();
            var i = await taskCompletionSource.Task;
            if(i == 0)
                await Task.Yield();
        }
    }

    [MemoryDiagnoser]
    public class AsyncMemory
    {
        private readonly TaskCompletionSource<int> _taskCompletionSource;

        public AsyncMemory()
        {
        }

        [Benchmark]
        public async Task AsyncWithStateMachine()
        {
            await Task.Delay(1);
        }
        
        [Benchmark]
        public async Task Async2LevelsWithStateMachine()
        {
            await FirstLevelStateMachine();
        }

        private async Task FirstLevelStateMachine()
        {
            await Task.Delay(1);
        }

        [Benchmark]
        public Task AsyncWithoutStateMachine()
        {
            return Task.Delay(1);
        }
        
        [Benchmark]
        public Task Async2LevelsWithoutStateMachine()
        {
            return FirstLevelNoStateMachine();
        }

        private Task FirstLevelNoStateMachine()
        {
            return Task.Delay(1);
        }

        [Benchmark]
        public async Task Async3LevelWithStateMachine()
        {
            await SecondLevelStateMachine();
        }

        private async Task SecondLevelStateMachine()
        {
            await FirstLevelStateMachine();
        }
    }
}
