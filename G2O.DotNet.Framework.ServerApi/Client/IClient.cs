// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClient.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  Julian Vogel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// -
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// -
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// -
// </copyright>
// <summary>
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.ServerApi.Client
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;

    using GothicOnline.G2.DotNet.ServerApi.Character;
    using GothicOnline.G2.DotNet.ServerApi.Server;

    public interface IClient
    {
        event EventHandler<ClientDisconnectedEventArgs> Disconnect;

        event EventHandler<PacketReceivedEventArgs> PacketReceived;

        event EventHandler<CommandReceivedEventArgs> CommandReceived;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        int ClientId { get; }

        IPAddress IpAddress { get; }

        bool IsConnected { get; }

        string MacAddress { get; }

        /// <summary>
        ///     Gets the nickname that was specified by the user before connecting.
        /// </summary>
        string Nickname { get; }

        ICharacter PlayerCharacter { get; }

        string Serial { get; }

        void SendMessage(int r, int g, int b, string message);

        void SendMessageToAll(int r, int g, int b, string message);

        void SendMessageToClient(IClient receiver, int r, int g, int b, string message);
    }
}