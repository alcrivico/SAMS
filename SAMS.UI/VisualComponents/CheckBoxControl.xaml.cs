using System.Windows;
using System.Windows.Controls;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for CheckBoxControl.xaml
    /// </summary>
    public partial class CheckBoxControl : UserControl
    {

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                "IsChecked",
                typeof(bool),
                typeof(CheckBoxControl),
                new PropertyMetadata(false));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(
                "IsEnabled",
                typeof(bool),
                typeof(CheckBoxControl),
                new PropertyMetadata(true, OnIsEnabledChanged));

        public string FieldName
        {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }

        public static readonly DependencyProperty FieldNameProperty =
            DependencyProperty.Register(
                               "FieldName",
                               typeof(string),
                               typeof(CheckBoxControl),
                               new PropertyMetadata(string.Empty));

        public CheckBoxControl()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent CheckedChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(CheckedChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CheckBoxControl));

        public event RoutedEventHandler CheckedChanged
        {
            add { AddHandler(CheckedChangedEvent, value); }
            remove { RemoveHandler(CheckedChangedEvent, value); }
        }

        private void CheckBox_Change(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(CheckedChangedEvent));
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            CheckBoxControl checkBoxControl = d as CheckBoxControl;

            if (checkBoxControl.IsEnabled)
            {
                checkBoxControl.Check.IsEnabled = true;
                checkBoxControl.Field.IsEnabled = true;
                checkBoxControl.Check.Opacity = 1;
                checkBoxControl.Field.Opacity = 1;
            }
            else
            {
                checkBoxControl.Check.IsEnabled = false;
                checkBoxControl.Field.IsEnabled = false;
                checkBoxControl.Check.Opacity = 0.5;
                checkBoxControl.Field.Opacity = 0.5;
            }

        }

    }

}
