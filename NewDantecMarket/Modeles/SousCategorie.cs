using NewDantecMarket.Modeles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Modeles
{
    public class SousCategorie
    {
        #region Attributs
        private int id;
        private string nom;
        private List<Produit> lesProduits;
        #endregion

        #region Constructeurs
        public SousCategorie(int id, string nom, List<Produit> lesProduits)
        {
            this.id = id;
            this.nom = nom;
            this.lesProduits = lesProduits;
        }
        #endregion

        #region Getters/Setters
        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }

        [JsonProperty("nom")]
        public string Nom { get => nom; set => nom = value; }

        [JsonProperty("lesProduits")]
        public List<Produit> LesProduits { get => lesProduits; set => lesProduits = value; }
        #endregion
    }
}
