// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateAccountCommand.cs" company="Colony Online Project">
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
    /// Class that defines the command for creating a new account.
    /// </summary>
    internal class CreateAccountCommand : ICommand
    {
        /// <summary>
        ///     The used instance of <see cref="IAccountController" />.
        /// </summary>
        private readonly IAccountController controller;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateAccountCommand" /> class.
        /// </summary>
        /// <param name="controller">The used instance of <see cref="IAccountController" />.</param>
        [ImportingConstructor]
        public CreateAccountCommand([Import] IAccountController controller)
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
        public string CommandIdentifier { get; } = "register";

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

            // Check client is already logged in.
            if (this.controller.IsClientLoggedIn(sender))
            {
                sender.SendMessage(255, 0, 0, "You are logged in.");
                return;
            }

            // Check param empty.
            if (string.IsNullOrWhiteSpace(parameter))
            {
                sender.SendMessage(255, 0, 0, "/Register <username> <password> <password repeat>");
                return;
            }

            string[] parts = parameter.Split(' ');

            // Check param count.
            if (parts.Length < 3)
            {
                sender.SendMessage(255, 0, 0, "/Register <username> <password> <password repeat>");
                return;
            }

            // Check name available
            if (this.controller.CheckAccountExists(parts[0]))
            {
                sender.SendMessage(255, 0, 0, "A account with the given name does already exist");
                return;
            }

            if (parts[1].Equals(parts[2], StringComparison.InvariantCulture))
            {
                sender.SendMessage(255, 0, 0, "The password and its repeat are not equal");
                return;
            }

            this.controller.CreateAccount(parts[0], parts[1]);
            sender.SendMessage(0, 255, 0, "Account created you can now log in.");
        }
    }
}