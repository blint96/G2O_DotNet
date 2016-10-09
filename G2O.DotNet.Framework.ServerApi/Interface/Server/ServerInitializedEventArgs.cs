// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerInitializedEventArgs.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ServerApi.Server
{
    using System;

    /// <summary>
    ///     <see cref="EventArgs" /> class for the Server initialized event.
    /// </summary>
    public class ServerInitializedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ServerInitializedEventArgs" /> class.
        /// </summary>
        /// <param name="server">The <see cref="IServer" /> that was initialized.</param>
        public ServerInitializedEventArgs(IServer server)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            this.Server = server;
        }

        /// <summary>
        ///     Gets the <see cref="IServer" /> that was initialized.
        /// </summary>
        public IServer Server { get; }
    }
}