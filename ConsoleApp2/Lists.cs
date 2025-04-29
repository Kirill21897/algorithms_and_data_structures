public class KeyValueItem<KeyType, ValueType>
{
    protected KeyType _key; // Ключ
    protected ValueType _value; // Значение
    public ValueType Value // Свойство
    {
        get { return this._value; }
        set { this._value = value; }
    }
    public KeyType Key // Свойство
    {
        get { return this._key; }
        set { this._key = value; }
    }
    public override string ToString()
    {
        return string.Format("(key={0}, Value={1})", Key, Value);
    }
    // Конструкторы
    public KeyValueItem() { this._key = default(KeyType)!; this._value = default(ValueType)!; }
    public KeyValueItem(KeyType key, ValueType value) { this._key = key; this._value = value; }
}

public class Node<KeyType, ValueType> : KeyValueItem<KeyType, ValueType>
{
    private Node<KeyType, ValueType> _next; // Ссылка на следующий узел

    // Конструкторы узла
    public Node(KeyType key, ValueType value) : base(key, value)
    {
        this._next = null!;
    }
    public Node() : base()
    {
        this._next = null!;
    }
    public Node<KeyType, ValueType> Next // Свойство
    {
        get { return this._next; }
        set { this._next = value; }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class LinkedList<KeyType, ValueType> where KeyType : IComparable where ValueType : IComparable
{
    private Node<KeyType, ValueType> _head = null!;   // Ссылка на начальный узел
    private int _count = 0;
    public Node<KeyType, ValueType> Head { get { return _head; } } // Свойство
    public LinkedList() { _head = null!; _count = 0; }
    public int Count { get { return _count; } } // Свойство

    // Добавить в начало
    public int AddToStart(KeyType key, ValueType value)
    {
        Node<KeyType, ValueType> newNode = new Node<KeyType, ValueType>(key, value);
        newNode.Next = _head; _head = newNode;
        return this._count++;
    }

    // Добавить в конец
    public int AddToEnd(KeyType key, ValueType value)
    {
        Node<KeyType, ValueType> newNode = new Node<KeyType, ValueType>(key, value);
        // Если список пустой    
        if (this._head == null) { this._head = newNode; return this._count++; }
        // Поиск последнего узла
        Node<KeyType, ValueType> current = this._head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        // Добавление в конец
        current.Next = newNode;
        return this._count++;
    }

    // Очистка списка
    public void Clear()
    {
        this._head = null!;
        this._count = 0;
    }

    // Проверка на значение
    public bool Contains(ValueType value)
    {
        if (this._head != null)
        {
            Node<KeyType, ValueType> current = this._head;
            do
            {
                if (current.Value.CompareTo(value) == 0)
                {
                    return true;
                }
                current = current.Next;
            } while (current != null);
        }
        return false;
    }

    public Node<KeyType, ValueType> GetAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            return null!;
        }
        Node<KeyType, ValueType> current = null!;
        int counter = 0;
        if (this._head != null)
        {
            current = this._head;
            while (counter != index)
            {
                current = current.Next;
                counter++;
            }
        }

        return current;
    }

    public void InsertAfter(int index, Node<KeyType, ValueType> newNode)
    {
        Node<KeyType, ValueType> current = GetAt(index);
        if (current != null)
        {
            newNode.Next = current.Next;
            current.Next = newNode;
            this._count++;
        }
    }

    public void InsertBefore(int index, Node<KeyType, ValueType> newNode)
    {
        if (index < 1) return;
                Node<KeyType, ValueType> current = GetAt(index - 1);
        if (current != null)
        {
            newNode.Next = current.Next;
            current.Next = newNode;
            this._count++;
        }
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < this.Count; i++)
        {
            result += this.GetAt(i).ToString() + " ";
        }

        return result;
    }

    public void RemoveLast()
    {
        if (this._head != null)
        {
            if (this.Count == 1)
            {
                this._head = null!;
            }
            else
            {
                Node<KeyType, ValueType> secondLast = this.GetAt(this.Count - 2);
                secondLast.Next = null!;
            }
            this._count--;
        }
    }

    public Node<KeyType, ValueType> GetLast()
    {
        if (this._head != null)
        {
            return GetAt(this.Count - 1);
        }

        return null!;
    }

    public Node<KeyType, ValueType> GetFirst()
    {
        return this._head;
    }

    public void RemoveFirst()
    {
        if (this._head != null)
        {
            this._head = this._head.Next;
            this._count--;
        }
    }

    public void RemoveByKey(Node<KeyType, ValueType> node)
    {
        if (this._head != null)
        {
            if (this._head.Key.CompareTo(node.Key) == 0)
            {
                this.RemoveFirst();
                return;
            }

            Node<KeyType, ValueType> current = this._head;
            while (current.Next != null)
            {
                if (current.Next.Key.CompareTo(node.Key) == 0)
                {
                    current.Next = current.Next.Next;
                    _count--;
                    return;
                }
                current = current.Next;
            }
        }
    }

    public void RemoveAt(int index)
    {
        if (index == 0)
        {
            this.RemoveFirst();
        }
        else
        {
            Node<KeyType, ValueType> current = GetAt(index - 1);
            if (current != null && current.Next != null)
            {
                current.Next = current.Next.Next;
                this._count--;
            }
        }
    }
}

