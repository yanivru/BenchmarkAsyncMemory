namespace BenchmarkAsyncMemory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var taskCompletionSource = new TaskCompletionSource();

            for (int i = 0; i < 1_000_000; i++)
            {
                SomeAsyncMethod3(taskCompletionSource);
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static async Task SomeAsyncMethod3(TaskCompletionSource taskCompletionSource)
        {
            await SomeAsyncMethod2(taskCompletionSource);
        }
        private static async Task SomeAsyncMethod2(TaskCompletionSource taskCompletionSource)
        {
            await SomeAsyncMethod(taskCompletionSource);
        }

        private static async Task SomeAsyncMethod(TaskCompletionSource taskCompletionSource)
        {
            await taskCompletionSource.Task;
        }
    }
}