using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerWrapper.Construction;

namespace TimerWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan interval = TimeSpan.FromMilliseconds(10);
            TimeSpan minimalInterval = TimeSpan.FromMilliseconds(5);

            string name = "timer relative interval or minimal interval (when relative is less then minimal)";

            using (ITimer timer = Creator
                                    .Create()
                                    .WithAction(() => Console.Write("........"))
                                    .IntervalPolicy()
                                    .Relative()
                                    .IntervalRange(interval, minimalInterval)
                                    .WithTimerName(name))
            {
                timer.Start();
                Console.ReadKey();
            }
        }
    }
}