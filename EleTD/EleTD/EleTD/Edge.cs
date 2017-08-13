using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace EleTD
{
   public class Edge
    {
       public String Between;
       public Tower Source;
       public Tower Destination;

       public int GoldCost;
       public List<ElementalTypes> ElementsRequiredToCreate = new List<ElementalTypes>();

       public Edge(Tower Source, Tower Destination, int GoldCost, params ElementalTypes[] ElementsRequiredToCreate)
       {
           this.Source = Source;
           this.Destination = Destination;
           this.GoldCost = GoldCost;
           for (int i = 0; i < ElementsRequiredToCreate.Length; i++)
           {
              this.ElementsRequiredToCreate.Add(ElementsRequiredToCreate[i]);
           }
           Between = Source.TowerName + " - " + Destination.TowerName;
       }
       
    }
}
