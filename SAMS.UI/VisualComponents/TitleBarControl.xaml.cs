using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for TitleBarControl.xaml
    /// </summary>

    public partial class TitleBarControl : UserControl
    {

        public event EventHandler<WindowState>? WindowStateChangeRequested;

        public bool ChangeLanguage
        {
            get { return (bool)GetValue(ChangeLanguageProperty); }
            set { SetValue(ChangeLanguageProperty, value); }
        }

        public static readonly DependencyProperty ChangeLanguageProperty =
            DependencyProperty.Register(
                nameof(ChangeLanguage),
                typeof(bool),
                typeof(TitleBarControl),
                new PropertyMetadata(true));

        public double ChangeOpacity
        {

            get { return (double)GetValue(ChangeOpacityProperty); }

            set
            {
                SetValue(ChangeOpacityProperty, value);
            }

        }

        public static readonly DependencyProperty ChangeOpacityProperty =
            DependencyProperty.Register(
                nameof(ChangeOpacity),
                typeof(double),
                typeof(TitleBarControl),
                new PropertyMetadata(1.0));

        public string FieldName
        {

            get { return (string)GetValue(FieldNameProperty); }

            set
            {
                SetValue(FieldNameProperty, value);
            }

        }

        public static readonly DependencyProperty FieldNameProperty =
            DependencyProperty.Register(
                nameof(FieldName),
                typeof(string),
                typeof(TitleBarControl),
                new PropertyMetadata(""));




        public TitleBarControl()
        {

            InitializeComponent();

        }

        private void SetWindowState(WindowState newState)
        {
            WindowStateChangeRequested?.Invoke(this, newState);
        }

        private WindowState GetWindowState()
        {

            Window parentWindow = Window.GetWindow(this);

            return parentWindow.WindowState;

        }

        private void MinusLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageBrush_MinusLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_RedMinusLogo"];
        }

        private void MinusLogo_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageBrush_MinusLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_MinusLogo"];
        }

        private void MaximizeLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            if (GetWindowState() == WindowState.Maximized)
            {
                ImageBrush_MaximizeLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_RedMinimizeLogo"];
            }
            else
            {
                ImageBrush_MaximizeLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_RedMaximizeLogo"];
            }
        }

        private void MaximizeLogo_MouseLeave(object sender, MouseEventArgs e)
        {
            if (GetWindowState() == WindowState.Maximized)
            {
                ImageBrush_MaximizeLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_MinimizeLogo"];
            }
            else
            {
                ImageBrush_MaximizeLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_MaximizeLogo"];
            }
        }

        private void MaximizeLogo_Click(object sender, MouseButtonEventArgs e)
        {
            if (GetWindowState() == WindowState.Maximized)
            {
                SetWindowState(WindowState.Normal);
                ImageBrush_MaximizeLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_RedMaximizeLogo"];
            }
            else
            {
                SetWindowState(WindowState.Maximized);
                ImageBrush_MaximizeLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_RedMinimizeLogo"];
            }
        }

        private void ExitLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageBrush_ExitLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_RedExitLogo"];
        }

        private void ExitLogo_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageBrush_ExitLogo.ImageSource = (ImageSource)Application.Current.Resources["Icon_ExitLogo"];
        }

        private void MinimizeLogo_Click(object sender, MouseButtonEventArgs e)
        {
            SetWindowState(WindowState.Minimized);
        }

        private void ExitLogo_Click(object sender, MouseButtonEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            Application.Current.Shutdown();
        }

        private void MinusLogo_Click(object sender, MouseButtonEventArgs e)
        {
            SetWindowState(WindowState.Minimized);
        }

    }
}
