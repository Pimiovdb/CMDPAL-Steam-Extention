// Bestand: Services/SteamJsonContext.cs
using System.Text.Json.Serialization;

namespace CMDPALSteam.Services
{
    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(GetOwnedGames.OwnedGamesResponse))]
    [JsonSerializable(typeof(GetOwnedGames.OwnedGamesInnerResponse))]
    [JsonSerializable(typeof(GetOwnedGames.Game))]
    internal partial class SteamJsonContext : JsonSerializerContext
    {
        // de compiler vult hier álles voor je in
    }
}
