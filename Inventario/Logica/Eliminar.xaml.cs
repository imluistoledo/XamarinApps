using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// Importar libreria
using ZXing.Net.Mobile.Forms;

namespace Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Eliminar : ContentPage
    {
        public Eliminar()
        {
            InitializeComponent();

            // Eventos botones

            // Escanear QR
            this.btnEscanearQR.Clicked += (object sender, EventArgs e) =>
            {
                // Llama al scanner
                Scanner();
            };

            // Eliminar por Clave

            // Salir de la pagina
            this.btnSalirClicked.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PopAsync();
            };
        }

        string claveProductoScanneado;
        // Metodo del Scanner
        private async void Scanner()
        {
            var ScannerPage = new ZXingScannerPage();
            ScannerPage.Title = "Lector de códigos QR";
            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread( async () => 
                {
                    await Navigation.PopAsync();
                    claveProductoScanneado = result.Text.ToString();
                    bool eliminarProductoConfirmacion = await DisplayAlert("Confirmar producto", "¿Desea eliminar el producto con la clave " + claveProductoScanneado + "?", "Aceptar", "Cancelar");
                    if (eliminarProductoConfirmacion)
                    {
                        var producto = await App.SQLiteDB.GetProductByKey(claveProductoScanneado);
                        if (producto != null)
                        {
                            await App.SQLiteDB.DeleteProductAsync(claveProductoScanneado);
                            await DisplayAlert("Producto eliminado", "El producto con la clave " + claveProductoScanneado + " se ha eliminado correctamente", "Aceptar");
                            claveProductoScanneado = "";
                            eliminarProductoConfirmacion = false;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Cancelado", "Usted canceló la elminación del producto", "Aceptar");
                    }
                });
            };
            await Navigation.PushAsync(ScannerPage);
        }

        private void txtClaveProducto_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtClaveProducto.Text))
            {
                this.btnEliminarProductoClicked.IsEnabled = true;
            }
            else
            {
                this.btnEliminarProductoClicked.IsEnabled = false;
            }
        }

        private async void btnEliminarProductoClicked_Clicked(object sender, EventArgs e)
        {
            if (this.btnEliminarProductoClicked.IsEnabled)
            {
                claveProductoScanneado = this.txtClaveProducto.Text;
                bool eliminarProductoConfirmacion = await DisplayAlert("Confirmar producto", "¿Desea eliminar el producto con la clave " + claveProductoScanneado + "?", "Aceptar", "Cancelar");
                if (eliminarProductoConfirmacion)
                {
                    var producto = await App.SQLiteDB.GetProductByKey(claveProductoScanneado);
                    if (producto != null)
                    {
                        await App.SQLiteDB.DeleteProductAsync(claveProductoScanneado);
                        await DisplayAlert("Producto eliminado", "El producto con la clave " + claveProductoScanneado + " se ha eliminado correctamente", "Aceptar");
                        claveProductoScanneado = "";
                        eliminarProductoConfirmacion = false;
                    }
                }
                else
                {
                    await DisplayAlert("Cancelado", "Usted canceló la elminación del producto", "Aceptar");
                }
            }
        }
    }
}