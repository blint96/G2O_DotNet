// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAccountController.cs" company="Colony Online Project">
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

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     Interface for the account controller class which provides methods for managing the login status of a clients.
    /// </summary>
    public interface IAccountController
    {
        /// <summary>
        ///     Invokes all registered handlers when a new account is created.
        /// </summary>
        event EventHandler<AccountCreatedEventArgs> AccountCreated;

        /// <summary>
        ///     Invokes all registered handlers when a client gets logged in.
        /// </summary>
        event EventHandler<LogedInOrOutEventArgs> ClientLoggedIn;

        /// <summary>
        ///     Invokes all registered handlers when a client gets logged out.
        /// </summary>
        event EventHandler<LogedInOrOutEventArgs> ClientLoggedOut;

        /// <summary>
        ///     Checks if a account with a given username exists.
        /// </summary>
        /// <param name="username">The username that should be checked.</param>
        /// <returns>True if a account with the given username does exist</returns>
        bool CheckAccountExists(string username);

        /// <summary>
        ///     Checks if a pair of username and password is valid.
        /// </summary>
        /// <param name="username">The username of the account that should be checked.</param>
        /// <param name="password">The password of the account that should be checked.</param>
        /// <returns>True if the login data is correct. False if not.</returns>
        bool CheckLogin(string username, string password);

        /// <summary>
        ///     Creates a new account with the given username and password.
        /// </summary>
        /// <param name="username">The username of the new account.</param>
        /// <param name="password">The password of the new account.</param>
        void CreateAccount(string username, string password);

        /// <summary>
        ///     Forces a client to be logged in to the account with the given name, ignoring the password of the account.
        /// </summary>
        /// <param name="username">The name of the account to which the client should be logged in.</param>
        /// <param name="client">The client that should be logged in.</param>
        void ForceLogin(string username, IClient client);

        /// <summary>
        ///     Checks if a given client is logged in.
        /// </summary>
        /// <param name="clientToCheck">The client that should be checked.</param>
        /// <returns>Returns true if the client is logged in, false if not.</returns>
        bool IsClientLoggedIn(IClient clientToCheck);

        /// <summary>
        ///     Logs out a given client that is logged in.
        /// </summary>
        /// <param name="client">The client that should be logged out.</param>
        void LogOut(IClient client);

        /// <summary>
        ///     Tries to login a client.
        /// </summary>
        /// <param name="username">The username of the account that should be logged in.</param>
        /// <param name="password">The password of the account that should be logged in.</param>
        /// <param name="client">The client that should be logged in.</param>
        /// <returns>True if the login was successful, false if not</returns>
        bool TryLogin(string username, string password, IClient client);

        /// <summary>
        ///     Checks if password fullfills the requirements of a save password.
        /// </summary>
        /// <param name="password">The password that should be validated.</param>
        /// <returns>True if the given string is a valid password, false if not.</returns>
        bool ValidatePassword(string password);
    }
}