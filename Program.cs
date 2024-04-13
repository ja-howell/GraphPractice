public class Program
{
    public static void Main(string[] args)
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
        List<char> path = BreadthFirstSearch(graph, start, end);
        Console.Write("[");
        foreach (char node in path)
        {
            Console.Write($" {node}");
        }
        Console.WriteLine(" ]");
    }

    public static List<char> BreadthFirstSearch(Dictionary<char, HashSet<char>> graph, char start, char end)
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
}