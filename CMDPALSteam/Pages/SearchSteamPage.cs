using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using CMDPALSteam.Services;
using Windows.Media.AppBroadcasting;
using System.Diagnostics;

namespace CMDPALSteam.Pages
{
    internal sealed partial class SearchSteamPage : DynamicListPage
    {
        private readonly TextSetting _APIKey;
        private readonly TextSetting _SteamID;
        private List<IListItem> _items = new();
        public SearchSteamPage(TextSetting APIKey, TextSetting SteamID)
        {
            _APIKey = APIKey;
            _SteamID = SteamID;

            Title = "Search Steam";
            Icon = new IconInfo("\uE753");
            ShowDetails = true;
        }

        public override IListItem[] GetItems() => _items.ToArray();
        public override async void UpdateSearchText(string oldSearch, string newSearch)
        {
            var svc = new GetOwnedGames(_APIKey.Value, _SteamID.Value);

            if (string.IsNullOrEmpty(_APIKey.Value))
            {
                _items = new List<IListItem>
                {
                    new ListItem()
                    {
                        Title = "Geen API-key ingesteld",
                        Subtitle = "Voer een geldige API-key in om verder te gaan",
                        Icon = new IconInfo("\uE7BA")
                    }
                };
                RaiseItemsChanged(_items.Count);
            }
            if (string.IsNullOrEmpty(_SteamID.Value))
            {
                _items = new List<IListItem>
                {
                    new ListItem()
                    {
                        Title = "Geen SteamID ingesteld",
                        Subtitle = "Voer een geldige SteamID in om verder te gaan",
                        Icon = new IconInfo("\uE7BA")
                    }

                };
                RaiseItemsChanged(_items.Count);
            }
            try
            {
                var allGames = await svc.GetOwnedGamesAsync();
                var filtered = allGames.Where(g => string.IsNullOrWhiteSpace(newSearch) || g.name.Contains(newSearch, StringComparison.OrdinalIgnoreCase)).OrderByDescending(g => g.playtime_forever).ToList();

                _items = filtered
                    .Select(g =>
                    {
                        var launchgame = new AnonymousCommand(action: () =>
                        {
                            Process.Start(new ProcessStartInfo($"steam://rungameid/{g.appid}")
                            {
                                UseShellExecute = true
                            });
                        }){ Name = "Launch Game"};

                        return (IListItem)new ListItem()
                        {
                            Title = g.name,
                            Subtitle = $"Playtime: {g.playtime_forever} min",
                            Icon = new IconInfo($"https://cdn.akamai.steamstatic.com/steam/apps/{g.appid}/library_600x900.jpg"),
                            Command = launchgame,
                            TextToSuggest = g.name,
                            Tags = new Tag[]
                            {
                                new Tag($"AppID:{g.appid}"),
                            },
                            Details = new Details()
                            {
                                Title = g.name,
                                Body = $"Playtime: {g.playtime_forever} min \n AppID: {g.appid}",
                                HeroImage = new IconInfo($"https://cdn.akamai.steamstatic.com/steam/apps/{g.appid}/header.jpg")
                            },
                            MoreCommands = new IContextItem[] 
                            {
                                new CommandContextItem(new OpenUrlCommand($"https://store.steampowered.com/app/{g.appid}"))
                                {
                                    Title = "Open URL"
                                }
                            }
                        };
                    }).ToList();
                RaiseItemsChanged(_items.Count);
            }
            catch (Exception ex)
            {
                _items = new List<IListItem>
                {
                    new ListItem()
                    {
                        Title = "Error: ",
                        Subtitle = ex.Message,
                        Icon = new IconInfo("\uE7BA")
                    }
                };
            }
        }

    } 
}

