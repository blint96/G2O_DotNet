// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientList.cs" company="Colony Online Project">
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
    using System.Linq;

    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.ServerApi.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class ClientList : IClientList
    {
        private static readonly AnsiString StringGetMaxSlots = "getMaxSlots";

        private readonly IClient[] clients;

        private readonly ISquirrelApi squirrelApi;

        private readonly IServer server;

        public ClientList(ISquirrelApi squirrelApi,IServer server)
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
            //Register connect and disconnect handlers so the count value can be updated internally.
            this.ClientConnect += (sender,args)=> this.Count++;
            this.ClientDisconnect += (sender, args) => this.Count--;
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnect;

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect;
      
        public IEnumerable<IClient> Clients
        {
            get
            {
                return this.clients.Where(client => client != null);
            }
        }

        public int Count { get; private set; }

        public int MaxSlots => this.squirrelApi.Call<int>(StringGetMaxSlots);

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

        internal void OnClientConnect(int pid)
        {
            var newClient = new ClientSquirrel(this.squirrelApi, pid, this.server);
            this.clients[pid] = newClient;
            this.ClientConnect?.Invoke(this, new ClientConnectedEventArgs(newClient));
        }

        internal void OnClientDisconnect(ClientDisconnectedEventArgs e)
        {
            this.clients[e.Client.ClientId] = null;
            this.ClientDisconnect?.Invoke(this, e);
        }
    }
}