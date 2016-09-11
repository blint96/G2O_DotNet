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
namespace GothicOnline.G2.DotNet.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class ClientList : IClientList
    {
        private static readonly AnsiString StringGetMaxSlots = "getMaxSlots";

        private readonly IClient[] clients;

        private readonly ISquirrelApi squirrelApi;

        private G2OEventCallback clientConnectCallback;

        private G2OEventCallback clientDisconnectCallback;

        public ClientList(ISquirrelApi squirrelApi)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            this.squirrelApi = squirrelApi;
            this.clients = new IClient[this.MaxSlots];
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnect
        {
            add
            {
                if (this.clientConnectCallback == null)
                {
                    this.clientConnectCallback = new G2OEventCallback(
                        this.squirrelApi,
                        "onPlayerJoin", 
                        new Action<int>(this.ClientConnectFunction));
                }

                this.OnClientConnect += value;
            }

            remove
            {
                this.OnClientConnect -= value;
            }
        }

        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect
        {
            add
            {
                if (this.clientDisconnectCallback == null)
                {
                    this.clientDisconnectCallback = new G2OEventCallback(
                        this.squirrelApi, 
                        "onPlayerDisconnect", 
                        new Action<int,int>(this.ClientDisconnectFunction));
                }

                this.OnClientDisconnect += value;
            }

            remove
            {
                this.OnClientDisconnect -= value;
            }
        }

        private event EventHandler<ClientConnectedEventArgs> OnClientConnect;

        private event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnect;

        public IEnumerable<IClient> Clients
        {
            get
            {
                return this.clients.Where(client => client != null);
            }
        }

        public int Count { get; private set; }

        public int MaxSlots => this.squirrelApi.CallWithReturn<int>(StringGetMaxSlots);

        public IClient this[int clientId]
        {
            get
            {
                if (clientId <= 0 || clientId > this.clients.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(clientId));
                }

                return this.clients[clientId];
            }
        }

        private void ClientConnectFunction(int pid)
        {
            this.Count++;
            this.clients[pid] = new Client(this.squirrelApi, pid);
            this.OnClientConnect?.Invoke(this, new ClientConnectedEventArgs(this.clients[pid]));
        }

        private void ClientDisconnectFunction(int pid,int reason)
        {
            this.Count--;
            var client = this.clients[pid];
            this.clients[pid] = null;
            this.OnClientDisconnect?.Invoke(this, new ClientDisconnectedEventArgs(client,(DisconnectReason)reason));
        }
    }
}