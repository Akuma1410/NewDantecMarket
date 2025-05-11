using dantecMarket.Services;
using NewDantecMarket.Modeles;
using NewDantecMarket.Services;
using System.Collections.ObjectModel;

namespace NewDantecMarket.Vues
{
    public partial class VuePanier : ContentPage
    {
        private Apis _apiService;
        private ObservableCollection<PanierItemDetail> _panierItems;
        private double _totalPanier = 0;

        public VuePanier()
        {
            InitializeComponent();
            _apiService = new Apis();
            _panierItems = new ObservableCollection<PanierItemDetail>();
            PanierCollectionView.ItemsSource = _panierItems;

            // Chargement des articles du panier au démarrage de la page
            Loaded += VuePanier_Loaded;
        }

        private async void VuePanier_Loaded(object sender, EventArgs e)
        {
            await ChargerPanier();
        }

        // Méthode pour charger les articles du panier
        private async Task ChargerPanier()
        {
            if (UserSession.IsLoggedIn)
            {
                try
                {
                    var panierItems = await _apiService.GetPanierItemsAsync(UserSession.CurrentUser.Id);

                    _panierItems.Clear();
                    foreach (var item in panierItems)
                    {
                        _panierItems.Add(item);
                    }

                    // Calcul du total du panier
                    _totalPanier = panierItems.Sum(item => item.Total);
                    TotalPanierLabel.Text = $"{_totalPanier:F2} €";

                    // Afficher message si panier vide
                    EmptyCartLabel.IsVisible = _panierItems.Count == 0;
                    PanierCollectionView.IsVisible = _panierItems.Count > 0;
                    CommanderButton.IsEnabled = _panierItems.Count > 0;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erreur", $"Impossible de charger le panier: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Non connecté", "Veuillez vous connecter pour accéder à votre panier", "OK");
                await Shell.Current.GoToAsync("//login");
            }
        }

        // Handler pour le bouton "Valider la commande"
        protected async void OnCommanderClicked(object sender, EventArgs e)
        {
            if (_panierItems.Count > 0)
            {
                bool confirm = await DisplayAlert("Confirmation", "Souhaitez-vous valider votre commande ?", "Oui", "Non");

                if (confirm)
                {
                    // Ajouter ici l'appel API pour valider la commande
                    await DisplayAlert("Succès", "Votre commande a été validée avec succès!", "OK");

                    // Recharger le panier après validation
                    await ChargerPanier();
                }
            }
            else
            {
                await DisplayAlert("Panier vide", "Votre panier est vide", "OK");
            }
        }

        // Méthode pour rafraîchir le panier si l'utilisateur revient sur cette page
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ChargerPanier();
        }
    }
}