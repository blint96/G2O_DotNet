// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandCollection.cs" company="Colony Online Project">
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

    using G2O.DotNet.Plugin;

    /// <summary>
    ///     A class provides access to a list of <see cref="CommandContainer" />.
    /// </summary>
    internal class CommandCollection
    {
        /// <summary>
        ///     A dictionary of all <see cref="CommandContainer" /> objects managed by the current object. The
        ///     <see cref="CommandContainer" /> are ordered by the command identifier of the commands.
        /// </summary>
        private readonly Dictionary<string, CommandContainer> commandContainers =
            new Dictionary<string, CommandContainer>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandCollection" /> class.
        /// </summary>
        /// <param name="commands">
        ///     A enumerable with all <see cref="ICommand" /> instances that should be contained in the new
        ///     instance of <see cref="CommandCollection" />.
        /// </param>
        public CommandCollection(IEnumerable<ICommand> commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            this.Commands = commands.ToList().AsReadOnly();

            // Fill the commands dictionary.
            foreach (var command in this.Commands)
            {
                if (!this.commandContainers.ContainsKey(command.CommandIdentifier.ToUpperInvariant()))
                {
                    this.commandContainers.Add(
                        command.CommandIdentifier.ToUpperInvariant(), 
                        new CommandContainer(command));
                }

                // Todo Add logging for duplicated command identifiers.
            }

            this.Containers = this.commandContainers.Values.ToList().AsReadOnly();
        }

        /// <summary>
        ///     Gets a enumerable of all commands in the current instance of <see cref="CommandCollection" />.
        /// </summary>
        public IEnumerable<ICommand> Commands { get; }

        /// <summary>
        ///     Gets a enumerable of all <see cref="CommandContainer" />s in the current instance of
        ///     <see cref="CommandCollection" />.
        /// </summary>
        public IEnumerable<CommandContainer> Containers { get; }

        /// <summary>
        ///     Tries to get a <see cref="CommandContainer" /> by the its command identifier.
        /// </summary>
        /// <param name="commandIdentifier">The command identifier of the command that should be searched.</param>
        /// <param name="cmd">The found <see cref="CommandContainer" /></param>
        /// <returns>Returns true if a command with the given identifier was found, false if not.</returns>
        public bool TryGetCommand(string commandIdentifier, out CommandContainer cmd)
        {
            if (string.IsNullOrWhiteSpace(commandIdentifier))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(commandIdentifier));
            }

            return this.commandContainers.TryGetValue(commandIdentifier.ToUpperInvariant(), out cmd);
        }
    }
}