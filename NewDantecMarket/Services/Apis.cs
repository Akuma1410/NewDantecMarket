using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewDantecMarket.Modeles;
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
                    // Vous pouvez traiter la réponse ici si nécessaire
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
        #endregion
    }
}
