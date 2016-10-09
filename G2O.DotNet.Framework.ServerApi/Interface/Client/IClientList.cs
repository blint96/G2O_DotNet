// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientList.cs" company="Colony Online Project">
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
// Interface for the classes that track the currently connected clients and allow access to the related server
// functions.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Client
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for the classes that track the currently connected clients and allow access to the related server
    ///     functions.
    /// </summary>
    public interface IClientList
    {
        /// <summary>
        ///     Invokes all registered handlers if a game client connects to the server.
        /// </summary>
        event EventHandler<ClientConnectedEventArgs> ClientConnect;

        /// <summary>
        ///     Invokes all registered handlers if a game client disconnects from the server.
        /// </summary>
        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect;

        /// <summary>
        ///     Gets a enumerable of all online clients.
        ///     <remarks>The order of the clients must not match their client id.</remarks>
        /// </summary>
        IEnumerable<IClient> Clients { get; }

        /// <summary>
        ///     Gets the count of currently connected game clients.
        /// </summary>
        int Count { get; }

        /// <summary>
        ///     Gets the maximum of game clients that can be connected to the server.
        /// </summary>
        int MaxSlots { get; }

        /// <summary>
        ///     Gets a <see cref="IClient" /> by its client id.
        ///     <remarks>Returns null if no client with the given id is online.</remarks>
        /// </summary>
        /// <param name="clientId">The client id for which the client should be returned</param>
        /// <returns>Returns the client that has the specified client id.</returns>
        IClient this[int clientId] { get; }
    }
}