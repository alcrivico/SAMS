using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for InformationControl.xaml
    /// </summary>
    public partial class InformationControl : UserControl
    {

        Window _window;

        public string InformationHeader
        {
            get { return Information_Header.Text; }
            set { Information_Header.Text = value; }
        }

        public string InformationContent
        {
            get { return Information_Content.Text; }
            set { Information_Content.Text = value; }
        }

        public string InformationButton
        {
            get { return Information_Button_Text.Text; }
            set { Information_Button_Text.Text = value; }
        }

        public InformationControl()
        {
            InitializeComponent();
        }

        public static void Show(string header, string content, string buttontext)
        {

            var messageBox = new InformationControl
            {

                InformationHeader = header,
                InformationContent = content,
                InformationButton = buttontext

            };

            var window = new Window
            {
                Content = messageBox,
                Height = 150,
                Width = 300,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Background = Brushes.Transparent,
                AllowsTransparency = true
            };

            messageBox._window = window;

            window.ShowDialog();

        }

        private void Information_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Information_Button.Background = FindResource("SolidColorBrush_DodgerBlue") as SolidColorBrush;
            Information_Button_Text.Foreground = FindResource("SolidColorBrush_White") as SolidColorBrush;
        }

        private void Information_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Information_Button.Background = FindResource("SolidColorBrush_LavenderWeb") as SolidColorBrush;
            Information_Button_Text.Foreground = FindResource("SolidColorBrush_DavysGrey") as SolidColorBrush;
        }

        private void Information_Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (_window != null)
            {
                _window.DialogResult = true;
            }

        }

    }
}
