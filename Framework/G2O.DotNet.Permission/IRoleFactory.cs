// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleFactory.cs" company="Colony Online Project">
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
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Primitives;

    /// <summary>
    ///     Interface for the role factory class.
    ///     <remarks>Provides methods for creating and searching roles.</remarks>
    /// </summary>
    public interface IRoleFactory
    {
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
        IRole CreateRole(
            string roleName,
            IEnumerable<IPermission> permissions,
            IEnumerable<IPermission> excludedPermissions,
            IEnumerable<IRole> baseRoles);

        /// <summary>
        ///     Checks if a <see cref="IRole" /> with a given name does exist.
        /// </summary>
        /// <param name="roleName">Name of the role that should be checked.</param>
        /// <returns>True if the role exists.</returns>
        bool DoesRoleExist(string roleName);

        /// <summary>
        ///     Tries to get a <see cref="IRole" /> with the given name.
        /// </summary>
        /// <param name="roleName">Name of the <see cref="IRole" /> that should be searched.</param>
        /// <param name="value">The found <see cref="IRole" />.</param>
        /// <returns>True if a <see cref="IRole" /> with the given name was found. False if not</returns>
        bool TryGetNamedRole(string roleName, out IRole value);
    }
}