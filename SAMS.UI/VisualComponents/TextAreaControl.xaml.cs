using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for TextAreaControl.xaml
    /// </summary>
    public partial class TextAreaControl : UserControl, INotifyPropertyChanged
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
                typeof(TextAreaControl),
                new PropertyMetadata(string.Empty));

        public string TextHeight
        {
            get { return (string)GetValue(TextHeightProperty); }
            set { SetValue(TextHeightProperty, value); }
        }

        public static readonly DependencyProperty TextHeightProperty =
            DependencyProperty.Register(
                "TextHeight",
                typeof(string),
                typeof(TextAreaControl),
                new PropertyMetadata("120"));

        public string FieldName
        {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }

        public static readonly DependencyProperty FieldNameProperty =
            DependencyProperty.Register(
                               "FieldName",
                               typeof(string),
                               typeof(TextAreaControl),
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
                               typeof(TextAreaControl),
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
                               typeof(TextAreaControl),
                               new PropertyMetadata(55));

        public TextAreaControl()
        {
            InitializeComponent();
        }

        public void Disable()
        {
            TextArea.IsEnabled = false;
            TextBox_Highlight.Visibility = Visibility.Visible;
        }

        public void Enable()
        {
            TextArea.IsEnabled = true;
            TextBox_Highlight.Visibility = Visibility.Hidden;
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextAreaControl control = (TextAreaControl)d;
            control.OnPropertyChanged(nameof(Text));
        }

        public static RoutedEvent TextAreaControlTextChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(TextAreaControlTextChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(TextAreaControl));

        public event RoutedEventHandler TextAreaControlTextChanged
        {
            add { AddHandler(TextAreaControlTextChangedEvent, value); }
            remove { RemoveHandler(TextAreaControlTextChangedEvent, value); }
        }

        private void TextBox_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(TextAreaControlTextChangedEvent));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
