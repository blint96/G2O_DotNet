// --------------------------------------------------------------------------------------------------------------------
// <copyright file="G2OServerSquirrel.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.ServerApi.Server
{
    using System;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Security.Cryptography;

    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.ServerApi.Client;
    using GothicOnline.G2.DotNet.Squirrel;
    using GothicOnline.G2.DotNet.Squirrel.Exceptions;

    /// <summary>
    /// Implementation of the G2O server api using the squirrel api to access the functions.
    /// </summary>
    [Export(typeof(IServer))]
    internal class G2OServerSquirrel : IServer
    {
        private static readonly AnsiString StringGetServerDescription = "getServerDescription";

        private static readonly AnsiString StringGetServerWorld = "getServerWorld";

        private static readonly AnsiString StringSendMessageToAll = "sendMessageToAll";

        private static readonly AnsiString StringSetServerDescription = "setServerDescription";

        private static readonly AnsiString StringSetServerWorld = "setServerWorld";

        private static readonly AnsiString StringSetTime = "setTime";
        private static readonly AnsiString StringGetTime = "getTime";

        private static readonly AnsiString StringHour = "hour";
        private static readonly AnsiString StringMinute = "min";
        private static readonly AnsiString StringDay = "day";

        private readonly ISquirrelApi squirrelApi;

        private readonly ServerEventListener serverEventListener;

        [ImportingConstructor]
        public G2OServerSquirrel([Import]ISquirrelApi squirrelApi)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            this.squirrelApi = squirrelApi;
            this.Clients = new ClientList(squirrelApi,this);
            this.serverEventListener = new ServerEventListener(squirrelApi, this);
        }

        public event EventHandler<ServerInitializedEventArgs> Initialize;



        public IClientList Clients { get; }

        public string Description
        {
            get
            {
                return this.squirrelApi.Call<string>(StringGetServerDescription);
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                this.squirrelApi.Call(StringSetServerDescription, value);
            }
        }

        public ServerTime Time
        {
            get
            {
                int top = this.squirrelApi.SqGetTop();
                try
                {
                    this.squirrelApi.SqPushRootTable();
                    this.squirrelApi.SqPushString(StringGetTime.Unmanaged, StringGetTime.Length);
                    if (!this.squirrelApi.SqGet(-2))
                    {
                        throw new SquirrelException(
                            $"The gothic online server function '{StringGetTime}' could not be found in the root table",
                            this.squirrelApi);
                    }

                    // Call the function
                    this.squirrelApi.SqPushRootTable();
                    if (!this.squirrelApi.SqCall(1, true, false))
                    {
                        throw new SquirrelException($"The call to the '{StringGetTime}' function failed", this.squirrelApi);
                    }
                    int resultTop = this.squirrelApi.SqGetTop();

                    //Hours
                    this.squirrelApi.SqPushString(StringHour.Unmanaged, StringHour.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringHour} value from the result of the '{StringGetTime}' function",
                            this.squirrelApi);
                    }
                    int hours;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out hours);

                    //Minutes
                    this.squirrelApi.SqPushString(StringMinute.Unmanaged, StringMinute.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringMinute} value from the result of the '{StringGetTime}' function",
                            this.squirrelApi);
                    }
                    int minutes;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out minutes);

                    //Days
                    this.squirrelApi.SqPushString(StringDay.Unmanaged, StringDay.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringDay} value from the result of the '{StringGetTime}' function",
                            this.squirrelApi);
                    }
                    int days;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out days);
                    return new ServerTime(hours, minutes, days);
                }
                finally
                {
                    //Reset the stack top if a exception occures.
                    this.squirrelApi.SqSetTop(top);
                }
            }

            set
            {
                this.squirrelApi.Call(StringSetTime, value.Hour, value.Minute, value.Day);
            }
        }

        public string World
        {
            get
            {
                return this.squirrelApi.Call<string>(StringGetServerWorld);
            }

            set
            {
                this.squirrelApi.Call(StringSetServerWorld, value);
            }
        }

        public void SendMessageToAll(Color color, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            this.squirrelApi.Call(StringSendMessageToAll, color.R, color.G, color.B, message);
        }

        public void SendMessageToAll(int r, int g, int b, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            this.squirrelApi.Call(StringSendMessageToAll, r, g, b, message);
        }

        public void SendPacketToAll(IPacket packet, PacketReliability reliability)
        {
            throw new NotImplementedException();
        }

        internal void OnInitialize(ServerInitializedEventArgs e)
        {
            this.Initialize?.Invoke(this, e);
        }
    }
}