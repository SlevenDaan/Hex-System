using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class PathingNode
    {

        //Variables
        private readonly HexVector CurrentPosition;
        private PathingNode Origin = null;

        public float fltDistanceFromEnd;

        //Constructors
        public PathingNode(HexVector pPosition, float pDistanceFromEnd)
        {
            CurrentPosition = pPosition;
            fltDistanceFromEnd = pDistanceFromEnd;
        }
        public PathingNode(HexVector pPosition, float pDistanceFromEnd, PathingNode pOrigin) : this(pPosition, pDistanceFromEnd)
        {
            Origin = pOrigin;
        }

        //Properties
        public PathingNode OriginNode
        {
            get
            {
                return Origin;
            }
            set
            {
                //Only sets the origin if it's a faster way
                if (value.CostFromStart < CostFromStart)
                {
                    Origin = value;
                }
            }
        }
        public HexVector Position
        {
            get
            {
                return CurrentPosition;
            }
        }

        public float CostFromStart
        {
            get
            {
                if (Origin != null)
                {
                    return Origin.CostFromStart + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public float CostFromEnd
        {
            get
            {
                return fltDistanceFromEnd;
            }
        }

        public float Cost
        {
            get
            {
                return CostFromEnd + CostFromStart;
            }
        }
    }
}
