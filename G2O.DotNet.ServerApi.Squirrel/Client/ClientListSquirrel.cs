// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientListSquirrel.cs" company="Colony Online Project">
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
// A implementation of the IClientList interface that uses the squirrel api to access the server functions.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Squirrel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using G2O.DotNet.Squirrel;

    /// <summary>
    ///     A implementation of the <see cref="IClientList" /> interface that uses the squirrel api to access the server
    ///     functions.
    /// </summary>
    internal class ClientListSquirrel : IClientList
    {
        /// <summary>
        ///     Stores the ANSI version of the string "getMaxSlots"
        /// </summary>
        private static readonly AnsiString StringGetMaxSlots = "getMaxSlots";

        /// <summary>
        ///     The internal storage array for used for the fast lookup based on the client id.
        /// </summary>
        private readonly IClient[] clients;

        /// <summary>
        ///     Reference to the instance of the server(needed for event invocation)
        /// </summary>
        private readonly IServer server;

        /// <summary>
        ///     The instance of the squirrel api used to access the server functions.
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientListSquirrel" /> class.
        /// </summary>
        /// <param name="squirrelApi">The instance of the squirrel api that should be used to access the server functions.</param>
        /// <param name="server">The instance of the server.</param>
        public ClientListSquirrel(ISquirrelApi squirrelApi, IServer server)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            this.squirrelApi = squirrelApi;
            this.server = server;
            this.clients = new IClient[this.MaxSlots];

            // Register connect and disconnect handlers so the count value can be updated internally.
            this.ClientConnect += (sender, args) => this.Count++;
            this.ClientDisconnect += (sender, args) => this.Count--;
        }

        /// <summary>
        ///     Invokes all registered handlers if a game client connects to the server.
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnect;

        /// <summary>
        ///     Invokes all registered handlers if a game client disconnects from the server.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect;

        /// <summary>
        ///     Gets a enumerable of all online clients.
        ///     <remarks>The order of the clients must not match their client id.</remarks>
        /// </summary>
        public IEnumerable<IClient> Clients
        {
            get
            {
                return this.clients.Where(client => client != null);
            }
        }

        /// <summary>
        ///     Gets the count of currently connected game clients.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        ///     Gets the maximum of game clients that can be connected to the server.
        /// </summary>
        public int MaxSlots => this.squirrelApi.Call<int>(StringGetMaxSlots);

        /// <summary>
        ///     Gets a <see cref="IClient" /> by its client id.
        ///     <remarks>Returns null if no client with the given id is online.</remarks>
        /// </summary>
        /// <param name="clientId">The client id for which the client should be returned</param>
        /// <returns>Returns the client that has the specified client id.</returns>
        public IClient this[int clientId]
        {
            get
            {
                if (clientId < 0 || clientId > this.clients.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(clientId));
                }

                return this.clients[clientId];
            }
        }

        /// <summary>
        ///     Invokes the ClientConnect event.
        /// </summary>
        /// <param name="pid">The client id(player id) of the client that has connected.</param>
        internal void OnClientConnect(int pid)
        {
            var newClient = new ClientSquirrel(this.squirrelApi, pid, this.server);
            this.clients[pid] = newClient;
            this.ClientConnect?.Invoke(this, new ClientConnectedEventArgs(newClient));
        }

        /// <summary>
        ///     Invokes the ClientDisconnect event.
        /// </summary>
        /// <param name="e"><see cref="ClientDisconnectedEventArgs" /> object.</param>
        internal void OnClientDisconnect(ClientDisconnectedEventArgs e)
        {
            this.clients[e.Client.ClientId] = null;
            this.ClientDisconnect?.Invoke(this, e);
        }
    }
}