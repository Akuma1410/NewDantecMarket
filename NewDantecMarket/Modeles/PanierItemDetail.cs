using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Modeles
{
    public class PanierItemDetail
    {
        #region Attributs
        private int id;
        private string nomProduit;
        private int quantite;
        private double prixretenu;
        private double total;
        private string imageUrl;
        #endregion

        #region Constructeurs
        public PanierItemDetail()
        {
            // Constructeur par défaut nécessaire pour la désérialisation JSON
        }
        #endregion

        #region Getters/Setters
        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }

        [JsonProperty("nomProduit")]
        public string NomProduit { get => nomProduit; set => nomProduit = value; }

        [JsonProperty("quantite")]
        public int Quantite { get => quantite; set => quantite = value; }

        [JsonProperty("prixretenu")]
        public double PrixRetenu { get => prixretenu; set => prixretenu = value; }

        [JsonProperty("total")]
        public double Total { get => total; set => total = value; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }

        // Propriété calculée pour l'URL complète de l'image
        [JsonIgnore]
        public string FullImageUrl => string.IsNullOrEmpty(ImageUrl) ? null : $"http://213.130.144.159/{ImageUrl}";
        #endregion
    }
}