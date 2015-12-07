

namespace BaseObject
{
    public interface IPlayerView
    {
        int GetXMax();
        int GetYMax();
        bool PutShip(IPlacement placement);
    }
}