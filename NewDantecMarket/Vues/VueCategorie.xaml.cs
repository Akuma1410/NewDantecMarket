using NewDantecMarket.Modeles;
using System.Diagnostics;
using dantecMarket.Services;

namespace NewDantecMarket.Vues
{
    public partial class VueCategorie : ContentPage
    {
        private readonly Apis _apiService = new Apis();
        private List<Categorie> _categories;

        public VueCategorie()
        {
            InitializeComponent();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            try
            {
                _categories = await _apiService.GetAllCategoriesAsync();
                CategoriesCollectionView.ItemsSource = _categories;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des catégories: {ex.Message}");
                await DisplayAlert("Erreur", "Impossible de charger les catégories.", "OK");
            }
        }

        private void OnCategoryTapped(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is Categorie category)
            {
                var parentStackLayout = label.Parent as StackLayout;
                var subCategoryContainer = parentStackLayout?.FindByName<StackLayout>("SubCategoryContainer");

                if (subCategoryContainer != null)
                {
                    // Inverse la visibilité du conteneur de sous-catégories
                    subCategoryContainer.IsVisible = !subCategoryContainer.IsVisible;
                }
            }
        }

        private async void OnSubCategoryTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is SousCategorie sousCategorie)
            {
                // Naviguer vers la page des produits avec la sous-catégorie sélectionnée
                await Navigation.PushAsync(new VueProduits(sousCategorie));
            }
        }
    }
}