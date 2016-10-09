// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientSquirrel.cs" company="Colony Online Project">
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
// Implementation of the IClient interface using the ISquirrelApi.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Client
{
    using System;
    using System.Net;

    using G2O.DotNet.ServerApi.Character;
    using G2O.DotNet.ServerApi.Server;
    using G2O.DotNet.Squirrel;
    using G2O.DotNet.Squirrel.Interop;

    /// <summary>
    ///     Implementation of the <see cref="IClient" /> interface using the <see cref="ISquirrelApi" />.
    /// </summary>
    internal class ClientSquirrel : IClient, IDisposable
    {
        /// <summary>
        ///     Stores the ansi version of the string "ban"
        /// </summary>
        private static readonly AnsiString StringBan = "ban";

        /// <summary>
        ///     Stores the ansi version of the string "getPlayerIP"
        /// </summary>
        private static readonly AnsiString StringGetPlayerIp = "getPlayerIP";

        /// <summary>
        ///     Stores the ansi version of the string "getPlayerMacAddr"
        /// </summary>
        private static readonly AnsiString StringGetPlayerMacAddr = "getPlayerMacAddr";

        /// <summary>
        ///     Stores the ansi version of the string "getPlayerPing"
        /// </summary>
        private static readonly AnsiString StringGetPlayerPing = "getPlayerPing";

        /// <summary>
        ///     Stores the ansi version of the string "getPlayerSerial"
        /// </summary>
        private static readonly AnsiString StringGetPlayerSerial = "getPlayerSerial";

        /// <summary>
        ///     Stores the ansi version of the string "kick"
        /// </summary>
        private static readonly AnsiString StringKick = "kick";

        /// <summary>
        ///     Stores the ansi version of the string "sendMessageToPlayer"
        /// </summary>
        private static readonly AnsiString StringSendMessageToPlayer = "sendMessageToPlayer";

        /// <summary>
        ///     Stores the ansi version of the string "sendPlayerMessageToAll"
        /// </summary>
        private static readonly AnsiString StringSendPlayerMessageToAll = "sendPlayerMessageToAll";

        /// <summary>
        ///     Stores the ansi version of the string "sendPlayerMessageToPlayer"
        /// </summary>
        private static readonly AnsiString StringSendPlayerMessageToPlayer = "sendPlayerMessageToPlayer";

        /// <summary>
        ///     The used instance of the <see cref="ISquirrelApi" />.
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        ///     Indicates whether this object is disposed or not.
        /// </summary>
        private bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientSquirrel" /> class.
        /// </summary>
        /// <param name="squirrelApi">The instance of the <see cref="ISquirrelApi" /> that should be used.</param>
        /// <param name="clientId">The id(player id) of the new <see cref="IClient" />.</param>
        /// <param name="server">The instance of the server API that should be used.</param>
        public ClientSquirrel(ISquirrelApi squirrelApi, int clientId, IServer server)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            if (clientId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(clientId));
            }

            this.squirrelApi = squirrelApi;

            this.ClientId = clientId;

            this.IpAddress = IPAddress.Parse(squirrelApi.Call<string>(StringGetPlayerIp, clientId));
            this.MacAddress = squirrelApi.Call<string>(StringGetPlayerMacAddr, clientId);
            this.Serial = squirrelApi.Call<string>(StringGetPlayerSerial, clientId);
            this.PlayerCharacter = new PlayerCharacterSquirrel(squirrelApi, this, server);
            this.Nickname = this.PlayerCharacter.Name;
        }

        /// <summary>
        ///     Invokes all registered handlers if a command is received from this <see cref="IClient" />.
        /// </summary>
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        /// <summary>
        ///     Invokes all registered handlers if the <see cref="IClient" /> disconnects from the server.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> Disconnect;

        /// <summary>
        ///     Invokes all registered handlers if a message is received from this <see cref="IClient" />.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        ///     Invokes all registered handlers if a <see cref="IPacket" /> is received from this <see cref="IClient" />.
        /// </summary>
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        /// <summary>
        ///     Gets the id(player id) of this <see cref="IClient" />.
        /// </summary>
        public int ClientId { get; }

        /// <summary>
        ///     Gets the IP address of this <see cref="IClient" />.
        /// </summary>
        public IPAddress IpAddress { get; }

        /// <summary>
        ///     Gets a value indicating whether the client is still connected.
        /// </summary>
        public bool IsConnected => this.disposed;

        /// <summary>
        ///     Gets the MAC address of the <see cref="IClient" />.
        /// </summary>
        public string MacAddress { get; }

        /// <summary>
        ///     Gets the nickname that was specified by the user before connecting.
        /// </summary>
        public string Nickname { get; }

        /// <summary>
        ///     Gets the ping of this <see cref="IClient" />.
        /// </summary>
        public int Ping => this.squirrelApi.Call<int>(StringGetPlayerPing, this.ClientId);

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> this <see cref="IClient" />.
        /// </summary>
        public ICharacter PlayerCharacter { get; }

        /// <summary>
        ///     Gets the serial(unique id generated by G2O) of the <see cref="IClient" />.
        /// </summary>
        public string Serial { get; }

        /// <summary>
        ///     Bans this <see cref="IClient" /> for a given duration
        /// </summary>
        /// <param name="reason">The Reason of the ban.</param>
        /// <param name="duration">The duration of the ban.</param>
        public void Ban(string reason, int duration)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(reason));
            }

            if (duration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(duration));
            }

            this.squirrelApi.Call(StringBan, this.ClientId, reason, duration);
        }

        /// <summary>
        ///     Releases all unmanaged resources related to this instance of <see cref="ClientSquirrel" />.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.Disconnect = null;
                this.PacketReceived = null;
            }
        }

        /// <summary>
        ///     Kick this <see cref="IClient" /> from the server.
        /// </summary>
        /// <param name="reason">The reason of the kick.</param>
        public void Kick(string reason)
        {
            if (string.IsNullOrEmpty(reason))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(reason));
            }

            this.squirrelApi.Call(StringKick, this.ClientId, reason);
        }

        /// <summary>
        ///     Sends a message to this <see cref="IClient" />.
        /// </summary>
        /// <param name="r">The Red value of the message.</param>
        /// <param name="g">The Green value of the message.</param>
        /// <param name="b">The Blue value of the message.</param>
        /// <param name="message">The message text.</param>
        public void SendMessage(int r, int g, int b, string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(ClientSquirrel), 
                    "The object is disposed because the client is no longer connected to the server.");
            }

            this.squirrelApi.Call(StringSendMessageToPlayer, this.ClientId, r, g, b, message);
        }

        /// <summary>
        ///     Sends a message from this client to all  <see cref="IClient" />s.
        /// </summary>
        /// <param name="r">The Red value of the message.</param>
        /// <param name="g">The Green value of the message.</param>
        /// <param name="b">The Blue value of the message.</param>
        /// <param name="message">The message text.</param>
        public void SendMessageToAll(int r, int g, int b, string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(ClientSquirrel), 
                    "The object is disposed because the client is no longer connected to the server.");
            }

            this.squirrelApi.Call(StringSendPlayerMessageToAll, this.ClientId, r, g, b, message);
        }

        /// <summary>
        ///     Sends a message from this <see cref="IClient" /> to another  <see cref="IClient" />.
        /// </summary>
        /// <param name="receiver">The <see cref="IClient" /> that should receive the message.</param>
        /// <param name="r">The Red value of the message.</param>
        /// <param name="g">The Green value of the message.</param>
        /// <param name="b">The Blue value of the message.</param>
        /// <param name="message">The message text.</param>
        public void SendMessageToClient(IClient receiver, int r, int g, int b, string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(ClientSquirrel), 
                    "The object is disposed because the client is no longer connected to the server.");
            }

            this.squirrelApi.Call(StringSendPlayerMessageToPlayer, this.ClientId, receiver.ClientId, r, g, b, message);
        }

        /// <summary>
        ///     Invokes the <see cref="CommandReceived" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="CommandReceivedEventArgs" />
        /// </param>
        internal void OnCommandReceived(CommandReceivedEventArgs e)
        {
            this.CommandReceived?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="Disconnect" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ClientDisconnectedEventArgs" />
        /// </param>
        internal void OnDisconnect(ClientDisconnectedEventArgs e)
        {
            this.Disconnect?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="MessageReceived" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="MessageReceivedEventArgs" />
        /// </param>
        internal void OnMessageReceived(MessageReceivedEventArgs e)
        {
            this.MessageReceived?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="PacketReceived" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="PacketReceivedEventArgs" />
        /// </param>
        internal void OnPacketReceived(PacketReceivedEventArgs e)
        {
            this.PacketReceived?.Invoke(this, e);
        }
    }
}