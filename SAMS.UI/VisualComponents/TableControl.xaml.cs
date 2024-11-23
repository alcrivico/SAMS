using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for TableControl.xaml
    /// </summary>
    public partial class TableControl : UserControl
    {

        public Action<object, RoutedEventArgs> OnDetallesClickedHandler { get; set; }
        public Action<object, RoutedEventArgs> OnEditarClickedHandler { get; set; }
        public Action<object, RoutedEventArgs> OnEliminarClickedHandler { get; set; }


        public static readonly RoutedEvent SelectedItemChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(SelectedItemChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(TableControl));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }

        public TableControl()
        {
            InitializeComponent();
            DataGridStructure.SelectionChanged += DataGridStructure_SelectionChanged;
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element &&
                (element.Name == "BotonDetalle" ||
                element.Name == "BotonEditar" ||
                element.Name == "BotonEliminar"))
            {
                e.Handled = true;
            }
        }

        public void DefineColumns(Dictionary<string, string>[] columns)
        {
            DataGridStructure.Columns.Clear();

            for (int i = 0; i < columns.Length; i++)
            {
                double ColumnWidth = -1;


                if (columns[i]["Width"] != "*")
                {
                    ColumnWidth = Convert.ToDouble(columns[i]["Width"]);
                }

                if (columns[i]["Type"] == "Text")
                {

                    DataGridTextColumn dataGridTextColumn = new DataGridTextColumn();
                    dataGridTextColumn.Header = columns[i]["Name"];
                    dataGridTextColumn.Binding = new Binding(columns[i]["BindingName"]);
                    dataGridTextColumn.IsReadOnly = true;

                    Style textBlockStyle = new Style(typeof(TextBlock));
                    textBlockStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                    textBlockStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));

                    dataGridTextColumn.ElementStyle = textBlockStyle;

                    if (i != 0)
                    {

                        dataGridTextColumn.HeaderStyle = FindResource("GeneralColumnHeader") as Style;
                        dataGridTextColumn.CellStyle = FindResource("GeneralCell") as Style;

                    }

                    if (ColumnWidth != -1)
                    {
                        dataGridTextColumn.Width = new DataGridLength(ColumnWidth, DataGridLengthUnitType.Pixel);
                    }
                    else
                    {
                        dataGridTextColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    }

                    DataGridStructure.Columns.Add(dataGridTextColumn);

                }
                else if (columns[i]["Type"] == "Actions")
                {
                    DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
                    dataGridTemplateColumn.Header = columns[i]["Name"];
                    dataGridTemplateColumn.IsReadOnly = true;

                    if (i != 0)
                    {
                        dataGridTemplateColumn.HeaderStyle = FindResource("GeneralColumnHeader") as Style;
                        dataGridTemplateColumn.CellStyle = FindResource("GeneralCell") as Style;
                    }
                    if (ColumnWidth != -1)
                    {
                        dataGridTemplateColumn.Width = new DataGridLength(ColumnWidth, DataGridLengthUnitType.Pixel);
                    }
                    else
                    {
                        dataGridTemplateColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    }

                    DataTemplate cellTemplate = new DataTemplate();

                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(ActionsControl));

                    factory.AddHandler(ActionsControl.DetallesClickedEvent, new RoutedEventHandler((s, e) => OnDetallesClickedHandler?.Invoke(s, e)));
                    factory.AddHandler(ActionsControl.EditarClickedEvent, new RoutedEventHandler((s, e) => OnEditarClickedHandler?.Invoke(s, e)));
                    factory.AddHandler(ActionsControl.EliminarClickedEvent, new RoutedEventHandler((s, e) => OnEliminarClickedHandler?.Invoke(s, e)));

                    if (columns[i]["Detalles"] == "False")
                    {
                        factory.SetValue(ActionsControl.DetallesActivoProperty, false);
                    }

                    if (columns[i]["Editar"] == "False")
                    {
                        factory.SetValue(ActionsControl.EditarActivoProperty, false);
                    }

                    if (columns[i]["Eliminar"] == "False")
                    {
                        factory.SetValue(ActionsControl.EliminarActivoProperty, false);
                    }

                    cellTemplate.VisualTree = factory;
                    dataGridTemplateColumn.CellTemplate = cellTemplate;

                    DataGridStructure.Columns.Add(dataGridTemplateColumn);

                }

            }

        }

        public void SetItemsSource(ObservableCollection<Object> itemsSource)
        {

            DataGridStructure.ItemsSource = null;
            DataGridStructure.ItemsSource = itemsSource;

        }

        public Object GetSelectedItem()
        {
            return DataGridStructure.SelectedItem;
        }

        private void DataGridStructure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs (SelectedItemChangedEvent));
        }
    }

}
