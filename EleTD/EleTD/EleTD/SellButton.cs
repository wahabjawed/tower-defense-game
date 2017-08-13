using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
    public class SellButton : DrawAble
    {
        public Tower Value;
        public Vector2 FixedScreenLocation;

        public SellButton(Texture2D BtnImage, Tower Value, Vector2 FixedScreenLocation)
            : base(new SpriteAnimation(BtnImage), FixedScreenLocation, 0, 0)
        {
            this.Value = Value;
            this.FixedScreenLocation = FixedScreenLocation;
        }


        public Boolean CheckIfClicked(Rectangle state)
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

    }
}
