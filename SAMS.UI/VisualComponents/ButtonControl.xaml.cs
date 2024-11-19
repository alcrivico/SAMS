using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for ButtonControl.xaml
    /// </summary>
    public partial class ButtonControl : UserControl
    {

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(ButtonControl),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsButtonEnabledProperty =
            DependencyProperty.Register(
                "IsButtonEnabled",
                typeof(bool),
                typeof(ButtonControl),
                new PropertyMetadata(true));

        public bool IsButtonEnabled
        {
            get { return (bool)GetValue(IsButtonEnabledProperty); }
            set { SetValue(IsButtonEnabledProperty, value); }
        }

        public int ButtonWidth
        {
            get { return (int)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register(
                               "ButtonWidth",
                               typeof(int),
                               typeof(ButtonControl),
                               new PropertyMetadata(150));

        public int ButtonHeight
        {
            get { return (int)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register(
                               "ButtonHeight",
                               typeof(int),
                               typeof(ButtonControl),
                               new PropertyMetadata(55));

        public Brush ButtonBackgroundColor
        {
            get { return (Brush)GetValue(ButtonBackgroundColorProperty); }
            set { SetValue(ButtonBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonBackgroundColorProperty =
            DependencyProperty.Register(
                "ButtonBackgroundColor",
                typeof(Brush),
                typeof(ButtonControl),
                new PropertyMetadata(null));

        public Brush ButtonForegroundColor
        {
            get { return (Brush)GetValue(ButtonForegroundColorProperty); }
            set { SetValue(ButtonForegroundColorProperty, value); }
        }

        private static Brush _defaultForegroundColor = Application.Current.FindResource("SolidColorBrush_White") as Brush;

        public static readonly DependencyProperty ButtonForegroundColorProperty =
            DependencyProperty.Register(
                "ButtonForegroundColor",
                typeof(Brush),
                typeof(ButtonControl),
                new PropertyMetadata(_defaultForegroundColor));

        public Brush ButtonBorderColor
        {
            get { return (Brush)GetValue(ButtonBorderColorProperty); }
            set { SetValue(ButtonBorderColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonBorderColorProperty =
            DependencyProperty.Register(
                "ButtonBorderColor",
                typeof(Brush),
                typeof(ButtonControl),
                new PropertyMetadata(null));

        public bool IsDropShadowEnabled
        {
            get { return (bool)GetValue(IsDropShadowEnabledProperty); }
            set { SetValue(IsDropShadowEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsDropShadowEnabledProperty =
            DependencyProperty.Register(
                "IsDropShadowEnabled",
                typeof(bool),
                typeof(ButtonControl),
                new PropertyMetadata(true));

        public ButtonControl()
        {
            InitializeComponent();
        }

        public static RoutedEvent ButtonControlClickEvent =
            EventManager.RegisterRoutedEvent(
                       nameof(ButtonControlClick),
                       RoutingStrategy.Bubble,
                       typeof(RoutedEventHandler),
                       typeof(ButtonControl));

        public event RoutedEventHandler ButtonControlClick
        {
            add { AddHandler(ButtonControlClickEvent, value); }
            remove { RemoveHandler(ButtonControlClickEvent, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (IsDropShadowEnabled)
            {
                Button_Border.Effect = FindResource("ButtonDropShadow") as DropShadowEffect;
            }

            RaiseEvent(new RoutedEventArgs(ButtonControlClickEvent));

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

            Button_Highlight.Opacity = 0.1;

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {

            Button_Highlight.Opacity = 0;

            if (IsDropShadowEnabled)
            {
                Button_Border.Effect = FindResource("ButtonDropShadow") as DropShadowEffect;
            }

        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Button_Border.Effect = null;

        }
    }
}
