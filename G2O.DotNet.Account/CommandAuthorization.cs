// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandAuthorization.cs" company="Colony Online Project">
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
    using System.Collections.Generic;
    using System.Linq;

    using G2O.DotNet.ApiInterceptorLayer;
    using G2O.DotNet.ServerApi;

    /// <summary>
    /// A class that takes care of authorizing commands that a specific client wants to use.
    /// </summary>
    internal class CommandAuthorization
    {
        /// <summary>
        /// Interceptor of the client for which this instance of <see cref="CommandAuthorization"/> should authorize commands.
        /// </summary>
        private readonly IClientInterceptor client;

        /// <summary>
        /// Reference to the <see cref="CommandCollection"/> that provides the available commands.
        /// </summary>
        private readonly CommandCollection commandCollection;

        /// <summary>
        /// A hash set that contains the names of all permissions that should never be authorized by this instance of <see cref="CommandAuthorization"/>.
        /// </summary>
        private readonly HashSet<string> excludedPermissions = new HashSet<string>();

        /// <summary>
        /// A list of permission names that are explicitly defined for this instance of <see cref="CommandAuthorization"/>.
        /// </summary>
        private readonly List<string> explicitPermissions = new List<string>();

        /// <summary>
        /// A hash set that contains the list of all permissions that should be authorized by this instance of <see cref="CommandAuthorization"/>. 
        /// </summary>
        private readonly HashSet<string> permissions = new HashSet<string>();

        /// <summary>
        /// A list of roles(permissions combinations) that should be authorized by this instance of <see cref="CommandAuthorization"/>. 
        /// </summary>
        private readonly List<Role> roles = new List<Role>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAuthorization"/> class.
        /// </summary>
        /// <param name="client">The client object for which this instance of  <see cref="CommandAuthorization"/> should authorize commands.</param>
        /// <param name="commandCollection">The <see cref="CommandCollection"/> that provides the access to the available commands.</param>
        public CommandAuthorization(IClientInterceptor client, CommandCollection commandCollection)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (commandCollection == null)
            {
                throw new ArgumentNullException(nameof(commandCollection));
            }

            this.client = client;
            this.commandCollection = commandCollection;
            this.client.BeforeCommandReceived += this.InvokeCommand;
            this.client.Disconnect += this.ClientDisconnect;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the commands should be authorized as if the client is logged in or not.
        /// </summary>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// Adds a permission name to the list of permissions that should never be authorized by this instance of <see cref="CommandAuthorization"/>.
        /// </summary>
        /// <param name="permission">The permission that should be excluded.</param>
        public void AddExcludedPermission(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permission));
            }

            this.excludedPermissions.Add(permission);
        }

        /// <summary>
        /// Adds a permission name to the list of permissions that a explicitly defined for this instance of <see cref="CommandAuthorization"/>(not added by a role).
        /// </summary>
        /// <param name="permission">The name of the explicitly added permission.</param>
        public void AddExplicitPermission(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permission));
            }

            this.explicitPermissions.Add(permission);
        }

        /// <summary>
        /// Adds a role to this instance of <see cref="CommandAuthorization"/>. The permissions of the role will then be authorized by this instance of <see cref="CommandAuthorization"/>.
        /// </summary>
        /// <param name="role">The <see cref="Role"/> object that should be added.</param>
        public void AddRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (!this.roles.Contains(role))
            {
                this.roles.Add(role);
            }
            else
            {
                throw new ArgumentException(
                    "The given role object is already contained in the internal list of this object.",
                    nameof(role));
            }
        }

        /// <summary>
        /// Generates the internal hash set of permission names which is used to fast look up permissions.
        /// </summary>
        public void RecomposePermissions()
        {
            this.permissions.Clear();
            var tempList = new List<string>(this.explicitPermissions);

            foreach (var role in this.roles)
            {
                tempList.AddRange(role.Permissions);
            }

            foreach (var tempPermission in tempList.Distinct().Where(p => !this.excludedPermissions.Contains(p)))
            {
                this.permissions.Add(tempPermission);
            }
        }

        /// <summary>
        /// Removes a permission name from the list of excluded permissions.
        /// </summary>
        /// <param name="permission">The name of the excluded permission that should be removed.</param>
        public void RemoveExcludedPermission(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permission));
            }

            if (this.excludedPermissions.Contains(permission))
            {
                this.excludedPermissions.Remove(permission);
            }
            else
            {
                throw new ArgumentException(
                    "A explicit permission with the given name is not contained in the internal list of this object",
                    nameof(permission));
            }
        }

        /// <summary>
        /// Removes a permission name from the list of explicitly defined permissions.
        /// </summary>
        /// <param name="permission">he name of the explicitly defined permission that should be removed.</param>
        public void RemoveExplicitPermission(string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permission));
            }

            if (this.explicitPermissions.Contains(permission))
            {
                this.explicitPermissions.Remove(permission);
            }
            else
            {
                throw new ArgumentException(
                    "A explicit permission with the given name is not contained in the internal list of this object",
                    nameof(permission));
            }
        }

        /// <summary>
        /// Removes a role object from the list of roles in this instance of <see cref="CommandAuthorization"/>. 
        /// </summary>
        /// <param name="role">The role that should be removed.</param>
        public void RemoveRole(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (this.roles.Contains(role))
            {
                this.roles.Add(role);
            }
            else
            {
                throw new ArgumentException(
                    "The given role object is not contained in the internal list of this object.",
                    nameof(role));
            }
        }

        /// <summary>
        /// Removes all roles,explicit and excluded permission from this instance of <see cref="CommandAuthorization"/>.
        /// </summary>
        public void Reset()
        {
            this.permissions.Clear();
            this.explicitPermissions.Clear();
            this.excludedPermissions.Clear();
            this.roles.Clear();
        }


        /// <summary>
        /// Removes event handlers and clears all lists if the related client disconnects.
        /// (This should ensure that the GC can collect the objects fast)
        /// </summary>
        /// <param name="sender">The client that has disconnected.</param>
        /// <param name="e">The <see cref="ClientDisconnectedEventArgs"/> object.</param>
        private void ClientDisconnect(object sender, ClientDisconnectedEventArgs e)
        {
            this.client.BeforeCommandReceived -= this.InvokeCommand;
            this.client.Disconnect -= this.ClientDisconnect;
            this.Reset();
        }

        /// <summary>
        /// I called when the related client calls a command. Invokes the command object if the client is has the required permissions.
        /// </summary>
        /// <param name="sender">The client that invoked the command.</param>
        /// <param name="eventArgs">Information about the the command.</param>
        private void InvokeCommand(object sender, BeforeCommandReceivedEventArgs eventArgs)
        {
            CommandContainer cmd;
            if (this.commandCollection.TryGetCommand(eventArgs.Command, out cmd))
            {
                // The command is defined a command objects list. Cancel the API event.
                eventArgs.ChancelCommand = true;

                // Login check.
                if (cmd.RequiresLogin && !this.IsLoggedIn)
                {
                    this.client.SendMessage(
                        255,
                        0,
                        0,
                        "You need to be logged in to use the '" + eventArgs.Command + "' command");
                    return;
                }

                // Command does not require a permission or the client has the All-Permission("*")
                if (cmd.RequiredPermission == null || this.permissions.Contains("*"))
                {
                    cmd.Command.Invoke(eventArgs.Arguments, this.client);
                }
                else
                {
                    // Build the name of the required permission(if the first parameter has to be taken into account.
                    string requiredPermission = cmd.RequiredPermission;
                    if (cmd.FirstParameterPermission)
                    {
                        string fistParam;
                        if (string.IsNullOrWhiteSpace(fistParam = eventArgs.Arguments.Split(' ').FirstOrDefault()))
                        {
                            this.client.SendMessage(
                                255,
                                0,
                                0,
                                "The first parameter of the command '" + eventArgs.Command + "' must not be empty.");
                            return;
                        }

                        requiredPermission = requiredPermission + "." + fistParam;
                    }

                    // Permission check.
                    if (this.permissions.Contains(requiredPermission))
                    {
                        cmd.Command.Invoke(eventArgs.Arguments, this.client);
                    }
                    else
                    {
                        this.client.SendMessage(255, 0, 0, "You do not have the required permission.");
                    }
                }
            }
            else
            {
                this.client.SendMessage(255, 0, 0, "The command '" + eventArgs.Command + "' does not exist.");
            }
        }
    }
}