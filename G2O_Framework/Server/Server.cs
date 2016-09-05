// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Server.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Server
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Client;
    using GothicOnline.G2.DotNet.Exceptions;
    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Server : IServer
    {
        private static readonly AnsiString StringGetServerDescription = "getServerDescription";
        private static readonly AnsiString StringGetServerWorld = "getServerWorld";
        private static readonly AnsiString StringSendMessageToAll = "sendMessageToAll";
        private static readonly AnsiString StringSetServerDescription = "setServerDescription";
        private static readonly AnsiString StringSetServerWorld = "setServerWorld";

        private readonly ISquirrelApi squirrelApi;


        public Server(ISquirrelApi squirrelApi)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            this.squirrelApi = squirrelApi;
        }

        public event EventHandler<ClientConnectedEventArgs> Initialize;

        public IClientList Clients { get; }

        public string Description
        {
            get
            {
                return this.squirrelApi.CallWithReturn<string>(StringGetServerDescription);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.squirrelApi.CallWithParameter<string>(StringSetServerDescription, value);
            }
        }

        public ServerTime Time { get; set; }

        public string World
        {
            get
            {
                return this.squirrelApi.CallWithReturn<string>(StringGetServerWorld);
            }

            set
            {
                this.squirrelApi.CallWithParameter<string>(StringSetServerWorld, value);
            }
        }

        public void SendMessageToAll(Color color, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            this.squirrelApi.CallWithParameter<int, int, int, string>(StringSendMessageToAll, color.R, color.G, color.B, message);
        }

        public void SendMessageToAll(int r, int g, int b, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            this.squirrelApi.CallWithParameter<int, int, int, string>(StringSendMessageToAll, r, g, b, message);
        }

        public void SendPacketToAll(IPacket packet, PacketReliability reliability)
        {
            throw new NotImplementedException();
        }
    }
}