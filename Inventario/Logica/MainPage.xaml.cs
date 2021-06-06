using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Inventario
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnAdministradorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Administrador());
        }

        private async void btnTrabajadorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Trabajador());
        }
    }
}
