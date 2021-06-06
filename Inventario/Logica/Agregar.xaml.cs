using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// Insertar librerias
using Inventario.Models;
using Acr.UserDialogs;

namespace Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agregar : ContentPage
    {
        public Agregar()
        {
            InitializeComponent();

            // Evento clicked para salir
            this.btnSalirClicked.Clicked += async (object sender, EventArgs e) =>
            {
                await Navigation.PopAsync();
            };
        }

        // Evento clicked para registrar
        public async void btnRegistrarProductoClicked_Clicked(object sender, EventArgs e)
        {
            // Verifica que no haya ningun dato faltante
            if (!String.IsNullOrEmpty(txtClave.Text) &&
                !String.IsNullOrEmpty(txtNombre.Text) &&
                !String.IsNullOrEmpty(txtPrecio.Text) &&
                !String.IsNullOrEmpty(txtCantidad.Text) &&
                !String.IsNullOrEmpty(txtProveedor.Text) &&
                !String.IsNullOrEmpty(txtProveedorTel.Text))
            {
                // Codigo para hacer el query e insertar producto
                UserDialogs.Instance.ShowLoading("Agregando producto");
                // Codigo insertar producto a la bdd **** //

                // Crea el objeto con los datos recibidos del producto
                ProductModel producto = new ProductModel
                {
                    prodClave = txtClave.Text,
                    prodName = txtNombre.Text,
                    prodPrec = int.Parse(txtPrecio.Text),
                    prodCant = int.Parse(txtCantidad.Text),
                    provName = txtProveedor.Text,
                    provTel = txtProveedorTel.Text
                };
                // Envia el objeto como parametro para insertarlo
                await App.SQLiteDB.SaveProductAsync(producto);
                // Limpia los campos del formulario
                txtClave.Text = "";
                txtNombre.Text = "";
                txtPrecio.Text = "";
                txtCantidad.Text = "";
                txtProveedor.Text = "";
                txtProveedorTel.Text = "";

                // Fin ********************************** //
                await Task.Delay(2000);
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Datos correctos", "El producto se agregó correctamente", "Aceptar");
            }
            else
                await DisplayAlert("Datos erróneos", "Por favor, ingrese correctamente todos los datos del producto", "Aceptar");
        }
    }
}