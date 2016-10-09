// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PacketReceivedEventArgs.cs" company="Colony Online Project">
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
// EventArgs class for the packet received event.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Server
{
    using System;

    /// <summary>
    /// <see cref="EventArgs"/> class for the packet received event.
    /// </summary>
    public class PacketReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PacketReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="packet">The <see cref="IPacket"/> that was received.</param>
        public PacketReceivedEventArgs(IPacket packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            this.Packet = packet;
        }

        /// <summary>
        /// Gets the <see cref="IPacket"/> that was received.
        /// </summary>
        public IPacket Packet { get; }
    }
}