using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewDantecMarket.Modeles;
using NewDantecMarket.Services;
using Newtonsoft.Json;

namespace dantecMarket.Services
{
    class Apis
    {
        #region attributs
        public readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://213.130.144.159") };
        #endregion

        #region Methodes
        public async Task<bool> GetFindUser(string email, string password)
        {
            try
            {
                // Vérification des paramètres
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("L'email et le mot de passe ne peuvent pas être vides.");
                }
                var postData = new
                {
                    Email = email,
                    Password = password
                };
                var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/mobile/GetFindUser", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Désérialiser la réponse en objet User
                    var user = JsonConvert.DeserializeObject<User>(responseContent);

                    // Stocker l'utilisateur connecté
                    SessionManager.SetCurrentUser(user);

                    return true;
                }
                else
                {
                    Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur réseau : {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
                return false;
            }
        }
        public async Task<List<Categorie>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/mobile/allcategoriesParent");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Désérialisation sécurisée
                    var categories = JsonConvert.DeserializeObject<List<Categorie>>(json);
                    if (categories == null)
                    {
                        throw new Exception("Les données retournées par l'API sont nulles.");
                    }

                    return categories;
                }
                else
                {
                    Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<Categorie>();
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur réseau : {ex.Message}");
                return new List<Categorie>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Erreur de désérialisation JSON : {ex.Message}");
                return new List<Categorie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
                return new List<Categorie>();
            }
        }

        public async Task<bool> AjouterProduitAuPanier(int userId, int produitId, int quantite, double prix)
        {
            try
            {
                // Vérification des paramètres
                if (userId <= 0 || produitId <= 0 || quantite <= 0)
                {
                    throw new ArgumentException("Les paramètres userId, produitId et quantite doivent être positifs.");
                }

                // Journalisation des données envoyées pour déboguer
                Console.WriteLine($"Ajout au panier - UserId: {userId}, ProduitId: {produitId}, Quantité: {quantite}, Prix: {prix}");

                var postData = new
                {
                    UserId = userId,
                    ProduitId = produitId,  // Notez la majuscule ici - pour correspondre au format attendu par l'API
                    Quantite = quantite,    // Notez la majuscule ici - pour correspondre au format attendu par l'API
                    Prix = prix             // Notez la majuscule ici - pour correspondre au format attendu par l'API
                };

                var json = JsonConvert.SerializeObject(postData);
                Console.WriteLine($"JSON envoyé: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/mobile/AjoutProduitCommandemobile", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Réponse API: {responseContent}");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erreur API: {response.StatusCode} - {response.ReasonPhrase}");
                    Console.WriteLine($"Contenu de l'erreur: {errorContent}");
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur réseau: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue: {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}
