// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisconnectReason.cs" company="Colony Online Project">
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
// Defines the different reasons for a client to disconnect from the server.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi
{
    /// <summary>
    ///     Defines the different reasons for a client to disconnect from the server.
    /// </summary>
    public enum DisconnectReason
    {
        /// <summary>
        ///     The client has disconnected normally(or the server has closed the connection)
        /// </summary>
        Disconnected, 

        /// <summary>
        ///     The client lost the connection unexpected.
        /// </summary>
        LostConnection, 

        /// <summary>
        ///     A fatal error was detected by this client before it closed the connection to the server.
        /// </summary>
        HasCrashed, 
    }
}