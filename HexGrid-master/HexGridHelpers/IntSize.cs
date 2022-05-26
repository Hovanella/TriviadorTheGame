using System;

namespace HexGridHelpers
{
    public struct IntSize : IEquatable<IntSize>
    {
        public IntSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }

        public int Height { get; }

        public static bool operator ==(IntSize a, IntSize b)
        {
            return a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(IntSize a, IntSize b)
        {
            return !(a == b);
        }

        public bool Equals(IntSize other)
        {
            return this == other;
        }
    }
}