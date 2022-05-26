using System;

namespace HexGridHelpers
{
    public struct IntPoint : IEquatable<IntPoint>
    {
        public IntPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static bool operator ==(IntPoint a, IntPoint b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(IntPoint a, IntPoint b)
        {
            return !(a == b);
        }

        public bool Equals(IntPoint other)
        {
            return this == other;
        }
    }
}