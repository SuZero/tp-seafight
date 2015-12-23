using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseObject
{
    public interface IPlacement
    {

        Orientation Orientation
        {
            get;
            set;
        }
        IShip Ship
        {
            get;
            set;
        }

    }
}
