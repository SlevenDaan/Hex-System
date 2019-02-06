using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class Path
    {

        //Variables
        private List<HexVector> arrPositions = new List<HexVector>();

        //Constructor
        public Path(PathingNode pEndNode)
        {
            while (pEndNode != null)
            {
                arrPositions.Insert(0, pEndNode.Position);
                pEndNode = pEndNode.OriginNode;
            }
        }

        //Properties
        public int Length
        {
            get
            {
                return arrPositions.Count;
            }
        }

        //Functions
        public HexVector Get(int pPathStep)
        {
            if (pPathStep < Length)
            {
                return arrPositions[pPathStep];
            }
            else
            {
                return null;
            }
        }

    }
}
