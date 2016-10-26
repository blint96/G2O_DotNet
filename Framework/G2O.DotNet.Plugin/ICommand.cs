// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Colony Online Project">
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
    using G2O.DotNet.Permission;
    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     Interface for all command classes.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Gets the identifier of the command.
        /// </summary>
        string CommandIdentifier { get; }

        /// <summary>
        ///     Gets the <see cref="IPermission" /> that is required to invoke the command.
        /// </summary>
        IPermission RequiredPermission { get; }

        /// <summary>
        ///     Gets a value indicating whether the current <see cref="ICommand" /> instance has to be send by a client.
        /// </summary>
        bool RequiresClient { get; }

        /// <summary>
        ///     Method that is called when the command is send.
        /// </summary>
        /// <param name="parameter">The command parameter string.</param>
        /// <param name="sender">The client that has send the command(null if it was no client)</param>
        void Invoke(string parameter, IClient sender);
    }
}