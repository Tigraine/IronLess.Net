namespace IronLess.Compiler
{
    using System;
    using System.Diagnostics;
    using Wrapper;

    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify less filename to compile to css");
                return;
            }
            string inputFilename = args[0];
            string outputFileName = "";
            Console.WriteLine("Compiling {0} to {1}", inputFilename, outputFileName);
            var elapsedTime = CountMilliseconds(() =>
                      new IronLessExecuter().CompileLess(inputFilename)
                );

            Console.WriteLine("Compilation finished after {0} seconds", elapsedTime);
        }

        private delegate void ExecutingAction();

        private static long CountMilliseconds(ExecutingAction method)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            method();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }
    }
}