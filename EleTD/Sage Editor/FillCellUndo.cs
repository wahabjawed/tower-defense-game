using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sage_Editor
{
    public class FillCellUndo : Command
    {
        public TileLayer currentLayer;
        public TileLayer LayerStateBeforeRecursion;
        int fillCounter = 50;


        public void FillCellIndex(int x, int y, int DesiredIndex)
        {
            int oldIndex = currentLayer.GetCellIndex(x, y);

            if (oldIndex == DesiredIndex || fillCounter == 0)
            {
                return;
            }

            fillCounter--;
            currentLayer.SetCellIndex(x, y, DesiredIndex);

            if (x > 0 && currentLayer.GetCellIndex(x - 1, y) == oldIndex)
            {
                FillCellIndex(x - 1, y, DesiredIndex);
            }
            if (x < currentLayer.LayerWidthinTiles - 1 && currentLayer.GetCellIndex(x + 1, y) == oldIndex)
            {
                FillCellIndex(x + 1, y, DesiredIndex);
            }
            if (y > 0 && currentLayer.GetCellIndex(x, y - 1) == oldIndex)
            {
                FillCellIndex(x, y - 1, DesiredIndex);
            }
            if (y < currentLayer.LayerHeightinTiles - 1 && currentLayer.GetCellIndex(x, y + 1) == oldIndex)
            {
                FillCellIndex(x, y + 1, DesiredIndex);
            }
        }


        public FillCellUndo(Form1 form)
            : base(form)
        {
            this.currentLayer = form.currentLayer;
        }

        public override void Excute()
        {
            if (form.TextureList.Items.Count > 0 && form.TextureList.SelectedItem != null)
            {
                LayerStateBeforeRecursion = new TileLayer(currentLayer.TileMapArray);
                foreach (Texture2D text in currentLayer.TexturesList)
                {
                    LayerStateBeforeRecursion.AddTexture(text);
                }

                fillCounter = 5000;
                FillCellIndex((int)form.TileX, (int)form.TileY, currentLayer.HasTexture(form.dictTextures[form.TextureList.SelectedItem as string]));
            }
        }

        public override void Undo()
        {
            string key = "";
            foreach (KeyValuePair<string, TileLayer> KeyVal in form.dictLayer)
            {
                if (KeyVal.Value == currentLayer)
                {
                    key = KeyVal.Key;
                    break;
                }
            }
            form.LayerList.SelectedItem = key;
            TileLayer FoundLayer = form.dictLayer[key];

            currentLayer = FoundLayer;

            FoundLayer.TileMapArray = LayerStateBeforeRecursion.TileMapArray;


        }



        public override Command Clone()
        {
            return new FillCellUndo(form);
        }

        private bool ComapreArrays(int[,] A, int[,] B)
        {
            
            if( (A.GetLength(0) == B.GetLength(0) && (A.GetLength(1) == B.GetLength(1))))
            {
                for(int x = 0; x< A.GetLength(1); x++)
                {
                    for(int y = 0; y< A.GetLength(0); y++)
                    {
                        if(!(A[x,y] == B[x,y]))
                            return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public override bool CompareTo(Command commandToCompare)
        {
            if (commandToCompare is FillCellUndo)
            {
                FillCellUndo commandComaparer = commandToCompare as FillCellUndo;
                if (ComapreArrays(commandComaparer.LayerStateBeforeRecursion.TileMapArray, this.LayerStateBeforeRecursion.TileMapArray) )
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }
}
