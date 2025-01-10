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
using SAMS.UI.DAO;
using SAMS.UI.DTO;
using SAMS.UI.Models.Entities;
using SAMS.UI.VisualComponents;

namespace SAMS.UI.Views
{
    /// <summary>
    /// Lógica de interacción para VerMermasView.xaml
    /// </summary>
    public partial class VerMermasView : Window
    {
        List<MermaDTO> listaMermas;
        ObservableCollection<Object> _mermas;
        EmpleadoLoginDTO _empleado;
        SideBarControl SideBarControl_MenuLateral;
        public VerMermasView(EmpleadoLoginDTO empleado)
        {
            _empleado = empleado;
            listaMermas = new List<MermaDTO>();
            _mermas = new ObservableCollection<Object>();

            InitializeComponent();
            DefinirColumnas();
            ObtenerMermas();

            SideBarControl_MenuLateral = new SideBarControl(_empleado);
            SideBarControl_MenuLateral.SideElementSelected = 3;
            MenuLateral.Children.Add(SideBarControl_MenuLateral);
            SideBarControl_MenuLateral.Employee = _empleado.tipoEmpleado;

            TablaMermas.OnDetallesClickedHandler += botonDetallesClick;
            TablaMermas.OnEditarClickedHandler += botonEditarClick;
            TablaMermas.OnEliminarClickedHandler += botonEliminarClick;
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
                    { "Name", "Nombre del producto" },
                    { "Width", "*" },
                    { "BindingName", "productoInventario" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Cantidad" },
                    { "Width", "*" },
                    { "BindingName", "cantidad" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Fecha del registro" },
                    { "Width", "*" },
                    { "BindingName", "fechaRegistro" }
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

            TablaMermas.DefineColumns(columnas);

        }

        private void ObtenerMermas()
        {
            try
            {
                listaMermas = MermaDAO.ObtenerMermas().ToList();
                _mermas.Clear();
                _mermas = new ObservableCollection<Object>(listaMermas);
                TablaMermas.SetItemsSource(_mermas);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                InformationControl.Show("Error", "Ocurrió un error al obtener los mermas", "Aceptar");
                this.Close();
            }
        }

        private void campoBuscar_TextBoxControlTextChanged(object sender, RoutedEventArgs e)
        {
            if (listaMermas != null)
            {
                if (!string.IsNullOrWhiteSpace(campoBuscar.Text))
                {
                    var textoBusqueda = campoBuscar.Text.ToUpper();
                    var mermasFiltradas = listaMermas.Where(
                        m => m.productoInventario.ToUpper().Contains(textoBusqueda)
                    ).ToList();
                    _mermas.Clear();
                    foreach (var merma in mermasFiltradas)
                    {
                        _mermas.Add(merma);
                    }
                }
                else
                {
                    _mermas.Clear();
                    foreach (var merma in listaMermas)
                    {
                        _mermas.Add(merma);
                    }
                }

                TablaMermas.SetItemsSource(_mermas);
            }
        }


        private void botonDetallesClick(object sender, RoutedEventArgs e)
        {
            ActionsControl actionBar = (ActionsControl)sender;
            MermaDTO merma = (MermaDTO)actionBar.DataContext;

            DetallesMermaView detallesMermasView = new DetallesMermaView(merma);
            detallesMermasView.ShowDialog();
        }

        private void botonEditarClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void botonEliminarClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void botonAgregar_ButtonControlClick(object sender, RoutedEventArgs e)
        {
            RegistrarMermaView registrarMermaView = new RegistrarMermaView();
            registrarMermaView.ShowDialog();
            ObtenerMermas();
        }
    }
}
