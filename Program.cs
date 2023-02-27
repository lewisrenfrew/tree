namespace Tree
{
    using System.Collections;

    class Program
    {
        static void Main(string[] arg)
        {
            TreeNode<string> root = new TreeNode<string>("root");
            TreeNode<string> n1 = new TreeNode<string>("n1");

            TreeNode<string> n11 = new TreeNode<string>("n11");
            TreeNode<string> n12 = new TreeNode<string>("n12");
            TreeNode<string> n121 = new TreeNode<string>("n121");

            TreeNode<string> n111 = new TreeNode<string>("n111");
            TreeNode<string> n112 = new TreeNode<string>("n112");

            TreeNode<string> n2 = new TreeNode<string>("n2");
            TreeNode<string> n21 = new TreeNode<string>("n21");


            n11.Children = new List<TreeNode<string>> { n111, n112 };
            n12.Children = new List<TreeNode<string>> { n121 };

            n1.Children = new List<TreeNode<string>> { n11, n12 };
            n2.Children = new List<TreeNode<string>> { n21 };

            root.Children = new List<TreeNode<string>> { n1, n2 };

            n111.Children = new List<TreeNode<string>>();

            foreach (var item in root)
            {
                Console.WriteLine(item.Data);
            }

        }
    }

    class TreeNode<T> : IEnumerable<TreeNode<T>>
    {
        public T Data { get; set; }

        public TreeNode<T> Parent { get; set; }

        public ICollection<TreeNode<T>> Children { get; set; }

        public TreeNode(T data)
        {
            this.Data = data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            var self = this;
            var queue = new Queue<TreeNode<T>>();
            queue.Enqueue(self);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                yield return current;
                if (current.Children != null)
                {
                    foreach (var child in current.Children)
                        queue.Enqueue(child);
                }
            }
        }
    }
}