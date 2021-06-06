using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace Inventario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Trabajador : ContentPage
    {
        public Trabajador()
        {
            InitializeComponent();
        }

        string txtID_Value = "";
        string txtPass_Value = "";
        public string tipoDeUsuaio = "Trabajador";

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtID_Value = "";
            txtPass_Value = "";
            txtID.Text = "";
            lblIDStatus.Text = "Error";
            lblIDStatus.TextColor = Color.Salmon;
            txtPass.Text = "";
            lblPassStatus.Text = "Error";
            lblPassStatus.TextColor = Color.Salmon;
        }

        private async void validate(String txt1, String txt2)
        {
            if (!String.IsNullOrEmpty(txt1) && !String.IsNullOrEmpty(txt2)
                && txt1.Length == 6 && txt2.Length == 10)
            {
                if (txt1 == "030322" && txt2 == "0123456789")
                {
                    await DisplayAlert("Usuario encontrado", "Bienvenido, Javier", "Continuar");
                    UserDialogs.Instance.ShowLoading("Cargando...");
                    await Task.Delay(3000);
                    UserDialogs.Instance.HideLoading();
                    // Navegar a la pagina
                    await Navigation.PushAsync(new Productos(tipoDeUsuaio));
                }
                else if (txt1 == "031105" && txt2 == "9876543210")
                {
                    await DisplayAlert("Usuario encontrado", "Bienvenido, Jesús", "Continuar");
                    UserDialogs.Instance.ShowLoading("Cargando...");
                    await Task.Delay(3000);
                    UserDialogs.Instance.HideLoading();
                    // Navegar a la pagina
                    await Navigation.PushAsync(new Productos(tipoDeUsuaio));
                }
            }
        }

        private async void txtPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPass_Value = txtPass.Text.ToString();
            if (txtPass.Text == "0123456789" || txtPass.Text == "9876543210")
            {
                lblPassStatus.Text = "Encontrado";
                lblPassStatus.TextColor = Color.PaleGreen;
                await Task.Delay(400);
            }
            else
            {
                lblPassStatus.Text = "Error";
                lblPassStatus.TextColor = Color.Salmon;
            }
            validate(txtID_Value, txtPass_Value);
        }

        private async void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtID_Value = txtID.Text.ToString();
            if (txtID.Text == "030322" || txtID.Text == "031105")
            {
                lblIDStatus.Text = "Encontrado";
                lblIDStatus.TextColor = Color.PaleGreen;
                await Task.Delay(400);
            }
            else
            {
                lblIDStatus.Text = "Error";
                lblIDStatus.TextColor = Color.Salmon;
            }
            validate(txtID_Value, txtPass_Value);
        }

        // Boton para cerrar la pagina
        private async void btnSalirClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}