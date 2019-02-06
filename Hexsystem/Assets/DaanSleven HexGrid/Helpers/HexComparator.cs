using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class HexComparator : IEqualityComparer<HexVector>
    {

        public bool Equals(HexVector pHex1, HexVector pHex2)
        {
            return pHex1.Equals(pHex2) && pHex2.Equals(pHex1);
        }

        public int GetHashCode(HexVector pHex)
        {
            return pHex.ToString().GetHashCode();
        }
    }
}
