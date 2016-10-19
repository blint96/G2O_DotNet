// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionFactory.cs" company="Colony Online Project">
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
    using System.ComponentModel.Composition;
    using System.Linq;

    /// <summary>
    ///     Default implementation of the <see cref="IPermissionFactory" /> interface.
    /// </summary>
    [Export(typeof(IPermissionFactory))]
    internal class PermissionFactory : IPermissionFactory
    {
        /// <summary>
        ///     Gets a permission with the given name and parent or creates it if it does not exist.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        /// <param name="parentPermission">The parent <see cref="IPermission" />.</param>
        /// <returns>The found or created <see cref="IPermission" /> object.</returns>
        public IPermission GetOrCreatePermission(string permissionName, IPermission parentPermission)
        {
            if (parentPermission == null)
            {
                throw new ArgumentNullException(nameof(parentPermission));
            }

            if (string.IsNullOrEmpty(permissionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(permissionName));
            }

            // Check if the permission allready exists.
            var permission =
                parentPermission.Children.FirstOrDefault(
                    perm => perm.Name.Equals(permissionName, StringComparison.InvariantCulture));

            // Create the permission if it does not exist.
            return permission ?? new Permission(permissionName, parentPermission);
        }

        /// <summary>
        ///     Gets the root permission.
        /// </summary>
        /// <returns>The root permission</returns>
        public IPermission GetRootPermission()
        {
            return Permission.RootPermission;
        }
    }
}