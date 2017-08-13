using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{
   public class CircleBack : ICircleCollided
    {
       public void OnCircleCollision(DrawAble sprite1, DrawAble sprite2)
       {

           Vector2 Direction = sprite2.CenterofSprite - sprite1.CenterofSprite;
           Direction.Normalize();
           int DistanceBetween = sprite1.CollisionRadius + sprite2.CollisionRadius;
           Direction *= DistanceBetween;
           sprite1.CenterofSprite += Direction;


       }
    }
}
