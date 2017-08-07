using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubDirectoryFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>()
            {
               @"XYZ\ABC\OPM",
               @"ABC",
               @"XYZ\ABC\OPM\III",
               @"ABC\III"
            };


            List<string> output = Calculate(input);

            foreach (var dir in output)
            {
                Console.WriteLine(dir);
            }

            Console.ReadKey();

            /*output
             * 
             * ABC\III
             * XYZ\ABC\OPM\III
             * 
             * 
            */
        }

        private static List<string> Calculate(List<string> input)
        {
            Node root = CreateGraph(input);

            return GraphToString(root);
        }

        private static Node CreateGraph(List<string> input)
        {
            string rootstr = string.Empty;

            Node root = new Node(rootstr);

            foreach (string subDir in input)
            {
                CreateNode(subDir, root);
            }

            return root;
        }

        private static void CreateNode(string subDir, Node root)
        {
            if(string.IsNullOrWhiteSpace(subDir)) return;

            string perFix = subDir.Substring(0, 3);
            string postFix = string.Empty;

            if (subDir.Length > 3)
            {
                postFix = subDir.Substring(4, subDir.Length - 4);
            }

            Node node = root.GetNode(perFix);

            CreateNode(postFix, node);
        }

        private static List<string> GraphToString(Node node)
        {
            if (node == null) return null;

            List<string> res = new List<string>();

            if (node.Children.Count == 0)
            {
                return new List<string>() {node.Name};
            }

            foreach (var child in node.Children)
            {
                res.Add(GraphToString(child, node.Name));
            }
            return res;
        }

        private static string GraphToString(Node node,string name)
        {
            if (node == null)
            {
                return null;
            }
            else
            {
                string appended = string.IsNullOrWhiteSpace(name) ? node.Name : $"{name}\\{node.Name}";

                if (node.Children.Count == 0)
                {
                    return appended;
                }
                else
                {
                    foreach (var child in node.Children)
                    {
                        return GraphToString(child, appended);
                    }
                }
            }
            return null;
        }
    }
}
