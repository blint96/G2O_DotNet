// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountCreatedEventArgs.cs" company="Colony Online Project">
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

    using G2O.DotNet.Database;

    /// <summary>
    ///     EventArgs class for the account created event.
    /// </summary>
    public class AccountCreatedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AccountCreatedEventArgs" /> class.
        /// </summary>
        /// <param name="account">The database entity of the new account.</param>
        public AccountCreatedEventArgs(AccountEntity account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            this.Account = account;
        }

        /// <summary>
        ///     Gets the database entity of the new account.
        /// </summary>
        public AccountEntity Account { get; }
    }
}