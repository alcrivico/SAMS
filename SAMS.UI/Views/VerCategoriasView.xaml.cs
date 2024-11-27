using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para VerCategoriasView.xaml
    /// </summary>
    public partial class VerCategoriasView : Window
    {

        List<CategoriaDTO> listaCategorias;
        ObservableCollection<Object> _categoria;
        EmpleadoLoginDTO _empleado;
        SideBarControl SideBarControl_MenuLateral;

        public VerCategoriasView(EmpleadoLoginDTO empleado)
        {
            _empleado = empleado;
            listaCategorias = new List<CategoriaDTO>();
            _categoria = new ObservableCollection<Object>();

            InitializeComponent();

            DefinirColumnas();
            ObtenerCategorias();

            SideBarControl_MenuLateral = new SideBarControl(_empleado);
            SideBarControl_MenuLateral.SideElementSelected = 4;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = _empleado.tipoEmpleado;
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
                    { "Name", "Nombre de la categoria" },
                    { "Width", "*" },
                    { "BindingName", "nombre" }

                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },
                    { "Detalles", "True" },
                    { "Editar", "True" },
                    { "Eliminar", "True" }

                }

            };

            TablaCategorias.DefineColumns(columnas);

        }

        private void ObtenerCategorias()
        {
            try
            {
                listaCategorias = CategoriaDAO.ObtenerCategorias().ToList();
                _categoria.Clear();
                _categoria = new ObservableCollection<Object>(listaCategorias);
                TablaCategorias.SetItemsSource(_categoria);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al obtener las categorias", "Aceptar");
                this.Close();
            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaCategorias != null)
            {

                if (campoBuscar.Text.Length > 0)
                {
                    var categoriaFiltradas = listaCategorias.Where(
            p => p.nombre.ToUpper().Contains(campoBuscar.Text.ToUpper()));

                    _categoria.Clear();

                    _categoria = new ObservableCollection<Object>(categoriaFiltradas);

                    TablaCategorias.SetItemsSource(_categoria);

                }
                else
                {

                    _categoria.Clear();

                    _categoria = new ObservableCollection<Object>(listaCategorias);

                    TablaCategorias.SetItemsSource(_categoria);

                }

            }

        }

        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            InformationControl.Show("Informacion", "No es posible acceder a esta funcionalida aun", "Aceptar");
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            InformationControl.Show("Informacion", "No es posible acceder a esta funcionalida aun", "Aceptar");
        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            RegistrarCategoriaView registrarCategoriaView = new RegistrarCategoriaView();
            registrarCategoriaView.ShowDialog();
        }


    }
}
