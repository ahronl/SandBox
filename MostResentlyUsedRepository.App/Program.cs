using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostResentlyUsedRepository.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new MostResentlyUsedRepository<int, int>(3);

            repository.Put(1, 1);
            repository.Put(2, 2);
            repository.Get(2);
            repository.Get(1);
            repository.Put(3, 2);
            repository.Put(3, 1);
            repository.Put(3, 5);
            repository.Put(3, 6);
            repository.Put(2, 2);

            foreach (var item in repository.All())
            {
                Console.WriteLine("key {0} value {1}", item.Item1, item.Item2);
            }

            Console.ReadKey();
        }
    }
}
