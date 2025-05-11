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
    public partial class VueDetailProduit : ContentPage
    {
        private readonly Apis _apiService;
        private Produit _produitCourant;
        private bool _isAddingToCart = false;

        public VueDetailProduit(Produit produit)
        {
            InitializeComponent();
            _apiService = new Apis();
            _produitCourant = produit;

            // Afficher les d�tails du produit
            AfficherDetailsProduit();
        }

        private void AfficherDetailsProduit()
        {
            // Afficher le nom du produit dans le titre
            Title = _produitCourant.NomProduit;
            NomProduitLabel.Text = _produitCourant.NomProduit;

            // Afficher le prix
            PrixLabel.Text = $"{_produitCourant.Prix:F2} �";

            // Afficher la quantit� disponible
            QuantiteLabel.Text = $"Quantit� disponible: {_produitCourant.QuantiteDisponible}";

            // Afficher la description
            DescriptionLabel.Text = _produitCourant.Description;

            // Afficher l'image si disponible
            if (_produitCourant.LesImages != null && _produitCourant.LesImages.Count > 0)
            {
                ProduitImage.Source = _produitCourant.LesImages[0].FullUrl;
            }
        }

        private async void OnAjouterPanierClicked(object sender, EventArgs e)
        {
            // �viter les clics multiples
            if (_isAddingToCart)
                return;

            _isAddingToCart = true;

            try
            {
                // V�rifier si l'utilisateur est connect�
                if (!UserSession.IsLoggedIn)
                {
                    await DisplayAlert("Attention", "Vous devez �tre connect� pour ajouter des produits au panier", "OK");
                    await Navigation.PushAsync(new VueLogin());
                    return;
                }

                // V�rifier s'il y a du stock disponible
                if (_produitCourant.QuantiteDisponible <= 0)
                {
                    await DisplayAlert("Stock �puis�", "Ce produit n'est plus disponible en stock", "OK");
                    return;
                }

                // Modifier le texte du bouton pour montrer que l'action est en cours
                AjouterPanierButton.Text = "Ajout en cours...";
                AjouterPanierButton.IsEnabled = false;

                // Ajouter le produit au panier via l'API
                bool succes = await _apiService.AjouterProduitAuPanierAsync(
                    UserSession.CurrentUser.Id,
                    _produitCourant.Id,
                    1, // Quantit� par d�faut
                    _produitCourant.Prix
                );

                if (succes)
                {
                    await DisplayAlert("Succ�s", "Le produit a �t� ajout� � votre panier", "OK");
                }
                else
                {
                    await DisplayAlert("Erreur", "Impossible d'ajouter le produit au panier", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Une erreur est survenue: {ex.Message}", "OK");
            }
            finally
            {
                // Remettre le bouton � son �tat initial
                AjouterPanierButton.Text = "Ajouter au panier";
                AjouterPanierButton.IsEnabled = true;
                _isAddingToCart = false;
            }
        }
    }
}