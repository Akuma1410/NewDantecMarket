using NewDantecMarket.Modeles;
using dantecMarket.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace NewDantecMarket.Vues
{
    public partial class VueCategorie : ContentPage
    {
        private readonly Apis _apis = new Apis();

        public VueCategorie()
        {
            InitializeComponent();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            var categories = await _apis.GetAllCategoriesAsync();
            CategoriesCollectionView.ItemsSource = categories;
        }

        // Gestion du tap sur une catégorie
        private void OnCategoryTapped(object sender, EventArgs e)
        {
            // Trouver le Label qui a été tapé
            var tappedLabel = (Label)sender;

            // Trouver le StackLayout parent
            var parentStackLayout = tappedLabel.Parent as StackLayout;

            // Trouver la StackLayout contenant les sous-catégories dans le parent
            var subCategoryContainer = parentStackLayout?.Children.OfType<StackLayout>().FirstOrDefault();

            // Si les sous-catégories existent, on inverse la visibilité
            if (subCategoryContainer != null)
            {
                subCategoryContainer.IsVisible = !subCategoryContainer.IsVisible;
            }
        }
    }
}
