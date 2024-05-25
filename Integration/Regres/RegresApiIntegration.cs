using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiliconValley.Integration.Regres.dto;

namespace SiliconValley.Integration.Regres
{
    public class RegresApiIntegration
    {
        private readonly ILogger<RegresApiIntegration> _logger;
        private const string API_URL = "https://reqres.in/api/users";

        public RegresApiIntegration(ILogger<RegresApiIntegration> logger)
        {
            _logger = logger;
        }

        public async Task<List<Users>> GetAll()
        {
            string requestUrl = $"{API_URL}";
            List<Users> listUsers = new List<Users>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(json);                    
                    var usersArray = jsonObject["data"].ToObject<List<Users>>();
                     return usersArray;
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
                }
            }
            return listUsers;
        }
    }
}