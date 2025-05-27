// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CMDPALSteam.Pages;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Windows.Media.Capture.Core;
using CMDPALSteam.Services;

namespace CMDPALSteam;


internal sealed partial class CMDPALSteamPage : ListPage
{
    private readonly TextSetting _APIKey;
    private readonly TextSetting _SteamID;
    private readonly SearchSteamPage _SearchPage;
    private readonly Settings _settings;
    public CMDPALSteamPage()
    {
        Icon = new IconInfo("\uE753");
        Title = "Steam for CMDPAL";
        Name = "Open";

        _settings = new Settings();

        _APIKey = new TextSetting("APIKey", "API Key", "Enter your Steam API Key", "Steam API Key");
        _SteamID = new TextSetting("SteamID", "Steam ID", "Enter your Steam ID64", "Steam ID");
        _settings.Add(_APIKey);
        _settings.Add(_SteamID);

        _SearchPage = new SearchSteamPage(_APIKey, _SteamID);
    }


    public override IListItem[] GetItems()
    {
        return new IListItem[]
        {
                new ListItem(_SearchPage)
                {
                    Title    = "Search Through Steam",
                    Subtitle = "Steam Search",
                    Icon     = new IconInfo("\uEC4A")
                },
                new ListItem(_settings.SettingsPage)
                {
                    Title    = "Settings",
                    Subtitle = "Configure your Steam API Key and Steam ID",
                    Icon     = new IconInfo("\uE713")
                }
        };
    }
}
