// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientInterceptor.cs" company="Colony Online Project">
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
    using System.Collections.Generic;
    using System.Net;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     A class that decorates the <see cref="IClient" /> interface with events that allow listeners to be informed
    ///     about calls that change the method.
    /// </summary>
    internal class ClientInterceptor : IClientInterceptor
    {
        /// <summary>
        ///     Stores a reference to the original <see cref="IClient" /> instance that is decorated by the current
        ///     <see cref="ClientInterceptor" /> object.
        /// </summary>
        private readonly IClient orgClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientInterceptor" /> class.
        /// </summary>
        /// <param name="orgClient">
        ///     The reference to the original <see cref="IClient" /> instance that is decorated by the new
        ///     <see cref="ClientInterceptor" /> object.
        /// </param>
        /// <param name="clientListInterceptor">Reference to the <see cref="ClientListInterceptor" /> object.</param>
        internal ClientInterceptor(IClient orgClient, ClientListInterceptor clientListInterceptor)
        {
            if (orgClient == null)
            {
                throw new ArgumentNullException(nameof(orgClient));
            }

            if (clientListInterceptor == null)
            {
                throw new ArgumentNullException(nameof(clientListInterceptor));
            }

            this.orgClient = orgClient;
            this.orgClient.CommandReceived += this.OrgClientCommandReceived;
            this.orgClient.Disconnect +=
                (o, a) => this.Disconnect?.Invoke(this, new ClientDisconnectedEventArgs(this, a.Reason));
            this.orgClient.MessageReceived += (o, a) => this.MessageReceived?.Invoke(this, a);

            this.PlayerCharacter = new CharacterInterceptor(orgClient.PlayerCharacter, this, clientListInterceptor);
        }

        /// <summary>
        ///     Invokes all registered handlers before the "CommandReceived" event is invoked.
        /// </summary>
        public event EventHandler<BeforeCommandReceivedEventArgs> BeforeCommandReceived;

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
        ///     Invokes all registered handlers when the "Ban" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string, int>> OnBan;

        /// <summary>
        ///     Invokes all registered handlers when the "Kick" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string>> OnKick;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessage" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int, int, int, string>> OnSendMessage;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessageToAll" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int, int, int, string>> OnSendMessageToAll;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessageToClient" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<IClient, int, int, int, string>> OnSendMessageToClient;

        /// <summary>
        ///     Invokes all registered handlers if a <see cref="IPacket" /> is received from this <see cref="IClient" />.
        /// </summary>
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        /// <summary>
        ///     Gets the id(player id) of this <see cref="IClient" />.
        /// </summary>
        public int ClientId => this.orgClient.ClientId;

        /// <summary>
        ///     Gets the IP address of this <see cref="IClient" />.
        /// </summary>
        public IPAddress IpAddress => this.orgClient.IpAddress;

        /// <summary>
        ///     Gets a value indicating whether the client is still connected.
        /// </summary>
        public bool IsConnected => this.orgClient.IsConnected;

        /// <summary>
        ///     Gets the MAC address of the <see cref="IClient" />.
        /// </summary>
        public string MacAddress => this.orgClient.MacAddress;

        /// <summary>
        ///     Gets the nickname that was specified by the user before connecting.
        /// </summary>
        public string Nickname => this.orgClient.Nickname;

        /// <summary>
        ///     Gets the ping of this <see cref="IClient" />.
        /// </summary>
        public int Ping => this.orgClient.Ping;

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> this <see cref="IClient" />.
        /// </summary>
        public ICharacter PlayerCharacter { get; }

        /// <summary>
        ///     Gets the serial(unique id generated by G2O) of the <see cref="IClient" />.
        /// </summary>
        public string Serial => this.orgClient.Serial;

        /// <summary>
        ///     Gets the <see cref="ICharacterInterceptor" /> instance that decorates the <see cref="ICharacter" />
        ///     instance that is the player character of the <see cref="IClient" /> decorated by this
        ///     <see cref="IClientInterceptor" /> instance.
        /// </summary>
        ICharacterInterceptor IClientInterceptor.PlayerCharacter => this.PlayerCharacter as CharacterInterceptor;

        /// <summary>
        ///     Bans this <see cref="IClient" /> for a given duration
        /// </summary>
        /// <param name="reason">The Reason of the ban.</param>
        /// <param name="duration">The duration of the ban.</param>
        public void Ban(string reason, int duration)
        {
            this.orgClient.Ban(reason, duration);
            this.OnBan?.Invoke(this, new NotifyAboutCallEventArgs<string, int>(reason, duration));
        }

        /// <summary>
        ///     Kick this <see cref="IClient" /> from the server.
        /// </summary>
        /// <param name="reason">The reason of the kick.</param>
        public void Kick(string reason)
        {
            this.orgClient.Kick(reason);
            this.OnKick?.Invoke(this, new NotifyAboutCallEventArgs<string>(reason));
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
            this.orgClient.SendMessage(r, g, b, message);
            this.OnSendMessage?.Invoke(this, new NotifyAboutCallEventArgs<int, int, int, string>(r, g, b, message));
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
            this.orgClient.SendMessageToAll(r, g, b, message);
            this.OnSendMessageToAll?.Invoke(this, new NotifyAboutCallEventArgs<int, int, int, string>(r, g, b, message));
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
            this.orgClient.SendMessageToClient(receiver, r, g, b, message);
            this.OnSendMessageToClient?.Invoke(
                this, 
                new NotifyAboutCallEventArgs<IClient, int, int, int, string>(receiver, r, g, b, message));
        }

        /// <summary>
        ///     Intercepts the invocation to the <see cref="CommandReceived" /> event.
        /// </summary>
        /// <param name="sender">the original sender</param>
        /// <param name="e">The original <see cref="CommandReceivedEventArgs" /> args.</param>
        private void OrgClientCommandReceived(object sender, CommandReceivedEventArgs e)
        {
            // Check if there is any handler for the BeforeCommandReceived event.
            if (this.BeforeCommandReceived != null)
            {
                var args = new List<BeforeCommandReceivedEventArgs>();
                foreach (var @delegate in this.BeforeCommandReceived.GetInvocationList())
                {
                    // Invoke all handlers and let them decide if the command received event should be invoked.
                    var handler = (EventHandler<BeforeCommandReceivedEventArgs>)@delegate;
                    var arg = new BeforeCommandReceivedEventArgs(e.Command, e.Parameters);
                    args.Add(arg);
                    handler.Invoke(this, arg);
                }

                // All handlers decided to continue with the invocation of CommandReceived
                if (args.TrueForAll(obj => !obj.ChancelCommand))
                {
                    this.CommandReceived?.Invoke(this, e);
                }
            }
            else
            {
                this.CommandReceived?.Invoke(this, e);
            }
        }
    }
}