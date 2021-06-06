using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// Importar librerias
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Acr.UserDialogs;

namespace Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Administrador : ContentPage
    {
        public Administrador()
        {
            InitializeComponent();
        }

        public string tipoDeUsuaio = "Administrador";
        private async void btnIngresarHuellaClicked(object sender, EventArgs e)
        {
            // Verifica si esta disponible
            var disponible = await CrossFingerprint.Current.IsAvailableAsync();
            if (!disponible)
            {
                await DisplayAlert("¡Oops!","Parece que no está disponible","Aceptar");
                return;
            }
            // Obtiene el resultado de la autenticacion
            var autorizacion = await CrossFingerprint.Current.AuthenticateAsync
                (new AuthenticationRequestConfiguration("Verificación", "Para continuar, ponga su dedo en el sensor para verificar su huella"));
            // Valida que se haya dado permiso
            if (autorizacion.Authenticated)
            {
                UserDialogs.Instance.ShowLoading("Cargando...");
                await Task.Delay(3000);
                UserDialogs.Instance.HideLoading();
                await Navigation.PushAsync(new Productos(tipoDeUsuaio));
                //await DisplayAlert("Permiso concedido", "Gracias por conceder su permiso", "Aceptar");
            }
        }
        // Boton para cerrar la pagina
        private async void btnSalirClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}