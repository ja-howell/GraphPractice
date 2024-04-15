public class Program
{
    enum Direction
    {
        North,
        East,
        South,
        West
    }

    public static void Main(string[] args)
    {
        TestAdjList();
        TestAdjMatrix();
        TestDownToOne();
        TestTurtlesBFS();
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

    private static void TestTurtlesBFS()
    {
        char[][] board = new char[][] {
            new char[] { '.', '.', '.', '.', '.', '.', '.', '.'},
            new char[] { '.', '.', '.', '.', '.', '.', '.', '.'},
            new char[] { '.', '.', '.', '.', '.', '.', '.', '.'},
            new char[] { '.', '.', '.', 'C', 'C', '.', '.', '.'},
            new char[] { '.', '.', 'C', '.', 'D', 'C', '.', '.'},
            new char[] { '.', 'C', '.', '.', 'C', '.', '.', '.'},
            new char[] { 'C', '.', 'I', 'C', '.', '.', '.', '.'},
            new char[] { 'T', '.', 'C', '.', '.', '.', '.', '.'}
        };
        string path = TurtlesBFS(board);
        Console.WriteLine(path);
    }
    private static string TurtlesBFS(char[][] board)
    {
        Dictionary<State, State> cameFrom = new Dictionary<State, State>();
        Queue<State> frontier = new Queue<State>();
        Position position = new Position(7,0);
        State start = new State(position, Direction.East, IsFacingIceCastle(board, position, Direction.East));
        State copy = new State(position, Direction.East, IsFacingIceCastle(board, position, Direction.East));
        cameFrom[start] = start;
        frontier.Enqueue(start);
        while (frontier.Count > 0)
        {
            State u = frontier.Dequeue();
            if (board[u.position.row][u.position.col] == 'D')
            {
                return BuildTurtlePath(cameFrom, u);
            }

            List<State> states = GetNextStates(board, u);
            foreach (var v in states)
            {
                if (!cameFrom.ContainsKey(v))
                {
                    frontier.Enqueue(v);
                    cameFrom[v] = u;
                }
            }
        }
        return "no solution";
    }

    private static List<State> GetNextStates(char[][] board, State state)
    {
        List<State> states = new List<State>();
        if (state.dir == Direction.North || state.dir == Direction.South)
        {
            states.Add(new State(state.position, Direction.East, IsFacingIceCastle(board, state.position, Direction.East)));
            states.Add(new State(state.position, Direction.West, IsFacingIceCastle(board, state.position, Direction.West)));
        }
        else
        {
            states.Add(new State(state.position, Direction.North, IsFacingIceCastle(board, state.position, Direction.North)));
            states.Add(new State(state.position, Direction.South, IsFacingIceCastle(board, state.position, Direction.South)));
        }

        Position position = GetNewPosition(state.position, state.dir);
        if (IsInbounds(position.row, position.col, board)) 
        {
            if (state.iceCastle)
            {
                states.Add(new State(state.position, state.dir, false));
            }
            else if (board[position.row][position.col] != 'C')
            {
                states.Add(new State(position, state.dir, IsFacingIceCastle(board, position, state.dir)));
            }
        }
        return states;
    }

    private static bool IsInbounds(int row, int col, char[][] board)
    {
        return 0 <= row && row < board[0].Length && 0 <= col && col < board[0].Length;
    }

    private static bool IsFacingIceCastle(char[][] board, Position position, Direction dir)
    {
        Position newPosition = GetNewPosition(position, dir);
        if (IsInbounds(newPosition.row, newPosition.col, board))
        {
            return board[newPosition.row][newPosition.col] == 'I';
        }
        return false;
    }

    private static Position GetNewPosition(Position position, Direction dir)
    {
        int candidateRow = position.row;
        int candidateCol = position.col;
        switch (dir)
        {
            case Direction.West:
                candidateCol = position.col - 1;
                break;
            case Direction.East:
                candidateCol = position.col + 1;
                break;
            case Direction.North:
                candidateRow = position.row - 1;
                break;
            case Direction.South:
                candidateRow = position.row + 1;
                break;
        }
        return new Position(candidateRow, candidateCol);
    }

    private static string BuildTurtlePath(Dictionary<State, State> cameFrom, State u)
    {
        string path = "";
        Console.WriteLine("path");
        while(cameFrom[u] != u)
        {
            if (u.position.row != cameFrom[u].position.row || u.position.col != cameFrom[u].position.col)
            {
                path = 'F' + path;
            }
            else if (u.dir != cameFrom[u].dir)
            {
                path = CalculateDirectionChange(u, cameFrom[u]) + path;
            }
            else
            {
                path = 'X' + path;
            }
            u = cameFrom[u];
        }
        return path;
    }
    
    private static char CalculateDirectionChange(State child, State parent)
    {
        if (child.dir == Direction.North)
        {
            if (parent.dir == Direction.East)
            {
                return 'L';
            }
            return 'R';
        }
        else if (child.dir == Direction.South)
        {
            if (parent.dir == Direction.West)
            {
                return 'L';
            }
            return 'R';
        }
        else if (child.dir == Direction.East)
        {
            if (parent.dir == Direction.South)
            {
                return 'L';
            }
            return 'R';
        }
        else
        {
            if (parent.dir == Direction.North)
            {
                return 'L';
            }
            return 'R';
        }
    }

    private class State
    {
        public State(Position position, Direction dir, bool iceCastle)
        {
            this.position = position;
            this.dir = dir;
            this.iceCastle = iceCastle;
        }
        public Position position { get; set; }
        public Direction dir { get; set; }
        public bool iceCastle { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            State other = (State)obj;
            return position.Equals(other.position) &&
                   dir == other.dir &&
                   iceCastle == other.iceCastle;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 23) + position.GetHashCode();
                hash = (hash * 23) + dir.GetHashCode();
                hash = (hash * 23) + iceCastle.GetHashCode();
                return hash;
            }
        }

    }

    private class Position
    {
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        public int row { get; set; }
        public int col { get; set; }
    }
}