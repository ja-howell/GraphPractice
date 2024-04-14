public class Program
{
    enum Direction = {North, South, East, West};

    public static void Main(string[] args)
    {
        TestAdjList();
        TestAdjMatrix();
        TestDownToOne();
    }

    private static void TestAdjList()
    {
        Dictionary<char, HashSet<char>> graph = new Dictionary<char, HashSet<char>>
        {
            { 'a', new HashSet<char> { 'b', 'd' } },
            { 'b', new HashSet<char> { 'c', 'e' } },
            { 'c', new HashSet<char> { 'd', 'f', 'i' } },
            { 'd', new HashSet<char> { 'g', 'i' } },
            { 'e', new HashSet<char> { 'c' } },
            { 'f', new HashSet<char> { 'e', 'j' } },
            { 'g', new HashSet<char> { 'h' } },
            { 'h', new HashSet<char> { 'd' } },
            { 'i', new HashSet<char> { 'a', 'd' } },
            { 'j', new HashSet<char> {} }
        };

        char start = 'g';
        char end = 'j';
        List<char> path = BreadthFirstSearchAdjList(graph, start, end);
        Console.Write("[");
        foreach (char node in path)
        {
            Console.Write($" {node}");
        }
        Console.WriteLine(" ]");
    }
    public static List<char> BreadthFirstSearchAdjList(Dictionary<char, HashSet<char>> graph, char start, char end)
    {
        List<char> path = new List<char>();
        Dictionary<char, char> cameFrom = new Dictionary<char, char>();
        Queue<char> frontier = new Queue<char>();
        frontier.Enqueue(start);
        cameFrom[start] = start;
        while (frontier.Count > 0)
        {
            char u = frontier.Dequeue();
            if (u == end)
            {
                while (cameFrom[u] != u)
                {
                    path.Add(u);
                    u = cameFrom[u];
                }
                path.Add(u);
                path.Reverse();
                return path;
            }
            foreach (var v in graph[u])
            {
                if (!cameFrom.ContainsKey(v))
                {
                    cameFrom[v] = u;
                    frontier.Enqueue(v);
                }
            }
        }
        return path;
    }

    private static void TestAdjMatrix()
    {

        int[][] graph = new int[][] {
                  //    0  1  2  3  4  5  6  7  8  9
            new int[] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
            new int[] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
            new int[] { 0, 0, 0, 1, 0, 1, 1, 0, 1, 0},
            new int[] { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0},
            new int[] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},
            new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
            new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
            new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            new int[] { 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        var path = BreadthFirstSearchAdjMatrix(graph, 6, 9);
        Console.Write("[ ");
        foreach (var step in path)
        {
            Console.Write($"{step} ");
        }
        Console.WriteLine("]");

    }

    private static List<int> BreadthFirstSearchAdjMatrix(int[][] graph, int start, int end)
    {
        Dictionary<int, int> cameFrom = new Dictionary<int, int>();
        Queue<int> frontier = new Queue<int>();
        cameFrom[start] = start;
        frontier.Enqueue(start);

        while (frontier.Count > 0)
        {
            int u = frontier.Dequeue();

            if (u == end)
            {
                return BuildPath(cameFrom, u);
            }

            List<int> neighbors = GetNeighbors(graph, u);
            foreach (var v in neighbors)
            {
                if (!cameFrom.ContainsKey(v))
                {
                    frontier.Enqueue(v);
                    cameFrom[v] = u;
                }
            }
        }

        return new List<int>();
    }

    private static List<int> GetNeighbors(int[][] graph, int u)
    {
        List<int> neighbors = new List<int>();
        for (int v = 0; v < graph[u].Length; v++)
        {
            if (graph[u][v] == 1)
            {
                neighbors.Add(v);
            }
        }
        return neighbors;
    }

    private static List<int> BuildPath(Dictionary<int, int> cameFrom, int u)
    {
        List<int> path = new List<int>();
        while (cameFrom[u] != u)
        {
            path.Insert(0, u);
            u = cameFrom[u];
        }
        path.Insert(0, u);
        return path;
    }

    private static void TestDownToOne()
    {
        int num = 10;
        List<int> path = DownToOne(num);
        Console.Write("[ ");
        foreach (var node in path)
        {
            Console.Write($"{node} ");
        }
        Console.WriteLine("]");
    }

    private static List<int> DownToOne(int num)
    {
        Dictionary<int, int> cameFrom = new Dictionary<int, int>();
        Queue<int> frontier = new Queue<int>();
        cameFrom[num] = num;
        frontier.Enqueue(num);

        while (frontier.Count > 0)
        {
            int u = frontier.Dequeue();

            if (u == 1)
            {
                return BuildPath(cameFrom, u);
            }

            List<int> children = GetChildren(u);
            foreach (var v in children)
            {
                if (!cameFrom.ContainsKey(v))
                {
                    frontier.Enqueue(v);
                    cameFrom[v] = u;
                }
            }
        }
        return new List<int>();
    }

    private static List<int> GetChildren(int u)
    {
        List<int> children = new List<int>();
        if (u % 3 == 0)
        {
            children.Add(u / 3);
        }
        if (u % 2 == 0)
        {
            children.Add(u / 2);
        }
        children.Add(u - 1);
        return children;
    }

//enum Direction = {North, South, East, West}
    private static List<int> GetTurtlets(char[][] board, State state)
    {
        List<char> turtlets = new List<char>();
        Direction dir = state.dir;
        int row = state.row;
        int col = state.col;
        turtlets.Add('R');
        turtlets.Add('L');
        if (dir == North && row > 0 && board[row - 1][col] != 'C')
        {
            if (board[row - 1][col] == 'I')
            {
                turtlets.Add('X');
            }
            else
            {
                turtlets.Add('F');
            }
        }
        if (dir == South && row < board.Length - 1 && board[row + 1][col] != 'C')
        {
            if (board[row + 1][col] == 'I')
            {
                turtlets.Add('X');
            }
            else
            {
                turtlets.Add('F');
            }
        }
        if (dir == East && col < board[row].Length -1 && board[row][col + 1] != 'C')
        {
            if (board[row][col + 1] == 'I')
            {
                turtlets.Add('X');
            }
            else
            {
                turtlets.Add('F');
            }
        }
        if (dir == West && col > 0 && board[row][col - 1] != 'C')
        {
            if (board[row][col - 1] == 'I')
            {
                turtlets.Add('X');
            }
            else
            {
                turtlets.Add('F');
            }
        }

}
    private class State
    {
        int row = {get, set};
        int col = { get, set };
        Direction dir = { get, set };

    }
}
