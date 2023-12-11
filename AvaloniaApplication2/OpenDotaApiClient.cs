using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AvaloniaApplication2
{
    public class OpenDotaApiClient
    {
        private readonly HttpClient httpClient;
        private const string OpenDotaApiBaseUrl = "https://api.opendota.com/api/";
        

        public async Task<PlayerInfo> GetPlayerInfoAsync(long accountId)
        {
            var requestUri = $"{OpenDotaApiBaseUrl}players/{accountId}";
            var response = await httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var playerInfo = JsonSerializer.Deserialize<PlayerInfo>(jsonContent);
                return playerInfo;
            }

            return null;
        }
    }

    public class PlayerInfo
    {
        public string ProfileUrl { get; set; }
        public string Personaname { get; set; }
        public int SoloCompetitiveRank { get; set; }
        // Другие свойства, которые вам нужны
    }
}