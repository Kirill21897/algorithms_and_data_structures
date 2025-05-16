class Item<K, T>
{
    public K Key { get; set; }
    public T Value { get; set; }

    public Item(K key, T value)
    {
        Key = key;
        Value = value;
    }

    public override string ToString()
    {
        return $"[{Key} → {Value}]";
    }
}

class HashTableList<K, T> where K : IComparable
{
    private List<Item<K, T>>[] table;
    private int size;

    public HashTableList(int size)
    {
        this.size = size;
        table = new List<Item<K, T>>[size];
        for (int i = 0; i < size; i++)
            table[i] = new List<Item<K, T>>();
    }

    private int Hash(K key) => Math.Abs(key.GetHashCode()) % size;

    public void Add(K key, T value)
    {
        int index = Hash(key);
        foreach (var item in table[index])
            if (item.Key.CompareTo(key) == 0)
                throw new ArgumentException("Ключ уже существует");

        table[index].Add(new Item<K, T>(key, value));
    }

    public bool Remove(K key)
    {
        int index = Hash(key);
        for (int i = 0; i < table[index].Count; i++)
        {
            if (table[index][i].Key.CompareTo(key) == 0)
            {
                table[index].RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public Item<K, T> Get(K key)
    {
        int index = Hash(key);
        foreach (var item in table[index])
            if (item.Key.CompareTo(key) == 0)
                return item;

        return null!;
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < size; i++)
        {
            result += $"Bucket {i}: ";
            foreach (var item in table[i])
                result += $"{item}, ";
            result += "\n";
        }
        return result;
    }
}

class HashTableArray<K, T> where K : IComparable
{
    private Item<K, T>[] table;
    private int size;
    private int count;

    public HashTableArray(int size)
    {
        this.size = size;
        table = new Item<K, T>[size];
    }

    private int Hash(K key) => Math.Abs(key.GetHashCode()) % size;

    public void Add(K key, T value)
    {
        if (count >= size)
            throw new Exception("Таблица переполнена");

        int index = Hash(key);
        for (int i = 0; i < size; i++)
        {
            int pos = (index + i) % size;
            if (table[pos] == null)
            {
                table[pos] = new Item<K, T>(key, value);
                count++;
                return;
            }
            else if (table[pos].Key.CompareTo(key) == 0)
                throw new ArgumentException("Ключ уже существует");
        }
    }

    public bool Remove(K key)
    {
        int index = Hash(key);
        for (int i = 0; i < size; i++)
        {
            int pos = (index + i) % size;
            if (table[pos] != null && table[pos].Key.CompareTo(key) == 0)
            {
                table[pos] = null!;
                Rehash(pos);
                count--;
                return true;
            }
        }
        return false;
    }

    private void Rehash(int startIndex)
    {
        for (int i = 1; i < size; i++)
        {
            int pos = (startIndex + i) % size;
            if (table[pos] == null) break;

            Item<K, T> temp = table[pos];
            table[pos] = null!;
            count--;
            Add(temp.Key, temp.Value);
        }
    }

    public Item<K, T> Get(K key)
    {
        int index = Hash(key);
        for (int i = 0; i < size; i++)
        {
            int pos = (index + i) % size;
            if (table[pos] == null) break;
            if (table[pos].Key.CompareTo(key) == 0)
                return table[pos];
        }
        return null!;
    }

    public override string ToString()
    {
        string res = "";
        for (int i = 0; i < size; i++)
            res += $"{i}: {(table[i] == null ? "empty" : table[i])}\n";
        return res;
    }
}
