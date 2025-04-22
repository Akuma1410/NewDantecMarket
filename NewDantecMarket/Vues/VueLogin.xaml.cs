using NewDantecMarket.Modeles;
using dantecMarket.Services;
using Microsoft.Maui.Controls;
using System;

namespace NewDantecMarket.Vues
{
    public partial class VueLogin : ContentPage
    {
        private readonly Apis _apis = new Apis();

        public VueLogin()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string inputEmail = EmailEntry.Text;
            string inputPassword = PasswordEntry.Text;

            bool isValidUser = await _apis.GetFindUser(inputEmail, inputPassword);

            if (isValidUser)
            {
                await DisplayAlert("Succès", "Connexion réussie", "OK");
                // Naviguez vers la page principale de l'application
                await Navigation.PushAsync(new VueCategorie());
            }
            else
            {
                await DisplayAlert("Erreur", "Email ou mot de passe incorrect", "OK");
            }
        }
    }
}
