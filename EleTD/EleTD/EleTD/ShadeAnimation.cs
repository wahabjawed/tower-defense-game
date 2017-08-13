using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
    class ShadeAnimation : SpriteAnimation
    {
        public bool doCollapse = false;
        public bool doExpand = false;

        public ShadeAnimation(Texture2D texture)
            : base(texture)
        {
        }

        public float collapseTime = 0.0f;
        public float expandTime = 0.5f;

        public override void Update(GameTime gameTime, Vector2 location)
        {
            if (doCollapse)
            {

                shader = Game1.flipEffect;
                collapseTime += 0.03f;

                if (collapseTime > 0.5f)
                {
                    collapseTime = 0f;
                    shader = null;
                    doCollapse = false;
                }

            }
            if (doExpand)
            {

                shader = Game1.flipEffect;
                expandTime -= 0.03f;

                if (expandTime < 0.01f)
                {
                    shader = null;
                    expandTime = 0.5f;
                    doExpand = false;
                }

            }

            base.Update(gameTime, location);


        }



        public override void shade()
        {
            base.shade();
            if (doExpand)
            {

                this.shader.Parameters["top"].SetValue(expandTime);
            }
            else if (doCollapse)
            {
                this.shader.Parameters["top"].SetValue(collapseTime);
            }
        }
    }
}
