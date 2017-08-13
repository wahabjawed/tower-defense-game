using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
   public class ButtonTower : DrawAble , Button
    {
       public Tower Value;
       public static SpriteFont font;
       public static SpriteFont smallerFont;
       public int GoldToCreate;
       public Vector2 FixedScreenLocation;
       public float CenterOfSpriteX;
       public float LocationOfCenterOfText;

       public ButtonTower(SpriteAnimation spriteAnimation,
           Vector2 location,
           Tower value,
           int GoldToCreate)
           : base(spriteAnimation, location, 0, 0)
       {
           Value = value;
           this.FixedScreenLocation = location;
           this.GoldToCreate = GoldToCreate;
           CenterOfSpriteX = this.CenterofSprite.X;
           LocationOfCenterOfText = font.MeasureString(value.TowerName).X / 2;
       }


       public bool CheckIfClicked(Rectangle state)
       {

           if (state.Intersects(GetCollisionRect))
           {
               return true;
           }
           return false;
       }

       public override void Update(GameTime gameTime)
       {
           Location = Camera.Position + FixedScreenLocation;
           base.Update(gameTime);
       }

       public override void Draw(SpriteBatch spriteBatch)
       {
           Vector2 SLocation = FixedScreenLocation;
           SLocation.X = CenterOfSpriteX - LocationOfCenterOfText;
           SLocation.Y -= 20;
           spriteBatch.Begin();
               spriteBatch.DrawString(font, Value.TowerName, SLocation, Color.Goldenrod);
        
           spriteBatch.End();
           base.Draw(spriteBatch);
       }
    }
}
