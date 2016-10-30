// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlreadyLoggedInException.cs" company="Colony Online Project">
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
    ///     A class that describes an error that occurs if a already logged in user tries to login again.
    /// </summary>
    public class AlreadyLoggedInException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AlreadyLoggedInException" /> class.
        /// </summary>
        public AlreadyLoggedInException()
            : base("An already logged in client tried to login again.")
        {
        }
    }
}