using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ComboBoxControl.xaml
    /// </summary>
    public partial class ComboBoxControl : UserControl
    {

        public string FieldName
        {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }

        public static readonly DependencyProperty FieldNameProperty =
            DependencyProperty.Register(
                               nameof(FieldName),
                               typeof(string),
                               typeof(ComboBoxControl),
                               new PropertyMetadata(string.Empty));

        public bool ListEnabled         
        {
            get { return (bool)GetValue(ListEnabledProperty); }
            set { SetValue(ListEnabledProperty, value); }
        }

        public static readonly DependencyProperty ListEnabledProperty =
            DependencyProperty.Register(
                nameof(ListEnabled),
                typeof(bool),
                typeof(ComboBoxControl),
                new PropertyMetadata(true));
        
        public double ListOpacity
        {
            get { return (double)GetValue(ListOpacityProperty); }
            set { SetValue(ListOpacityProperty, value); }
        }

        public static readonly DependencyProperty ListOpacityProperty =
            DependencyProperty.Register(
                nameof(ListOpacity),
                typeof(double),
                typeof(ComboBoxControl),
                new PropertyMetadata(1.0));

        public string MemberPath
        {
            get { return (string)GetValue(MemberPathProperty); }
            set { SetValue(MemberPathProperty, value); }
        }

        public static readonly DependencyProperty MemberPathProperty =
            DependencyProperty.Register(
                "MemberPath",
                typeof(string),
                typeof(ComboBoxControl),
                new PropertyMetadata(string.Empty));

        public int ComboBoxWidth
        {
            get { return (int)GetValue(ComboBoxWidthProperty); }
            set { SetValue(ComboBoxWidthProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxWidthProperty =
            DependencyProperty.Register(
                "ComboBoxWidth",
                typeof(int),
                typeof(ComboBoxControl),
                new PropertyMetadata(200));

        public int ComboBoxHeight
        {
            get { return (int)GetValue(ComboBoxHeightProperty); }
            set { SetValue(ComboBoxHeightProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxHeightProperty =
            DependencyProperty.Register(
                "ComboBoxHeight",
                typeof(int),
                typeof(ComboBoxControl),
                new PropertyMetadata(40));

        public object SelectedItem
        {
            get { return ComboBoxControlType.SelectedItem; }
            set { ComboBoxControlType.SelectedItem = value; }
        }

        public ComboBoxControl()
        {
            InitializeComponent();
        }

        public ComboBox languageComboBox
        {
            get { return ComboBoxControlType; }
        }

        public static readonly RoutedEvent SelectedItemChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(SelectedItemChanged), 
                RoutingStrategy.Bubble,                
                typeof(RoutedEventHandler), 
                typeof(ComboBoxControl));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }

        private void ComboBoxControl_Loaded(object sender, RoutedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            ToggleButton? toggleButton = comboBox.Template.FindName("toggleButton", comboBox) as ToggleButton;

            if (toggleButton != null)
            {

                Border? border = toggleButton.Template.FindName("templateRoot", toggleButton) as Border;

                if (border != null)
                {
                    border.Background = FindResource("SolidColorBrush_White") as SolidColorBrush;
                    border.BorderBrush = FindResource("SolidColorBrush_DodgerBlue") as SolidColorBrush;
                    border.BorderThickness = new Thickness(3);
                    border.CornerRadius = new CornerRadius(10);
                }

            }

        }

        public void SetItemsSource(ObservableCollection<Object> itemsSource, String ItemPath)
        {
            ComboBoxControlType.ItemsSource = null;
            ComboBoxControlType.ItemsSource = itemsSource;

            ComboBoxControlType.DisplayMemberPath = ItemPath;

            ComboBoxControlType.SelectedIndex = 0;
        }

        public void SelectDefaultItem(int index)
        {
            ComboBoxControlType.SelectedIndex = index;
        }

        public void SetSelectedItem(Object item)
        {
            ComboBoxControlType.SelectedItem = item;
        }

        private void ComboBoxControlType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(SelectedItemChangedEvent));
        }

    }

}
