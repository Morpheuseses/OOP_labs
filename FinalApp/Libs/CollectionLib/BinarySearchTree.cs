using System;
using System.Collections;
using System.Collections.Generic;
using Lib;

namespace CollectionLib
{
    [Serializable]
    public class BinarySearchTree<T> : ICollection<T>, IEnumerable<T>, ICloneable
        where T : IComparable, ICloneable, IInit
    {
        public TreeNode<T> RootNode { get; set; }
        public int Count { get; private set; }
        public int Length => Count;
        public bool IsReadOnly => false;

        public IComparer<T> Comparer { get; private set; }

        public BinarySearchTree()
        {
            RootNode = null;
            Comparer = Comparer<T>.Default;
        }

        public BinarySearchTree(IComparer<T>? comparer)
        {
            Comparer = comparer ?? Comparer<T>.Default;
        }

        public BinarySearchTree(T[] collection)
        {
            RootNode = null;
            Comparer = Comparer<T>.Default;
            AddRange(collection);
        }

        public BinarySearchTree(BinarySearchTree<T> btr) : this(btr.Comparer)
        {
            if (btr.Count != 0)
            {
                RootNode = CloneNode(btr.RootNode);
                Count = btr.Count;
            }
        }

        protected TreeNode<T> CloneNode(TreeNode<T> node)
        {
            if (node == null)
                return null;

            return new TreeNode<T>(node)
            {
                Left = CloneNode(node.Left),
                Right = CloneNode(node.Right)
            };
        }

        public virtual void Add(T data)
        {
            RootNode = Add(RootNode, data);
            
        }

        private TreeNode<T> Add(TreeNode<T>? node, T data)
        {
            if (node == null)
            {
                Count++;
                return new TreeNode<T>(data);
            }
            int compResult = data.CompareTo(node.Data);
            if (compResult < 0)
                node.Left = Add(node.Left, data);
            else if (compResult != 0)
                node.Right = Add(node.Right, data);

            return node;
        }

        public virtual void AddRange(T[] objects)
        {
            foreach (T item in objects)
                Add(item);
        }

        public virtual void Clear()
        {
            RootNode = null;
            Count = 0;
        }

        public virtual bool Remove(T data)
        {
            if (!Contains(data))
                return false;

            RootNode = Remove(data, RootNode);
            
            return true;
        }

        private TreeNode<T> Remove(T data, TreeNode<T>? node)
        {
            if (node == null)
            {
                Count--;
                return null;
            }

            int comparison = data.CompareTo(node.Data);
            if (comparison < 0)
                node.Left = Remove(data, node.Left);
            else if (comparison > 0)
                node.Right = Remove(data, node.Right);
            else
            {
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                TreeNode<T> min = FindMin(node.Right);
                min.Right = RemoveMin(node.Right);
                min.Left = node.Left;
                return min;
            }

            return node;
        }

        private TreeNode<T> FindMin(TreeNode<T> node)
        {
            return node.Left != null ? FindMin(node.Left) : node;
        }

        private TreeNode<T> RemoveMin(TreeNode<T> node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = RemoveMin(node.Left);
            return node;
        }

        public bool Contains(T item)
        {
            return FindNode(item, RootNode) != null;
        }

        private TreeNode<T> FindNode(T data, TreeNode<T>? node)
        {
            if (node == null)
                return null;

            int result = data.CompareTo(node.Data);
            if (result == 0)
                return node;
            else if (result < 0)
                return FindNode(data, node.Left);
            else
                return FindNode(data, node.Right);
        }

        public TreeNode<T> FindNodeByTitle(string name, TreeNode<T>? node)
        {
            if (node == null)
                return null;

            // Приводим к IInit или используем кастинг к известному типу
            if (node.Data is Assessment a && a.Title == name)
                return node;

            var leftSearch = FindNodeByTitle(name, node.Left);
            if (leftSearch != null)
                return leftSearch;

            return FindNodeByTitle(name, node.Right);
        }

        public bool UpdateNodeByTitle(string name, T newData)
        {
            var node = FindNodeByTitle(name, RootNode);
            if (node == null)
                return false;

            node.Data = newData;
            return true;
        }

        public T GetItemByName(string name)
        {
            var node = FindNodeByTitle(name, RootNode);
            if (node == null)
                throw new KeyNotFoundException($"Элемент с названием {name} не найден");
            return node.Data;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count) throw new ArgumentException("Not enough space in target array");

            foreach (var value in this)
                array[arrayIndex++] = value;
        }

        private IEnumerable<T>? PreOrder(TreeNode<T>? node)
        {
            if (node != null)
            {
                yield return node.Data;
                foreach (var item in PreOrder(node.Left))
                    yield return item;
                foreach (var item in PreOrder(node.Right))
                    yield return item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return PreOrder(RootNode).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object ShallowCopy()
        {
            return (BinarySearchTree<T>)MemberwiseClone();
        }

        public object Clone()
        {
            return new BinarySearchTree<T>(this);
        }

        public override string ToString()
        {
            return Count == 0 ? "None" : GetConsoleTreeString();
        }

        public string GetConsoleTreeString()
        {
            return GetConsoleTreeString(RootNode, "", true, "Root");
        }

        private string GetConsoleTreeString(TreeNode<T> node, string indent, bool isLast, string branch) 
        { 
            if (node is null) 
                return ""; 
            var sb = new System.Text.StringBuilder(); 
            sb.Append(indent); 
            sb.Append(isLast ? "\u2514\u2500\u2500 " : "\u251C\u2500\u2500 "); 
            string toPrint = $"[{branch}] {node.Data.ToString().Replace("\n", "|").Replace("-", "")}\n"; 
            sb.Append(toPrint); var children = new List<(TreeNode<T> node, string branch)>(); 
            if (node.Left != null) 
                children.Add((node.Left, "L")); if (node.Right != null) 
            children.Add((node.Right, "R")); 

            for (int i = 0; i < children.Count; i++) 
                sb.Append( GetConsoleTreeString( children[i].node, indent + (isLast ? " " : "\u2502 "), 
                            i == children.Count - 1, children[i].branch ) ); 
            return sb.ToString(); }
    }
}
