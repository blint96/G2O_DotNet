// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientListInterceptor.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ApiInterceptorLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     A class that decorates the <see cref="IClientList" /> interface with events that allow listeners to be informed
    ///     about calls that change the method.
    /// </summary>
    internal class ClientListInterceptor : IClientListInterceptor
    {
        /// <summary>
        ///     Array that stores references to all <see cref="ClientInterceptor" /> objects.
        /// </summary>
        private readonly ClientInterceptor[] clients;

        /// <summary>
        ///     Stores a reference to the original <see cref="IClientList" /> instance that is decorated by the current
        ///     <see cref="ClientListInterceptor" /> object.
        /// </summary>
        private readonly IClientList orgClientList;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientListInterceptor" /> class.
        /// </summary>
        /// <param name="orgClientList">
        ///     The reference to the original <see cref="IClientList" /> instance that is decorated by the
        ///     new <see cref="ClientListInterceptor" /> object
        /// </param>
        internal ClientListInterceptor(IClientList orgClientList)
        {
            if (orgClientList == null)
            {
                throw new ArgumentNullException(nameof(orgClientList));
            }

            this.orgClientList = orgClientList;
            this.clients = new ClientInterceptor[orgClientList.MaxSlots];
        }

        /// <summary>
        ///     Invokes all registered handlers if a game client connects to the server.
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnect
        {
            add
            {
                this.orgClientList.ClientConnect += value;
            }

            remove
            {
                this.orgClientList.ClientConnect -= value;
            }
        }

        /// <summary>
        ///     Invokes all registered handlers if a game client disconnects from the server.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect
        {
            add
            {
                this.orgClientList.ClientDisconnect += value;
            }

            remove
            {
                this.orgClientList.ClientDisconnect -= value;
            }
        }

        /// <summary>
        ///     Gets a enumerable of all online clients.
        ///     <remarks>The order of the clients must not match their client id.</remarks>
        /// </summary>
        public IEnumerable<IClient> Clients => this.clients.ToList().AsReadOnly();

        /// <summary>
        ///     Gets the count of currently connected game clients.
        /// </summary>
        public int Count => this.orgClientList.Count;

        /// <summary>
        ///     Gets the maximum of game clients that can be connected to the server.
        /// </summary>
        public int MaxSlots => this.orgClientList.MaxSlots;

        /// <summary>
        ///     Gets a enumerable of all <see cref="IClientInterceptor" /> instances in the current
        ///     <see cref="IClientListInterceptor" /> instance.
        /// </summary>
        IEnumerable<IClientInterceptor> IClientListInterceptor.Clients => this.clients.ToList().AsReadOnly();

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
                if (clientId < 0 || this.clients.Length <= clientId)
                {
                    throw new ArgumentOutOfRangeException(nameof(clientId));
                }

                return this.clients[clientId];
            }
        }

        /// <summary>
        ///     Gets the <see cref="IClientInterceptor" /> object that decorates the <see cref="IClient" /> object with the given
        ///     client id.
        /// </summary>
        /// <param name="clientId">
        ///     Client id of the <see cref="IClient" /> object for which the <see cref="IClientInterceptor" />
        ///     object  should be returned.
        /// </param>
        /// <returns>
        ///     The <see cref="IClientInterceptor" /> object that decorates the <see cref="IClient" /> object with the given
        ///     client id
        /// </returns>
        IClientInterceptor IClientListInterceptor.this[int clientId]
        {
            get
            {
                if (clientId < 0 || clientId >= this.clients.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(clientId));
                }

                return this.clients[clientId];
            }
        }
    }
}