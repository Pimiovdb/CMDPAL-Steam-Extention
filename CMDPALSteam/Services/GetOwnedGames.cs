using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMDPALSteam.Services
{
    public class GetOwnedGames
    {
        private readonly string _apiKey;
        private readonly string _steamId;
        private readonly HttpClient _http;

        public GetOwnedGames(string apiKey, string steamId, HttpClient httpClient = null)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("API Key is not set.", nameof(apiKey));

            if (string.IsNullOrWhiteSpace(steamId))
                throw new ArgumentException("Steam ID is not set.", nameof(steamId));

            _apiKey = apiKey;
            _steamId = steamId;
            _http = httpClient ?? new HttpClient();
        }

        public class OwnedGamesResponse
        {
            public OwnedGamesInnerResponse response { get; set; }
        }

        public class OwnedGamesInnerResponse
        {
            public int game_count { get; set; }
            public List<Game> games { get; set; }
        }

        public class Game
        {
            public int appid { get; set; }
            public string name { get; set; }
            public int playtime_forever { get; set; }
        }

        public async Task<List<Game>> GetOwnedGamesAsync()
        {
            var url = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={_steamId}&format=json&include_appinfo=true";
            using var resp = await _http.GetAsync(url);
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize(
                json,
                SteamJsonContext.Default.OwnedGamesResponse
            );
            return result?.response?.games ?? new List<Game>();
        }

    }

}
