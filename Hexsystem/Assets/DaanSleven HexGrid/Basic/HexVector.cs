using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class HexVector
    {

        //Variables
        int intX = 0;
        int intY = 0;

        //Constructors
        public HexVector() : this(0, 0)
        {

        }
        public HexVector(int pX, int pY)
        {
            intX = pX;
            intY = pY;
        }

        //Properties
        public int X
        {
            get
            {
                return intX;
            }
        }
        public int Y
        {
            get
            {
                return intY;
            }
        }
        public int Z
        {
            get
            {
                return -intX - intY; ;
            }
        }

        //Functions
        public HexVector Add(HexVector pAdd)
        {
            return new HexVector(X + pAdd.X, Y + pAdd.Y);
        }
        public HexVector Multiply(int intAmount)
        {
            return new HexVector(X * intAmount, Y * intAmount);
        }

        public Vector3 ToWorldPosition()
        {
            Vector2 pTemp = ToWorldPosition2D();
            return new Vector3(pTemp.x, 0, pTemp.y);
        }
        public Vector2 ToWorldPosition2D()
        {
            return new Vector2(Mathf.Sqrt(3f) * HexGridSettings.HexSideLength * intX + Mathf.Sqrt(3f) / 2f * HexGridSettings.HexSideLength * intY, 3f / 2f * HexGridSettings.HexSideLength * intY);
        }

        public override bool Equals(object obj)
        {
            HexVector other = (HexVector)obj;
            return other.X == X && other.Y == Y && other.Z == Z;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("[HexVector: X={0}, Y={1}]", X, Y);
        }

        //Static Properties
        public static HexVector TopRight
        {
            get
            {
                return new HexVector(0, 1);
            }
        }
        public static HexVector TopLeft
        {
            get
            {
                return new HexVector(-1, 1);
            }
        }
        public static HexVector Right
        {
            get
            {
                return new HexVector(1, 0);
            }
        }
        public static HexVector Left
        {
            get
            {
                return new HexVector(-1, 0);
            }
        }
        public static HexVector BottomRight
        {
            get
            {
                return new HexVector(1, -1);
            }
        }
        public static HexVector BottomLeft
        {
            get
            {
                return new HexVector(0, -1);
            }
        }

        //Static Functions
        public static int Distance(HexVector pHex1, HexVector pHex2)
        {
            int intDiffX = Mathf.Abs(pHex1.X - pHex2.X);
            int intDiffY = Mathf.Abs(pHex1.Y - pHex2.Y);
            int intDiffZ = Mathf.Abs(pHex1.Z - pHex2.Z);

            return Mathf.Max(intDiffX, intDiffY, intDiffZ);
        }
    }
}
