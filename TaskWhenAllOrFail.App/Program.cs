using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWhenAllOrFail.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Func<CancellationToken, int>> funcs = new List<Func<CancellationToken, int>>();

            for (int i = 0; i < 1000; i++)
            {
                int index = i;

                if (i == 500)
                {
                    funcs.Add((cToken) => { throw new NotImplementedException(); });
                }
                else
                {
                    funcs.Add((cToken) =>
                    {
                        if (cToken.IsCancellationRequested)
                        {
                            Console.WriteLine($"I was canceled id:{index}");
                            return -1;
                        }

                        Console.WriteLine($"Task number {index} is sleeping");

                        var rand = new Random(100);

                        Thread.Sleep(rand.Next(100, 200));

                        if (cToken.IsCancellationRequested)
                        {
                            Console.WriteLine($"I was canceled id:{index}");
                            return -1;
                        }

                        Console.WriteLine($"Task number {index} is done");
                        return index;
                    });
                }

            }

            Task<int[]> task = funcs.WhenAllOrFail();

            try
            {
                task.Wait();

                int[] result = task.Result;


                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine($"result number {i} was {result[i]}");
                }

            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.Flatten().InnerExceptions)
                {
                    Console.WriteLine(e);
                }
            }

            Console.Read();

        }
    }
}
