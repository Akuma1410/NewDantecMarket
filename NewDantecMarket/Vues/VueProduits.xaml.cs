using NewDantecMarket.Modeles;

namespace NewDantecMarket.Vues
{
    public partial class VueProduits : ContentPage
    {
        private List<Produit> _produits;

        public VueProduits(string categoryName, List<Produit> produits)
        {
            InitializeComponent();

            // D�finir le titre de la page
            CategoryTitle.Text = categoryName;

            // Stocker les produits
            _produits = produits;

            // Afficher les produits ou un message si la liste est vide
            if (produits != null && produits.Count > 0)
            {
                ProduitsCollectionView.ItemsSource = produits;
                NoProductsLabel.IsVisible = false;
            }
            else
            {
                NoProductsLabel.IsVisible = true;
            }

            // Ajouter un gestionnaire d'�v�nements pour la s�lection d'un produit
            ProduitsCollectionView.SelectionChanged += OnProduitSelectionChanged;
        }

        // Bouton panier dans la barre d'outils
        private async void OnPanierClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VuePanier());
        }

        private async void OnProduitSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // V�rifier si un �l�ment a �t� s�lectionn�
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                // R�cup�rer le produit s�lectionn�
                var produitSelectionne = e.CurrentSelection[0] as Produit;

                // R�initialiser la s�lection
                ProduitsCollectionView.SelectedItem = null;

                // Naviguer vers la page de d�tail du produit
                if (produitSelectionne != null)
                {
                    await Navigation.PushAsync(new VueDetailProduit(produitSelectionne));
                }
            }
        }
    }
}