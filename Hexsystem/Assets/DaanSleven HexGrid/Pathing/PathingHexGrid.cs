using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class PathingHexGrid<HexTemplateType> : HexGrid<HexTemplateType> where HexTemplateType : PathingHex
    {

        //Functions
        public Path FindPath(HexVector pStart, HexVector pEnd)
        {
            return FindPath(pStart, pEnd, int.MaxValue);
        }

        public Path FindPath(HexVector pStart, HexVector pEnd, int pMaxHeightDifference)
        {
            //Check if start and end exist
            if (Exists(pStart) && Exists(pEnd))
            {
                //Variables
                List<HexVector> arrClosed = new List<HexVector>(); //List of all positions that are already checked
                List<PathingNode> arrOpen = new List<PathingNode>(); //List of all positions that are not checked but are neightbours of checked hexes

                arrOpen.Add(new PathingNode(pStart, HexVector.Distance(pStart, pEnd)));

                //Loop until path found
                while (arrOpen.Count > 0)
                {
                    //Take lowest cost node
                    PathingNode CurrentNode = arrOpen[0];
                    for (int intN = 1; intN < arrOpen.Count; intN++)
                    {
                        if ((arrOpen[intN].Cost < CurrentNode.Cost) || (arrOpen[intN].Cost == CurrentNode.Cost && arrOpen[intN].CostFromEnd < CurrentNode.CostFromEnd))
                        {
                            CurrentNode = arrOpen[intN];
                        }
                    }
                    arrOpen.Remove(CurrentNode);
                    arrClosed.Add(CurrentNode.Position);

                    //Test if path is found
                    if (CurrentNode.Position.Equals(pEnd))
                    {
                        return new Path(CurrentNode);
                    }

                    //Get neighbours
                    List<HexTemplateType> arrNeighbours = base.GetNeighbours(CurrentNode.Position);
                    foreach (HexTemplateType thisHex in arrNeighbours)
                    {
                        if (thisHex.IsObstacle || arrClosed.Contains(thisHex.Position) || Mathf.Abs(Get(CurrentNode.Position).Height - thisHex.Height) > pMaxHeightDifference)
                        {
                            continue;
                        }
                        else
                        {
                            //Search if node already exists
                            PathingNode neighbourNode = null;
                            neighbourNode = arrOpen.Find(h => (h.Position.Equals(thisHex.Position)));

                            if (neighbourNode == null)
                            {
                                //Make a node for the new hex
                                float fltDistanceToEnd = HexVector.Distance(thisHex.Position, pEnd);
                                neighbourNode = new PathingNode(thisHex.Position, fltDistanceToEnd, CurrentNode);
                                arrOpen.Add(neighbourNode);
                            }
                            else
                            {
                                //update the found hex's origin
                                neighbourNode.OriginNode = CurrentNode;
                            }
                        }
                    }
                }

                return null;
            }
            return null;
        }
    }
}
