using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            Button_Border.Effect = FindResource("ButtonDropShadow") as DropShadowEffect;

            RaiseEvent(new RoutedEventArgs(ButtonControlClickEvent));

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

            Button_Highlight.Opacity = 0.1;

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {

            Button_Highlight.Opacity = 0;
            Button_Border.Effect = FindResource("ButtonDropShadow") as DropShadowEffect;

        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Button_Border.Effect = null;

        }
    }
}
