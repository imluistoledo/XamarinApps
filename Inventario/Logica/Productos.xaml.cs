using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Productos : ContentPage
    {
        public string tipoDeUsuarioRecibido;
        public Productos(String tipoUsuario)
        {
            InitializeComponent();
            tipoDeUsuarioRecibido = tipoUsuario;
            // Modifica la etiqueta e imagen segun el tipo de trabajador
            if (tipoDeUsuarioRecibido == "Administrador")
            {
                lblTipoUsuario.Text = tipoDeUsuarioRecibido;
                imgTipoUsuario.Source = "admin";
            }
            else if (tipoDeUsuarioRecibido == "Trabajador")
            {
                lblTipoUsuario.Text = tipoDeUsuarioRecibido;
                imgTipoUsuario.Source = "trabajador";
                // Le quita las funciones correspondientes al admin
                // Restrignir boton agregar
                btnAgregarProducto.IsEnabled = false;
                btnAgregarProducto.Opacity = 0.5;
                lblAgregar.Opacity = 0.5;
                // Restringir boton eliminar
                btnEliminarProducto.IsEnabled = false;
                btnEliminarProducto.Opacity = 0.5;
                lblEliminar.Opacity = 0.5;
            }

            // Eventos para navegar segun la funcion
            this.btnAgregarProducto.Clicked += async (sender, args) =>
            {
                if (btnAgregarProducto.IsEnabled)
                {
                    await Navigation.PushAsync(new Agregar());
                }
            };

            this.btnEliminarProducto.Clicked += async (sender, args) =>
            {
                if (btnEliminarProducto.IsEnabled)
                {
                    await Navigation.PushAsync(new Eliminar());
                }
            };

            this.btnModificarProducto.Clicked += async (sender, args) =>
            {
                if (btnModificarProducto.IsEnabled)
                {
                    await Navigation.PushAsync(new Modificar());
                }
            };

            this.btnVerProducto.Clicked += async (sender, args) =>
            {
                if (btnVerProducto.IsEnabled)
                {
                    await Navigation.PushAsync(new Ver());
                }
            };
        }

        // Boton para cerrar la pagina
        private async void btnSalirClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}