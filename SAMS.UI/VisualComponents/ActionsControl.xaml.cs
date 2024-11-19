using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for ActionsControl.xaml
    /// </summary>
    public partial class ActionsControl : UserControl
    {

        public static readonly DependencyProperty DetallesActivoProperty = DependencyProperty.Register(
            nameof(DetallesActivo),
            typeof(bool),
            typeof(ActionsControl),
            new PropertyMetadata(true, OnDetallesActivoChanged));

        public static readonly DependencyProperty EditarActivoProperty = DependencyProperty.Register(
            nameof(EditarActivo),
            typeof(bool),
            typeof(ActionsControl),
            new PropertyMetadata(true, OnEditarActivoChanged));

        public static readonly DependencyProperty EliminarActivoProperty = DependencyProperty.Register(
            nameof(EliminarActivo),
            typeof(bool),
            typeof(ActionsControl),
            new PropertyMetadata(true, OnEliminarActivoChanged));

        public bool DetallesActivo
        {
            get { return (bool)GetValue(DetallesActivoProperty); }
            set { SetValue(DetallesActivoProperty, value); }
        }

        public bool EditarActivo
        {
            get { return (bool)GetValue(EditarActivoProperty); }
            set { SetValue(EditarActivoProperty, value); }
        }

        public bool EliminarActivo
        {
            get { return (bool)GetValue(EliminarActivoProperty); }
            set { SetValue(EliminarActivoProperty, value); }
        }

        public ActionsControl()
        {
            InitializeComponent();
        }

        private static void OnDetallesActivoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ActionsControl)d;
            control.OnDetallesActivoChanged((bool)e.NewValue);
        }

        private static void OnEditarActivoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ActionsControl)d;
            control.OnEditarActivoChanged((bool)e.NewValue);
        }

        private static void OnEliminarActivoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ActionsControl)d;
            control.OnEliminarActivoChanged((bool)e.NewValue);
        }

        private void OnDetallesActivoChanged(bool newValue)
        {

            BotonDetalle.IsEnabled = newValue;
            BotonDetalle.Opacity = newValue ? 1 : 0.5;
            TextDetalle.Opacity = newValue ? 1 : 0.5;

        }

        private void OnEditarActivoChanged(bool newValue)
        {

            BotonDetalle.IsEnabled = newValue;
            BotonDetalle.Opacity = newValue ? 1 : 0.5;
            TextDetalle.Opacity = newValue ? 1 : 0.5;

        }

        private void OnEliminarActivoChanged(bool newValue)
        {

            BotonEliminar.IsEnabled = newValue;
            BotonEliminar.Opacity = newValue ? 1 : 0.5;
            TextEliminar.Opacity = newValue ? 1 : 0.5;

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
            Debug.WriteLine("BotonDetalle_Click");
            RaiseEvent(new RoutedEventArgs(DetallesClickedEvent));
        }

        private void BotonEditar_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Debug.WriteLine("BotonEditar_Click");
            RaiseEvent(new RoutedEventArgs(EditarClickedEvent));
        }

        private void BotonEliminar_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Debug.WriteLine("BotonEliminar_Click");
            RaiseEvent(new RoutedEventArgs(EliminarClickedEvent));
        }

    }

}
