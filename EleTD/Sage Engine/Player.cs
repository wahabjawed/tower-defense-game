using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sage_Engine
{
   public class Player : Humonoid
   {

       #region Variables

       #endregion 

       #region Properties
       #endregion
       
       #region Constructer

       public Player(SpriteAnimation spriteAnimation,
           Vector2 location,
           float speed,
           int collisionRadius)
           : base(spriteAnimation, location, speed, collisionRadius)
       {
       }

       #endregion

       #region Healper Methods

       public override void Update(GameTime gameTime)
       {
           MovingInput();
           CalculateDirectionFacing(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
          SwitchAnimationToFaceDirection();
           base.Update(gameTime);
       }



       private void MovingInput()
       {
           Vector2 Mover = Vector2.Zero;

           if (InputHandler.KeyDown(Keys.W))
           {
               Mover.Y = -1;
           }
           if (InputHandler.KeyDown(Keys.S))
           {
               Mover.Y = 1;
           }
           if (InputHandler.KeyDown(Keys.D))
           {
               Mover.X = 1;
           }
           if (InputHandler.KeyDown(Keys.A))
           {
               Mover.X = -1;
           }
           Direction = Mover;
       }
       #endregion
   }
}
