// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRole.cs" company="Colony Online Project">
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
//  Interface for the role classes.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.Permission
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for the role classes.
    ///     <remarks>
    ///         A role is a group of permissions that can be assigned to a user. A role can consist of multiple
    ///         permissions and other roles.
    ///     </remarks>
    /// </summary>
    public interface IRole
    {
        /// <summary>
        ///     Gets all base roles of the current <see cref="IRole" /> object.
        /// </summary>
        IEnumerable<IRole> BaseRoles { get; }

        /// <summary>
        ///     Gets all Permissions directly excluded from this role.
        ///     <remarks>Excluded permission suppress existing permission ownership(e.g. from inheritance)</remarks>
        /// </summary>
        IEnumerable<IPermission> ExcludedPermissions { get; }

        /// <summary>
        ///     Gets the name of the <see cref="IRole" />.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets all direct permissions of the current <see cref="IRole" />.
        /// </summary>
        IEnumerable<IPermission> Permissions { get; }

        /// <summary>
        ///     Checks if the current <see cref="IRole" /> has given permission(also checks inherited permission)
        /// </summary>
        /// <param name="permission">The <see cref="IPermission" /> which should be checked.</param>
        /// <returns>True if the current <see cref="IRole" /> has the given permission, false if not.</returns>
        bool CheckPermission(IPermission permission);
    }
}