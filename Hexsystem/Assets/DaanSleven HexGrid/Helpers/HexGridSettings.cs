using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public static class HexGridSettings
    {

        //Variables
        private static float fltHexSideLength = 1;

        //Properties
        public static float HexSideLength
        {
            get
            {
                return fltHexSideLength;
            }
            set
            {
                fltHexSideLength = value;
            }
        }
    }
}
