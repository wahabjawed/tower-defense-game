using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sage_Engine;

namespace Sage_Editor
{
   public class EraseCellCommand : Command
    {
        public int PreviousTexture;
        protected TileLayer layer;
        public Vector2 TileLocation;

        public EraseCellCommand(Form1 form)
           :base(form)
       {
       }

        public override void Excute()
        {
            TileLayer currentLayer = this.form.currentLayer;
          
                if ((this.form.TileX != null) || (this.form.TileY != null))
                {
                    int TileX = (int)this.form.TileX;
                    int TileY = (int)this.form.TileY;

                    this.TileLocation = new Vector2(TileX, TileY);
                    this.layer = currentLayer;
                    this.PreviousTexture = currentLayer.GetCellIndex(TileX, TileY);

                    int IndexToSet = -1;
                    currentLayer.SetCellIndex(TileX, TileY, IndexToSet);
                }
            
        }

        public override void Undo()
        {

            if (!(form.currentLayer.Equals(null)))
            {
                form.currentLayer = this.layer;
                layer.SetCellIndex((int)TileLocation.X, (int)TileLocation.Y, this.PreviousTexture);
              
                string key = "";
                foreach (KeyValuePair<string, TileLayer> KeyVal in form.dictLayer)
                {
                    if (KeyVal.Value == layer)
                    {
                        key = KeyVal.Key;
                    }
                }

                form.LayerList.SelectedItem = key;
                
            }
        }
        public override Command Clone()
        {
            return new EraseCellCommand(this.form);
        }

        public override bool CompareTo(Command commandToCompare)
        {
            if (commandToCompare is EraseCellCommand)
            {
                EraseCellCommand commandComaparer = commandToCompare as EraseCellCommand;
                if( (commandComaparer.TileLocation == this.TileLocation) && (commandComaparer.PreviousTexture == this.PreviousTexture) )
                {
                    return true; // Same Object
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
