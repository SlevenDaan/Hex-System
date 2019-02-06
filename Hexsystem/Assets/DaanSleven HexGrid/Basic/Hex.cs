using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaanSleven.HexGrid
{
    public class Hex
    {

        private HexVector position;

        //Constructor
        public Hex(int pX, int pY) : this(new HexVector(pX, pY))
        {

        }
        public Hex(HexVector pPosition)
        {
            position = pPosition;
        }

        //Properties
        public HexVector Position
        {
            get
            {
                return position;
            }
        }

        //Functions
        public override bool Equals(object obj)
        {
            Hex other = (Hex)obj;
            return other.Position.Equals(Position);
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("[Hex: Position={0}]", Position);
        }
    }
}
