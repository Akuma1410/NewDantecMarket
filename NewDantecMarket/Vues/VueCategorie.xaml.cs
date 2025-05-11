using dantecMarket.Services;
using NewDantecMarket.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Vues
{
    public partial class VueCategorie : ContentPage
    {
        private readonly Apis _apiService;
        private List<Categorie> _categories;

        public VueCategorie()
        {
            InitializeComponent();
            _apiService = new Apis();


            // Ajouter le bouton panier dans la barre d'outils
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Panier",
                IconImageSource = "cart.png", // Si vous avez une icône de panier
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                Command = new Command(async () => await Navigation.PushAsync(new VuePanier()))
            });

            // Charger les catégories au démarrage
            ChargerCategories();
        }

        private async void ChargerCategories()
        {
            try
            {
                _categories = await _apiService.GetAllCategoriesAsync();
                CategoriesCollectionView.ItemsSource = _categories;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", $"Impossible de charger les catégories: {ex.Message}", "OK");
            }
        }

        private void OnCategoryTapped(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is Categorie categorie)
            {
                // Trouver le conteneur de sous-catégories associé à cette catégorie
                var stackLayout = label.Parent as StackLayout;
                if (stackLayout != null)
                {
                    var subCategoryContainer = stackLayout.FindByName<StackLayout>("SubCategoryContainer");
                    if (subCategoryContainer != null)
                    {
                        // Basculer la visibilité
                        subCategoryContainer.IsVisible = !subCategoryContainer.IsVisible;
                    }
                }
            }
        }

        private async void OnSubCategoryTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is SousCategorie sousCategorie)
            {
                // Naviguer vers la page des produits de cette sous-catégorie
                await Navigation.PushAsync(new VueProduits(sousCategorie.Nom, sousCategorie.LesProduits));
            }
        }
    }
}