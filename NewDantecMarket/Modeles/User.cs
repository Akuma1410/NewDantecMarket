using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Modeles
{
    class User
    {
        #region attributs
        private int id;
        private string email;
        private string userIdentifier;
        private string password;
        private string nom;
        private string prenom;
        private string telephone;
        private string classe;
        private int fidelite;
        #endregion

        #region constructeurs
        public User(int id, string email, string userIdentifier, string password, string nom, string prenom, string telephone, string classe, int fidelite)
        {
            this.id = id;
            this.email = email;
            this.userIdentifier = userIdentifier;
            this.password = password;
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
            this.classe = classe;
            this.fidelite = fidelite;
        }
        #endregion

        #region getters/setters
        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty("email")]
        public string Email { get => email; set => email = value; }
        [JsonProperty("userIdentifier")]
        public string UserIdentifier { get => userIdentifier; set => userIdentifier = value; }
        [JsonProperty("password")]
        public string Password { get => password; set => password = value; }
        [JsonProperty("nom")]
        public string Nom { get => nom; set => nom = value; }
        [JsonProperty("prenom")]
        public string Prenom { get => prenom; set => prenom = value; }
        [JsonProperty("telephone")]
        public string Telephone { get => telephone; set => telephone = value; }
        [JsonProperty("classe")]
        public string Classe { get => classe; set => classe = value; }
        [JsonProperty("fidelite")]
        public int Fidelite { get => fidelite; set => fidelite = value; }
        #endregion

        #region methodes
        public bool VerifyCredentials(string inputEmail, string inputPassword)
        {
            return this.email == inputEmail && this.password == inputPassword;
        }
        #endregion
    }
}
