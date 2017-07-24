using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWrapper.Log;

namespace LogWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = LogManager.GetCurrentClassLogger();
            logger.Info("hello world");
        
            Console.ReadKey();
        }
    }
}
