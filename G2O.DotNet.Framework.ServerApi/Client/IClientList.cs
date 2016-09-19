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
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.ServerApi.Client
{
    using System;
    using System.Collections.Generic;

    public interface IClientList
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnect;

        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect;

        int Count { get; }

        int MaxSlots { get; }

        /// <summary>
        /// Gets a enumerable of all online clients.
        /// <remarks>The order of the clients must not match their client id.</remarks>
        /// </summary>
        IEnumerable<IClient> Clients { get; }

        /// <summary>
        /// Gets a <see cref="IClient"/> by its client id.
        /// <remarks>Returns null if no client with the given id is online.</remarks>
        /// </summary>
        /// <param name="clientId">The client id for which the client should be returned</param>
        /// <returns></returns>
        IClient this[int clientId] { get; }
    }
}