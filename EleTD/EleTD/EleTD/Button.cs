using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
   public interface Button
    {
        bool CheckIfClicked(Rectangle mouse);

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
        
    }
}
