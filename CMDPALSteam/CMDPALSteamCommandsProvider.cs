// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace CMDPALSteam;

public partial class CMDPALSteamCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public CMDPALSteamCommandsProvider()
    {
        DisplayName = "Steam for CMDPAL";
        Icon = new IconInfo("\uE753");
        _commands = [
            new CommandItem(new CMDPALSteamPage()) { Title = DisplayName },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
