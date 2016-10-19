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
namespace G2O.DotNet.Permission
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///     Default implementation of the <see cref="IRole" /> interface.
    ///     <remarks>
    ///         A role is a group of permissions that can be assigned to a user. A role can consist of multiple
    ///         permissions and other roles.
    ///     </remarks>
    /// </summary>
    internal class Role : IRole
    {
        /// <summary>
        ///     Stores the names of all <see cref="Role" /> objects that were created in the current <see cref="AppDomain" />.
        /// </summary>
        private static readonly HashSet<string> ExistingRoles = new HashSet<string>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Role" /> class.
        /// </summary>
        /// <param name="name">Name of the new role.</param>
        /// <param name="permissions">Enumerable of all permissions that are directly associated with the new role.</param>
        /// <param name="excludedPermissions">Enumerable of all permissions that are directly excluded from this role.</param>
        /// <param name="baseRoles">
        ///     Enumerable of all <see cref="IRole" /> objects that are base roles of the new role(the new role
        ///     will inherit the permissions of these roles.)
        /// </param>
        public Role(
            string name, 
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

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            // Check if the role does already exist.
            if (ExistingRoles.Contains(name))
            {
                throw new ArgumentException("A role with the given name does already exist.", nameof(name));
            }

            this.BaseRoles = baseRoles.ToList().AsReadOnly();
            this.ExcludedPermissions = excludedPermissions.ToList().AsReadOnly();
            this.Name = name;
            this.Permissions = permissions.ToList().AsReadOnly();
            ExistingRoles.Add(name);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Role" /> class.
        /// </summary>
        /// <param name="name">Name of the new role.</param>
        public Role(string name)
            : this(name, new IPermission[0], new IPermission[0], new IRole[0])
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Role" /> class.
        /// </summary>
        /// <param name="name">Name of the new role.</param>
        /// <param name="permissions">Enumerable of all permissions that are directly associated with the new role.</param>
        /// <param name="excludedPermissions">Enumerable of all permissions that are directly excluded from this role.</param>
        public Role(string name, IEnumerable<IPermission> permissions, IEnumerable<IPermission> excludedPermissions)
            : this(name, permissions, excludedPermissions, new IRole[0])
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Role" /> class.
        /// </summary>
        /// <param name="name">Name of the new role.</param>
        /// <param name="permissions">Enumerable of all permissions that are directly associated with the new role.</param>
        public Role(string name, IEnumerable<IPermission> permissions)
            : this(name, permissions, new IPermission[0], new IRole[0])
        {
        }

        /// <summary>
        ///     Gets all base roles of the current <see cref="IRole" /> object.
        /// </summary>
        public IEnumerable<IRole> BaseRoles { get; }

        /// <summary>
        ///     Gets all Permissions directly excluded from this role.
        ///     <remarks>Excluded permission suppress existing permission ownership(e.g. from inheritance)</remarks>
        /// </summary>
        public IEnumerable<IPermission> ExcludedPermissions { get; }

        /// <summary>
        ///     Gets the name of the <see cref="IRole" />.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets all direct permissions of the current <see cref="IRole" />.
        /// </summary>
        public IEnumerable<IPermission> Permissions { get; }

        /// <summary>
        ///     Checks if the current <see cref="IRole" /> has given permission(also checks inherited permission)
        /// </summary>
        /// <param name="permission">The <see cref="IPermission" /> which should be checked.</param>
        /// <returns>True if the current <see cref="IRole" /> has the given permission, false if not.</returns>
        public bool CheckPermission(IPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            // Check the current level.
            if (this.ExcludedPermissions.Any(perm => perm.IsEqualOrParent(permission)))
            {
                return false;
            }

            if (this.Permissions.Contains(permission))
            {
                return true;
            }

            // Check the base roles and their bases and their....
            Queue<IRole> baseRoles = new Queue<IRole>(this.BaseRoles);
            while (baseRoles.Count > 0)
            {
                IRole baseRole = baseRoles.Dequeue();
                if (baseRole.ExcludedPermissions.Contains(permission))
                {
                    return false;
                }

                if (baseRole.Permissions.Any(perm => perm.IsEqualOrParent(permission)))
                {
                    return true;
                }

                foreach (var role in baseRole.BaseRoles)
                {
                    baseRoles.Enqueue(role);
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns the name of the current <see cref="Role" /> object.
        /// </summary>
        /// <returns> The name of the current <see cref="Role" /> object.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}