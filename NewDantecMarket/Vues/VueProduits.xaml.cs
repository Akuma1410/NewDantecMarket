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
            // Définir le titre avec le nom de la sous-catégorie
            CategoryTitle.Text = _sousCategorie.Nom;

            // LIGNE MANQUANTE: Ajouter l'événement de sélection
            ProduitsCollectionView.SelectionChanged += OnProduitSelected;

            // Charger les produits
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                // Vérifier si la sous-catégorie a des produits
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

        private async void OnProduitSelected(object sender, SelectionChangedEventArgs e)
        {
            // Récupérer le produit sélectionné
            if (e.CurrentSelection.FirstOrDefault() is Produit produitSelectionne)
            {
                // Réinitialiser la sélection
                ProduitsCollectionView.SelectedItem = null;
                // Naviguer vers la page de détail du produit
                await Navigation.PushAsync(new VueDetailProduit(produitSelectionne));
            }
        }
    }
}