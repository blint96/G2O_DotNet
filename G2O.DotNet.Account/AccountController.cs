// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Colony Online Project">
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


    /// <summary>
    /// Default implementation of the <see cref="IAccountController"/> interface.
    /// </summary>
    [Export(typeof(IAccountController))]
    internal class AccountController : IAccountController
    {
        /// <summary>
        /// The instance of <see cref="IDatabaseContextFactory"/> used to access the database.
        /// </summary>
        private readonly IDatabaseContextFactory contextFactory;

        /// <summary>
        /// Instance of <see cref="PasswordHasher"/> used to hash passwords and generated salts.
        /// </summary>
        private readonly PasswordHasher hasher = new PasswordHasher();

        /// <summary>
        /// Contains all logged in clients grouped with their account database entity.
        /// </summary>
        private readonly Dictionary<IClient, AccountEntity> loggedInClients = new Dictionary<IClient, AccountEntity>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="contextFactory">The instance of <see cref="IDatabaseContextFactory"/> that should be used to open database contexts.</param>
        [ImportingConstructor]
        public AccountController([Import(typeof(IDatabaseContextFactory))] IDatabaseContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        /// <summary>
        ///     Invokes all registered handlers when a client gets logged in.
        /// </summary>
        public event EventHandler<LogedInOrOutEventArgs> ClientLoggedIn;

        /// <summary>
        ///     Invokes all registered handlers when a client gets logged out.
        /// </summary>
        public event EventHandler<LogedInOrOutEventArgs> ClientLoggedOut;

        /// <summary>
        ///     Invokes all registered handlers when a new account is created.
        /// </summary>
        public event EventHandler<AccountCreatedEventArgs> AccountCreated;

        /// <summary>
        ///     Checks if a account with a given username exists.
        /// </summary>
        /// <param name="username">The username that should be checked.</param>
        /// <returns>True if a account with the given username does exist</returns>
        public bool CheckAccountExists(string username)
        {
            using (var db = this.contextFactory.CreateContext())
            {
                return
                    db.Accounts.Any(ac => ac.AccountName.Equals(username, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        /// <summary>
        ///     Checks if a pair of username and password is valid.
        /// </summary>
        /// <param name="username">The username of the account that should be checked.</param>
        /// <param name="password">The password of the account that should be checked.</param>
        /// <returns>True if the login data is correct. False if not.</returns>
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

        /// <summary>
        ///     Creates a new account with the given username and password.
        /// </summary>
        /// <param name="username">The username of the new account.</param>
        /// <param name="password">The password of the new account.</param>
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

        /// <summary>
        ///     Forces a client to be logged in to the account with the given name, ignoring the password of the account.
        /// </summary>
        /// <param name="username">The name of the account to which the client should be logged in.</param>
        /// <param name="client">The client that should be logged in.</param>
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

        /// <summary>
        ///     Checks if a given client is logged in.
        /// </summary>
        /// <param name="clientToCheck">The client that should be checked.</param>
        /// <returns>Returns true if the client is logged in, false if not.</returns>
        public bool IsClientLoggedIn(IClient clientToCheck)
        {
            return this.loggedInClients.ContainsKey(clientToCheck);
        }

        /// <summary>
        ///     Logs out a given client that is logged in.
        /// </summary>
        /// <param name="client">The client that should be logged out.</param>
        public void LogOut(IClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            this.OnClientLoggedOut(client);
        }

        /// <summary>
        ///     Tries to login a client.
        /// </summary>
        /// <param name="username">The username of the account that should be logged in.</param>
        /// <param name="password">The password of the account that should be logged in.</param>
        /// <param name="client">The client that should be logged in.</param>
        /// <returns>True if the login was successful, false if not</returns>
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

        /// <summary>
        ///     Checks if password fullfills the requirements of a save password.
        /// </summary>
        /// <param name="password">The password that should be validated.</param>
        /// <returns>True if the given string is a valid password, false if not.</returns>
        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            return password.Length >= 5 && password.All(char.IsLetterOrDigit) && password.Any(char.IsLetter)
                   && password.Any(char.IsDigit);
        }

        /// <summary>
        /// Invokes the ClientLoggedIn event and fills the <see cref="loggedInClients"/> dictionary.
        /// </summary>
        /// <param name="account">The account to which the client should be logged in.</param>
        /// <param name="client">The client that was logged in.</param>
        private void OnClientLoggedIn(AccountEntity account, IClient client)
        {
            if (this.loggedInClients.ContainsKey(client))
            {
                throw new AlreadyLoggedInException();
            }
            if (this.loggedInClients.ContainsValue(account))
            {
                throw new AccountInUseException(account.AccountName);
            }

            //Add the client/account to the dictionary of logged in clients
            this.loggedInClients.Add(client, account);

            this.ClientLoggedIn?.Invoke(this, new LogedInOrOutEventArgs(account, client));
        }

        /// <summary>
        /// Invokes the ClientLoggedOut event. Removes entries from the <see cref="loggedInClients"/> dictionary.
        /// </summary>
        /// <param name="client">The client that should be logged out.</param>
        private void OnClientLoggedOut(IClient client)
        {
            AccountEntity account;
            if (!this.loggedInClients.TryGetValue(client, out account))
            {
                throw new NotLoggedInException();
            }
            //Remove the client/account from the dictionary of logged in clients
            this.loggedInClients.Remove(client);

            this.ClientLoggedOut?.Invoke(this, new LogedInOrOutEventArgs(account, client));
        }
    }
}