using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for ConfirmationControl.xaml
    /// </summary>
    public partial class ConfirmationControl : UserControl
    {

        Window _window;

        public string ConfirmationHeader
        {
            get { return Confirmation_Header.Text; }
            set { Confirmation_Header.Text = value; }
        }

        public string ConfirmationContent
        {
            get { return Confirmation_Content.Text; }
            set { Confirmation_Content.Text = value; }
        }

        public string ConfirmationButton
        {
            get { return Confirmation_Button_Text.Text; }
            set { Confirmation_Button_Text.Text = value; }
        }

        public string CancelButton
        {
            get { return Cancel_Button_Text.Text; }
            set { Cancel_Button_Text.Text = value; }
        }

        public ConfirmationControl()
        {
            InitializeComponent();
        }

        public static bool Show(string header, string content, string okButtonText, string cancelButtonText)
        {

            var messageBox = new ConfirmationControl
            {

                ConfirmationHeader = header,
                ConfirmationContent = content,
                ConfirmationButton = okButtonText,
                CancelButton = cancelButtonText

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

            bool result = window.ShowDialog() ?? false;

            return result;

        }

        private void Confirmation_Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (_window != null)
            {
                _window.DialogResult = true;
            }

        }

        private void Cancel_Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (_window != null)
            {
                _window.DialogResult = false;
            }

        }

        private void Confirmation_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Confirmation_Button.Background = FindResource("SolidColorBrush_DodgerBlue") as SolidColorBrush;
            Confirmation_Button_Text.Foreground = FindResource("SolidColorBrush_White") as SolidColorBrush;
        }

        private void Confirmation_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Confirmation_Button.Background = FindResource("SolidColorBrush_LavenderWeb") as SolidColorBrush;
            Confirmation_Button_Text.Foreground = FindResource("SolidColorBrush_DavysGrey") as SolidColorBrush;
        }

        private void Cancel_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Cancel_Button.Background = FindResource("SolidColorBrush_PantoneOrange") as SolidColorBrush;
            Cancel_Button_Text.Foreground = FindResource("SolidColorBrush_White") as SolidColorBrush;
        }

        private void Cancel_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Cancel_Button.Background = FindResource("SolidColorBrush_LavenderWeb") as SolidColorBrush;
            Cancel_Button_Text.Foreground = FindResource("SolidColorBrush_DavysGrey") as SolidColorBrush;
        }

    }

}