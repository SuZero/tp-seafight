namespace BaseObject
{
    public interface IShip
    {
        int Length { get; }
        IPlacement SailTo(Coordinate coord, Orientation orientation);
        IPlacement SailTo(int x, int y, Orientation orientation);
    }
}