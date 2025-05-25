using System;
using System.Runtime.InteropServices;

class Set<T> where T : IComparable
{
    private int size;
    private int capacity;
    private T[] data;

    public Set(int initialCapacity)
    {
        size = 0;
        capacity = initialCapacity > 0 ? initialCapacity : 1; 
        data = new T[capacity];
    }

    public int GetSize() => capacity;
    public int GetCount() => size;
    public int Count => size; 

    public int GetIndex(T value)
    {
        for (int i = 0; i < size; i++)
        {
            if (data[i].Equals(value))
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsContains(T element)
    {
        return GetIndex(element) >= 0;
    }

    public T this[int index]
    {
        get => (index >= 0 && index < size) ? data[index] : default(T)!;
    }

    public bool Exists(T value)
    {
        return GetIndex(value) >= 0;
    }

    public void Add(T value)
    {
        if (!IsContains(value))
        {
            if (size >= capacity)
            {
                Resize();
            }
            data[size++] = value;
        }
    }

    private void Resize()
    {
        capacity *= 2;
        T[] newData = new T[capacity];
        Array.Copy(data, newData, size);
        data = newData;
    }

    public void RemoveAt(int index)
    {
        if (index >= 0 && index < size)
        {
            for (int i = index; i < size - 1; i++)
            {
                data[i] = data[i + 1];
            }
            data[--size] = default(T)!; 
        }
    }

    public void Remove(T value)
    {
        int index = GetIndex(value);
        if (index >= 0)
        {
            RemoveAt(index);
        }
    }

    public T GetValue(int index)
    {
        return (index >= 0 && index < size) ? data[index] : default(T)!;
    }

    public override string ToString()
    {
        if (size == 0) return "{}";
        string result = "{";
        for (int i = 0; i < size; i++)
        {
            result += data[i].ToString();
            if (i < size - 1) result += ", ";
        }
        result += "}";
        return result;
    }

    public Set<T> Union(Set<T> other)
    {
        Set<T> result = new Set<T>(this.size + other.size);
        for (int i = 0; i < this.size; i++)
        {
            result.Add(this.data[i]);
        }
        for (int i = 0; i < other.size; i++)
        {
            result.Add(other.data[i]);
        }
        return result;
    }

    public static Set<T> operator +(Set<T> s1, Set<T> s2) => s1.Union(s2);

    public List<Set<T>> SubSets()
    {
        int totalSubsets = 1 << size; 
        List<Set<T>> subsets = new List<Set<T>>(totalSubsets);

        for (int i = 0; i < totalSubsets; i++)
        {
            Set<T> subset = new Set<T>(size);
            int mask = 1; // Начинаем с 2^0
            foreach (T item in data)
            {
                if ((i & mask) != 0)
                {
                    subset.Add(item);
                }
                mask <<= 1; // Увеличиваем маску (эквивалент mask *= 2)
            }

            subsets.Add(subset);
        }

        return subsets;
    }

    public Set<T> Intersection(Set<T> other)
    {
        Set<T> result = new Set<T>(Math.Min(this.size, other.size));
        for (int i = 0; i < this.size; i++)
        {
            if (other.IsContains(this.data[i]))
            {
                result.Add(this.data[i]);
            }
        }
        return result;
    }

    public static Set<T> operator *(Set<T> s1, Set<T> s2) => s1.Intersection(s2);

    public Set<T> leftMoods(Set<T> other)
    {
        Set<T> result = new Set<T>(Math.Max(this.size, other.size));
        for (int i = 0; i < this.size; i++)
        {
            if (!(other.IsContains(this.data[i])))
            {
                result.Add(this.data[i]);
            }
        }
        return result;
    }

    public static Set<T> operator /(Set<T> s1, Set<T> s2) => s1.leftMoods(s2);

    public Set<T> rightMoods(Set<T> other)
    {
        Set<T> result = new Set<T>(Math.Max(this.size, other.size));
        for (int i = 0; i < other.size; i++)
        {
            if (!(this.IsContains(other.data[i])))
            {
                result.Add(other.data[i]);
            }
        }
        return result;
    }

    public static Set<T> operator %(Set<T> s1, Set<T> s2) => s1.rightMoods(s2);
}