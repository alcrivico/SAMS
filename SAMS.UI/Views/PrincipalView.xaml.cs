using SAMS.UI.VisualComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
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
    /// Interaction logic for PrincipalView.xaml
    /// </summary>
    public partial class PrincipalView : Window
    {

        List<Game> _games;
        ObservableCollection<Object> _gamesDTO;

        public PrincipalView()
        {
            InitializeComponent();

            ActionsControl BarraAccion = new ActionsControl();
            BarraAccion.DetallesClicked += BarraAccion_DetallesClicked;
            BarraAccion.EditarClicked += BarraAccion_EditarClicked;
            BarraAccion.EliminarClicked += BarraAccion_EliminarClicked;

            ViewBoxAcciones.Child = BarraAccion;

            DefineGamesTable();

            Game albhieri = new Game();
            Game cristoff = new Game();

            _games = new List<Game>();

            albhieri.GameCode = "123";
            albhieri.CreatorName = "Albhieri";
            albhieri.Language = "English";

            cristoff.GameCode = "321";
            cristoff.CreatorName = "Cristoff";
            cristoff.Language = "Spanish";

            _games.Add(albhieri);

            _games.Add(cristoff);

            _gamesDTO = new ObservableCollection<object>(_games);

            GamesTable.SetItemsSource(_gamesDTO);

        }

        public class Game
        {
            public string GameCode { get; set; }
            public string CreatorName { get; set; }
            public string Language { get; set; }
            public ActionsControl Actions { get; set; }

            public Game()
            {
                Actions = new ActionsControl();
                Actions.DetallesClicked += ShowDetails;
                Actions.EditarClicked += Edit;
                Actions.EliminarClicked += Delete;
            }

            private void ShowDetails(object sender, RoutedEventArgs e)
            {
                Debug.WriteLine("ShowDetails");
                MessageBox.Show("Detalles de " + CreatorName);
            }

            private void Edit(object sender, RoutedEventArgs e)
            {
                Debug.WriteLine("Edit");
                MessageBox.Show("Editar " + CreatorName);
            }

            private void Delete(object sender, RoutedEventArgs e)
            {
                Debug.WriteLine("Delete");
                MessageBox.Show("Eliminar " + CreatorName);
            }

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

        private void CerrarSesionLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            CerrarSesionLogo.Width = 60;
            CerrarSesionLogo.Height = 60;
            ImageBrush_CerrarSesionLogo.ImageSource = FindResource("Icon_DoorOpen") as ImageSource;
        }

        private void CerrarSesionLogo_MouseLeave(object sender, MouseEventArgs e)
        {
            CerrarSesionLogo.Width = 30;
            CerrarSesionLogo.Height = 30;
            ImageBrush_CerrarSesionLogo.ImageSource = FindResource("Icon_DoorClosed") as ImageSource;
        }

        private void CerrarSesionLogo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            IniciarSesionView iniciarSesionView = new IniciarSesionView();

            iniciarSesionView.Show();
            this.Close();

        }
        
        private void ButtonControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            InformationControl.Show("Información", "Mensaje de información", "Aceptar");
        }

        private void DefineGamesTable()
        {

            Dictionary<string, string>[] columns =
            {
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Codigo" },
                    { "Width", "*" },
                    { "BindingName", "GameCode" }

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Creado por" },
                    { "Width", "*" },
                    { "BindingName", "CreatorName" },

                },
                new Dictionary<string, string> {

                    { "Type", "Text" },
                    { "Name", "Idioma" },
                    { "Width", "*" },
                    { "BindingName", "Language" }

                },
                new Dictionary<string, string> {

                    { "Type", "Actions" },
                    { "Name", "Acciones" },
                    { "Width", "*" },

                }


            };

            GamesTable.DefineColumns(columns);

        }

        private void CheckBoxEfectivo_CheckedChanged(object sender, RoutedEventArgs e)
        {

            if (CheckBoxEfectivo.IsChecked)
            {
                InformationControl.Show("Información", "El checkbox se marcó", "Aceptar");
            }
            else
            {
                InformationControl.Show("Información", "El checkbox se desmarcó", "Aceptar");
            }

        }

        private void BarraAccion_DetallesClicked(object sender, RoutedEventArgs e)
        {
            InformationControl.Show("Información", "Detalles", "Aceptar");

        }

        private void BarraAccion_EditarClicked(object sender, RoutedEventArgs e)
        {
            InformationControl.Show("Información", "Editar", "Aceptar");
        }

        private void BarraAccion_EliminarClicked(object sender, RoutedEventArgs e)
        {
            InformationControl.Show("Información", "Eliminar", "Aceptar");
        }

    }

}
