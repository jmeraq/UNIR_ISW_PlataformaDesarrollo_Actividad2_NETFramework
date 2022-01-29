using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Actividad_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window ConfirmarPedido = new Window();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBorrar(object sender, RoutedEventArgs e)
        {
            NombreMedicamento.Text = "";
            TipoMedicamento.SelectedIndex=0;
            Cantidad.Text = "";
            DistribuidorCemefar.IsChecked = false;
            DistribuidorCofarma.IsChecked = false;
            DistribuidorEmpsephar.IsChecked = false;
            SucursalPrincipal.IsChecked = false;
            SucursalSecundaria.IsChecked = false;
        }

        private bool ValidarAlfanumerico(String NombreMedicamento) {
            Regex r = new Regex("^[a-zA-Z0-9]*$");

            if (NombreMedicamento == "") {
                return false;
            } else if (NombreMedicamento==null) {
                return false;
            } else if (r.IsMatch(NombreMedicamento)) {
                return true;
            }
            return false;
        }

        private bool ValidarNumerico(String Cantidad) {
            int valor= 0;
            try { 
                valor=int.Parse(Cantidad);
                if (valor > 0)
                {
                    return true;
                }
                return false;
            }
            catch(System.FormatException) {
                return false;
            }
        }

        private bool ValidarDistribuidor() {
            return (this.DistribuidorCemefar.IsChecked==true) || (this.DistribuidorCofarma.IsChecked==true) || (this.DistribuidorEmpsephar.IsChecked==true);
        }

        private bool ValidarSucursal() { 
            return (this.SucursalPrincipal.IsChecked==true) || (this.SucursalSecundaria.IsChecked==true);
        }

        private String getDistribuidor(){
            if (this.DistribuidorCemefar.IsChecked==true){
                return this.DistribuidorCemefar.Content.ToString();
            }else if (this.DistribuidorCofarma.IsChecked==true){
                return this.DistribuidorCofarma.Content.ToString();
            }else if (this.DistribuidorEmpsephar.IsChecked == true){
                return this.DistribuidorEmpsephar.Content.ToString();
            }

            return "No seleccionado";
        }

        private String getDireccionSucursal(){
            String direccion = "";

            if(this.SucursalPrincipal.IsChecked==true && this.SucursalSecundaria.IsChecked == true) {
                direccion = "Para la farmacia situada en la Calle de la Rosa n.28 y para la situada en la Calle Alzabilla n.3";
            }else if(this.SucursalPrincipal.IsChecked == true) {
                direccion = "Para la farmacia situada en la Calle de la Rosa n.28";
            }else if (this.SucursalSecundaria.IsChecked == true) {
                direccion = "Para la farmacia situada en la Calle Alzabilla n.3";
            }

            return direccion;
        }

        private void BotonCancelarPedido(object sender, RoutedEventArgs e)
        {
            ConfirmarPedido.Hide();
            ButtonBorrar(sender,e);
        }

        private void BotonEnviarPedido(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pedido Enviado");
            ConfirmarPedido.Hide();
            ButtonBorrar(sender, e);
        }

        private void ButtonConfirmar(object sender, RoutedEventArgs e)
        {
            int error = 0;
            String description = "";

            if (!ValidarAlfanumerico(this.NombreMedicamento.Text)) { 
                error=1;
                description = "El nombre del medicamento esta incorrecto";
            }

            if (this.TipoMedicamento.SelectedIndex == 0) { 
                error=1;
                description = "No se ha seleccionado un tipo de medicamento";
            }

            if (!ValidarNumerico(this.Cantidad.Text)) { 
                error = 1;
                description = "La cantidad no es un numero entero positivo";
            }

            if (!ValidarDistribuidor()) {
                error = 1;
                description = "No se ha seleccionado un distribuidor";
            }

            if (!ValidarSucursal()){
                error = 1;
                description = "No se ha seleccionado una sucursal";
            }




            if (error == 1)
            {
                MessageBox.Show(description);
            }
            else {
                
                ConfirmarPedido.Height = 450;
                ConfirmarPedido.Width = 800;
                ConfirmarPedido.Title = "Pedido al distribuidor " + getDistribuidor();

                Grid grd = new Grid();

                Label PedidoDistribuidor = new Label();
                PedidoDistribuidor.Content = "Pedido al distribuidor " + getDistribuidor();
                PedidoDistribuidor.Margin = new Thickness(200,100,0,0);

                ComboBoxItem comboBoxItemTipoMedicamento = (ComboBoxItem)this.TipoMedicamento.SelectedItem;

                Label PedidoMedicamento = new Label();
                PedidoMedicamento.Content = this.Cantidad.Text + " unidades del " + comboBoxItemTipoMedicamento.Content.ToString() + " ( "+ this.NombreMedicamento.Text +" )";
                PedidoMedicamento.Margin = new Thickness(200, 150, 0, 0);

                Label PedidoSucursal = new Label();
                PedidoSucursal.Content = getDireccionSucursal();
                PedidoSucursal.Margin = new Thickness(200, 200, 0, 0);

                Button CancelarPedido = new Button();
                CancelarPedido.Content = "Cancelar Pedido";
                CancelarPedido.Margin= new Thickness(234, 357, 0, 0);
                CancelarPedido.Height = 33;
                CancelarPedido.Width = 109;
                CancelarPedido.VerticalAlignment = VerticalAlignment.Top;
                CancelarPedido.HorizontalAlignment = HorizontalAlignment.Left;
                CancelarPedido.Click += BotonCancelarPedido;

                Button EnviarPedido = new Button();
                EnviarPedido.Content = "Enviar Pedido";
                EnviarPedido.Margin = new Thickness(419, 357, 0, 0);
                EnviarPedido.Height = 33;
                EnviarPedido.Width = 109;
                EnviarPedido.VerticalAlignment = VerticalAlignment.Top;
                EnviarPedido.HorizontalAlignment = HorizontalAlignment.Left;
                EnviarPedido.Click += BotonEnviarPedido;

                grd.Children.Add(PedidoDistribuidor);
                grd.Children.Add(PedidoMedicamento);
                grd.Children.Add(PedidoSucursal);
                grd.Children.Add(CancelarPedido);
                grd.Children.Add(EnviarPedido);

                ConfirmarPedido.Content = grd;
                ConfirmarPedido.Show();
            }
        }
    }
}
