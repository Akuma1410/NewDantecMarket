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
                return false;
            }
        }
        public async Task<List<Categorie>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("/api/mobile/allcategoriesParent");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Categorie>>(json);
            }
            return new List<Categorie>();
        }
        #endregion
    }
}
