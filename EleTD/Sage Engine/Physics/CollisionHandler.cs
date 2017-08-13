using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Sage_Engine
{
  public class CollisionHandler
    {

      public static ICircleCollided CircleCollision;
      public static IRectCollided RectCollided;


      /// <summary>
      /// Input Two Sprites to see if their Collidable Rectangles Intersect.
      /// </summary>
      /// <param name="Sprite1"></param>
      /// <param name="sprite2"></param>
      /// <returns></returns>
      public static bool RectBasedCollision(DrawAble Sprite1, DrawAble sprite2)
      {
          if (Sprite1.GetCollisionRect.Intersects(sprite2.GetCollisionRect))
          {
              RectCollided.OnRectCollided(Sprite1, sprite2);
              return true;
          }

          return false;
      }

      /// <summary>
      /// Input two Sprties to see if their Radius Based Collision Happens or not.
      /// </summary>
      /// <param name="sprite1"></param>
      /// <param name="sprite2"></param>
      /// <returns></returns>
      public static bool CircleBasedSpriteCollision(DrawAble sprite1, DrawAble sprite2)
      {

          Vector2 Distance = sprite1.CenterofSprite - sprite1.CenterofSprite;
          if(Distance.Length() < (sprite1.CollisionRadius + sprite2.CollisionRadius))
          {
              CircleCollision.OnCircleCollision(sprite1, sprite2); //Stuff to do after Collision has been detected. To be Decided How to Implement this.
              return true;
          }
          return false;
      }

     
    }
}
