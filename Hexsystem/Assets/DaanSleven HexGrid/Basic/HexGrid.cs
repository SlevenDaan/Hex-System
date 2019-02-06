using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class HexGrid : HexGrid<Hex>
    {

    }

    public class HexGrid<HexTemplateType> where HexTemplateType : Hex
    {

        private Dictionary<HexVector, HexTemplateType> arrHexes = new Dictionary<HexVector, HexTemplateType>(new HexComparator());

        //Functions
        public HexTemplateType Get(HexVector pPosition)
        {
            //Returns null if no hex exists with that position
            if (Exists(pPosition))
            {
                return arrHexes[pPosition];
            }
            else
            {
                return null;
            }
        }
        public HexTemplateType Add(HexTemplateType pHex)
        {
            //Returns the old hex if the new hex has the same position
            if (Exists(pHex.Position))
            {
                HexTemplateType oldHex = arrHexes[pHex.Position];
                arrHexes[pHex.Position] = pHex;
                return oldHex;
            }
            else
            {
                arrHexes.Add(pHex.Position, pHex);
                return null;
            }
        }
        public bool Remove(HexVector pPosition)
        {
            //Returns false if no hex exist with that position
            if (Exists(pPosition))
            {
                arrHexes.Remove(pPosition);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Exists(HexVector pPosition)
        {
            if (arrHexes.ContainsKey(pPosition))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            arrHexes.Clear();
        }

        public List<HexVector> GetExistingPositions()
        {
            List<HexVector> arrPositions = new List<HexVector>();
            foreach (HexVector key in arrHexes.Keys)
            {
                arrPositions.Add(key);
            }
            return arrPositions;
        }

        public List<HexTemplateType> GetNeighbours(HexTemplateType pCenterHex)
        {
            return GetNeighbours(pCenterHex.Position);
        }
        public List<HexTemplateType> GetNeighbours(HexVector pCenter)
        {
            List<HexTemplateType> arrNeighbours = new List<HexTemplateType>();

            HexVector[] arrDirections = new HexVector[] {
            HexVector.TopRight,
            HexVector.TopLeft,
            HexVector.Right,
            HexVector.Left,
            HexVector.BottomRight,
            HexVector.BottomLeft
        };

            foreach (HexVector thisDirection in arrDirections)
            {
                HexTemplateType pHex = Get(pCenter.Add(thisDirection));
                if (pHex != null)
                {
                    arrNeighbours.Add(pHex);
                }
            }

            return arrNeighbours;
        }

        public List<HexTemplateType> InRange(HexVector pCenter, int pMaxRange)
        {
            List<HexTemplateType> arrInRange = new List<HexTemplateType>();

            for (int intX = -pMaxRange; intX <= pMaxRange; intX++)
            {
                for (int intY = Mathf.Max(-pMaxRange, -intX - pMaxRange); intY <= Mathf.Min(pMaxRange, -intX + pMaxRange); intY++)
                {
                    HexTemplateType pHex = Get(pCenter.Add(new HexVector(intX, intY)));
                    if (pHex != null)
                    {
                        arrInRange.Add(pHex);
                    }
                }
            }

            return arrInRange;
        }
    }
}