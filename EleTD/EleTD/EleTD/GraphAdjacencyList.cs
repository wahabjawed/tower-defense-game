using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
    public class GraphAdjacencyList
    {
        Dictionary<String, Tower> AdjacentList = new Dictionary<string, Tower>();

        public Tower Search(string TowerName)
        {
            if (AdjacentList.ContainsKey(TowerName))
            {
                return AdjacentList[TowerName];
            }
            return null;
        }

        public void InsertIntoList(Tower T)
        {
            AdjacentList.Add(T.TowerName, T);
        }

        public Tower CreateTower(SpriteAnimation spriteAnimation,
            SpriteAnimation TurretAnim,
            Texture2D ButtonImage,
            string Name,
            ElementalTypes Type,
           Vector2 location,
           float speed,
           int collisionRadius,
            float TowerRange,
            float DelayBetweenShots,
            int Damage,
            int Aoe,
            params Edge[] edges)
        {

            Tower T = new Tower(spriteAnimation, TurretAnim, ButtonImage,Name, Type, location, speed, collisionRadius, TowerRange,
                DelayBetweenShots, Damage,Aoe, edges);
            AdjacentList.Add(Name, T);

            return T;
        }

        public Edge CreateEdge(Tower Source, Tower Destination, int GoldToUpgrade, params ElementalTypes[] ElementalRequirements)
        {
            Edge e = new Edge(Source, Destination, GoldToUpgrade, ElementalRequirements);
            return e;
        }

      

        public void CreateTwoTowers(string v1, string v2, int GoldToUPgrade, TowerDataHolder T1 = null, TowerDataHolder T2=null, params ElementalTypes[] ElementsRequiredToBuild)
        {
            Tower t1;
            Tower t2;

            if ((!AdjacentList.ContainsKey(v1)) && (!AdjacentList.ContainsKey(v2)))
            {
                t1 = CreateTower(T1.spriteAnimation, T1.TurretAnim, T1.ButtonImage, v1,T1.Type ,new Vector2(0, 0), 0, 0, T1.TowerRange, T1.DelayBetweenShots, T1.Damage , T1.Aoe);
                t2 = CreateTower(T2.spriteAnimation, T2.TurretAnim, T2.ButtonImage, v2, T2.Type, new Vector2(0, 0), 0, 0, T2.TowerRange, T2.DelayBetweenShots, T2.Damage, T2.Aoe);
                Edge e = CreateEdge(t1, t2, GoldToUPgrade, ElementsRequiredToBuild);
                t1.InsertEdge(e);
                t2.InsertEdge(e);
            }
            else if (AdjacentList.ContainsKey(v1))
            {
                t1 = AdjacentList[v1];
                t2 = CreateTower(T2.spriteAnimation, T2.TurretAnim, T2.ButtonImage, v2, T2.Type, new Vector2(0, 0), 0, 0, T2.TowerRange, T2.DelayBetweenShots, T2.Damage, T2.Aoe);
                Edge e = CreateEdge(t1, t2, GoldToUPgrade, ElementsRequiredToBuild);
                t1.InsertEdge(e);
                t2.InsertEdge(e);
            }
            else
            {
                t1 = CreateTower(T1.spriteAnimation, T1.TurretAnim, T1.ButtonImage, v1, T1.Type, new Vector2(0, 0), 0, 0, T1.TowerRange, T1.DelayBetweenShots, T1.Damage,T1.Aoe);
                t2 = AdjacentList[v2];
                Edge e = CreateEdge(t1, t2, GoldToUPgrade,ElementsRequiredToBuild);
                t1.InsertEdge(e);
                t2.InsertEdge(e);
            }
        }

        public void FigureOutElements()
        {

            //Frogot What This Method was for. MB Travese Tree to look for Elemental Charts. No need now if that was the reason.
            Tower BaseT = this.Search("Base Tower");
            List<Tower> TowersFromBase = new List<Tower>();

            foreach (Edge e in BaseT.TowersThisCanBeUpgradedInto)
            {
                TowersFromBase.Add(e.Destination);
            }
        }

        public void FinsishedAdding()
        {
            foreach(KeyValuePair<string, Tower> K in AdjacentList)
            {
                K.Value.CopyElementsInOtherEdges();
            }
        }

    }
}
