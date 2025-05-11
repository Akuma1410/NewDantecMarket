using dantecMarket.Services;
using NewDantecMarket.Modeles;
using NewDantecMarket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Vues
{
    public partial class VuePanier : ContentPage
    {
        private readonly Apis _apiService;
        private List<PanierItemDetail> _panierItems;
        private double _totalPanier = 0;

        public VuePanier()
        {
            InitializeComponent();
            _apiService = new Apis();

            // Charger le contenu du panier
            ChargerPanier();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Recharger le panier à chaque fois que la page apparaît
            ChargerPanier();
        }

        private async void ChargerPanier()
        {
            try
            {
                // Vérifier si l'utilisateur est connecté
                if (!UserSession.IsLoggedIn)
                {
                    await DisplayAlert("Attention", "Vous devez être connecté pour accéder à votre panier", "OK");
                    await Navigation.PushAsync(new VueLogin());
                    return;
                }

                // Afficher un indicateur de chargement
                IsBusy = true;

                // Récupérer les produits du panier
                _panierItems = await _apiService.GetPanierItemsAsync(UserSession.CurrentUser.Id);

                // Afficher les produits du panier ou un message si le panier est vide
                if (_panierItems != null && _panierItems.Count > 0)
                {
                    PanierCollectionView.ItemsSource = _panierItems;
                    EmptyCartLabel.IsVisible = false;
                    CommanderButton.IsEnabled = true;

                    // Calculer le total du panier
                    _totalPanier = _panierItems.Sum(item => item.Total);
                    TotalPanierLabel.Text = $"{_totalPanier:F2} €";
                }
                else
                {
                    EmptyCartLabel.IsVisible = true;
                    TotalPanierLabel.Text = "0.00 €";
                    CommanderButton.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger votre panier: {ex.Message}", "OK");
            }
            finally
            {
                // Masquer l'indicateur de chargement
                IsBusy = false;
            }
        }

        private async void OnCommanderClicked(object sender, EventArgs e)
        {
            // Cette méthode sera implémentée plus tard pour valider la commande
            await DisplayAlert("Information", "La fonctionnalité de validation de commande sera disponible prochainement", "OK");
        }
    }
}