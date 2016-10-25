// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BeforeCommandEventArgs.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ApiInterceptorLayer
{
    using System;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     Event args for the BeforeCommand interception event.
    /// </summary>
    public class BeforeCommandReceivedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BeforeCommandReceivedEventArgs" /> class.
        /// </summary>
        /// <param name="command">The command string of the command received event that should be invoked</param>
        /// <param name="arguments">The arguments string of the command received event that should be invoked.</param>
        public BeforeCommandReceivedEventArgs(string command, string arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(command));
            }

            this.Command = command;
            this.Arguments = arguments;
        }

        /// <summary>
        ///     Gets  or set the arguments string of the command received event that should be invoked.
        /// </summary>
        public string Arguments { get; }

        /// <summary>
        ///     Gets or set a value indicating whether the command should be chanceled.
        ///     <remarks>A command that is chanceled will not cause a call to the <see cref="IClient" />.CommandReceived event.</remarks>
        /// </summary>
        public bool ChancelCommand { get; set; }

        /// <summary>
        ///     Gets  or set the command string of the command received event that should be invoked.
        /// </summary>
        public string Command { get; }
    }
}