// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountControler.cs" company="Colony Online Project">
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
    using System.Linq;

    using G2O.DotNet.Database;
    using G2O.DotNet.ServerApi;

    [Export(typeof(IAccountControler))]
    internal class AccountControler : IAccountControler
    {
        private readonly IDatabaseContextFactory contextFactory;

        private readonly PasswordHasher hasher = new PasswordHasher();

        private Dictionary<IClient, AccountEntity> LoggedInClients = new Dictionary<IClient, AccountEntity>();

        [ImportingConstructor]
        public AccountControler([Import(typeof(IDatabaseContextFactory))] IDatabaseContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public event EventHandler<LogedInOrOutEventArgs> ClientLoggedIn;

        public event EventHandler<LogedInOrOutEventArgs> ClientLoggedOut;

        public event EventHandler<AccountCreatedEventArgs> AccountCreated;

        public bool CheckAccountExists(string username)
        {
            using (var db = this.contextFactory.CreateContext())
            {
                return
                    db.Accounts.Any(ac => ac.AccountName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public bool CheckLogin(string username, string password)
        {
            using (var db = this.contextFactory.CreateContext())
            {
                AccountEntity account =
                    db.Accounts.FirstOrDefault(
                        ac => ac.AccountName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
                if (account == null)
                {
                    return false;
                }

                // Check if the stores password has equals the one computed now.
                return account.PasswordHash.Equals(this.hasher.HashPassword(password, account.PasswordSalt));
            }
        }

        public void CreateAccount(string username, string password)
        {
            using (var db = this.contextFactory.CreateContext())
            {
                AccountEntity account =
                    db.Accounts.FirstOrDefault(
                        ac => ac.AccountName.Equals(username, StringComparison.InvariantCultureIgnoreCase));

                // Check if account does allready exist
                if (account != null)
                {
                    throw new Exception("The account does allready exist");
                }

                if (!this.ValidatePassword(password))
                {
                    throw new ArgumentException("The given password is to weak.", nameof(password));
                }

                // Calculate password hash and salt.
                string salt = this.hasher.GenerateSalt();
                string hash = this.hasher.HashPassword(password, salt);

                // Create the new account
                var newAccount = new AccountEntity
                {
                    AccountName = username,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreationTime = DateTime.Now
                };
                db.Accounts.Add(newAccount);

                //Invoke the account created event.
                this.AccountCreated?.Invoke(this, new AccountCreatedEventArgs(newAccount));
            }
        }

        public void ForceLogin(string username, IClient client)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(username));
            }

            using (var db = this.contextFactory.CreateContext())
            {
                AccountEntity account =
                    db.Accounts.FirstOrDefault(
                        ac => ac.AccountName.Equals(username, StringComparison.InvariantCultureIgnoreCase));

                if (account == null)
                {
                    throw new ArgumentException("The given account does not exist", nameof(username));
                }

                // Check if the stores password has equals the one computed now.
                this.OnClientLoggedIn(account, client);
            }
        }

        public bool IsClientLoggedIn(IClient clientToCheck)
        {
            return this.LoggedInClients.ContainsKey(clientToCheck);
        }

        public void LogOut(IClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            this.OnClientLoggedOut(client);
        }

        public bool TryLogin(string username, string password, IClient client)
        {
            using (var db = this.contextFactory.CreateContext())
            {
                AccountEntity account =
                    db.Accounts.FirstOrDefault(
                        ac => ac.AccountName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
                if (account == null)
                {
                    return false;
                }

                // Check if the stores password has equals the one computed now.
                if (account.PasswordHash.Equals(this.hasher.HashPassword(password, account.PasswordSalt)))
                {
                    this.OnClientLoggedIn(account, client);
                    return true;
                }
            }

            // Login failed.
            return false;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            return password.Length >= 5 && password.All(char.IsLetterOrDigit) && password.Any(char.IsLetter)
                   && password.Any(char.IsDigit);
        }

        private void OnClientLoggedIn(AccountEntity account, IClient client)
        {
            if (this.LoggedInClients.ContainsKey(client))
            {
                throw new AlreadyLoggedInException();
            }
            if (this.LoggedInClients.ContainsValue(account))
            {
                throw new AccountInUseException(account.AccountName);
            }

            this.ClientLoggedIn?.Invoke(this, new LogedInOrOutEventArgs(account, client));
        }

        private void OnClientLoggedOut(IClient client)
        {
            AccountEntity account;
            if (!this.LoggedInClients.TryGetValue(client, out account))
            {
                throw new NotLoggedInException();
            }

            this.ClientLoggedOut?.Invoke(this, new LogedInOrOutEventArgs(account, client));
        }
    }
}