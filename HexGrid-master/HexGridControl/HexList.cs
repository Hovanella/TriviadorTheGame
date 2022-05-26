using System.Windows;
using System.Windows.Controls;

namespace HexGridControl
{
    public class HexList : ListBox
    {
        public static readonly DependencyProperty OrientationProperty =
            HexGrid.OrientationProperty.AddOwner(typeof(HexList));

        public static readonly DependencyProperty RowCountProperty = HexGrid.RowCountProperty.AddOwner(typeof(HexList));

        public static readonly DependencyProperty ColumnCountProperty =
            HexGrid.ColumnCountProperty.AddOwner(typeof(HexList));

        static HexList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexList), new FrameworkPropertyMetadata(typeof(HexList)));
        }

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

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

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is HexItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new HexItem();
        }
    }
}