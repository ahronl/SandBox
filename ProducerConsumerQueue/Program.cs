using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumerQueue q = ProducerConsumerQueue.Create(10, "my queue");

            Task task1 = q.Enqueue(() =>
            {
                Console.WriteLine("going to sleep 1");
                Thread.Sleep(100);
                Console.WriteLine("woke up 1");
            });

            Task task2 =q.Enqueue(() =>
            {
                Console.WriteLine("going to sleep 2");
                Thread.Sleep(100);
                Console.WriteLine("woke up 2");
            });

            Task.WaitAll(task1, task2);
            Console.ReadKey();
        }
    }
}
