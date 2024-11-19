using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarProductoView.xaml
    /// </summary>
    public partial class RegistrarProductoView : Window
    {
        public RegistrarProductoView()
        {
            InitializeComponent();
        }

        private void TitleBarControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.OriginalSource is FrameworkElement element &&
                (element.Name == "MinusLogo" ||
                element.Name == "MaximizeLogo" ||
                element.Name == "ExitLogo"))
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void TitleBarControl_WindowStateChangeRequested(object sender, WindowState e)
        {
            this.WindowState = e;
        }

        private void Button_Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancelar_ButtonControlClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
