using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PresentationWPF.Views;

public partial class FilterView : UserControl
{
    public FilterView()
    {
        InitializeComponent();
    }

    private void ButtonFlipExpander_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button { Content: Grid grid }) return;

        var icons = grid.Children.OfType<Image>().FirstOrDefault();
        if (icons == null) return;

        double offset = 0;

        if (icons.RenderTransform is RotateTransform rt)
        {
            offset = rt.Angle;
        }
        
        icons.RenderTransformOrigin = new Point(0.5, 0.5);
        icons.RenderTransform = new RotateTransform(180 + offset);
    }
}