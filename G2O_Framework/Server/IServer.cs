// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServer.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Server
{
    using System;

    using GothicOnline.G2.DotNet.Client;
    using System.Drawing;

    /// <summary>
    ///     Interface for the server. Provides access to all methods that directly related to the server.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        ///     Invokes all registered handlers when the server gets initialized.
        /// </summary>
        event EventHandler<ClientConnectedEventArgs> Initialize;

        /// <summary>
        ///     Gets the client list.
        /// </summary>
        IClientList Clients { get; }

        /// <summary>
        ///     Gets or sets the server description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the server time.
        /// </summary>
        ServerTime Time { get; set; }

        /// <summary>
        ///     Gets or sets the server world.
        /// </summary>
        string World { get; set; }

        /// <summary>
        ///     Sends a message with a given color to all clients.
        /// </summary>
        /// <param name="color">The message color</param>
        /// <param name="message">The text of the message</param>
        void SendMessageToAll(Color color, string message);

        /// <summary>
        ///     Sends a message with a given color to all clients.
        /// </summary>
        /// <param name="r">Red value of the message color.</param>
        /// <param name="g">Green value of the message color.</param>
        /// <param name="b">Blue value of the message color.</param>
        /// <param name="message">The text of the message</param>
        void SendMessageToAll(int r,int g,int b, string message);


        /// <summary>
        ///     Sends a data packet to all clients.
        /// </summary>
        /// <param name="packet">The packet that should be send to all clients.</param>
        /// <param name="reliability">The reliability of the packet.</param>
        void SendPacketToAll(IPacket packet, PacketReliability reliability);
    }
}