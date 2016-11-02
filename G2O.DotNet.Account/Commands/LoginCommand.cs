// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginCommand.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Account.Commands
{
    using System;
    using System.ComponentModel.Composition;

    using G2O.DotNet.Plugin;
    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     A class that defines the command for loging into a account.
    /// </summary>
    internal class LoginCommand : ICommand
    {
        /// <summary>
        ///     The instance of <see cref="IAccountController" /> that should be used by this command.
        /// </summary>
        private readonly IAccountController controller;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginCommand" /> class.
        /// </summary>
        /// <param name="controller">The instance of <see cref="IAccountController" /> that should be used by this command.</param>
        [ImportingConstructor]
        public LoginCommand([Import] IAccountController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            this.controller = controller;
        }

        /// <summary>
        ///     Gets the identifier of the command.
        /// </summary>
        public string CommandIdentifier => "login";

        /// <summary>
        ///     Method that is called when the command is send.
        /// </summary>
        /// <param name="parameter">The command parameter string.</param>
        /// <param name="sender">The client that has send the command(null if it was no client)</param>
        [RequiresLogin(false)]
        public void Invoke(string parameter, IClient sender)
        {
            if (sender == null)
            {
                throw new ArgumentNullException(nameof(sender));
            }
            if (this.controller.IsClientLoggedIn(sender))
            {
                sender.SendMessage(255, 0, 0, "You are already logged in.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameter))
            {
                sender.SendMessage(255, 0, 0, "/Login <username> <password>");
                return;
            }

            string[] parts = parameter.Split(' ');

            if (parts.Length < 2)
            {
                sender.SendMessage(255, 0, 0, "/Login <username> <password>");
                return;
            }

            // Try to login
            if (this.controller.TryLogin(parts[0], parts[1], sender))
            {
                sender.SendMessage(0, 255, 0, "Login successfull");
            }
            else
            {
                // Tell the user that the login has failed and why.
                var reason = this.controller.CheckAccountExists(parts[0])
                                 ? "Wrong password."
                                 : "Username does not exist.";
                sender.SendMessage(255, 0, 0, "Login failed." + reason);
            }
        }
    }
}