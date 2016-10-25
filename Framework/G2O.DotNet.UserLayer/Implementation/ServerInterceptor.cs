// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerInterceptor.cs" company="Colony Online Project">
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
    using System.ComponentModel.Composition;
    using System.Drawing;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     A class that decorates the <see cref="IServer" /> interface with events that allow listeners to be informed
    ///     about calls that change the method.
    /// </summary>
    [Export(typeof(IServerInterceptor))]
    [Export(typeof(IServer))]
    internal class ServerInterceptor : IServerInterceptor
    {
        /// <summary>
        ///     Stores a reference to the original <see cref="IServer" /> instance that gets decorated by the current instance of
        ///     <see cref="ServerInterceptor" />.
        /// </summary>
        private readonly IServer server;

        /// <summary>
        ///     Initializes the a new instance of the <see cref="ServerInterceptor" /> class.
        /// </summary>
        /// <param name="server">Original <see cref="IServer" /> instance.</param>
        [ImportingConstructor]
        internal ServerInterceptor([Import("SquirrelServerAPI")] IServer server)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            this.server = server;
            server.Initialize += (o, a) => this.Initialize?.Invoke(this, new ServerInitializedEventArgs(this));

            this.Clients = new ClientListInterceptor(server.Clients);
        }

        /// <summary>
        ///     Invokes all registered handlers when the server gets initialized.
        /// </summary>
        public event EventHandler<ServerInitializedEventArgs> Initialize;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessageToAll" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int, int, int, string>> OnSendMessageToAll;

        /// <summary>
        ///     Invokes all registered handlers when the "SendPacketToAll" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<IPacket, PacketReliability>> OnSendPacketToAll;

        /// <summary>
        ///     Invokes all registered handlers when the value of the "Description" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string>> OnSetDescription;

        /// <summary>
        ///     Invokes all registered handlers when the value of the "Time" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<ServerTime>> OnSetTime;

        /// <summary>
        ///     Invokes all registered handlers when the value of the "World" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string>> OnSetWorld;

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
                return this.server.Description;
            }

            set
            {
                this.server.Description = value;
                this.OnSetDescription?.Invoke(this, new NotifyAboutCallEventArgs<string>(value));
            }
        }

        /// <summary>
        ///     Gets or sets the server time.
        /// </summary>
        public ServerTime Time
        {
            get
            {
                return this.server.Time;
            }

            set
            {
                this.server.Time = value;
                this.OnSetTime?.Invoke(this, new NotifyAboutCallEventArgs<ServerTime>(value));
            }
        }

        /// <summary>
        ///     Gets or sets the server world.
        /// </summary>
        public string World
        {
            get
            {
                return this.server.World;
            }

            set
            {
                this.server.World = value;
                this.OnSetWorld?.Invoke(this, new NotifyAboutCallEventArgs<string>(value));
            }
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
            this.server.SendMessageToAll(r, g, b, message);
            this.OnSendMessageToAll?.Invoke(this, new NotifyAboutCallEventArgs<int, int, int, string>(r, g, b, message));
        }

        /// <summary>
        ///     Sends a message with a given color to all clients.
        /// </summary>
        /// <param name="color">The message color</param>
        /// <param name="message">The text of the message</param>
        public void SendMessageToAll(Color color, string message)
        {
            this.server.SendMessageToAll(color, message);
        }

        /// <summary>
        ///     Sends a data packet to all clients.
        /// </summary>
        /// <param name="packet">The packet that should be send to all clients.</param>
        /// <param name="reliability">The reliability of the packet.</param>
        public void SendPacketToAll(IPacket packet, PacketReliability reliability)
        {
            this.server.SendPacketToAll(packet, reliability);
            this.OnSendPacketToAll?.Invoke(
                this, 
                new NotifyAboutCallEventArgs<IPacket, PacketReliability>(packet, reliability));
        }
    }
}