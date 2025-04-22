using NewDantecMarket.Modeles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Modeles
{
    public class Categorie
    {
        #region Attributs
        private int id;
        private string nom;
        private List<SousCategorie> lesCategories;
        #endregion

        #region Constructeurs
        public Categorie(int id, string nom, List<SousCategorie> lesCategories)
        {
            this.id = id;
            this.nom = nom;
            this.lesCategories = lesCategories;
        }
        #endregion

        #region Getters/Setters
        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }

        [JsonProperty("nom")]
        public string Nom { get => nom; set => nom = value; }

        [JsonProperty("lescategories")]
        public List<SousCategorie> LesCategories { get => lesCategories; set => lesCategories = value; }
        #endregion

        #region methodes

        #endregion
    }
}
