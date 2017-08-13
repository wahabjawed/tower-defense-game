using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace EleTD
{
    public class AstarMovement
    {
        private Vector2 endLocation;
        private  TileMap tileMap;

        public AstarMovement(TileMap tilemap, Vector2 endLocation)
        {
            tileMap = tilemap;
            this.endLocation = endLocation;
        }

        private enum NodeStatus { 
            Open,
            Closed };

        private  Dictionary<Vector2, NodeStatus> nodeStatus =
            new Dictionary<Vector2, NodeStatus>();

        private const int CostStraight = 10;
        private const int CostDiagonal = 15;

        private  List<TileNode> openList = new List<TileNode>();

        private  Dictionary<Vector2, float> nodeCosts =
            new Dictionary<Vector2, float>();



         private void addNodeToOpenList(TileNode node)
        {
            int index = 0;
            float cost = node.TotalCost;

            while ((openList.Count() > index) &&
                (cost < openList[index].TotalCost))
            {
                index++;
            }

            openList.Insert(index, node);
            nodeCosts[node.TileLocation] = node.TotalCost;
            nodeStatus[node.TileLocation] = NodeStatus.Open;
        }

         private List<TileNode> findAdjacentNodes(
    TileNode currentNode,
    TileNode endNode)
        {
            List<TileNode> adjacentNodes = new List<TileNode>();

            int X = currentNode.TileX;
            int Y = currentNode.TileY;

            bool upLeft = true;
            bool upRight = true;
            bool downLeft = true;
            bool downRight = true;

            if ((X < 49) && (!tileMap.IsWallTile(X + 1, Y)))
            {
                adjacentNodes.Add(new TileNode(
                        currentNode,
                        endNode,
                        new Vector2(X + 1, Y),
                        CostStraight + currentNode.DirectCost));
            }
            else
            {
                upRight = false;
                downRight = false;
            }

            if ((X > 0) && (!tileMap.IsWallTile(X - 1, Y)))
            {
                adjacentNodes.Add(new TileNode(
                        currentNode,
                        endNode,
                        new Vector2(X - 1, Y),
                        CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                downLeft = false;
            }



            if ((Y > 0) && (!tileMap.IsWallTile(X, Y - 1)))
            {
                adjacentNodes.Add(new TileNode(
                    currentNode,
                    endNode,
                    new Vector2(X, Y - 1),
                    CostStraight + currentNode.DirectCost));
            }
            else
            {
                upLeft = false;
                upRight = false;
            }

            if ((Y < 49) && (!tileMap.IsWallTile(X, Y + 1)))
            {
                adjacentNodes.Add(new TileNode(
                    currentNode,
                    endNode,
                    new Vector2(X, Y + 1),
                    CostStraight + currentNode.DirectCost));
            }
            else
            {
                downLeft = false;
                downRight = false;
            }


            if ((upLeft) && (!tileMap.IsWallTile(X - 1, Y - 1)))
            {
                adjacentNodes.Add(new TileNode(
                    currentNode,
                    endNode,
                    new Vector2(X - 1, Y - 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            if ((upRight) && (!tileMap.IsWallTile(X + 1, Y - 1)))
            {
                adjacentNodes.Add(new TileNode(
                    currentNode,
                    endNode,
                    new Vector2(X + 1, Y - 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            if ((downLeft) && (!tileMap.IsWallTile(X - 1, Y + 1)))
            {
                adjacentNodes.Add(new TileNode(
                    currentNode,
                    endNode,
                    new Vector2(X - 1, Y + 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            if ((downRight) && (!tileMap.IsWallTile(X + 1, Y + 1)))
            {
                adjacentNodes.Add(new TileNode(
                    currentNode,
                    endNode,
                    new Vector2(X + 1, Y + 1),
                    CostDiagonal + currentNode.DirectCost));
            }

            return adjacentNodes;
        }


         public List<Vector2> FindPath(
            Vector2 startTile,
            Vector2 endTile)
        {
            if (tileMap.IsWallTile(endTile) ||
                tileMap.IsWallTile(startTile))
            {
                return null;
            }

            openList.Clear();
            nodeCosts.Clear();
            nodeStatus.Clear();

            TileNode startNode;
            TileNode endNode;

            endNode = new TileNode(null, null, endTile, 0);
            startNode = new TileNode(null, endNode, startTile, 0);

            addNodeToOpenList(startNode);

            while (openList.Count > 0)
            {
                TileNode currentNode = openList[openList.Count - 1];

                if (currentNode.IsEqualToNode(endNode))
                {
                    List<Vector2> bestPath = new List<Vector2>();
                    while (currentNode != null)
                    {
                        bestPath.Insert(0, currentNode.TileLocation);
                        currentNode = currentNode.ParentNode;
                    }
                    return bestPath;
                }

                openList.Remove(currentNode);
                nodeCosts.Remove(currentNode.TileLocation);

                foreach (
                    TileNode possibleNode in
                    findAdjacentNodes(currentNode, endNode))
                {
                    if (nodeStatus.ContainsKey(possibleNode.TileLocation))
                    {
                        if (nodeStatus[possibleNode.TileLocation] ==
                            NodeStatus.Closed)
                        {
                            continue;
                        }

                        if (
                            nodeStatus[possibleNode.TileLocation] ==
                            NodeStatus.Open)
                        {
                            if (possibleNode.TotalCost >=
                                nodeCosts[possibleNode.TileLocation])
                            {
                                continue;
                            }
                        }
                    }

                    addNodeToOpenList(possibleNode);
                }

                nodeStatus[currentNode.TileLocation] = NodeStatus.Closed;
            }

            return null;
        }



    }


    class TileNode
    {

        public TileNode ParentNode;
        public TileNode EndNode;
        private Vector2 tileLocation;
        public float TotalCost;
        public float DirectCost;


        public Vector2 TileLocation
        {
            get { return tileLocation; }
            set
            {
                tileLocation = value;
            }
        }

        public int TileX
        {
            get { return (int)tileLocation.X; }
        }

        public int TileY
        {
            get { return (int)tileLocation.Y; }
        }

        
         public TileNode(
            TileNode parentNode,
            TileNode endNode,
            Vector2 location,
            float cost)
        {
            ParentNode = parentNode;
            TileLocation = location;
            EndNode = endNode;
            DirectCost = cost;
            if (!(endNode == null))
            {
                TotalCost = DirectCost + LinearCost();
            }
        }


         public float LinearCost()
         {
             return (
                 Vector2.Distance(
                 EndNode.TileLocation,
                 this.TileLocation));
         }


         public bool IsEqualToNode(TileNode node)
         {
             return (TileLocation == node.TileLocation);
         }

    }
}
