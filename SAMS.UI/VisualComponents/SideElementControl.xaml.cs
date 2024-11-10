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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for SideElementControl.xaml
    /// </summary>
    
    public partial class SideElementControl : UserControl
    {
        public string MenuName
        {
            get { return (string)GetValue(MenuNameProperty); }
            set { SetValue(MenuNameProperty, value); }
        }

        public static readonly DependencyProperty MenuNameProperty =
            DependencyProperty.Register(
                               nameof(MenuName),
                               typeof(string),
                               typeof(SideElementControl),
                               new PropertyMetadata(string.Empty));

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register(
                nameof(IconSource),
                typeof(ImageSource),
                typeof(SideElementControl),
                new PropertyMetadata(null));

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register(
                nameof(BackgroundColor),
                typeof(Brush),
                typeof(SideElementControl),
                new PropertyMetadata(null));

        public Brush ForegroundColor
        {
            get { return (Brush)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(
                nameof(ForegroundColor),
                typeof(Brush),
                typeof(SideElementControl),
                new PropertyMetadata(null));

        public SideElementControl()
        {
            InitializeComponent();
        }

    }

}
