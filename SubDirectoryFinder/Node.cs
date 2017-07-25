using System.Collections.Generic;

namespace SubDirectoryFinder
{
    internal class Node
    {
        private readonly Dictionary<string,Node> _dictionary = new Dictionary<string, Node>();

        public List<Node> Children { get; internal set; }
        public string Name { get; internal set; }

        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
        }

        internal Node GetNode(string name)
        {
            if (_dictionary.ContainsKey(name) == false)
            {
                _dictionary[name] = new Node(name);
                Children.Add(_dictionary[name]);
            }

            return _dictionary[name];
        }
    }
}