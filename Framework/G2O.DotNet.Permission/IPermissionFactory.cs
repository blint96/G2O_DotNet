// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionFactory.cs" company="Colony Online Project">
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
    /// <summary>
    ///     Interface of the permission factory. Allows the creating and search for <see cref="IPermission" /> instance.
    /// </summary>
    public interface IPermissionFactory
    {
        /// <summary>
        ///     Gets a permission with the given name and parent or creates it if it does not exist.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        /// <param name="parentPermission">The parent <see cref="IPermission" />.</param>
        /// <returns>The found or created <see cref="IPermission" /> object.</returns>
        IPermission GetOrCreatePermission(string permissionName, IPermission parentPermission);

        /// <summary>
        ///     Gets the root permission.
        /// </summary>
        /// <returns>The root permission</returns>
        IPermission GetRootPermission();
    }
}