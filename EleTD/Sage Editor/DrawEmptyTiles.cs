using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Editor
{
   public static class DrawEmptyTiles
    {
       public static Texture2D EmptyTileTexture;
       public static Color BaseColor = Color.White;
       public static Color SelectedColor = Color.Red;
       public static Form1 form;
       public static Texture2D CrossTexture;

       public static void Initialise(Form1 forms)
       {
           form = forms;
           EmptyTileTexture = form.EmptyTile;
           CrossTexture = form.Cross;
       }

       public static void DrawTiles()
       {
           form.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);
          
           for (int x = 0; x < form.currentLayer.LayerWidthinTiles; x++)
           {
               for (int y = 0; y < form.currentLayer.LayerHeightinTiles; y++)
               {
                   form.spriteBatch.Draw(EmptyTileTexture,
                       new Rectangle(x * TileLayer.GetTileWidth, y * TileLayer.GetTileHeight, TileLayer.GetTileWidth, TileLayer.GetTileHeight),
                       Color.Green);
               }
           }

           DrawCollisionX(form.spriteBatch);


           form.spriteBatch.End();
       }


       public static void DrawCollisionX(SpriteBatch batch)
       {

           if (form.checkShowCollision.Checked == true)
           {
               int[,] collmap = form.Map.CollisionMap;

               for (int x = 0; x < form.currentLayer.LayerWidthinTiles; x++)
               {
                   for (int y = 0; y < form.currentLayer.LayerHeightinTiles; y++)
                   {
                       if (collmap[y, x] == 1)
                       {
                           form.spriteBatch.Draw(CrossTexture,
                               new Rectangle(x * TileLayer.GetTileWidth, y * TileLayer.GetTileHeight, TileLayer.GetTileWidth, TileLayer.GetTileHeight),
                             new Color(new Vector4(1f, 1f, 1f, 0.5f)));
                       }
                   }

               }
           }
       }

       public static void DrawSelectedTile()
       {
           form.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);

           if ((form.TileX != null) && (form.TileY != null))
           {
               form.spriteBatch.Draw(EmptyTileTexture, new Rectangle(
                 (int)form.TileX * TileLayer.GetTileWidth, (int)form.TileY * TileLayer.GetTileHeight, TileLayer.GetTileWidth, TileLayer.GetTileHeight),
                  SelectedColor);
           }
           form.spriteBatch.End();
       }
    }
}
