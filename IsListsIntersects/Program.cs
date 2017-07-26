using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsListsIntersects
{
    class Program
    {
        static void Main(string[] args)
        {
            Node node1 = new Node();
            Node node2 = new Node();
            Node node3 = new Node();
            Node node4 = new Node();
            Node node5 = new Node();
            Node node6 = new Node();
            Node node7 = new Node();
            Node node8 = new Node();
            Node node9 = new Node();
            Node node10 = new Node();


            List<Node> first = new List<Node>() {node8, node9, node10, node1, node2, node3, node4, node5};
            List<Node> sec = new List<Node>() {node6, node7, node3, node4, node5};

            Console.WriteLine("The lists intersect at {0}", IsIntersects(first, sec));

            first = new List<Node>() {node8, node9, node10, node2};
            sec = new List<Node>() { node6, node7, node1, node4, node5 };

            Console.WriteLine("The lists intersect at {0}", IsIntersects(first, sec));


            Console.ReadKey();

        }

        private static int IsIntersects(List<Node> first, List<Node> sec)
        {
            if (first.Count < sec.Count)
            {
                var tmp = first;
                first = sec;
                sec = tmp;
            }

            //first is longer

            if (first.Count != sec.Count)
            {
                first = Skip(first, sec.Count);
            }

            return IsIntersects1(first, sec);
        }

        private static List<Node> Skip(List<Node> first, int count)
        {
            List<Node> res = new List<Node>();
            int skipAmount = first.Count - count;

            for (int i = skipAmount; i < first.Count; i++)
            {
                res.Add(first[i]);
            }
            return res;
        }

        private static int IsIntersects1(List<Node> first, List<Node> sec)
        {
            for (int i = 0; i < first.Count; i++)
            {
                if (first[i] == sec[i]) return i;
            }

            return -1;
        }
    }

    public class Node
    {
        public Guid Id { get; set; }

        public Node()
        {
            Id = Guid.NewGuid();
        }
    }
}
