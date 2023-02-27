namespace Tree
{
    using System.Collections;

    public class Program
    {
        public static void Main(string[] arg)
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

            var enumer = new DepthFirstTreeEnumerator<string>(root);

            foreach (var treeNode in enumer)
            {
                Console.WriteLine(treeNode.Id);
            }
        }
    }

    public abstract class TreeEnumerator<T> : IEnumerator<TreeNode<T>>
    {
        protected TreeNode<T> Tree;

        protected TreeNode<T> CurrentNode;

        public TreeEnumerator(TreeNode<T> tree)
        {
            this.Tree = tree;
        }

        public TreeNode<T> Current
        {
            get
            {
                return this.CurrentNode;
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return this.CurrentNode;
            }
        }

        public abstract bool MoveNext();

        public void Reset()
        {
            this.CurrentNode = null;
        }

        public virtual TreeEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }

    public class BreadthFirstTreeEnumerator<T> : TreeEnumerator<T>
    {
        private Queue<TreeNode<T>> q;

        public BreadthFirstTreeEnumerator(TreeNode<T> tree)
            : base(tree)
        {
            this.q = new Queue<TreeNode<T>>();
            q.Enqueue(tree);
        }

        public override bool MoveNext()
        {
            if (this.q.Count == 0)
            {
                return false;
            }

            this.CurrentNode = this.q.Dequeue();

            if (this.CurrentNode.Children == null)
            {
                return this.CurrentNode.Index != this.CurrentNode.Parent.Children.Count - 1;
            }

            var i = 0;
            foreach (var child in this.CurrentNode.Children)
            {
                child.Index = ++i;
                child.Parent = this.CurrentNode;
                this.q.Enqueue(child);
            }

            return true;
        }
    }

    public class DepthFirstTreeEnumerator<T> : TreeEnumerator<T>
    {
        private Stack<TreeNode<T>> q;

        public DepthFirstTreeEnumerator(TreeNode<T> tree)
            : base(tree)
        {
            this.q = new Stack<TreeNode<T>>();
            q.Push(tree);
        }

        public override bool MoveNext()
        {
            if (this.q.Count == 0)
            {
                return false;
            }

            this.CurrentNode = this.q.Pop();

            if (this.CurrentNode.Children == null)
            {
                return this.CurrentNode.Index != 0;
            }

            var i = 0;
            foreach (var child in this.CurrentNode.Children.Reverse())
            {
                child.Index = ++i;
                child.Parent = this.CurrentNode;
                this.q.Push(child);
            }

            return true;
        }
    }

    public class TreeNode<T>
    {
        public T Data { get; set; }

        public string Id { get; set; }

        public int Index { get; set; }

        public TreeNode<T> Parent { get; set; }

        public ICollection<TreeNode<T>> Children { get; set; }

        public TreeNode(string id)
        {
            this.Id = id;
        }
    }
}