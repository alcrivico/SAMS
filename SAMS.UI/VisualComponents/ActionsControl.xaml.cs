using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ActionsControl.xaml
    /// </summary>
    public partial class ActionsControl : UserControl
    {
        public ActionsControl()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent DetallesClickedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(DetallesClicked), 
                RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler), 
                typeof(ActionsControl));

        public static readonly RoutedEvent EditarClickedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(EditarClicked), 
                RoutingStrategy.Bubble, 
                typeof(RoutedEventHandler), 
                typeof(ActionsControl));

        public static readonly RoutedEvent EliminarClickedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(EliminarClicked),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(ActionsControl));

        public event RoutedEventHandler DetallesClicked
        {
            add { AddHandler(DetallesClickedEvent, value); }
            remove { RemoveHandler(DetallesClickedEvent, value); }
        }

        public event RoutedEventHandler EditarClicked
        {
            add { AddHandler(EditarClickedEvent, value); }
            remove { RemoveHandler(EditarClickedEvent, value); }
        }

        public event RoutedEventHandler EliminarClicked
        {
            add { AddHandler(EliminarClickedEvent, value); }
            remove { RemoveHandler(EliminarClickedEvent, value); }
        }

        private void BotonDetalle_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(DetallesClickedEvent));
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(EditarClickedEvent));
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RaiseEvent(new RoutedEventArgs(EliminarClickedEvent));
        }

    }

}
