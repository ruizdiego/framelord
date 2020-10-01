using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

namespace FrameLord
{
    public struct IntVector2 : IEquatable<IntVector2>
    {
        public int x, y;

        public int this[int index]
        {
            set
            {
                if (index == 0) x = value;
                else if (index == 1) y = value;
            }
            get
            {
                if (index == 0) return x;
                else if (index == 1) return y;
                return 0;
            }
        }

        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //
        // Static Properties
        //
        public static IntVector2 one = new IntVector2(1, 1);

        public static IntVector2 zero = new IntVector2(0, 0);

        public static IntVector2 right = new IntVector2(1, 0);

        public static IntVector2 up = new IntVector2(0, 1);

        public int sqrMagnitude
        {
            get { return x * x + y * y; }
        }

        public int manhattanDistance
        {
            get { return Math.Abs(x) + Math.Abs(y); }
        }

        public int ManhattanDistanceTo(IntVector2 other)
        {
            return Math.Abs(x - other.x) + Math.Abs(y - other.y);
        }

        static public int ManhattanDistance(IntVector2 fromPoint, IntVector2 toPoint)
        {
            return fromPoint.ManhattanDistanceTo(toPoint);
        }

        public override bool Equals(object obj)
        {
            return obj is IntVector2 && this == (IntVector2) obj;
        }

        public override int GetHashCode()
        {
            return x | (y << 16);
        }

        public static IntVector2 operator +(IntVector2 left, IntVector2 right)
        {
            return new IntVector2(left.x + right.x, left.y + right.y);
        }

        public static IntVector2 operator -(IntVector2 left, IntVector2 right)
        {
            return new IntVector2(left.x - right.x, left.y - right.y);
        }

        public static IntVector2 operator *(IntVector2 left, int v)
        {
            return new IntVector2(left.x * v, left.y * v);
        }

        public static IntVector2 operator *(IntVector2 left, IntVector2 right)
        {
            return new IntVector2(left.x * right.x, left.y * right.y);
        }

        public static bool operator !=(IntVector2 left, IntVector2 right)
        {
            return left.x != right.x || left.y != right.y;
        }

        public static bool operator ==(IntVector2 left, IntVector2 right)
        {
            return left.x == right.x && left.y == right.y;
        }

        public bool Equals(IntVector2 other)
        {
            return other.x == x && other.y == y;
        }

        public override string ToString()
        {
            return x + "," + y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }
    }
}

