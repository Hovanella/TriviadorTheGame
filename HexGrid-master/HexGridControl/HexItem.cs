using System.Windows;
using System.Windows.Controls;

namespace HexGridControl
{
    /// <summary>
    ///     Hexagonal content control
    /// </summary>
    public class HexItem : ListBoxItem
    {
        public static readonly DependencyProperty OrientationProperty =
            HexGrid.OrientationProperty.AddOwner(typeof(HexItem));

        static HexItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexItem), new FrameworkPropertyMetadata(typeof(HexItem)));
        }

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
    }
}