// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserClientList.cs" company="Colony Online Project">
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
namespace G2O.DotNet.UserLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using G2O.DotNet.Database;
    using G2O.DotNet.ServerApi;

    internal class UserClientList : IClientList
    {
        private readonly UserClient[] clients;

        private readonly IClientList orgClientList;

        private readonly IDatabaseContextFactory contextFactory;

        internal UserClientList(IClientList orgClientList, IDatabaseContextFactory contextFactory)
        {
            if (orgClientList == null)
            {
                throw new ArgumentNullException(nameof(orgClientList));
            }
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.orgClientList = orgClientList;
            this.contextFactory = contextFactory;
            this.clients = new UserClient[orgClientList.MaxSlots];
            this.orgClientList.ClientConnect += this.OrgClientListClientConnect;
            this.orgClientList.ClientDisconnect += this.OrgClientListClientDisconnect;
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnect;

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect;

        public IEnumerable<IClient> Clients => this.clients.ToList().AsReadOnly();

        public int Count => this.orgClientList.Count;

        public int MaxSlots => this.orgClientList.MaxSlots;

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

        private void OrgClientListClientConnect(object sender, ClientConnectedEventArgs e)
        {
            var newUserClient = new UserClient(e.NewClient, this, this.contextFactory);
            this.clients[e.NewClient.ClientId] = newUserClient;
            this.ClientConnect?.Invoke(this, new ClientConnectedEventArgs(newUserClient));
        }

        private void OrgClientListClientDisconnect(object sender, ClientDisconnectedEventArgs e)
        {
            this.ClientDisconnect?.Invoke(
                this,
                new ClientDisconnectedEventArgs(this.clients[e.Client.ClientId], e.Reason));
        }
    }
}