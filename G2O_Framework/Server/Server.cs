// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Server.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  <ulian Vogel
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
namespace GothicOnline.G2.DotNet.Loader.Server
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Client;
    using GothicOnline.G2.DotNet.Exceptions;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Server : IServer
    {
        private const string stringGetPlayerRespawnTime = "getPlayerRespawnTime";

        private const string stringGetServerDescription = "getServerDescription";

        private const string stringGetServerWorld = "getServerWorld";

        private const string stringSendMessageToAll = "sendMessageToAll";

        private const string stringSendMessageToPlayer = "sendMessageToPlayer";

        private const string stringSetPlayerRespawnTime = "setPlayerRespawnTime";

        private const string stringSetServerDescription = "setServerDescription";

        private const string stringSetServerWorld = "setServerWorld";

        private static readonly IntPtr ansiGetPlayerRespawnTime;

        private static readonly IntPtr ansiGetServerDescription;

        private static readonly IntPtr ansiGetServerWorld;

        private static readonly IntPtr ansiSendMessageToAll;

        private static readonly IntPtr ansiSendMessageToPlayer;

        private static readonly IntPtr ansiSetPlayerRespawnTime;

        private static readonly IntPtr ansiSetServerDescription;

        private static readonly IntPtr ansiSetServerWorld;

        private readonly ISquirrelApi squirrelApi;

        static Server()
        {
            ansiSendMessageToAll = Marshal.StringToHGlobalAnsi(stringSendMessageToAll);
            ansiSendMessageToPlayer = Marshal.StringToHGlobalAnsi(stringSendMessageToPlayer);
            ansiGetServerDescription = Marshal.StringToHGlobalAnsi(stringGetServerDescription);
            ansiSetServerDescription = Marshal.StringToHGlobalAnsi(stringSetServerDescription);
            ansiSetPlayerRespawnTime = Marshal.StringToHGlobalAnsi(stringSetPlayerRespawnTime);
            ansiSetPlayerRespawnTime = Marshal.StringToHGlobalAnsi(stringSetPlayerRespawnTime);
            ansiGetPlayerRespawnTime = Marshal.StringToHGlobalAnsi(stringGetPlayerRespawnTime);
            ansiGetServerWorld = Marshal.StringToHGlobalAnsi(stringGetServerWorld);
            ansiSetServerWorld = Marshal.StringToHGlobalAnsi(stringSetServerWorld);
        }

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
                // Get the stack top index
                int top = this.squirrelApi.SqGetTop();

                // Get the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(ansiGetServerDescription, stringGetServerDescription.Length);
                if (!this.squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{stringGetServerDescription}' could not be found in the root table");
                }

                // Call the function
                this.squirrelApi.SqPushRootTable();
                if (!this.squirrelApi.SqCall(1, true, false))
                {
                    throw new SquirrelException($"The call to the '{stringGetServerDescription}' function failed");
                }

                // Get the result
                string result;
                this.squirrelApi.SqGetString(this.squirrelApi.SqGetTop(), out result);

                // Set back top
                this.squirrelApi.SqSetTop(top);
                return result;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                // Get the stack top index
                int top = this.squirrelApi.SqGetTop();

                // Get the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(ansiSetServerDescription, stringSetServerDescription.Length);
                if (!this.squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{stringSetServerDescription}' could not be found in the root table");
                }

                // Call the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(value);
                if (!this.squirrelApi.SqCall(2, true, false))
                {
                    throw new SquirrelException($"The call to the '{stringSetServerDescription}' function failed");
                }

                // Set back top.
                this.squirrelApi.SqSetTop(top);
            }
        }

        public int RespawnTime { get; set; }

        public ServerTime Time { get; set; }

        public string World
        {
            get
            {
                // Get the stack top index
                int top = this.squirrelApi.SqGetTop();

                // Get the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(ansiGetServerWorld, stringGetServerWorld.Length);
                if (!this.squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{stringGetServerWorld}' could not be found in the root table");
                }

                // Call the function
                this.squirrelApi.SqPushRootTable();
                if (!this.squirrelApi.SqCall(1, true, false))
                {
                    throw new SquirrelException($"The call to the '{stringGetServerWorld}' function failed");
                }

                // Get the result
                string result;
                this.squirrelApi.SqGetString(this.squirrelApi.SqGetTop(), out result);

                // Set back top
                this.squirrelApi.SqSetTop(top);
                return result;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                // Get the stack top index
                int top = this.squirrelApi.SqGetTop();

                // Get the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(ansiSetServerWorld, stringSetServerWorld.Length);
                if (!this.squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{stringSetServerWorld}' could not be found in the root table");
                }

                // Call the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(value);
                if (!this.squirrelApi.SqCall(2, true, false))
                {
                    throw new SquirrelException($"The call to the '{stringSetServerWorld}' function failed");
                }

                // Set back top.
                this.squirrelApi.SqSetTop(top);
            }
        }

        public void SendMessageToAll(Color color, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            // Get the stack top index
            int top = this.squirrelApi.SqGetTop();

            // Get the function
            this.squirrelApi.SqPushRootTable();
            this.squirrelApi.SqPushString(ansiSendMessageToAll, stringSendMessageToAll.Length);
            if (!this.squirrelApi.SqGet(-2))
            {
                throw new SquirrelException(
                    $"The gothic online server function '{stringSendMessageToAll}' could not be found in the root table");
            }

            // Call the function
            this.squirrelApi.SqPushRootTable();
            this.squirrelApi.SqPushInteger(color.R);
            this.squirrelApi.SqPushInteger(color.G);
            this.squirrelApi.SqPushInteger(color.B);
            this.squirrelApi.SqPushString(message);
            if (!this.squirrelApi.SqCall(2, true, false))
            {
                throw new SquirrelException($"The call to the '{stringSendMessageToAll}' function failed");
            }

            // Set back top.
            this.squirrelApi.SqSetTop(top);
        }

        public void SendPacketToAll(IPacket packet, PacketReliability reliability)
        {
            throw new NotImplementedException();
        }
    }
}