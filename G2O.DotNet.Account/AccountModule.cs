// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountModule.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using G2O.DotNet.ApiInterceptorLayer;
    using G2O.DotNet.Database;
    using G2O.DotNet.Plugin;
    using G2O.DotNet.ServerApi;

    internal class AccountModule : IPlugin
    {
        private readonly Dictionary<IClientInterceptor, CommandAuthorization> commandAuthorizations =
            new Dictionary<IClientInterceptor, CommandAuthorization>();

        private readonly CommandCollection commandCollection;

        private readonly IDatabaseContextFactory contextFactory;

        private IAccountController accountController;

        [ImportingConstructor]
        public AccountModule(
            [Import(typeof(IServerInterceptor))] IServerInterceptor server,
            [ImportMany(typeof(ICommand))] IEnumerable<ICommand> commands,
            [Import(typeof(IDatabaseContextFactory))] IDatabaseContextFactory contextFactory)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.commandCollection = new CommandCollection(commands);
            server.Clients.ClientConnect += this.ClientsClientConnect;
            this.contextFactory = contextFactory;
            this.accountController = new AccountController(contextFactory);
            this.accountController.ClientLoggedIn += this.AccountControllerClientLoggedIn;
            this.accountController.ClientLoggedOut += this.AccountControllerClientLoggedOut;
        }

        public void PluginShutdown()
        {
            // If the account system gets shut down, remove all permissions from all players.
            foreach (var commandAuthorization in this.commandAuthorizations)
            {
                commandAuthorization.Value.Reset();
                commandAuthorization.Value.IsLoggedIn = false;
            }
            this.commandAuthorizations.Clear();
        }

        private void AccountControllerClientLoggedIn(object sender, LogedInOrOutEventArgs e)
        {
            IClientInterceptor client = (IClientInterceptor)e.Client;

            CommandAuthorization authorization = this.commandAuthorizations[client];
            authorization.IsLoggedIn = false;
        }

        private void AccountControllerClientLoggedOut(object sender, LogedInOrOutEventArgs e)
        {
            IClientInterceptor client = (IClientInterceptor)e.Client;

            // Most important remove all permissions if a client is logged out.
            CommandAuthorization authorization = this.commandAuthorizations[client];
            authorization.Reset();
            authorization.IsLoggedIn = false;
        }

        private void ClientDisconnect(object sender, ClientDisconnectedEventArgs e)
        {
            this.commandAuthorizations.Remove((IClientInterceptor)sender);
        }

        private void ClientsClientConnect(object sender, ClientConnectedEventArgs e)
        {
            IClientInterceptor client = (IClientInterceptor)e.NewClient;
            this.commandAuthorizations.Add(client, new CommandAuthorization(client, this.commandCollection));
            client.Disconnect += this.ClientDisconnect;
        }
    }
}