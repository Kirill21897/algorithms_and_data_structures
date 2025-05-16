using System;
namespace ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        bool flag = true;
        while(flag){
            Console.WriteLine("Выберете тест:\n1) Множества\n2) Сортировки\n3) Списки\n4) Hash - таблицы\n5) Бинарное дерево \n6) Графы \n7) Выход");
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
                    //flag = false;
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
                    //flag = false;
                    break;

                case 4:
                    Console.WriteLine("\n-----Hash - таблицы-----");
                    // === Демонстрация работы HashTableList ===
                    Console.WriteLine("Тестирование HashTableList (цепочки)");
                    var hashTableList = new HashTableList<string, int>(3);

                    hashTableList.Add("a", 10);
                    hashTableList.Add("b", 20);
                    hashTableList.Add("c", 30);

                    Console.WriteLine("Поиск 'a': " + hashTableList.Get("a")?.Value);
                    Console.WriteLine("Поиск 'b': " + hashTableList.Get("b")?.Value);
                    Console.WriteLine("Поиск 'd': " + (hashTableList.Get("d") == null ? "не найдено" : ""));

                    hashTableList.Remove("b");
                    Console.WriteLine("После удаления 'b', поиск 'b': " + (hashTableList.Get("b") == null ? "не найдено" : "ошибка"));

                    Console.WriteLine("\nСодержимое HashTableList:");
                    Console.WriteLine(hashTableList);

                    // === Демонстрация работы HashTableArray ===

                    Console.WriteLine("\n\nТестирование HashTableArray (пробирование)");
                    var hashTableArray = new HashTableArray<string, int>(5);

                    hashTableArray.Add("x", 100);
                    hashTableArray.Add("y", 200);
                    hashTableArray.Add("z", 300);

                    Console.WriteLine("Поиск 'x': " + hashTableArray.Get("x")?.Value);
                    Console.WriteLine("Поиск 'y': " + hashTableArray.Get("y")?.Value);
                    Console.WriteLine("Поиск 'w': " + (hashTableArray.Get("w") == null ? "не найдено" : ""));

                    hashTableArray.Remove("y");
                    Console.WriteLine("После удаления 'y', поиск 'y': " + (hashTableArray.Get("y") == null ? "не найдено" : "ошибка"));


                    Console.WriteLine("\nСодержимое HashTableArray:");
                    Console.WriteLine(hashTableArray);

                    //flag = false;
                    break;
                
                case 5:
                    Console.WriteLine("\n-----Бинарное дерево-----");
                    // Создаем дерево
                    var tree = new BinarySearchTree<string>();

                    // Добавляем элементы
                    tree.Add("Пять", 5);
                    tree.Add("Три", 3);
                    tree.Add("Семь", 7);
                    tree.Add("Один", 1);
                    tree.Add("Девять", 9);
                    tree.Add("Шесть", 6);

                    Console.WriteLine("=== Обход дерева до балансировки ===");
                    tree.DisplayTree(); // Выводим несбалансированное дерево

                    Console.WriteLine("=== Балансировка дерева ===");
                    tree.Rebalance();

                    Console.WriteLine("=== Обход дерева после балансировки ===");
                    tree.DisplayTree();

                    Console.WriteLine("=== Поиск элемента с ключом 6 ===");
                    var found = tree.Search(6);
                    if (found != null)
                        Console.WriteLine($"Найдено: {found.Value} (ключ: {found.Key})");
                    else
                        Console.WriteLine("Не найдено");

                    Console.WriteLine("=== Удаление элемента с ключом 7 ===");
                    tree.Remove(7);

                    Console.WriteLine("=== Обход дерева после удаления ===");
                    tree.DisplayTree();

                    Console.WriteLine("=== Минимальный элемент ===");
                    var minNode = tree.FindMinimum(tree.Search(5));
                    Console.WriteLine(minNode?.Value);

                    Console.WriteLine("=== Следующий элемент после ключа 5 ===");
                    var next = tree.GetNext(tree.Search(5));
                    Console.WriteLine(next?.Value ?? "Нет следующего");

                    Console.WriteLine("=== Обход по возрастанию через InOrderWalk ===");
                    tree.InOrderWalk();

                    Console.WriteLine("=== Обход по убыванию через BackwardWalk ===");
                    tree.BackwardWalk();
                    //flag = false;
                    break;

                case 6:
                    Console.WriteLine("\n-----Графы-----");
                    var network = new Network();

                    var a = new Point("A");
                    var b = new Point("B");
                    var c = new Point("C");
                    var d = new Point("D");

                    network.AllPoints.Add(a);
                    network.AllPoints.Add(b);
                    network.AllPoints.Add(c);
                    network.AllPoints.Add(d);

                    network.AddBidirectionalConnection(a, b, 1);
                    network.AddBidirectionalConnection(a, c, 2);
                    network.AddBidirectionalConnection(b, d, 3);
                    network.AddBidirectionalConnection(c, d, 4);

                    network.PrintAdjacencyList();

                    network.BFS(a);
                    network.DFS(a);

                    var centers = network.FindCenters();
                    Console.WriteLine("\nЦентры графа:");
                    foreach (var center in centers)
                    {
                        Console.WriteLine(center);
                    }
                    //flag = false;
                    break;

                case 7:
                    flag = false;
                    break;


                default:
                    Console.WriteLine("Неправильоное значение! Введите верный номер");
                    break;
            }
        }
    }
}


