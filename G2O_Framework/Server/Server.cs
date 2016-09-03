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
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Client;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Server : IServer
    {
        private ISquirrelApi squirrelApi;

        private const string sendMessageToAll = "sendMessageToAll";
        private static readonly IntPtr stringSendMessageToAll = Marshal.StringToHGlobalAnsi(sendMessageToAll);
        private static readonly IntPtr stringSendMessageToPlayer = Marshal.StringToHGlobalAnsi("sendMessageToPlayer");

        private const string getServerDescription = "getServerDescription";
        private static readonly IntPtr stringGetServerDescription = Marshal.StringToHGlobalAnsi(getServerDescription);

        private const string setServerDescription = "setServerDescription";
        private static readonly IntPtr stringSetServerDescription = Marshal.StringToHGlobalAnsi(setServerDescription);



        private static readonly IntPtr stringSetPlayerRespawnTime = Marshal.StringToHGlobalAnsi("setPlayerRespawnTime");
        private static readonly IntPtr stringGetPlayerRespawnTime = Marshal.StringToHGlobalAnsi("getPlayerRespawnTime");

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
                int top = this.squirrelApi.SqGetTop();
                string result;

                this.squirrelApi.SqPushString(stringGetServerDescription, getServerDescription.Length);
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqGet(-1);
                this.squirrelApi.SqCall(0, true, false);           
                this.squirrelApi.SqGetString(1, out result);

                // Set back top.
                this.squirrelApi.SqSetTop(top);
                return result;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                int top = this.squirrelApi.SqGetTop();

                this.squirrelApi.SqPushString(stringSetServerDescription, setServerDescription.Length);
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqGet(-1);
                this.squirrelApi.SqPushString(value);
                this.squirrelApi.SqCall(2, false, false);

                // Set back top.
                this.squirrelApi.SqSetTop(top);
            }
        }

        public int RespawnTime { get; set; }

        public ServerTime Time { get; set; }

        public string World { get; set; }

        public void SendMessageToAll(int r, int g, int b, string message)
        {
            throw new NotImplementedException();
        }

        public void SendPacketToAll(IPacket packet, int reliability)
        {
            throw new NotImplementedException();
        }
    }
}