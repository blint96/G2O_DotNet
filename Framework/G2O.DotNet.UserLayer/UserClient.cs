// --------------------------------------------------------------------------------------------------------------------
// <copyright file="USerClient.cs" company="Colony Online Project">
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
    using System.Linq;
    using System.Net;

    using G2O.DotNet.Database;
    using G2O.DotNet.ServerApi;

    internal class UserClient : IClient
    {
        private readonly IClient orgClient;

        private readonly IDatabaseContextFactory contextFactory;

        internal UserClient(IClient orgClient, UserClientList clientList, IDatabaseContextFactory contextFactory)
        {
            if (orgClient == null)
            {
                throw new ArgumentNullException(nameof(orgClient));
            }
            if (clientList == null)
            {
                throw new ArgumentNullException(nameof(clientList));
            }
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.orgClient = orgClient;
            this.contextFactory = contextFactory;
            this.orgClient.CommandReceived += (o, a) => this.CommandReceived?.Invoke(this, a);
            this.orgClient.Disconnect +=
                (o, a) => this.Disconnect?.Invoke(this, new ClientDisconnectedEventArgs(this, a.Reason));
            this.orgClient.MessageReceived += (o, a) => this.MessageReceived?.Invoke(this, a);

            this.PlayerCharacter = new UserCharacter(orgClient.PlayerCharacter, this, clientList, contextFactory);
        }

        public int? AccountId
        {
            get
            {
                return this.accountId;
            }
            set
            {
                if (value != null && value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                if (value != null && this.LoadAccount(value.Value))
                {
                    this.accountId = value;
                }
                else
                {
                    this.accountId = null;
                }
            }
        }

        /// <summary>
        /// Loads all account specific data for a given account id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LoadAccount(int id)
        {
            using (var db = this.contextFactory.CreateContext())
            {
                AccountEntity account = db.Accounts.FirstOrDefault(obj => obj.AccountId == id);
                //Account with the given id does not exist.
                if (account == null)
                {
                    return false;
                }

                account.LastLoginTime = DateTime.Now;
                account.LastMACAddress = this.MacAddress;
                account.LastIpAddress = this.IpAddress.ToString();

                return true;
            }
        }

        private int? accountId;

        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        public event EventHandler<ClientDisconnectedEventArgs> Disconnect;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        public int ClientId => this.orgClient.ClientId;

        public IPAddress IpAddress => this.orgClient.IpAddress;

        public bool IsConnected => this.orgClient.IsConnected;

        public string MacAddress => this.orgClient.MacAddress;

        public string Nickname => this.orgClient.Nickname;

        public int Ping => this.orgClient.Ping;

        public ICharacter PlayerCharacter { get; }

        public string Serial => this.orgClient.Serial;

        public void Ban(string reason, int duration)
        {
            this.orgClient.Ban(reason, duration);
        }

        public void Kick(string reason)
        {
            this.orgClient.Kick(reason);
        }

        public void SendMessage(int r, int g, int b, string message)
        {
            this.orgClient.SendMessage(r, g, b, message);
        }

        public void SendMessageToAll(int r, int g, int b, string message)
        {
            this.orgClient.SendMessageToAll(r, g, b, message);
        }

        public void SendMessageToClient(IClient receiver, int r, int g, int b, string message)
        {
            this.orgClient.SendMessageToClient(receiver, r, g, b, message);
        }
    }
}