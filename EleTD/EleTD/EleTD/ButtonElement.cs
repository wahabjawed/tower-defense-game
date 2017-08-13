using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
    class ButtonElement:DrawAble,Button
    {
       public ElementalTypes Type; 
       public static SpriteFont font;
       public Vector2 FixedScreenLocation;
       public float CenterOfSpriteX;
       public float LocationOfCenterOfText;

       public ButtonElement(SpriteAnimation spriteAnimation,
           Vector2 location, ElementalTypes value)
           : base(spriteAnimation, location, 0,0)
       {
           Type = value;
           this.FixedScreenLocation = location;
           CenterOfSpriteX = this.CenterofSprite.X;
          // LocationOfCenterOfText = font.MeasureString(Type.ToString()).X / 2;
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
           SLocation.X = CenterOfSpriteX - LocationOfCenterOfText-15;
           SLocation.Y -= 20;
          
           spriteBatch.Begin();
           spriteBatch.DrawString(ButtonTower.font, Type.ToString(), SLocation, TowerGui.CollorPallet[(int)Type]);

           spriteBatch.End();
           
           base.Draw(spriteBatch);
       }
    }
    }

