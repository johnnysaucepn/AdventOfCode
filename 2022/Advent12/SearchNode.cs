public record class SearchEdge
{
    public SearchNode From;
    public SearchNode To;
    
    public bool Visited = false;

    public SearchEdge(SearchNode from, SearchNode to)
    {
        From = from;
        To = to;
    }
}


public record class SearchNode
{
    public int X;
    public int Y;
    public int Elevation;

    public bool Visited = false;

    public SearchNode? Parent = null;

    public List<SearchNode> Neighbours = new List<SearchNode>();

    public SearchNode(int x, int y, int elevation)
    {
        X = x;
        Y = y;
        Elevation = elevation;
    }

}

