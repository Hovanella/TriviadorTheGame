using System;
using System.Windows;
using System.Windows.Controls;

namespace HexGridControl
{
    /// <summary>
    ///     Hexagonal panel
    /// </summary>
    public class HexGrid : Panel
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(HexGrid),
                new FrameworkPropertyMetadata(Orientation.Horizontal,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(HexGrid),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange),
                ValidateCountCallback);

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(HexGrid),
                new FrameworkPropertyMetadata(1,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange),
                ValidateCountCallback);


        public int RowCount
        {
            get => (int)GetValue(RowCountProperty);
            set => SetValue(RowCountProperty, value);
        }

        public int ColumnCount
        {
            get => (int)GetValue(ColumnCountProperty);
            set => SetValue(ColumnCountProperty, value);
        }

        private static bool ValidateCountCallback(object value)
        {
            if (value is int)
            {
                var count = (int)value;
                return count > 0;
            }

            return false;
        }

        private int GetRow(UIElement e)
        {
            var row = (int)e.GetValue(Grid.RowProperty);
            if (row >= RowCount)
                row = RowCount - 1;
            return row;
        }

        private int GetColumn(UIElement e)
        {
            var column = (int)e.GetValue(Grid.ColumnProperty);
            if (column >= ColumnCount)
                column = ColumnCount - 1;
            return column;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var w = availableSize.Width;
            var h = availableSize.Height;

            // if there is Infinity size dimension
            if (double.IsInfinity(w) || double.IsInfinity(h))
            {
                // determine maximum desired size
                h = 0;
                w = 0;
                foreach (UIElement e in InternalChildren)
                {
                    e.Measure(availableSize);
                    var s = e.DesiredSize;
                    if (s.Height > h)
                        h = s.Height;
                    if (s.Width > w)
                        w = s.Width;
                }

                // multiply maximum size to RowCount and ColumnCount to get total size
                if (Orientation == Orientation.Horizontal)
                    return new Size(w * (ColumnCount * 3 + 1) / 4, h * (RowCount * 2 + 1) / 2);

                return new Size(w * (ColumnCount * 2 + 1) / 2, h * (RowCount * 3 + 1) / 4);
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            // determine if there is empty space at grid borders
            bool first, last;
            HasShift(out first, out last);

            // compute final hex size
            var hexSize = GetHexSize(finalSize);

            // compute arrange line sizes
            double columnWidth, rowHeight;
            if (Orientation == Orientation.Horizontal)
            {
                rowHeight = 0.50 * hexSize.Height;
                columnWidth = 0.25 * hexSize.Width;
            }
            else
            {
                rowHeight = 0.25 * hexSize.Height;
                columnWidth = 0.50 * hexSize.Width;
            }

            // arrange elements
            var elements = InternalChildren;
            for (var i = 0; i < elements.Count; i++)
            {
                if (elements[i].Visibility == Visibility.Collapsed)
                    continue;
                ArrangeElement(elements[i], hexSize, columnWidth, rowHeight, first);
            }

            return finalSize;
        }

        private void ArrangeElement(UIElement e, Size hexSize, double columnWidth, double rowHeight, bool shift)
        {
            var row = GetRow(e);
            var column = GetColumn(e);

            double x;
            double y;

            if (Orientation == Orientation.Horizontal)
            {
                x = 3 * columnWidth * column;
                y = rowHeight * (2 * row + (column % 2 == 1 ? 1 : 0) + (shift ? -1 : 0));
            }
            else
            {
                x = columnWidth * (2 * column + (row % 2 == 1 ? 1 : 0) + (shift ? -1 : 0));
                y = 3 * rowHeight * row;
            }

            e.Arrange(new Rect(x, y, hexSize.Width, hexSize.Height));
        }

        #region Orientation

        public static void SetOrientation(DependencyObject element, Orientation value)
        {
            element.SetValue(OrientationProperty, value);
        }

        public static Orientation GetOrientation(DependencyObject element)
        {
            return (Orientation)element.GetValue(OrientationProperty);
        }

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        #endregion

        #region HasShift

        private void HasShift(out bool first, out bool last)
        {
            if (Orientation == Orientation.Horizontal)
                HasRowShift(out first, out last);
            else
                HasColumnShift(out first, out last);
        }

        private void HasRowShift(out bool firstRow, out bool lastRow)
        {
            firstRow = lastRow = true;

            var elements = InternalChildren;
            for (var i = 0; i < elements.Count && (firstRow || lastRow); i++)
            {
                var e = elements[i];
                if (e.Visibility == Visibility.Collapsed)
                    continue;

                var row = GetRow(e);
                var column = GetColumn(e);

                var mod = column % 2;

                if (row == 0 && mod == 0)
                    firstRow = false;

                if (row == RowCount - 1 && mod == 1)
                    lastRow = false;
            }
        }

        private void HasColumnShift(out bool firstColumn, out bool lastColumn)
        {
            firstColumn = lastColumn = true;

            var elements = InternalChildren;
            for (var i = 0; i < elements.Count && (firstColumn || lastColumn); i++)
            {
                var e = elements[i];
                if (e.Visibility == Visibility.Collapsed)
                    continue;

                var row = GetRow(e);
                var column = GetColumn(e);

                var mod = row % 2;

                if (column == 0 && mod == 0)
                    firstColumn = false;

                if (column == ColumnCount - 1 && mod == 1)
                    lastColumn = false;
            }
        }

        #endregion

        #region GetHexSize

        private Size GetHexSize(Size gridSize)
        {
            double minH = 0;
            double minW = 0;

            foreach (UIElement e in InternalChildren)
            {
                var f = e as FrameworkElement;
                if (f != null)
                {
                    if (f.MinHeight > minH)
                        minH = f.MinHeight;
                    if (f.MinWidth > minW)
                        minW = f.MinWidth;
                }
            }

            bool first, last;
            HasShift(out first, out last);

            var possibleSize = GetPossibleSize(gridSize);
            var possibleW = possibleSize.Width;
            var possibleH = possibleSize.Height;

            var w = Math.Max(minW, possibleW);
            var h = Math.Max(minH, possibleH);

            return new Size(w, h);
        }

        private Size GetPossibleSize(Size gridSize)
        {
            bool first, last;
            HasShift(out first, out last);

            if (Orientation == Orientation.Horizontal)
                return GetPossibleSizeHorizontal(gridSize, first, last);

            return GetPossibleSizeVertical(gridSize, first, last);
        }

        private Size GetPossibleSizeVertical(Size gridSize, bool first, bool last)
        {
            var columns = (first ? 0 : 1) + 2 * ColumnCount - (last ? 1 : 0);
            var w = 2 * (gridSize.Width / columns);

            var rows = 1 + 3 * RowCount;
            var h = 4 * (gridSize.Height / rows);

            return new Size(w, h);
        }

        private Size GetPossibleSizeHorizontal(Size gridSize, bool first, bool last)
        {
            var columns = 1 + 3 * ColumnCount;
            var w = 4 * (gridSize.Width / columns);

            var rows = (first ? 0 : 1) + 2 * RowCount - (last ? 1 : 0);
            var h = 2 * (gridSize.Height / rows);

            return new Size(w, h);
        }

        #endregion
    }
}