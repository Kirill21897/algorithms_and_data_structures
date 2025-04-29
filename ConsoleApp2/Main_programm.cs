using System;
namespace ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        bool flag = true;
        while(flag){
            Console.WriteLine("Выберете тест:\n1) Множества\n2) Сортировки\n3) Списки\n4) Выход");
            string ex = Console.ReadLine()!;
            int ex_number = int.Parse(ex);
            switch(ex_number)
            {
                case 1:
                    //Тестирование класса Set
                    Console.WriteLine("\n-----Операции с множествами-----");
                    // Создание множества целых чисел
                    Set<int> set1 = new Set<int>(5);
                    set1.Add(1);
                    set1.Add(2);
                    set1.Add(3);

                    // Создание множества символов
                    Set<char> set3 = new Set<char>(5);
                    set3.Add('a');
                    set3.Add('b');
                    set3.Add('c');
                    Console.WriteLine("Set 1: " + set1);

                    // Проверка наличия элемента
                    Console.WriteLine("Set 1 contains 2: " + set1.IsContains(2));
                    Console.WriteLine("Set 1 contains 4: " + set1.IsContains(4));

                    // Удаление элемента
                    set1.Remove(2);
                    Console.WriteLine("Set 1 after removing 2: " + set1);

                    // Создание второго множества
                    Set<int> set2 = new Set<int>(5);
                    set2.Add(3);
                    set2.Add(4);
                    set2.Add(5);
                    Console.WriteLine("Set 2: " + set2);

                    // Объединение множеств
                    Set<int> unionSet = set1 + set2;
                    Console.WriteLine("Union of Set 1 and Set 2: " + unionSet);

                    // Пересечние множеств
                    Set<int> intersectionSet = set1 * set2;
                    Console.WriteLine("Intersection of Set1 and Set 2: " + intersectionSet);

                    // Левое дополнение
                    Set<int> leftMoodsSet = set1 / set2;
                    Console.WriteLine("left moods of Set1 and Set 2: " + leftMoodsSet);

                    // Правое дополнение
                    Set<int> rightMoodsSet = set1 % set2;
                    Console.WriteLine("right moods of Set1 and Set 2: " + rightMoodsSet);

                    // Создание подмножеств
                    List<Set<char>> subsets = set3.SubSets();
                    Console.WriteLine("Subsets of Set 3:");
                    foreach (var subset in subsets)
                    {
                        Console.WriteLine(subset);
                    }         
                    flag = false;
                    break;

                case 2:
                    Console.WriteLine("\n-----Сортировки-----");
                    //Создание массива данных
                    int[] array = { 64, 34, 25, 12, 22, 11, 90 };

                    //Вывод исходного массива
                    Console.WriteLine("Исходный массив:");
                    SortAlgorithms.PrintArray(array);

                    //Bubble sort
                    Console.WriteLine("\n-Bubble sort-");
                    SortAlgorithms.BubbleSort<int> (array);
                    SortAlgorithms.PrintArray(array);

                    //Сортировка вставками
                    Console.WriteLine("\n-Сортировка вставками-");
                    array = new int[] { 64, 34, 25, 12, 22, 11, 90 };
                    SortAlgorithms.InsertionSort<int> (array);
                    SortAlgorithms.PrintArray(array);

                    //Сортировка выбором
                    Console.WriteLine("\n-Сортировка выбором-");
                    array = new int[] { 64, 34, 25, 12, 22, 11, 90 };
                    SortAlgorithms.SelectionSort<int> (array);
                    SortAlgorithms.PrintArray(array);

                    //Сортировка слиянием
                    Console.WriteLine("\n-Сортировка слиянием-");
                    array = new int[] { 64, 34, 25, 12, 22, 11, 90 };
                    SortAlgorithms.MergeSort<int> (array);
                    SortAlgorithms.PrintArray(array);

                    //Быстрая сортировка (Сортировка Хоара)
                    Console.WriteLine("\n-Быстрая сортировка-");
                    array = new int[] { 64, 34, 25, 12, 22, 11, 90 };
                    SortAlgorithms.QuickSort<int> (array, 0, array.Length - 1);
                    SortAlgorithms.PrintArray(array);

                    //Сортировка кучей
                    Console.WriteLine("\n-Сортировка кучей-");
                    array = new int[] { 64, 34, 25, 12, 22, 11, 90 };
                    SortAlgorithms.HeapSort<int> (array);
                    SortAlgorithms.PrintArray(array);

                    // Counting Sort (int >=0) Cormen
                    Console.WriteLine("\n-Counting sort-");
                    array = new int[] { 4, 3, 2, 2, 3, 0, 1, -1 };
                    SortAlgorithms.CountingSort(array);
                    SortAlgorithms.PrintArray(array);


                    // Нерекурсивный вариант QuickSort
                    Console.WriteLine("\n-Нерекурсивный вариант быстрой сортировки-");
                    array = new int[] { 64, 34, 25, 12, 22, 11, 90 };
                    SortAlgorithms.QuickSort_noRec(array);
                    SortAlgorithms.PrintArray(array);
                    flag = false;
                    break;

                case 3:
                    Console.WriteLine("\n-----Списки-----");
                    var list = new LinkedList<int, string>();
                    list.AddToStart(1, "One");
                    list.AddToEnd(2, "Two");
                    list.AddToEnd(3, "Three");
                    Console.WriteLine(list); 

                    list.RemoveFirst();
                    Console.WriteLine(list); 

                    var doublyList = new DoublyLinkedList<int, string>();
                    doublyList.AddToStart(1, "One");
                    doublyList.AddToEnd(2, "Two");
                    Console.WriteLine(doublyList); 
                    flag = false;
                    break;

                case 4:
                    flag = false;
                    break;

                default:
                    Console.WriteLine("Неправильоное значение! Введите верный номер");
                    break;
            }
        }
    }
}


