using System;
using System.Collections.Generic;

// Состояние точки (аналог цвета)
public enum PointState
{
    Inactive,
    Processing,
    Processed
}

// Класс связи между точками
public class Connection
{
    public Point Start { get; }
    public Point End { get; }
    public double Length { get; }

    public Connection(Point start, Point end, double length)
    {
        Start = start;
        End = end;
        Length = length;
    }

    public override string ToString()
    {
        return $"[{Start.Label} -> {End.Label}, Длина: {Length}]";
    }
}

// Класс точки (вершины графа)
public class Point
{
    private static int _idCounter = 0;

    public string Label { get; }
    public List<Connection> Connections { get; }
    public double TotalDistance { get; set; }
    public PointState State { get; set; }
    public Point Previous { get; set; }
    public bool Visited { get; set; }
    public int Id { get; }

    public Point(string label)
    {
        Label = label;
        Id = ++_idCounter;
        Connections = new List<Connection>();
        TotalDistance = double.MaxValue;
        State = PointState.Inactive;
        Previous = null!;
        Visited = false;
    }

    public int GetId() => Id;

    public List<Connection> GetConnections() => Connections;

    public override string ToString()
    {
        return $"{Label}(ID={Id})";
    }

    public void ShowConnections()
    {
        Console.Write($"Связи точки {this}: ");
        foreach (var conn in Connections)
        {
            Console.Write($"{conn} ");
        }
        Console.WriteLine();
    }

    public bool AddConnection(Connection connection)
    {
        if (connection.Start != this) return false;

        foreach (var existing in Connections)
        {
            if (existing.End == connection.End) return false;
        }

        Connections.Add(connection);
        return true;
    }
}

// Класс сети (графа)
public class Network
{
    public List<Point> AllPoints { get; } = new List<Point>();
    public List<Connection> AllConnections { get; } = new List<Connection>();

    // Одностороннее добавление связи
    public bool AddConnection(Point from, Point to, double length)
    {
        if (!AllPoints.Contains(from) || !AllPoints.Contains(to)) return false;

        foreach (var edge in from.GetConnections())
        {
            if (edge.End.GetId() == to.GetId()) return false;
        }

        var connection = new Connection(from, to, length);
        from.GetConnections().Add(connection);
        AllConnections.Add(connection);
        return true;
    }

    // Двустороннее добавление связи
    public bool AddBidirectionalConnection(Point a, Point b, double length)
    {
        if (!AllPoints.Contains(a) || !AllPoints.Contains(b)) return false;

        foreach (var edge in a.GetConnections())
        {
            if (edge.End.GetId() == b.GetId()) return false;
        }

        foreach (var edge in b.GetConnections())
        {
            if (edge.End.GetId() == a.GetId()) return false;
        }

        var connAB = new Connection(a, b, length);
        var connBA = new Connection(b, a, length);

        a.GetConnections().Add(connAB);
        b.GetConnections().Add(connBA);
        AllConnections.Add(connAB);
        AllConnections.Add(connBA);

        return true;
    }

    // Вывод списка смежности
    public void PrintAdjacencyList()
    {
        Console.WriteLine("Список смежности:");
        foreach (var point in AllPoints)
        {
            Console.Write($"{point.Label}: ");
            foreach (var conn in point.GetConnections())
            {
                Console.Write($"{conn.End.Label}({conn.Length}) ");
            }
            Console.WriteLine();
        }
    }

    // Обход в ширину (BFS)
    public void BFS(Point source)
    {
        Queue<Point> queue = new Queue<Point>();
        List<Point> visitedPoints = new List<Point>();

        foreach (var point in AllPoints)
        {
            point.TotalDistance = double.MaxValue;
            point.Previous = null!;
            point.State = PointState.Inactive;
        }

        source.State = PointState.Processing;
        source.TotalDistance = 0;
        source.Previous = null!;
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var conn in current.GetConnections())
            {
                var next = conn.End;

                if (next.State == PointState.Inactive)
                {
                    next.State = PointState.Processing;
                    next.TotalDistance = current.TotalDistance + conn.Length;
                    next.Previous = current;
                    queue.Enqueue(next);
                    visitedPoints.Add(next);
                }
            }

            current.State = PointState.Processed;
        }

        Console.WriteLine("\nОбход в ширину (BFS):");
        Console.Write(source + " -> ");
        foreach (var p in visitedPoints)
        {
            Console.Write(p + " -> ");
        }
        Console.WriteLine("END");
    }

    // Обход в глубину (DFS)
    private static int time = 0;

    public void DFS(Point start)
    {
        List<Point> traversalOrder = new List<Point>();

        foreach (var point in AllPoints)
        {
            point.State = PointState.Inactive;
            point.Previous = null!;
        }

        time = 0;   
        DFSVisit(start, traversalOrder);

        Console.WriteLine("\nОбход в глубину (DFS):");
        Console.Write(start + " -> ");
        foreach (var p in traversalOrder)
        {
            Console.Write(p + " -> ");
        }
        Console.WriteLine("END");
    }

    private void DFSVisit(Point u, List<Point> order)
    {
        u.State = PointState.Processing;
        time++;
        u.TotalDistance = time;

        foreach (var conn in u.GetConnections())
        {
            var v = conn.End;
            if (v.State == PointState.Inactive)
            {
                v.Previous = u;
                order.Add(v);
                DFSVisit(v, order);
            }
        }

        u.State = PointState.Processed;
        time++;
        u.Visited = true;
    }

    // Поиск центров графа
    public List<Point> FindCenters()
    {
        List<Point> centers = new List<Point>();
        int minEccentricity = int.MaxValue;

        foreach (var point in AllPoints)
        {
            BFS(point);
            int maxDist = int.MinValue;

            foreach (var other in AllPoints)
            {
                if (other.TotalDistance > maxDist)
                {
                    maxDist = (int)other.TotalDistance;
                }
            }

            if (maxDist < minEccentricity)
            {
                minEccentricity = maxDist;
            }
        }

        foreach (var point in AllPoints)
        {
            BFS(point);
            int maxDist = int.MinValue;

            foreach (var other in AllPoints)
            {
                if (other.TotalDistance > maxDist)
                {
                    maxDist = (int)other.TotalDistance;
                }
            }

            if (maxDist == minEccentricity)
            {
                centers.Add(point);
            }
        }

        return centers;
    }
}