using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Modeles
{
    public class PanierItem
    {
        #region Attributs
        private int userId;
        private int produitId;
        private int quantite;
        private double prix;
        #endregion

        #region Constructeurs
        public PanierItem(int userId, int produitId, int quantite, double prix)
        {
            this.userId = userId;
            this.produitId = produitId;
            this.quantite = quantite;
            this.prix = prix;
        }
        #endregion

        #region Getters/Setters
        [JsonProperty("userId")]
        public int UserId { get => userId; set => userId = value; }

        [JsonProperty("produitId")]
        public int ProduitId { get => produitId; set => produitId = value; }

        [JsonProperty("quantite")]
        public int Quantite { get => quantite; set => quantite = value; }

        [JsonProperty("prix")]
        public double Prix { get => prix; set => prix = value; }
        #endregion
    }
}