using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewDantecMarket.Modeles;
using Newtonsoft.Json;

namespace dantecMarket.Services
{
    public class Apis
    {
        #region attributs
        public readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://213.130.144.159") };
        #endregion

        #region Methodes
        public async Task<User> GetFindUserAsync(string email, string password)
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
                    var user = JsonConvert.DeserializeObject<User>(responseContent);
                    return user;
                }
                else
                {
                    Console.WriteLine($"Erreur API : {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erreur réseau : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
                return null;
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

        public async Task<bool> AjouterProduitAuPanierAsync(int userId, int produitId, int quantite, double prix)
        {
            try
            {
                // Création de l'objet panier
                var panierItem = new PanierItem(userId, produitId, quantite, prix);

                // Conversion en JSON
                var json = JsonConvert.SerializeObject(panierItem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Envoi de la requête
                var response = await _httpClient.PostAsync("/api/mobile/AjoutProduitCommandemobile", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erreur API : {response.StatusCode} - {responseContent}");
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
        #endregion
    }
}