using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Modeles
{
    public class Produit
    {

        #region Attributs
        private int id;
        private string nomProduit;
        private string description;
        private double prix;
        private int quantiteDisponible;
        private List<ImageProduit> lesImages;
        private string descriptioncourte;
        #endregion

        #region Constructeurs
        public Produit(int id, string nomProduit, string description, double prix, int quantiteDisponible, List<ImageProduit> lesImages, string descriptioncourte)
        {
            this.id = id;
            this.nomProduit = nomProduit;
            this.description = description;
            this.prix = prix;
            this.quantiteDisponible = quantiteDisponible;
            this.lesImages = lesImages;
            this.descriptioncourte = descriptioncourte;
        }
        #endregion

        #region Getters/Setters
        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty("nomProduit")]
        public string NomProduit { get => nomProduit; set => nomProduit = value; }
        [JsonProperty("description")]
        public string Description { get => description; set => description = value; }
        [JsonProperty("prix")]
        public double Prix { get => prix; set => prix = value; }
        [JsonProperty("quantiteDisponible")]
        public int QuantiteDisponible { get => quantiteDisponible; set => quantiteDisponible = value; }
        [JsonProperty("lesImages")]
        public List<ImageProduit> LesImages { get => lesImages; set => lesImages = value; }
        [JsonProperty("laCategorie")]
        public string DescriptionCourte { get => descriptioncourte; set => descriptioncourte = value; }
        #endregion

        #region methodes

        #endregion
    }

    public class ImageProduit
    {
        private string _url = string.Empty;

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url
        {
            get => _url;
            set => _url = value;
        }

        [JsonProperty("imageName")]
        public string ImageName { get; set; }

        [JsonIgnore] // Cette propriété ne sera pas sérialisée/désérialisée
        public string FullUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                    return string.Empty;

                if (_url.StartsWith("http://") || _url.StartsWith("https://"))
                    return _url;

                if (_url.StartsWith("/"))
                    return $"http://213.130.144.159{_url}";

                return $"http://213.130.144.159/{_url}";
            }
        }
    }
}
