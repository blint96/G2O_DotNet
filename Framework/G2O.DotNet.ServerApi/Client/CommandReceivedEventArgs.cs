// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandReceivedEventArgs.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ServerApi
{
    using System;

    public class CommandReceivedEventArgs : EventArgs
    {
        public CommandReceivedEventArgs(string command, string parameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            this.Command = command;
            this.Parameters = parameters;
        }

        public string Command { get; }

        public string Parameters { get; }
    }
}