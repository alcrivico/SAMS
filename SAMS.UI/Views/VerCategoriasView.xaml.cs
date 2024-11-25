using SAMS.UI.DTO;
using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public VerCategoriasView()
        {
            InitializeComponent();
        }
    }
}
