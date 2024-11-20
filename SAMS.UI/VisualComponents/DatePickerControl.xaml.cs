using System.Windows;
using System.Windows.Controls;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Lógica de interacción para DatePickerControl.xaml
    /// </summary>
    public partial class DatePickerControl : UserControl
    {
        public DatePickerControl()
        {
            InitializeComponent();
        }

        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = !CalendarPopup.IsOpen;
        }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(DatePickerControl), new PropertyMetadata(null));

        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public string FieldName
        {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }

        public static readonly DependencyProperty FieldNameProperty =
            DependencyProperty.Register(
                               "FieldName",
                               typeof(string),
                               typeof(DatePickerControl),
                               new PropertyMetadata(string.Empty));

        public int TextBoxWidth
        {
            get { return (int)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register(
                               "TextBoxWidth",
                               typeof(int),
                               typeof(DatePickerControl),
                               new PropertyMetadata(150));

        public int TextBoxHeight
        {
            get { return (int)GetValue(TextBoxHeightProperty); }
            set { SetValue(TextBoxHeightProperty, value); }
        }

        public static readonly DependencyProperty TextBoxHeightProperty =
           DependencyProperty.Register(
                              "TextBoxHeight",
                              typeof(int),
                              typeof(DatePickerControl),
                              new PropertyMetadata(150));
    }
}
