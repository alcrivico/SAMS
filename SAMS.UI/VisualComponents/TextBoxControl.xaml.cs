using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for TextBoxControl.xaml
    /// </summary>
    public partial class TextBoxControl : UserControl, INotifyPropertyChanged
    {

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged(nameof(Text));
            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBoxControl),
                new PropertyMetadata(string.Empty));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(
                "IsReadOnly",
                typeof(bool),
                typeof(TextBoxControl),
                new PropertyMetadata(false));

        public static readonly DependencyProperty EnableTextBoxProperty =
            DependencyProperty.Register(
                "EnableTextBox",
                typeof(bool),
                typeof(TextBoxControl),
                new PropertyMetadata(true, OnEnableTextBoxPropertyChanged));

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register(
                "MaxLengthTextBox",
                typeof(int),
                typeof(TextBoxControl),
                new PropertyMetadata(1000));

        public int MaxLengthTextBox
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public bool EnableTextBox
        {
            get { return (bool)GetValue(EnableTextBoxProperty); }
            set { SetValue(EnableTextBoxProperty, value); }
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
                               typeof(TextBoxControl),
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
                               typeof(TextBoxControl),
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
                               typeof(TextBoxControl),
                               new PropertyMetadata(55));

        public TextBoxControl()
        {
            InitializeComponent();
        }

        private static void OnEnableTextBoxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxControl control = (TextBoxControl)d;
            control.EnableTextBox = (bool)e.NewValue;

            if (control.EnableTextBox)
            {
                control.Enable();
            }
            else
            {
                control.Disable();
            }

        }

        public void Disable()
        {

            //TextBox.IsEnabled = false;
            TextBox_Border.Opacity = 0.5;
            TextBox_Text.Foreground = (SolidColorBrush)FindResource("SolidColorBrush_EerieBlack");
            TextBox_Text.Opacity = 1;
            TextBox_Text.Focusable = false;
            TextBox_Text.Cursor = Cursors.Arrow;
            TextBox_FieldName.Background = (SolidColorBrush)FindResource("SolidColorBrush_GhostWhite");

        }

        public void Enable()
        {

            //TextBox.IsEnabled = true;
            TextBox_Border.Opacity = 1;
            TextBox_Text.Foreground = (SolidColorBrush)FindResource("SolidColorBrush_DavysGrey");
            TextBox_Text.Opacity = 1;
            TextBox_Text.Focusable = true;
            TextBox_Text.Cursor = Cursors.IBeam;
            TextBox_FieldName.Background = (SolidColorBrush)FindResource("SolidColorBrush_White");

        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxControl control = (TextBoxControl)d;
            control.OnPropertyChanged(nameof(Text));
        }

        public static RoutedEvent TextBoxControlTextChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(TextBoxControlTextChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(TextBoxControl));

        public event RoutedEventHandler TextBoxControlTextChanged
        {
            add { AddHandler(TextBoxControlTextChangedEvent, value); }
            remove { RemoveHandler(TextBoxControlTextChangedEvent, value); }
        }

        private void TextBox_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(TextBoxControlTextChangedEvent));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
