// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountInUseException.cs" company="Colony Online Project">
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

    /// <summary>
    /// A class that describes a an error that occurs if someone tries to login on a account that is already in use(by another client).
    /// </summary>
    public class AccountInUseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountInUseException"/> class.
        /// </summary>
        /// <param name="accountName">The name of the account that is already logged in.</param>
        public AccountInUseException(string accountName)
            : base("The account '" + accountName + "' is allready logged in.")
        {
            this.AccountName = accountName;
        }

        /// <summary>
        /// Gets the name of the account that is already logged in.
        /// </summary>
        public string AccountName { get; }
    }
}