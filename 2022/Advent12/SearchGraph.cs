public class SearchGraph
{
    public List<SearchNode> AllNodes = new List<SearchNode>();

    public SearchNode Start;
    public SearchNode End;

    private Topography _topo;

    private Queue<SearchNode> _queue;
    private SearchNode[,] _nodes;

    public SearchGraph(Topography topo, Func<SearchNode, SearchNode, bool> stepComparator)
    {
        _topo = topo;
        _queue = new Queue<SearchNode>();
        _nodes = new SearchNode[topo.Width, topo.Height];

        for (var y = 0; y < topo.Height; y++)
        {
            for (var x = 0; x < topo.Width; x++)
            {
                _nodes[x, y] = new SearchNode(x, y, topo.Elevations[x, y]);
            }
        }

        foreach (var node in _nodes)
        {
            if (node.Y > 0)
            {
                var north = _nodes[node.X, node.Y - 1];
                if (stepComparator(node, north)) node.Neighbours.Add(north);
            }
            if (node.Y < topo.Height - 1)
            {
                var south = _nodes[node.X, node.Y + 1];
                if (stepComparator(node, south)) node.Neighbours.Add(south);
            }
            if (node.X > 0)
            {
                var west = _nodes[node.X - 1, node.Y];
                if (stepComparator(node, west)) node.Neighbours.Add(west);
            }
            if (node.X < topo.Width - 1)
            {
                var east = _nodes[node.X + 1, node.Y];
                if (stepComparator(node, east)) node.Neighbours.Add(east);
            }
            
            if (node.X == topo.EndX && node.Y == topo.EndY) End = node;
            if (node.X == topo.StartX && node.Y == topo.StartY) Start = node;

            AllNodes.Add(node);
        }
    }

    public static bool CloseEnoughUp(SearchNode source, SearchNode target)
    {
        if (target.Elevation <= source.Elevation) return true;
        if (target.Elevation - source.Elevation > 1) return false;
        return true;
        //return (Math.Abs(source.Elevation - target.Elevation) <= 1);
    }

    public static bool CloseEnoughDown(SearchNode source, SearchNode target)
    {
        if (target.Elevation >= source.Elevation) return true;
        if (target.Elevation - source.Elevation < -1) return false;
        return true;
        //return (Math.Abs(source.Elevation - target.Elevation) <= 1);
    }

    public SearchNode Traverse(SearchNode first, SearchNode target)
    {
        _queue.Enqueue(first);
        first.Visited = true;

        var cycle=0;
        while (_queue.Any())
        {
            var head = _queue.Dequeue();

            if (head == target)
            {
                return head;
            }

            var neighbours = head.Neighbours.Where(n => !n.Visited);
            foreach (var neighbour in neighbours)
            {
                neighbour.Visited = true;
                neighbour.Parent = head;
                _queue.Enqueue(neighbour);
            }
            cycle = (cycle+1) % 100;
            if (cycle == 0)  
            {
                Dump();
                Console.ReadKey(false);
            }
        }
        throw new System.Exception("GAVE UP!");
    }

    public SearchNode TraverseToLowest(SearchNode first)
    {
        _queue.Enqueue(first);
        first.Visited = true;

        var cycle=0;
        while (_queue.Any())
        {
            var head = _queue.Dequeue();

            if (head.Elevation == 1)
            {
                return head;
            }

            var neighbours = head.Neighbours.Where(n => !n.Visited);
            foreach (var neighbour in neighbours)
            {
                neighbour.Visited = true;
                neighbour.Parent = head;
                _queue.Enqueue(neighbour);
            }
            cycle = (cycle+1) % 100;
            if (cycle == 0)  
            {
                Dump();
                Console.ReadKey(false);
            }
        }
        throw new System.Exception("GAVE UP!");
    }

    public IEnumerable<SearchNode> Backtrack(SearchNode target)
    {
        var currentNode = target;
        while (currentNode.Parent != null)
        {
            yield return currentNode;
            currentNode = currentNode.Parent;
        }
    }

    public void Dump(IList<SearchNode>? path = null)
    {
        Console.WriteLine();
        for (var y=0; y < _topo.Height; y++)
        {
            for (var x=0; x < _topo.Width; x++)
            {
                var symbol = ".";
                var node = _nodes[x,y];
                if (node.Visited) symbol ="#";
                if (_queue.Contains(node)) symbol = "?";
                if (path != null && path.Contains(node)) symbol = "*";
                if (x == _topo.StartX && y == _topo.StartY) symbol="S";
                if (x == _topo.EndX && y == _topo.EndY) symbol="E";
                
                Console.Write(symbol);
            }
            Console.WriteLine();
        }
        Console.WriteLine(_queue.Count);
    }
}

