// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandWrapper.cs" company="Colony Online Project">
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
    using System.Reflection;

    using G2O.DotNet.Plugin;

    /// <summary>
    /// A class that stores a instance of <see cref="ICommand"/> and allow access to the attributes that can be specified on command classes.
    /// </summary>
    internal class CommandContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandContainer"/> class.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that should be encapsulated by the new instance of <see cref="CommandContainer"/>.</param>
        public CommandContainer(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            this.Command = command;

            //Get the method info for the invoke method of the command interface.
            MethodInfo invokeMethod = command.GetType().GetMethod(nameof(command.Invoke));
            RequiresPermission requiresPermission =
                invokeMethod.GetCustomAttribute(typeof(RequiresPermission)) as RequiresPermission;
            RequiresLogin requiresLogin = invokeMethod.GetCustomAttribute(typeof(RequiresLogin)) as RequiresLogin;

            // Get the attribute values.
            this.RequiredPermission = requiresPermission?.RequiredPermission;
            this.FirstParameterPermission = requiresPermission?.RequiresFirstParameterPermission ?? false;
            this.RequiresLogin = requiresLogin?.LoginRequired ?? true;
        }

        /// <summary>
        /// Gets the contained <see cref="ICommand"/> instance.
        /// </summary>
        public ICommand Command { get; }

        /// <summary>
        /// Gets a value indicating whether the first parameter is appended to the required permission
        /// <example>Required permission "Teleport.Goto.OldMine" where "OldMine" is the first parameter.</example>
        /// </summary>
        public bool FirstParameterPermission { get; }

        /// <summary>
        /// Gets the name of the permission that is required to use the contained <see cref="ICommand"/>.k
        /// </summary>
        public string RequiredPermission { get; }

        /// <summary>
        /// Gets a value indicating whether the client that calls the contained command needs to be logged in.
        /// </summary>
        public bool RequiresLogin { get; }
    }
}