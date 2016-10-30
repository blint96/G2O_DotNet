// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequiresLogin.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Plugin
{
    using System;

    /// <summary>
    ///     A attribute class that allow the specify whether a command requires the invoking client to be logged in.
    /// </summary>
    public class RequiresLogin : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RequiresLogin" /> class.
        /// </summary>
        /// <param name="loginRequired">A value indicating whether attributed action requires the invoker to be logged in.</param>
        public RequiresLogin(bool loginRequired)
        {
            this.LoginRequired = loginRequired;
        }

        /// <summary>
        ///     Gets a value indicating whether attributed action requires the invoker to be logged in.
        /// </summary>
        public bool LoginRequired { get; }
    }
}