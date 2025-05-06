using NewDantecMarket.Modeles;
using System.Diagnostics;

namespace NewDantecMarket.Vues
{
    public partial class VueProduits : ContentPage
    {
        private readonly SousCategorie _sousCategorie;

        public VueProduits(SousCategorie sousCategorie)
        {
            InitializeComponent();
            _sousCategorie = sousCategorie;

            // D�finir le titre avec le nom de la sous-cat�gorie
            CategoryTitle.Text = _sousCategorie.Nom;

            // Charger les produits
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                // V�rifier si la sous-cat�gorie a des produits
                if (_sousCategorie.LesProduits != null && _sousCategorie.LesProduits.Count > 0)
                {
                    ProduitsCollectionView.ItemsSource = _sousCategorie.LesProduits;
                    NoProductsLabel.IsVisible = false;
                }
                else
                {
                    // Afficher un message s'il n'y a pas de produits
                    NoProductsLabel.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des produits: {ex.Message}");
                DisplayAlert("Erreur", "Impossible de charger les produits.", "OK");
            }
        }
    }
}