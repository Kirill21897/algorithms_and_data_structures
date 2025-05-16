// Возможные цвета узла (для расширения дерева)
public enum NodeColor
{
    Red,
    Black,
    Neutral
}

// Базовый класс узла дерева
public class TreeNode
{
    public object Data;         // Информация узла
    public int Key;             // Ключ узла
    public TreeNode LeftChild, RightChild, ParentNode; // Ссылки на потомков и родителя
    public NodeColor Color;

    // Конструкторы
    public TreeNode()
    {
        Key = 0;
        Data = null!;
        LeftChild = null!;
        RightChild = null!;
        ParentNode = null!;
        Color = NodeColor.Neutral;
    }

    public TreeNode(int key, object data)
    {
        Key = key;
        Data = data;
        LeftChild = null!;
        RightChild = null!;
        ParentNode = null!;
        Color = NodeColor.Neutral;
    }
}

// Обобщённый узел дерева
class TreeElement<T>
{
    public TreeElement<T> Left;
    public TreeElement<T> Right;
    public TreeElement<T> Parent;

    public T Value;
    public int Key;

    public TreeElement(T value, int key)
    {
        Value = value;
        Key = key;
        Left = null!;
        Right = null!;
        Parent = null!;
    }

    public TreeElement()
    {
        Value = default(T)!;
        Key = 0;
        Left = null!;
        Right = null!;
        Parent = null!;
    }
}

// Класс бинарного дерева
class BinarySearchTree<T>
{
    private TreeElement<T> Root; // Корень дерева

    // Вставка нового элемента
    public void Add(T value, int key)
    {
        TreeElement<T> newNode = new TreeElement<T>(value, key);

        if (Root == null)
        {
            Root = newNode;
            return;
        }

        TreeElement<T> current = Root;
        TreeElement<T> prev = null!;

        while (true)
        {
            prev = current;
            if (key < current.Key)
            {
                current = current.Left;
                if (current == null)
                {
                    prev.Left = newNode;
                    newNode.Parent = prev;
                    break;
                }
            }
            else
            {
                current = current.Right;
                if (current == null)
                {
                    prev.Right = newNode;
                    newNode.Parent = prev;
                    break;
                }
            }
        }
    }

    // Поиск по ключу
    public TreeElement<T> Search(int key)
    {
        TreeElement<T> current = Root;
        while (current != null)
        {
            if (current.Key == key) return current;
            current = key < current.Key ? current.Left : current.Right;
        }
        return null!;
    }

    // Удаление по ключу
    public void Remove(int key)
    {
        TreeElement<T> target = Search(key);
        if (target == null) return;

        TreeElement<T> left = target.Left;
        TreeElement<T> right = target.Right;

        if (left == null && right == null)
        {
            RemoveLeaf(target);
        }
        else if (left == null)
        {
            RemoveWithOneChild(target, right);
        }
        else if (right == null)
        {
            RemoveWithOneChild(target, left);
        }
        else
        {
            RemoveWithTwoChildren(target, right, left);
        }
    }

    private void RemoveLeaf(TreeElement<T> node)
    {
        if (node.Parent == null)
            Root = null!;
        else if (node.Parent.Left == node)
            node.Parent.Left = null!;
        else
            node.Parent.Right = null!;
    }

    private void RemoveWithOneChild(TreeElement<T> node, TreeElement<T> child)
    {
        if (node.Parent == null)
            Root = child;
        else if (node.Parent.Left == node)
            node.Parent.Left = child;
        else
            node.Parent.Right = child;

        if (child != null)
            child.Parent = node.Parent!;
    }

    private void RemoveWithTwoChildren(TreeElement<T> node, TreeElement<T> right, TreeElement<T> left)
    {
        TreeElement<T> minNode = FindMinimum(right);
        node.Key = minNode.Key;
        node.Value = minNode.Value;

        if (minNode.Right != null)
        {
            RemoveWithOneChild(minNode, minNode.Right);
        }
        else
        {
            RemoveLeaf(minNode);
        }
    }

    private void SwapNodes(TreeElement<T> oldNode, TreeElement<T> newNode)
    {
        if (oldNode.Parent == null)
            Root = newNode;
        else if (oldNode == oldNode.Parent.Left)
            oldNode.Parent.Left = newNode;
        else
            oldNode.Parent.Right = newNode;

        if (newNode != null)
            newNode.Parent = oldNode.Parent!;
    }

    // Вывод содержимого дерева
    public void DisplayTree()
    {
        TraverseInOrder(Root);
        Console.WriteLine();
    }

    private void TraverseInOrder(TreeElement<T> node)
    {
        if (node == null) return;
        TraverseInOrder(node.Left);
        PrintNode(node);
        TraverseInOrder(node.Right);
    }

    private void TraverseReverseOrder(TreeElement<T> node)
    {
        if (node == null) return;
        TraverseReverseOrder(node.Right);
        PrintNode(node);
        TraverseReverseOrder(node.Left);
    }

    private void TraversePreOrder(TreeElement<T> node)
    {
        if (node == null) return;
        PrintNode(node);
        TraversePreOrder(node.Right);
        TraversePreOrder(node.Left);
    }

    private void PrintNode(TreeElement<T> node)
    {
        Console.Write(node.Value + " ");
    }

    // Минимальный и максимальный узлы
    public TreeElement<T> FindMinimum(TreeElement<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }

    public TreeElement<T> FindMaximum(TreeElement<T> node)
    {
        while (node.Right != null)
        {
            node = node.Right;
        }
        return node;
    }

    // Найти следующий узел по ключу
    public TreeElement<T> GetNext(TreeElement<T> node)
    {
        if (node == null) return null!;

        if (node.Right != null)
            return FindMinimum(node.Right);

        TreeElement<T> parent = node.Parent;
        while (parent != null && node == parent.Right)
        {
            node = parent;
            parent = parent.Parent;
        }
        return parent!;
    }

    // Найти предыдущий узел по ключу
    public TreeElement<T> GetPrevious(TreeElement<T> node)
    {
        if (node == null || node.Left == null)
            return null!;

        TreeElement<T> current = node;
        TreeElement<T> parent = current.Parent;

        while (parent != null && current == parent.Left)
        {
            current = parent;
            parent = parent.Parent;
        }
        return parent!;
    }

    // Обход по возрастанию
    public void InOrderWalk()
    {
        TreeElement<T> current = FindMinimum(Root);
        while (current != null)
        {
            PrintNode(current);
            current = GetNext(current);
        }
        Console.WriteLine();
    }

    // Обход по убыванию
    public void BackwardWalk()
    {
        TreeElement<T> current = FindMaximum(Root);
        while (current != null)
        {
            PrintNode(current);
            current = GetPrevious(current);
        }
        Console.WriteLine();
    }

    // Повороты узлов
    private TreeElement<T> RotateLeft(TreeElement<T> x)
    {
        TreeElement<T> y = x.Right;
        x.Right = y.Left;
        if (y.Left != null) y.Left.Parent = x;
        y.Parent = x.Parent;

        if (x.Parent == null)
            Root = y;
        else if (x == x.Parent.Left)
            x.Parent.Left = y;
        else
            x.Parent.Right = y;

        y.Left = x;
        x.Parent = y;
        return y;
    }

    private TreeElement<T> RotateRight(TreeElement<T> x)
    {
        TreeElement<T> y = x.Left;
        x.Left = y.Right;
        if (y.Right != null) y.Right.Parent = x;
        y.Parent = x.Parent;

        if (x.Parent == null)
            Root = y;
        else if (x == x.Parent.Right)
            x.Parent.Right = y;
        else
            x.Parent.Left = y;

        y.Right = x;
        x.Parent = y;
        return y;
    }

    public void Rebalance()
    {
        RebalanceSubtree(Root);
    }

    private void RebalanceSubtree(TreeElement<T> node)
    {
        if (node == null) return;

        RebalanceSubtree(node.Left);
        RebalanceSubtree(node.Right);
        BalanceNode(node);
    }

    private TreeElement<T> BalanceNode(TreeElement<T> node)
    {
        int balance = GetBalanceFactor(node);

        if (balance > 1)
        {
            if (GetBalanceFactor(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
            }
            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            if (GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
            }
            node = RotateLeft(node);
        }

        return node;
    }

    private int GetBalanceFactor(TreeElement<T> node)
    {
        if (node == null) return 0;
        return Height(node.Left) - Height(node.Right);
    }

    private int Height(TreeElement<T> node)
    {
        if (node == null) return 0;

        int leftHeight = Height(node.Left);
        int rightHeight = Height(node.Right);
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}