// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogOutCommand.cs" company="Colony Online Project">
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
    /// Class that defines the command for logging out a client.
    /// </summary>
    internal class LogOutCommand : ICommand
    {
        /// <summary>
        /// The used instance of <see cref="IAccountControler"/>.
        /// </summary>
        private readonly IAccountControler controler;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOutCommand"/> class.
        /// </summary>
        /// <param name="controler">The used instance of <see cref="IAccountControler"/>.</param>
        [ImportingConstructor]
        public LogOutCommand([Import] IAccountControler controler)
        {
            if (controler == null)
            {
                throw new ArgumentNullException(nameof(controler));
            }

            this.controler = controler;
        }

        /// <summary>
        ///     Gets the identifier of the command.
        /// </summary>
        public string CommandIdentifier { get; } = "logout";

        /// <summary>
        ///     Method that is called when the command is send.
        /// </summary>
        /// <param name="parameter">The command parameter string.</param>
        /// <param name="sender">The client that has send the command(null if it was no client)</param>
        public void Invoke(string parameter, IClient sender)
        {
            if (!this.controler.IsClientLoggedIn(sender))
            {
                sender.SendMessage(255, 0, 0, "You are not logged in.");
            }
            else
            {
                this.controler.LogOut(sender);
                sender.SendMessage(0, 255, 0, "You have been logged out.");
            }
        }
    }
}