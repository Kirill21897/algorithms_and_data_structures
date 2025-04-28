public class Item<K, T>
{
    protected K key; // Ключ
    protected T value; // Значение
    public T Value // Свойство
    {
        get { return this.value; }
        set { this.value = value; }
    }
    public K Key // Свойство
    {
        get { return this.key; }
        set { this.key = value; }
    }
    public override string ToString()
    {
        return string.Format("(key={0}, Value={1})", Key, Value);
    }
    // Конструкторы
    public Item() { this.key = default(K); this.value = default(T); }
    public Item(K key, T value) { this.key = key; this.value = value; }
}

public class SingleNode<K, T> : Item<K, T>
{
    private SingleNode<K, T> next; // Ссылка на следующий узел

    // Конструкторы узла
    public SingleNode(K key, T value) : base(key, value)
    {
        this.next = null;
    }
    public SingleNode() : base()
    {
        this.next = null;
    }
    public SingleNode<K, T> Next // Свойство
    {
        get { return this.next; }
        set { this.next = value; }
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

public class SingleLinkedList<K, T> where K : IComparable where T : IComparable
{
    private SingleNode<K, T> first = null;   // Ссылка на начальный узел
    private int pos = 0;
    public SingleNode<K, T> First { get { return first; } } // Свойство
    public SingleLinkedList() { first = null; pos = 0; }
    public int Count { get { return pos; } } // Свойство

    // Добавить в начало
    public int AddBegin(K key, T value)
    {
        SingleNode<K, T> s = new SingleNode<K, T>(key, value);
        s.Next = first; first = s;
        return this.pos++;
    }

    // Добавить в конец
    public int AddEnd(K key, T value)
    {
        SingleNode<K, T> s = new SingleNode<K, T>(key, value);
        // Если список пустой    
        if (this.first == null) { this.first = s; return this.pos++; }
        // Поиск последнего узла
        SingleNode<K, T> e = this.first;
        while (e.Next != null)
        {
            e = e.Next;
        }
        // Добавление в конец
        e.Next = s;
        return this.pos++;
    }

    // Очистка списка
    public void Clear()
    {
        this.first = null;
        this.pos = 0;
    }

    // Проверка на значение
    public bool ContainsValue(T value)
    {
        if (this.first != null)
        {
            SingleNode<K, T> e = this.first;
            do
            {
                if (e.Value.CompareTo(value) == 0)
                {
                    return true;
                }
                e = e.Next;
            } while (e != null);
        }
        return false;
    }

    public SingleNode<K, T> getAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            return null;
        }
        SingleNode<K, T> e = null;
        int counter = 0;
        if (this.first != null)
        {
            e = this.first;
            while (counter != index)
            {
                e = e.Next;
                counter++;
            }
        }

        return e;
    }

    public void InsertAfter(int index, SingleNode<K, T> item)
    {
        SingleNode<K, T> e = getAt(index);
        if (e != null)
        {
            item.Next = e.Next;
            e.Next = item;
            this.pos++;
        }
    }

    public void InsertBefore(int index, SingleNode<K, T> item)
    {
        if (index < 1) return;
        SingleNode<K, T> e = getAt(index - 1);
        if (e != null)
        {
            item.Next = e.Next;
            e.Next = item;
            this.pos++;
        }
    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < this.Count; i++)
        {
            res += this.getAt(i).ToString() + " ";
        }

        return res;
    }

    public void DeleteLast()
    {
        if (this.first != null)
        {
            if (this.Count == 1)
            {
                this.first = null;
            }
            else
            {
                SingleNode<K, T> last = this.getAt(this.Count - 2);
                last.Next = null;
            }
            this.pos--;
        }
    }

    public SingleNode<K, T> GetLast()
    {
        if (this.first != null)
        {
            return getAt(this.Count - 1);
        }

        return null;
    }

    public SingleNode<K, T> GetFirst()
    {
        return this.first;
    }

    public void DeleteFirst()
    {
        if (this.first != null)
        {
            this.first = this.first.Next;
            this.pos--;
        }
    }

    public void DeleteByKey(SingleNode<K, T> item)
    {
        if (this.first != null)
        {
            if (this.first.Key.CompareTo(item.Key) == 0)
            {
                this.DeleteFirst();
                return;
            }

            SingleNode<K, T> e = this.first;
            while (e.Next != null)
            {
                if (e.Next.Key.CompareTo(item.Key) == 0)
                {
                    e.Next = e.Next.Next;
                    pos--;
                    return;
                }
                e = e.Next;
            }
        }
    }

    public void DeleteAt(int index)
    {
        if (index == 0)
        {
            this.DeleteFirst();
        }
        else
        {
            SingleNode<K, T> e = getAt(index - 1);
            if (e != null && e.Next != null)
            {
                e.Next = e.Next.Next;
                this.pos--;
            }
        }
    }
}