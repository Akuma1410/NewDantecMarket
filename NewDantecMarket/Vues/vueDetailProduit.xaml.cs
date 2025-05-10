using NewDantecMarket.Modeles;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using NewDantecMarket.Services;

namespace NewDantecMarket.Vues
{
    public partial class VueDetailProduit : ContentPage
    {
        private readonly Produit _produit;

        public VueDetailProduit(Produit produit)
        {
            InitializeComponent();
            _produit = produit;

            // Charger les d�tails du produit
            ChargerDetailsProduit();
        }

        private void ChargerDetailsProduit()
        {
            try
            {
                // D�finir le titre de la page avec le nom du produit
                Title = _produit.NomProduit;

                // Afficher les informations du produit
                NomProduitLabel.Text = _produit.NomProduit;
                PrixLabel.Text = $"{_produit.Prix:F2} �";
                QuantiteLabel.Text = $"Quantit� disponible: {_produit.QuantiteDisponible}";

                // Nettoyer la description (retirer les balises HTML)
                string descriptionNettoyee = _produit.Description;
                if (!string.IsNullOrEmpty(descriptionNettoyee))
                {
                    // Enlever les balises HTML simples
                    descriptionNettoyee = descriptionNettoyee.Replace("<div>", "")
                                                            .Replace("</div>", "")
                                                            .Replace("<br>", "\n")
                                                            .Replace("&nbsp;", " ");
                }

                DescriptionLabel.Text = descriptionNettoyee;

                // Charger l'image si disponible
                if (_produit.LesImages != null && _produit.LesImages.Count > 0)
                {
                    var premiereImage = _produit.LesImages.FirstOrDefault();
                    if (premiereImage != null && !string.IsNullOrEmpty(premiereImage.FullUrl))
                    {
                        ProduitImage.Source = premiereImage.FullUrl;
                        ProduitImage.IsVisible = true;
                    }
                    else
                    {
                        ProduitImage.IsVisible = false;
                    }
                }
                else
                {
                    ProduitImage.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des d�tails du produit: {ex.Message}");
                DisplayAlert("Erreur", "Impossible de charger les d�tails du produit.", "OK");
            }
        }

        private async void OnAjouterPanierClicked(object sender, EventArgs e)
        {
            try
            {
                // V�rifier si l'utilisateur est connect�
                if (!SessionManager.IsLoggedIn)
                {
                    await DisplayAlert("Connexion requise", "Veuillez vous connecter pour ajouter des produits au panier.", "OK");
                    // Rediriger vers la page de connexion
                    // await Navigation.PushAsync(new PageConnexion());
                    return;
                }

                // V�rifier que l'utilisateur a bien un ID valide
                if (SessionManager.CurrentUser == null || SessionManager.CurrentUser.Id <= 0)
                {
                    Debug.WriteLine("Erreur: ID utilisateur non valide ou manquant");
                    await DisplayAlert("Erreur", "Votre session semble invalide. Veuillez vous reconnecter.", "OK");
                    return;
                }

                // Afficher un indicateur de chargement
                AjouterPanierButton.Text = "Ajout en cours...";
                AjouterPanierButton.IsEnabled = false;

                // Journaliser les informations pour d�boguer
                Debug.WriteLine($"Tentative d'ajout au panier - UserId: {SessionManager.CurrentUser.Id}, ProduitId: {_produit.Id}, Prix: {_produit.Prix}");

                // Cr�er une instance de l'API
                var api = new dantecMarket.Services.Apis();

                // Appeler la m�thode pour ajouter au panier
                bool success = await api.AjouterProduitAuPanier(
                    SessionManager.CurrentUser.Id,
                    _produit.Id,
                    1, // quantit� par d�faut � 1
                    _produit.Prix
                );

                if (success)
                {
                    await DisplayAlert("Succ�s", $"{_produit.NomProduit} a �t� ajout� � votre panier.", "OK");
                }
                else
                {
                    await DisplayAlert("Erreur", "Impossible d'ajouter le produit au panier. Veuillez r�essayer plus tard.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de l'ajout au panier: {ex.Message}");
                await DisplayAlert("Erreur", $"Une erreur s'est produite: {ex.Message}", "OK");
            }
            finally
            {
                // Restaurer l'�tat du bouton
                AjouterPanierButton.Text = "Ajouter au panier";
                AjouterPanierButton.IsEnabled = true;
            }
        }
    }
}