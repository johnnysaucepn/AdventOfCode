public enum SandState
{
    Falling,
    Settled,
    Disappeared,
    Blocked
};

public class Sand
{
    public Point Origin;
    public Point Location;

    public SandState State;

    public Sand(Point origin)
    {
        Origin = origin;
        Location = origin;
    }

public void Drop1(Topography topo)
{
    if (Location.Y == topo.Height - 1)
    {
        State = SandState.Disappeared;
        return;
    }
    else if (topo.Space[Location.X, Location.Y + 1] == Material.Air)
    {
        Location.Y++;
    }
    else if (Location.X == 0)
    {
        State = SandState.Disappeared;
        return;
    }
    else if (topo.Space[Location.X - 1, Location.Y + 1] == Material.Air)
    {
        Location.X--;
        Location.Y++;
    }
    else if (Location.X == topo.Width - 1)
    {
        State = SandState.Disappeared;
        return;
    }
    else if (topo.Space[Location.X + 1, Location.Y + 1] == Material.Air)
    {
        Location.X++;
        Location.Y++;
    }
    else if (Location == Origin)
    {
        State = SandState.Blocked;
        return;
    }
    else
    {
        topo.Space[Location.X, Location.Y] = Material.Sand;
        State = SandState.Settled;
        return;
    }
    State = SandState.Falling;
}

}
