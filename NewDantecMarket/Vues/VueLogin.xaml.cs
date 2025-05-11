using dantecMarket.Services;
using NewDantecMarket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Vues
{
    public partial class VueLogin : ContentPage
    {
        private readonly Apis _apiService;

        public VueLogin()
        {
            InitializeComponent();
            _apiService = new Apis();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Erreur", "Veuillez remplir tous les champs", "OK");
                return;
            }

            try
            {
                // Afficher l'indicateur de chargement
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Appeler l'API pour vérifier les identifiants
                var user = await _apiService.GetFindUserAsync(email, password);

                if (user != null)
                {
                    // Stocker l'utilisateur dans la session
                    UserSession.CurrentUser = user;

                    // Rediriger vers la page des catégories
                    await Navigation.PushAsync(new VueCategorie());
                }
                else
                {
                    await DisplayAlert("Erreur de connexion", "Email ou mot de passe incorrect", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur est survenue: {ex.Message}", "OK");
            }
            finally
            {
                // Cacher l'indicateur de chargement
                LoadingIndicator.IsVisible = false;
                LoadingIndicator.IsRunning = false;
            }
        }
    }
}