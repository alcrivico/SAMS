﻿using System;
using System.Collections.Generic;
using System.Linq;
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

    }

}
