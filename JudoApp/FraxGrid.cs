using System.Windows;
using System.Windows.Controls;

namespace JudoApp;

public class FraxGrid : Grid
{
    public static readonly RoutedEvent ShowStoryboardEvent = EventManager.RegisterRoutedEvent("OnShowStoryboard", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(FraxGrid));
    public static readonly RoutedEvent HideStoryboardEvent = EventManager.RegisterRoutedEvent("OnHideStoryboard", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(FraxGrid));

    public event RoutedEventHandler OnStartStoryboard
    {
        add
        {
            this.AddHandler(ShowStoryboardEvent, value);
        }

        remove
        {
            this.RemoveHandler(ShowStoryboardEvent, value);
        }
    }
    public event RoutedEventHandler OnHideStoryboardEvent
    {
        add
        {
            this.AddHandler(HideStoryboardEvent, value);
        }

        remove
        {
            this.RemoveHandler(HideStoryboardEvent, value);
        }
    }
}