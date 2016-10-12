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
namespace G2O.DotNet.ServerApi.Squirrel
{
    using System;

    using G2O.DotNet.Squirrel;

    using System.ComponentModel.Composition;
    using System.Drawing;

    /// <summary>
    ///     Implementation of the G2O server api using the squirrel api to access the functions.
    ///     <remarks>This is a MEF component.</remarks>
    /// </summary>
    [Export(typeof(IServer))]
    internal class G2OServerSquirrel : IServer
    {
        /// <summary>
        ///     Stores the ansi version of the string "day"
        /// </summary>
        private static readonly AnsiString StringDay = "day";

        /// <summary>
        ///     Stores the ansi version of the string "getServerDescription"
        /// </summary>
        private static readonly AnsiString StringGetServerDescription = "getServerDescription";

        /// <summary>
        ///     Stores the ansi version of the string "getServerWorld"
        /// </summary>
        private static readonly AnsiString StringGetServerWorld = "getServerWorld";

        /// <summary>
        ///     Stores the ansi version of the string "getTime"
        /// </summary>
        private static readonly AnsiString StringGetTime = "getTime";

        /// <summary>
        ///     Stores the ansi version of the string "hour"
        /// </summary>
        private static readonly AnsiString StringHour = "hour";

        /// <summary>
        ///     Stores the ansi version of the string "min"
        /// </summary>
        private static readonly AnsiString StringMinute = "min";

        /// <summary>
        ///     Stores the ansi version of the string "sendMessageToAll"
        /// </summary>
        private static readonly AnsiString StringSendMessageToAll = "sendMessageToAll";

        /// <summary>
        ///     Stores the ansi version of the string "setServerDescription"
        /// </summary>
        private static readonly AnsiString StringSetServerDescription = "setServerDescription";

        /// <summary>
        ///     Stores the ansi version of the string "setServerWorld"
        /// </summary>
        private static readonly AnsiString StringSetServerWorld = "setServerWorld";

        /// <summary>
        ///     Stores the ansi version of the string "setTime"
        /// </summary>
        private static readonly AnsiString StringSetTime = "setTime";

        /// <summary>
        ///     Instance of the <see cref="ServerEventListenerSquirrel" /> class that encapsulates the listening to the server events.
        /// </summary>
        private readonly ServerEventListenerSquirrel serverEventListenerSquirrel;

        /// <summary>
        ///     The instance of the squirrel api that is used to call the G2O server functions.
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        ///     Initializes a new instance of the <see cref="G2OServerSquirrel" /> class.
        ///     <remarks>This is a MEF importing constructor.</remarks>
        /// </summary>
        /// <param name="squirrelApi">The instance of the squirrel api that should be used to call the G2O server functions.</param>
        [ImportingConstructor]
        public G2OServerSquirrel([Import] ISquirrelApi squirrelApi)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            this.squirrelApi = squirrelApi;
            this.Clients = new ClientListSquirrel(squirrelApi, this);
            this.serverEventListenerSquirrel = new ServerEventListenerSquirrel(squirrelApi, this);
        }

        /// <summary>
        ///     Invokes all registered handlers when the server wants to initializes all logic scripts and modules.
        /// </summary>
        public event EventHandler<ServerInitializedEventArgs> Initialize;

        /// <summary>
        ///     Gets the client list.
        /// </summary>
        public IClientList Clients { get; }

        /// <summary>
        ///     Gets or sets the server description.
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the server time.
        /// </summary>
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
                        throw new SquirrelException(
                            $"The call to the '{StringGetTime}' function failed", 
                            this.squirrelApi);
                    }

                    int resultTop = this.squirrelApi.SqGetTop();

                    // Hours
                    this.squirrelApi.SqPushString(StringHour.Unmanaged, StringHour.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringHour} value from the result of the '{StringGetTime}' function", 
                            this.squirrelApi);
                    }

                    int hours;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out hours);

                    // Minutes
                    this.squirrelApi.SqPushString(StringMinute.Unmanaged, StringMinute.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringMinute} value from the result of the '{StringGetTime}' function", 
                            this.squirrelApi);
                    }

                    int minutes;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out minutes);

                    // Days
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
                    // Reset the stack top if a exception occures.
                    this.squirrelApi.SqSetTop(top);
                }
            }

            set
            {
                this.squirrelApi.Call(StringSetTime, value.Hour, value.Minute, value.Day);
            }
        }

        /// <summary>
        ///     Gets or sets the server world.
        /// </summary>
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

        /// <summary>
        ///     Sends a message with a given color to all clients.
        /// </summary>
        /// <param name="color">The message color</param>
        /// <param name="message">The text of the message</param>
        public void SendMessageToAll(Color color, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            this.squirrelApi.Call(StringSendMessageToAll, color.R, color.G, color.B, message);
        }

        /// <summary>
        ///     Sends a message with a given color to all clients.
        /// </summary>
        /// <param name="r">Red value of the message color.</param>
        /// <param name="g">Green value of the message color.</param>
        /// <param name="b">Blue value of the message color.</param>
        /// <param name="message">The text of the message</param>
        public void SendMessageToAll(int r, int g, int b, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            }

            this.squirrelApi.Call(StringSendMessageToAll, r, g, b, message);
        }

        /// <summary>
        ///     Sends a data packet to all clients.
        /// </summary>
        /// <param name="packet">The packet that should be send to all clients.</param>
        /// <param name="reliability">The reliability of the packet.</param>
        public void SendPacketToAll(IPacket packet, PacketReliability reliability)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Invokes the <see cref="Initialize" /> event.
        /// </summary>
        /// <param name="e">The instance of the <see cref="ServerInitializedEventArgs" /> class.</param>
        internal void OnInitialize(ServerInitializedEventArgs e)
        {
            this.Initialize?.Invoke(this, e);
        }
    }
}