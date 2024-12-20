﻿using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SAMS.UI.Views;

/// <summary>
/// Lógica de interacción para RegistrarPromocionView.xaml
/// </summary>
public partial class RegistrarPromocionView : Window
{
    List<ProductoInventarioPromocionDTO> listaProductoInventario;
    ObservableCollection<Object> _productosInventario;
    private EmpleadoLoginDTO empleado;
    private SideBarControl SideBarControl_MenuLateral;
    public RegistrarPromocionView(EmpleadoLoginDTO? empleado = null)
    {
        _productosInventario = new ObservableCollection<Object>();
        this.empleado = empleado ?? new EmpleadoLoginDTO() { tipoEmpleado = "Paqueteria" };
        InitializeComponent();

        SideBarControl_MenuLateral = new SideBarControl(empleado);
        MenuLateral.Children.Add(SideBarControl_MenuLateral);
        SideBarControl_MenuLateral.Employee = empleado.tipoEmpleado;

        DefinirColumnas();
        ObtenerProductosInventario();
    }

    private void TitleBarControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {

        if (e.OriginalSource is FrameworkElement element &&
            (element.Name == "MinusLogo" ||
            element.Name == "MaximizeLogo" ||
            element.Name == "ExitLogo"))
        {
            return;
        }

        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }

    }

    private void TitleBarControl_WindowStateChangeRequested(object sender, WindowState e)
    {
        this.WindowState = e;
    }

    private void DefinirColumnas()
    {

        Dictionary<string, string>[] columnas =
        {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Producto" },
                    { "Width", "*" },
                    { "BindingName", "nombre" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Perecedero" },
                    { "Width", "*" },
                    { "BindingName", "PerecederoTexto" }

                }
            };

        TablaProductos.DefineColumns(columnas);

    }

    private void ObtenerProductosInventario()
    {
        try
        {
            listaProductoInventario = ProductoInventarioDAO.OptenerProductosSinPromocion();
            _productosInventario.Clear();
            _productosInventario = new ObservableCollection<Object>(listaProductoInventario);
            TablaProductos.SetItemsSource(_productosInventario);
        }
        catch
        {
            InformationControl.Show("Error", "Ocurrió un error al obtener Inventario de Producto", "Aceptar");

            Close();
        }
    }

    private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
    {
        if (listaProductoInventario != null)
        {
            var reporteFiltado = listaProductoInventario.Where(
                x => x.nombre.ToLower().Contains(campoBuscar.Text.ToLower())).ToList();

            _productosInventario.Clear();

            _productosInventario = new ObservableCollection<Object>(reporteFiltado);

            TablaProductos.SetItemsSource(_productosInventario);
        }
        else
        {
            _productosInventario.Clear();

            _productosInventario = new ObservableCollection<Object>(listaProductoInventario);

            TablaProductos.SetItemsSource(_productosInventario);
        }
    }

    private void Registrar_ButtonControlClick(object sender, RoutedEventArgs e)
    {
        var objetoSeleccionado = TablaProductos.GetSelectedItem();

        if (objetoSeleccionado is ProductoInventarioPromocionDTO productoSeleccionado)
        {
            
            FormularioPromocionView formularioPromocionView = new(productoSeleccionado.id, this, empleado);
            formularioPromocionView.Show();
        }
        else
        {
            InformationControl.Show("Aviso", "Debe seleccionar un producto antes de registrar una promoción.", "Aceptar");
        }
    }


}
