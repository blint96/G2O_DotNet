// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Role.cs" company="Colony Online Project">
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
    using System.Linq;

    /// <summary>
    /// A class that stores a list of permissions with combined with a name.
    /// </summary>
    internal class Role
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="name">Name of the new role.</param>
        /// <param name="permissions">Enumerable of all permissions that should be contained in the new role.</param>
        public Role(string name, IEnumerable<string> permissions)
        {
            if (permissions == null)
            {
                throw new ArgumentNullException(nameof(permissions));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
            this.Permissions = permissions.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a enumerable that contains all permissions of the current <see cref="Role"/> object.
        /// </summary>
        public IEnumerable<string> Permissions { get; }
    }
}