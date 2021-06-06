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
    public partial class Ver : ContentPage
    {
        public Ver()
        {
            InitializeComponent();
            this.btnSalir.Clicked += (sender, args) =>
            {
                Navigation.PopAsync();
            };
        }
        // Metodo para mostrar los productos
        private async void mostrarProductos(object sender, EventArgs e)
        {
            // Utiliza la funcion creaa en SQLiteHelper
            var productList = await App.SQLiteDB.GetProductAsync();
            if (productList != null)
            {
                // Llena los elementos del ListView segun los registros exstentes 
                ProductListView.ItemsSource = productList;
            }
        }
    }
}