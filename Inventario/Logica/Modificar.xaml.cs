using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// Importar librerias
using ZXing.Net.Mobile.Forms;
using Acr.UserDialogs;
using Inventario.Models;

namespace Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Modificar : ContentPage
    {
        public Modificar()
        {
            InitializeComponent();
            mostrarProductos();
            // Inhablita los campos
            txtIDProducto.IsEnabled = false;
            txtClaveProducto.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txtPrecio.IsEnabled = false;
            txtCantidad.IsEnabled = false;
            txtProveedor.IsEnabled = false;
            txtProveedorTel.IsEnabled = false;
            // Cerrar pagina
            this.btnSalirClicked.Clicked += async (object sender, EventArgs e) =>
            {
                await Navigation.PopAsync();
            };
            // Boton para hacer la modificacion
            this.btnModificarProductoClicked.Clicked += async (object sender, EventArgs e) =>
            {
                if (!String.IsNullOrEmpty(txtClaveProducto.Text))
                {
                    // Instancia de un nuevo objeto
                    ProductModel producto = new ProductModel
                    {
                        // Recupera los datos
                        prodId = int.Parse(txtIDProducto.Text),
                        prodClave = txtClaveProducto.Text,
                        prodName = txtNombre.Text,
                        prodPrec = int.Parse(txtPrecio.Text),
                        prodCant = int.Parse(txtCantidad.Text),
                        provName = txtProveedor.Text,
                        provTel = txtProveedorTel.Text
                    };
                    // uestra un mensaje de espera
                    UserDialogs.Instance.ShowLoading("Modificando");
                    
                    // Los actualiza en la BDD
                    await App.SQLiteDB.SaveProductAsync(producto);
                    
                    await Task.Delay(2000);
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Modificado", "El producto se actualizó correctamente", "Aceptar");

                    // Limpia los campos una vez se hizo la modificacion
                    txtClaveProducto.Text = "";
                    txtNombre.Text = "";
                    txtPrecio.Text = "";
                    txtCantidad.Text = "";
                    txtProveedor.Text = "";
                    txtProveedorTel.Text = "";

                    // Inhabilita los campos tambien
                    txtClaveProducto.IsEnabled = false;
                    txtNombre.IsEnabled = false;
                    txtPrecio.IsEnabled = false;
                    txtCantidad.IsEnabled = false;
                    txtProveedor.IsEnabled = false;
                    txtProveedorTel.IsEnabled = false;

                    // Muestra el nuevo producto en la tabla
                    mostrarProductos();
                }
            };
        }

        // Metodo para mostrar los productos
        private async void mostrarProductos()
        {
            // Utiliza la funcion creaa en SQLiteHelper
            var productList = await App.SQLiteDB.GetProductAsync();
            if (productList != null)
            {
                // Llena los elementos del ListView segun los registros exstentes 
                ProductListView.ItemsSource = productList;
            }
        }

        /*
        Evento ItemSelected
        Al seleccionar un producto de la lista, lo busca en la BDD
        Cuando sabe que producto selecciona, deja los campos listos para actualizar
         */
        private async void ProductListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var productoSeleccionado = (ProductModel)e.SelectedItem;
            if (!String.IsNullOrEmpty(productoSeleccionado.prodClave.ToString()))
            {
                var producto = await App.SQLiteDB.GetProductByID(productoSeleccionado.prodId);
                if (producto != null)
                {
                    // Llena los campos con la informacion del producto seleccionado
                    this.txtIDProducto.Text = producto.prodId.ToString();
                    this.txtClaveProducto.Text = producto.prodClave.ToString();
                    this.txtNombre.Text = producto.prodName.ToString();
                    this.txtPrecio.Text = producto.prodPrec.ToString();
                    this.txtCantidad.Text = producto.prodCant.ToString();
                    this.txtProveedor.Text = producto.provName.ToString();
                    this.txtProveedorTel.Text = producto.provTel.ToString();

                    // Habilita los campos para poder modificarlos
                    txtClaveProducto.IsEnabled = true;
                    txtNombre.IsEnabled = true;
                    txtPrecio.IsEnabled = true;
                    txtCantidad.IsEnabled = true;
                    txtProveedor.IsEnabled = true;
                    txtProveedorTel.IsEnabled = true;
                }
            }
        }
    }
}