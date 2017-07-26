using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsCircularList
{
    class Program
    {
        static void Main(string[] args)
        {
            Node node1 = new Node(Guid.NewGuid());
            Node node2 = new Node(Guid.NewGuid());
            Node node3 = new Node(Guid.NewGuid());
            Node node4 = new Node(Guid.NewGuid());
            Node node5 = new Node(Guid.NewGuid());

            List<Node> firstcircularlist = new List<Node>(new[] {node1, node2, node1, node4, node5, node3});


            Console.WriteLine("first list is circular {0}", IsCircular(firstcircularlist));

            List<Node> secondCircularlist = new List<Node>(new[] { node1, node2, node3, node4, node5 });


            Console.WriteLine("second list is circular {0}", IsCircular(secondCircularlist));

            List<Node> thirdCircularlist = new List<Node>(new[] {node1, node2, node3, node1, node5});

            Console.WriteLine("third list is circular {0}", IsCircular(thirdCircularlist));

            Console.ReadKey();
        }

        private static bool IsCircular(List<Node> list)
        {
            int index = -1;

            do
            {
                index++;

                var doubleIndex = (index + 2) % list.Count;

                if (list[index] == list[doubleIndex])
                {
                    return true;
                }
            } while (index < list.Count - 1);

            return false;
        }
    }

    public class Node
    {
        public Guid Id { get; set; }

        public Node(Guid id)
        {
            Id = id;
        }
    }
}
