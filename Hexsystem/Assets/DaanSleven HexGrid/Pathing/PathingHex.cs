using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class PathingHex : Hex
    {

        //Variables
        private bool blnObstacle = false;
        private int intHeight = 0;

        //Constructor
        public PathingHex(HexVector pPosition) : this(pPosition, false, 0)
        {

        }
        public PathingHex(HexVector pPosition, bool pObstacle) : this(pPosition, pObstacle, 0)
        {

        }
        public PathingHex(HexVector pPosition, bool pObstacle, int pHeight) : base(pPosition)
        {
            blnObstacle = pObstacle;
            intHeight = pHeight;
        }

        //Properties
        public bool IsObstacle
        {
            get
            {
                return blnObstacle;
            }
            set
            {
                blnObstacle = value;
            }
        }

        public int Height
        {
            get
            {
                return intHeight;
            }
            set
            {
                intHeight = value;
            }
        }
    }
}
