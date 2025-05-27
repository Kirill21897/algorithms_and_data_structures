using System;
using System.Runtime.InteropServices;
using System.Linq;

class SortAlgorithms
{
    public static void BubbleSort<T> (T[] array) where T : IComparable
    {
            int length = array.Length;
            for (int i = 0; i < length; i ++){
                for (int j = 0; j < length - i - 1; j ++){
                    if (array[j].CompareTo(array[j + 1]) > 0){
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
    }

    public static void InsertionSort<T>(T[] array) where T : IComparable
    {
    int n = array.Length;
    for (int i = 1; i < n; i++)
    {
        T key = array[i];
        int j = i - 1;

        while (j >= 0 && array[j].CompareTo(key) > 0)
        {
            array[j + 1] = array[j];
            j--;
        }
        array[j + 1] = key;
    }
    }


    public static void SelectionSort<T> (T[] array) where T : IComparable
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (array[j].CompareTo(array[minIndex]) < 0)
                {
                    minIndex = j;
                }
            }
            T temp = array[minIndex];
            array[minIndex] = array[i];
            array[i] = temp;
        }
    }

    public static void MergeSort<T>(T[] array) where T : IComparable
    {
        if (array == null || array.Length <= 1)
            return;

        MergeSort(array, 0, array.Length - 1);
    }

    private static void MergeSort<T>(T[] array, int low, int high) where T : IComparable
    {
        if (low >= high)
            return;

        int mid = (low + high) / 2;
        MergeSort(array, low, mid);     // Левая половина
        MergeSort(array, mid + 1, high); // Правая половина

        Merge(array, low, mid, high);
    }

    private static void Merge<T>(T[] array, int low, int mid, int high) where T : IComparable
    {
        // Создаем временный массив для хранения слитых данных
        T[] temp = new T[high - low + 1];
        int i = low;      // Указатель на левую половину
        int j = mid + 1;  // Указатель на правую половину
        int k = 0;        // Указатель на временный массив

        // Слияние двух частей
        while (i <= mid && j <= high)
        {
            if (array[i].CompareTo(array[j]) <= 0)
                temp[k++] = array[i++];
            else
                temp[k++] = array[j++];
        }

        // Копируем оставшиеся элементы из левой части
        while (i <= mid)
            temp[k++] = array[i++];

        // Копируем оставшиеся элементы из правой части
        while (j <= high)
            temp[k++] = array[j++];

        // Копируем отсортированные элементы обратно в исходный массив
        for (k = 0; k < temp.Length; k++)
            array[low + k] = temp[k];
    }

    public static void QuickSort<T> (T[] array, int low, int high) where T : IComparable
    {
        if (low < high)
        {
            int pivot = Partition(array, low, high);
            QuickSort(array, low, pivot - 1);
            QuickSort(array, pivot + 1, high);
        }
    }

    private static int Partition<T> (T[] array, int low, int high) where T : IComparable
    {
        T pivot = array[high];
        int i = (low - 1);

        for (int j = low; j < high; j++)
        {
            if (array[j].CompareTo(pivot) < 0)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, high);
        return i + 1;
    }

    private static void Swap<T> (T[] array, int i, int j) where T : IComparable
    {
        T temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public static void HeapSort<T> (T[] array) where T : IComparable
    {
        int n = array.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(array, n, i);

        for (int i = n - 1; i >= 0; i--)
        {
            Swap(array, 0, i);
            Heapify(array, i, 0);
        }
    }

    private static void Heapify<T> (T[] array, int n, int i) where T : IComparable
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        if (left < n && array[left].CompareTo(array[largest]) > 0)
            largest = left;

        if (right < n && array[right].CompareTo(array[largest]) > 0)
            largest = right;

        if (largest != i)
        {
            Swap(array, i, largest);
            Heapify(array, n, largest);
        }
    }

    public static void CountingSort(int[] array)
    {
        if (array.Length == 0)
            return;

       // int max = array[0];
       // for (int i = 1; i < array.Length; i++)
       // {
        //    if (array[i] > max)
        //        max = array[i];
       // }
        int maxm=array.Max();
        int minm=array.Min();
        if (minm < 0){
            Console.WriteLine("-----Ошибка! В массиве есть отрицаетльные числа!-----");
            return;
        }

        int[] count = new int[maxm + 1];

        for (int i = 0; i < array.Length; i++)
        {
            count[array[i]]++;
        }

        int index = 0;
        for (int i = 0; i < count.Length; i++)
        {
            while (count[i] > 0)
            {
                array[index++] = i;
                count[i]--;
            }
        }
    }

    public static void QuickSort_noRec<T> (T[] array) where T : IComparable
    {
        if (array.Length <= 1)
            return;
        int k=0;
        Stack<(int, int)> stack = new Stack<(int, int)>();
        stack.Push((0, array.Length - 1));

        while (stack.Count > 0)
        {
            k++;
            var (low, high) = stack.Pop();

            int pivotIndex = Partition(array, low, high);

            if (pivotIndex - 1 > low)
                stack.Push((low, pivotIndex - 1));

            if (pivotIndex + 1 < high)
                stack.Push((pivotIndex + 1, high));
        }
        Console.WriteLine("K={0}",k);
    }

    public static void PrintArray<T> (T[] array) where T : IComparable
    {
        foreach (var item in array)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}