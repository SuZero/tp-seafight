using Microsoft.Xna.Framework;

namespace seafight
{
    /// <summary>
    /// Interface describing necessary implementation for working with the DragAndDropController.
    /// </summary>
    public interface IDragAndDropItem
    {
        Vector2 Position { get; set; }
        bool IsSelected { set; }
        bool IsMouseOver { set; }
        bool Contains(Vector2 pointToCheck);
        Rectangle Border { get; }
    }
}