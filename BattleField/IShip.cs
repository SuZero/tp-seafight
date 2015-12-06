namespace BattleField
{
    public interface IShip
    {
        int Length { get; }
        Placement SailTo(Coordinate coord, Orientation orientation);
        Placement SailTo(int x, int y, Orientation orientation);
    }
}