﻿using System.Windows;
using System.Windows.Controls;

namespace SAMS.UI.VisualComponents
{
    /// <summary>
    /// Interaction logic for PasswordBoxControl.xaml
    /// </summary>
    public partial class PasswordBoxControl : UserControl
    {
        public string PasswordText
        {
            get { return PasswordBox_Text.Password; }
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
                               typeof(PasswordBoxControl),
                               new PropertyMetadata(string.Empty));

        public int PasswordBoxWidth
        {
            get { return (int)GetValue(PasswordBoxWidthProperty); }
            set { SetValue(PasswordBoxWidthProperty, value); }
        }

        public static readonly DependencyProperty PasswordBoxWidthProperty =
            DependencyProperty.Register(
                               "PasswordBoxWidth",
                               typeof(int),
                               typeof(PasswordBoxControl),
                               new PropertyMetadata(150));

        public int PasswordBoxHeight
        {
            get { return (int)GetValue(PasswordBoxHeightProperty); }
            set { SetValue(PasswordBoxHeightProperty, value); }
        }

        public static readonly DependencyProperty PasswordBoxHeightProperty =
            DependencyProperty.Register(
                               "PasswordBoxHeight",
                               typeof(int),
                               typeof(PasswordBoxControl),
                               new PropertyMetadata(55));

        public PasswordBoxControl()
        {
            InitializeComponent();
        }

    }

}
