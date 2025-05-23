using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

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
        }

        public override IListItem[] GetItems() => _items.ToArray();
        public override void UpdateSearchText(string oldSearch, string newSearch) 
        {
            if (_APIKey == null) 
            { 
                string APIKey = _APIKey.Value;
                string SteamID = _SteamID.Value;

                try 
                {
                    if (string.IsNullOrEmpty(APIKey))
                    {
                        throw new Exception("API Key is not set.");
                        
                    }
                    if (string.IsNullOrEmpty(SteamID))
                    {
                        throw new Exception("Steam ID is not set.");
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
