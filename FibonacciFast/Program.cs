using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FibonacciFast
{
    class Program
    {
        private static List<Tuple<int, double>> _fiboKnown;
        private static List<Tuple<int, Tuple<int, int>>> _stack;
        private static int _number;

        static void Main(string[] args)
        {
            // These values we know
            _fiboKnown = new List<Tuple<int, double>> { new Tuple<int, double>(0, 0), new Tuple<int, double>(1, 1), new Tuple<int, double>(2, 1) };

            // We need a StopWatch to measure it's performance
            var sw = new Stopwatch();

            // Take the value from the keyboard and start the StopWatch
            ReadNo();
            sw.Start();

            // Here is our main stack of Tuples with the first Tuple inserted
            _stack = new List<Tuple<int, Tuple<int, int>>>();
            _stack.Add(new Tuple<int, Tuple<int, int>>(_number, new Tuple<int, int>(_number / 2, _number / 2 + 1)));

            // Here we create the entire stack of Tuples
            _stack = CreateStack(_stack);

            // Do the magic
            CreateFibonacci();
            sw.Stop();

            Console.WriteLine("The {0} number of Fibonacci is: {1}. Calculations done in {2}ms", _number, _fiboKnown.Last().Item2, sw.ElapsedMilliseconds);
            Console.ReadKey();
        }

        // Static method for key introducing
        static void ReadNo()
        {
            Console.WriteLine("Note: F(0) = 0, F(1) = 1, F(2) = 1, n>= 2");
            Console.WriteLine("Enter n, where n is the n'th Fibonnacci Number: ");
            var readLine = Convert.ToInt16(Console.ReadLine());
            if (readLine < 2)
                ReadNo();
            else
                _number = readLine;
        }

        // Here we create a stack of Tuple<int,Tuple<int,int>>
        // First, for n = 55(for example), we will have Tuple<55,Tuple<22,23>>
        // Then we will iterate through all the numbers from the second Tuple
        // And add other Tuple ultil we get tu Tuple<1,Tuple<0,1>>
        static List<Tuple<int, Tuple<int, int>>> CreateStack(List<Tuple<int, Tuple<int, int>>> stack)
        {
            for (var i = stack.Count - 1; i >= 0; i--)
            {
                if (stack.Any(t => t.Item1 == stack[i].Item2.Item1))
                    continue;
                stack.Add(new Tuple<int, Tuple<int, int>>(stack[i].Item2.Item1,
                    new Tuple<int, int>(stack[i].Item2.Item1 / 2, stack[i].Item2.Item1 / 2 + 1)));
                if (stack.Any(t => t.Item1 == stack[i].Item2.Item2))
                    continue;
                stack.Add(new Tuple<int, Tuple<int, int>>(stack[i].Item2.Item2,
                    new Tuple<int, int>(stack[i].Item2.Item2 / 2, stack[i].Item2.Item2 / 2 + 1)));
                CreateStack(stack);
            }

            stack.Sort((a, b) => b.Item1.CompareTo(a.Item1));
            return stack;
        }

        // After we have the entire stack of Tuples
        // We can create the Fibonacii's list of Tuples
        // By starting from the ones we already know
        // And when we encounter one we don't know, we just apply
        // The fast doubling formula for Fibonacci
        static void CreateFibonacci()
        {
            for (var i = _stack.Count - 1; i >= 0; i--)
            {
                var val1 = _fiboKnown.First(t => t.Item1 == _stack[i].Item2.Item1).Item2;
                var val2 = _fiboKnown.First(t => t.Item1 == _stack[i].Item2.Item2).Item2;
                var value = _stack[i].Item1 % 2 == 0 ? val1 * (2 * val2 - val1) : Math.Pow(val1, 2) + Math.Pow(val2, 2);
                _fiboKnown.Add(new Tuple<int, double>(_stack[i].Item1, value));
            }
        }
    }
}
