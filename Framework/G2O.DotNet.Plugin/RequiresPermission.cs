// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequiresPermission.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Plugin
{
    using System;

    /// <summary>
    /// An attribute that describes the permission that is required to invoke a command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RequiresPermission : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresPermission"/> class.
        /// </summary>
        /// <param name="requiredPermission">The name of the required permission.</param>
        public RequiresPermission(string requiredPermission)
            : this(requiredPermission, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresPermission"/> class.
        /// </summary>
        /// <param name="requiredPermission">The name of the required permission.</param>
        /// <param name="requiresFirstParameterPermission">A value indicating whether the a specialised permission for the first parameter is required</param>
        public RequiresPermission(string requiredPermission, bool requiresFirstParameterPermission)
        {
            if (string.IsNullOrWhiteSpace(requiredPermission))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(requiredPermission));
            }

            this.RequiredPermission = requiredPermission;
            this.RequiresFirstParameterPermission = requiresFirstParameterPermission;
        }

        /// <summary>
        /// Gets the required permissions name defined by the current instance of <see cref="RequiresPermission"/>.
        /// </summary>
        public string RequiredPermission { get; }

        /// <summary>
        /// Gets a value indicating whether the a specialised permission for the first parameter is required.
        /// </summary>
        public bool RequiresFirstParameterPermission { get; }
    }
}