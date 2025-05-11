using dantecMarket.Services;
using System.Diagnostics;

namespace NewDantecMarket.Vues
{
    public partial class VueLogin : ContentPage
    {
        private readonly Apis _apiService = new Apis();

        public VueLogin()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Erreur", "Veuillez entrer votre email et mot de passe.", "OK");
                return;
            }

            try
            {
                // Afficher l'indicateur de chargement
                LoadingIndicator.IsVisible = true;
                LoadingIndicator.IsRunning = true;

                // Appeler l'API pour vérifier les identifiants
                bool isLoginSuccessful = await _apiService.GetFindUser(email, password);

                if (isLoginSuccessful)
                {
                    // Rediriger vers la page des catégories
                    await Navigation.PushAsync(new VueCategorie());
                }
                else
                {
                    await DisplayAlert("Échec de connexion", "Email ou mot de passe incorrect.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur de connexion: {ex.Message}");
                await DisplayAlert("Erreur", "Une erreur s'est produite lors de la connexion.", "OK");
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