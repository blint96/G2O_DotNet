// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Permission.cs" company="Colony Online Project">
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
    ///     Class that depict one node in the permission tree.
    /// </summary>
    internal class Permission : IPermission
    {
        /// <summary>
        ///     Stores the full names of all <see cref="Permission" /> objects that where created in the active
        ///     <see cref="AppDomain" />.
        /// </summary>
        private static readonly HashSet<string> ExistingPermissions = new HashSet<string>();

        /// <summary>
        ///     Stores all children of the current <see cref="Permission" /> object.
        /// </summary>
        private readonly List<IPermission> children;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Permission" /> class. The new permission is a direct child of the Root
        ///     permission
        /// </summary>
        /// <param name="name">The name of the new permission</param>
        public Permission(string name)
            : this(name, RootPermission)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Permission" /> class.
        /// </summary>
        /// <param name="name">The name of the new permission</param>
        /// <param name="parent">The parent <see cref="Permission" /> of the new <see cref="Permission" />.</param>
        public Permission(string name, IPermission parent)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (name.All(char.IsLetterOrDigit))
            {
                throw new ArgumentException(
                    "The Permission name must only contain letters and or digits.", 
                    nameof(name));
            }

            // Check if a permission with this name was already created.
            if (ExistingPermissions.Contains($"{parent.FullName}.{name}"))
            {
                throw new ArgumentException("A permission with this name does allready exist", nameof(name));
            }

            this.Name = name;
            this.Parent = parent;
            ExistingPermissions.Add(this.FullName);

            // Add a reference to the parents children list.
            var parentPerm = parent as Permission;
            parentPerm?.children?.Add(this);
        }

        /// <summary>
        ///     Initializes the Root permission.
        /// </summary>
        private Permission()
        {
            this.Name = "*";
        }

        /// <summary>
        ///     Gets all Children of the current <see cref="IPermission" />.
        /// </summary>
        public IEnumerable<IPermission> Children => this.children.AsReadOnly();

        /// <summary>
        ///     Gets the full name of the <see cref="IPermission" />.
        ///     <remarks>Contains the Names of the parent permissions.</remarks>
        /// </summary>
        public string FullName => this.Parent != null ? $"{this.Parent.FullName}.{this.Name}" : this.Name;

        /// <summary>
        ///     Gets the name of the <see cref="IPermission" />.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Gets the Parent <see cref="IPermission" /> of the current <see cref="IPermission" />.
        ///     <remarks>The root permission does not have a parent.</remarks>
        /// </summary>
        public IPermission Parent { get; }

        /// <summary>
        ///     Gets the Root permission.
        ///     <remarks>The Root permission is the parent of all permissions.</remarks>
        /// </summary>
        public static IPermission RootPermission { get; } = new Permission();

        /// <summary>
        ///     Search for a existing instance of <see cref="IPermission" /> by its name.
        ///     <remarks>This method should only be used on initialization because it scanns the whole Permission tree.</remarks>
        /// </summary>
        /// <param name="permissionName">Name of the permission that should be found</param>
        /// <returns></returns>
        public static IPermission GetNamedPermission(string permissionName)
        {
            if (string.IsNullOrEmpty(permissionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(permissionName));
            }

            if (RootPermission.FullName.Equals(permissionName, StringComparison.InvariantCulture))
            {
                return RootPermission;
            }

            // Check if a permission with this name was already created.
            if (!ExistingPermissions.Contains(permissionName))
            {
                throw new ArgumentException("A permission with this name does not exist.", nameof(permissionName));
            }

            var permissions = new Queue<IPermission>(RootPermission.Children);
            while (permissions.Count > 0)
            {
                IPermission current = permissions.Dequeue();
                if (current.FullName.Equals(permissionName, StringComparison.InvariantCulture))
                {
                    return current;
                }

                // Do not search in branches of the permission tree that do not even share a part oft the name with the searched permission.
                foreach (var permission in current.Children.Where(child => permissionName.Contains(child.FullName)))
                {
                    permissions.Enqueue(permission);
                }
            }

            throw new Exception("Permission object not found");
        }

        /// <summary>
        ///     Checks if the current <see cref="IPermission" /> object is a child of a given other permission.
        /// </summary>
        /// <param name="permission">The parent permission.</param>
        /// <returns>True if the given <see cref="IPermission" /> is a parent of the current <see cref="IPermission" />.</returns>
        public bool IsChildOf(IPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            IPermission current = this.Parent;
            while (current != null)
            {
                if (current == permission)
                {
                    return true;
                }

                current = current.Parent;
            }

            return false;
        }

        /// <summary>
        ///     Checks if a <see cref="IPermission" /> is the equal to current <see cref="IPermission" /> instance or if this
        ///     <see cref="IPermission" /> is a parent of the given <see cref="Permission" />
        /// </summary>
        /// <param name="permission">
        ///     The <see cref="IPermission" /> object that should be compared with the current
        ///     <see cref="IPermission" />.
        /// </param>
        /// <returns>
        ///     True if the current <see cref="IPermission" /> is equal or a parent of the given <see cref="IPermission" />
        ///     instance.
        /// </returns>
        public bool IsEqualOrParent(IPermission permission)
        {
            return permission == this || this.IsParentOf(permission);
        }

        /// <summary>
        ///     Checks if a <see cref="IPermission" /> object is a parent of another <see cref="IPermission" />.
        /// </summary>
        /// <param name="permission">The child <see cref="IPermission" /> object.</param>
        /// <returns>
        ///     True if the given <see cref="IPermission" /> object is a child of the current <see cref="IPermission" />
        ///     object.
        /// </returns>
        public bool IsParentOf(IPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            return permission.IsChildOf(this);
        }

        /// <summary>
        ///     Returns the <see cref="FullName" /> of the current <see cref="Permission" /> object.
        /// </summary>
        /// <returns>The <see cref="FullName" /> of the current <see cref="Permission" /> object.</returns>
        public override string ToString()
        {
            return this.FullName;
        }
    }
}