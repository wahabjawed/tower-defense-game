using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage_Engine
{
   public interface IRectCollided
    {
        void OnRectCollided(DrawAble sprite1, DrawAble sprite2);
    }
}
