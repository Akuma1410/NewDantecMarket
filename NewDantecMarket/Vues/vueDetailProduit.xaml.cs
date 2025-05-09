using NewDantecMarket.Modeles;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace NewDantecMarket.Vues
{
    public partial class VueDetailProduit : ContentPage
    {
        private readonly Produit _produit;

        public VueDetailProduit(Produit produit)
        {
            InitializeComponent();
            _produit = produit;

            // Charger les détails du produit
            ChargerDetailsProduit();
        }

        private void ChargerDetailsProduit()
        {
            try
            {
                // Définir le titre de la page avec le nom du produit
                Title = _produit.NomProduit;

                // Afficher les informations du produit
                NomProduitLabel.Text = _produit.NomProduit;
                PrixLabel.Text = $"{_produit.Prix:F2} €";
                QuantiteLabel.Text = $"Quantité disponible: {_produit.QuantiteDisponible}";

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
                Debug.WriteLine($"Erreur lors du chargement des détails du produit: {ex.Message}");
                DisplayAlert("Erreur", "Impossible de charger les détails du produit.", "OK");
            }
        }

        private async void OnAjouterPanierClicked(object sender, EventArgs e)
        {
            // Ici, vous intégrerez plus tard votre API pour ajouter le produit au panier
            // Pour l'instant, affichons un message simple
            await DisplayAlert("Produit ajouté", $"{_produit.NomProduit} a été ajouté à votre panier.", "OK");

            // NOTE: Vous pourrez remplacer ce code par l'appel à votre API
            // Exemple:
            // await VotreServicePanier.AjouterAuPanier(_produit.Id, 1);
        }
    }
}