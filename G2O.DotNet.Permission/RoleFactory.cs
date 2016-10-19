// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleFactory.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Permission
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    /// <summary>
    ///     The default implementation of the <see cref="IRoleFactory" /> interface.
    ///     <remarks>This class is a MEF component.</remarks>
    /// </summary>
    [Export(typeof(IRoleFactory))]
    internal class RoleFactory : IRoleFactory
    {
        /// <summary>
        ///     Stores all instances of <see cref="IRole" /> created by this instance of <see cref="RoleFactory" />.
        ///     <remarks>
        ///         This class is meant to be to be a MEF singleton so this should contain all instances of
        ///         <see cref="IRole" />.
        ///     </remarks>
        /// </summary>
        private readonly Dictionary<string, IRole> createdRoles = new Dictionary<string, IRole>();

        /// <summary>
        ///     Creates a new instance of <see cref="IRole" />.
        /// </summary>
        /// <param name="roleName">Name of the new role.</param>
        /// <param name="permissions">Enumerable of all permissions that are directly associated with the new role.</param>
        /// <param name="excludedPermissions">Enumerable of all permissions that are directly excluded from this role.</param>
        /// <param name="baseRoles">
        ///     Enumerable of all <see cref="IRole" /> objects that are base roles of the new role(the new role
        ///     will inherit the permissions of these roles.)
        /// </param>
        /// <returns>The newly created role.</returns>
        public IRole CreateRole(
            string roleName,
            IEnumerable<IPermission> permissions,
            IEnumerable<IPermission> excludedPermissions,
            IEnumerable<IRole> baseRoles)
        {
            if (permissions == null)
            {
                throw new ArgumentNullException(nameof(permissions));
            }

            if (excludedPermissions == null)
            {
                throw new ArgumentNullException(nameof(excludedPermissions));
            }

            if (baseRoles == null)
            {
                throw new ArgumentNullException(nameof(baseRoles));
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }

            var newRole = new Role(roleName, permissions, excludedPermissions, baseRoles);
            this.createdRoles.Add(roleName, newRole);
            return newRole;
        }

        /// <summary>
        ///     Checks if a <see cref="IRole" /> with a given name does exist.
        /// </summary>
        /// <param name="roleName">Name of the role that should be checked.</param>
        /// <returns>True if the role exists.</returns>
        public bool DoesRoleExist(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }

            return this.createdRoles.ContainsKey(roleName);
        }

        /// <summary>
        ///     Tries to get a <see cref="IRole" /> with the given name.
        /// </summary>
        /// <param name="roleName">Name of the <see cref="IRole" /> that should be searched.</param>
        /// <param name="value">The found <see cref="IRole" />.</param>
        /// <returns>True if a <see cref="IRole" /> with the given name was found. False if not</returns>
        public bool TryGetNamedRole(string roleName, out IRole value)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(roleName));
            }

            return this.createdRoles.TryGetValue(roleName, out value);
        }
    }
}