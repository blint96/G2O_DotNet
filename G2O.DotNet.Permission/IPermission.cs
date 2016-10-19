// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermission.cs" company="Colony Online Project">
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
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    ///     Interface for the Permission classes.
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        ///     Gets all Children of the current <see cref="IPermission" />.
        /// </summary>
        IEnumerable<IPermission> Children { get; }

        /// <summary>
        ///     Gets the full name of the <see cref="IPermission" />.
        ///     <remarks>Contains the Names of the parent permissions.</remarks>
        /// </summary>
        string FullName { get; }

        /// <summary>
        ///     Gets the name of the <see cref="IPermission" />.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the Parent <see cref="IPermission" /> of the current <see cref="IPermission" />.
        ///     <remarks>The root permission does not have a parent.</remarks>
        /// </summary>
        IPermission Parent { get; }

        /// <summary>
        ///     Checks if the current <see cref="IPermission" /> object is a child of a given other permission.
        /// </summary>
        /// <param name="permission">The parent permission.</param>
        /// <returns>True if the given <see cref="IPermission" /> is a parent of the current <see cref="IPermission" />.</returns>
        bool IsChildOf(IPermission permission);

        /// <summary>
        ///     Checks if a <see cref="IPermission" /> object is a parent of another <see cref="IPermission" />.
        /// </summary>
        /// <param name="permission">The child <see cref="IPermission" /> object.</param>
        /// <returns>
        ///     True if the given <see cref="IPermission" /> object is a child of the current <see cref="IPermission" />
        ///     object.
        /// </returns>
        bool IsParentOf(IPermission permission);

        /// <summary>
        /// Checks if a <see cref="IPermission"/> is the equal to current <see cref="IPermission"/> instance or if this <see cref="IPermission"/> is a parent of the given <see cref="Permission"/>
        /// </summary>
        /// <param name="permission">The <see cref="IPermission"/> object that should be compared with the current <see cref="IPermission"/>.</param>
        /// <returns>True if the current <see cref="IPermission"/> is equal or a parent of the given <see cref="IPermission"/> instance.</returns>
        bool IsEqualOrParent(IPermission permission);

    }
}