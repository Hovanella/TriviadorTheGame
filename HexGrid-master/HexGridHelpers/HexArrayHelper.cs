using System;
using System.Collections.Generic;
using System.Linq;

namespace HexGridHelpers
{
    public class HexArrayHelper
    {
        private readonly HexDirection[] _directions =
            Enum.GetValues(typeof(HexDirection)).OfType<HexDirection>().ToArray();

        public bool IsHorizontal { get; set; }

        public bool IsVertical => !IsHorizontal;

        /// <summary>
        ///     Returns adjacent hex
        /// </summary>
        /// <param name="size">Board dimensions</param>
        /// <param name="origin">Current hex coordinates</param>
        /// <param name="dir">Direction</param>
        public IntPoint? GetNextHex(IntSize size, IntPoint origin, HexDirection dir)
        {
            if (IsHorizontal)
                return GetNextHexHorizontal(size, origin, dir);

            return GetNextHexVertical(size, origin, dir);
        }

        /// <summary>
        ///     Returns all adjacent hexes
        /// </summary>
        /// <param name="size">Board dimensions</param>
        /// <param name="origin">Current hex coordinates</param>
        public IEnumerable<IntPoint> GetNeighbours(IntSize size, IntPoint origin)
        {
            for (var index = 0; index < _directions.Length; index++)
            {
                var dir = _directions[index];
                var point = GetNextHex(size, origin, dir);
                if (point.HasValue)
                    yield return point.Value;
            }
        }


        /// <summary>
        ///     Returns all hexes around current hex which meet provided criteria
        /// </summary>
        /// <param name="size">Board dimensions</param>
        /// <param name="origin">Current hex coordinates</param>
        /// <param name="predicate">Search criteria</param>
        /// <returns></returns>
        public IEnumerable<IntPoint> GetArea(IntSize size, IntPoint origin, Func<IntPoint, bool> predicate)
        {
            if (false == predicate(origin))
                yield break;
            var idx = 0;

            var points = new List<IntPoint>();
            points.Add(origin);
            do
            {
                var p = points[idx];
                yield return p;
                foreach (var point in GetNeighbours(size, p).Where(predicate))
                    if (points.IndexOf(point) < 0)
                        points.Add(point);
                idx++;
            } while (idx < points.Count);
        }

        /// <summary>
        ///     Returns all in the requested direction from current hex to board border
        /// </summary>
        /// <param name="size">Board dimensions</param>
        /// <param name="origin">Current hex coordinates</param>
        /// <param name="dir">Direction</param>
        public IEnumerable<IntPoint> GetRay(IntSize size, IntPoint origin, HexDirection dir)
        {
            IntPoint? next;
            do
            {
                next = GetNextHex(size, origin, dir);
                if (next != null)
                {
                    yield return next.Value;
                    origin = next.Value;
                }
            } while (next != null);
        }

        public bool IsNeigbour(IntPoint neighbour, IntSize size, IntPoint origin)
        {
            var nb = GetNeighbours(size, origin);
            return nb.Any(p => p == neighbour);
        }

        private IntPoint? GetNextHexHorizontal(IntSize size, IntPoint origin, HexDirection dir)
        {
            if (dir == HexDirection.Left || dir == HexDirection.Right)
                return null;

            var rows = size.Height;
            var columns = size.Width;

            var row = origin.X;
            var column = origin.Y;

            if (row == 0 && dir == HexDirection.Up)
                return null;

            if (row == rows - 1 && dir == HexDirection.Down)
                return null;

            if (column == 0 && (dir == HexDirection.UpLeft || dir == HexDirection.DownLeft))
                return null;

            if (column == columns - 1 && (dir == HexDirection.UpRight || dir == HexDirection.DownRight))
                return null;

            var evenColumn = column % 2 == 0;

            if (evenColumn && row == 0 && (dir == HexDirection.UpLeft || dir == HexDirection.UpRight))
                return null;
            if (evenColumn == false && row == rows - 1 &&
                (dir == HexDirection.DownLeft || dir == HexDirection.DownRight))
                return null;

            switch (dir)
            {
                case HexDirection.Up:
                    row--;
                    break;
                case HexDirection.Down:
                    row++;
                    break;
                case HexDirection.UpRight:
                    if (evenColumn)
                        row--;
                    column++;
                    break;
                case HexDirection.DownRight:
                    if (evenColumn == false)
                        row++;
                    column++;
                    break;
                case HexDirection.UpLeft:
                    if (evenColumn)
                        row--;
                    column--;
                    break;
                case HexDirection.DownLeft:
                    if (evenColumn == false)
                        row++;
                    column--;
                    break;
            }

            if (row >= 0 && row < rows && column >= 0 && column <= columns)
                return new IntPoint(row, column);
            return null;
        }

        private IntPoint? GetNextHexVertical(IntSize size, IntPoint origin, HexDirection dir)
        {
            if (dir == HexDirection.Up || dir == HexDirection.Down)
                return null;

            var rows = size.Height;
            var columns = size.Width;

            var row = origin.X;
            var column = origin.Y;

            if (row == 0 && (dir == HexDirection.UpRight || dir == HexDirection.UpLeft))
                return null;

            if (row == rows - 1 && (dir == HexDirection.DownRight || dir == HexDirection.DownLeft))
                return null;

            if (column == 0 && dir == HexDirection.Left)
                return null;

            if (column == columns - 1 && dir == HexDirection.Right)
                return null;

            var evenRow = row % 2 == 0;

            if (evenRow && column == 0 && (dir == HexDirection.UpLeft || dir == HexDirection.DownLeft))
                return null;
            if (evenRow == false && column == columns - 1 &&
                (dir == HexDirection.UpRight || dir == HexDirection.DownRight))
                return null;

            switch (dir)
            {
                case HexDirection.Right:
                    column++;
                    break;
                case HexDirection.Left:
                    column--;
                    break;
                case HexDirection.UpRight:
                    if (evenRow == false) column++;
                    row--;
                    break;
                case HexDirection.DownRight:
                    if (evenRow == false) column++;
                    row++;
                    break;
                case HexDirection.UpLeft:
                    if (evenRow) column--;
                    row--;
                    break;
                case HexDirection.DownLeft:
                    if (evenRow) column--;
                    row++;
                    break;
            }

            if (row >= 0 && row < rows && column >= 0 && column <= columns)
                return new IntPoint(row, column);
            return null;
        }
    }
}