// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientDisconnectedEventArgs.cs" company="Colony Online Project">
// Copyright (C) <2016>  <Julian Vogel>
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// </copyright>
// <summary>
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.Squirrel
{
    using System;
    using System.ComponentModel;

    public class ClientDisconnectedEventArgs : EventArgs
    {
        public ClientDisconnectedEventArgs(IClient client, DisconnectReason reason)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!Enum.IsDefined(typeof(DisconnectReason), reason))
            {
                throw new InvalidEnumArgumentException(nameof(reason), (int)reason, typeof(DisconnectReason));
            }

            this.Client = client;
            this.Reason = reason;
        }

        public IClient Client { get; }

        public DisconnectReason Reason { get; }
    }
}