// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientInterceptor.cs" company="Colony Online Project">
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

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     Interface for the client interceptor class.
    /// </summary>
    public interface IClientInterceptor : IClient
    {
        /// <summary>
        ///     Invokes all registered handlers before the "CommandReceived" event is invoked.
        /// </summary>
        event EventHandler<BeforeCommandReceivedEventArgs> BeforeCommandReceived;

        /// <summary>
        ///     Invokes all registered handlers when the "Ban" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string, int>> OnBan;

        /// <summary>
        ///     Invokes all registered handlers when the "Kick" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string>> OnKick;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessage" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int, int, int, string>> OnSendMessage;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessageToAll" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int, int, int, string>> OnSendMessageToAll;

        /// <summary>
        ///     Invokes all registered handlers when the "SendMessageToClient" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<IClient, int, int, int, string>> OnSendMessageToClient;

        /// <summary>
        ///     Gets the <see cref="ICharacterInterceptor" /> instance that decorates the <see cref="ICharacter" />
        ///     instance that is the player character of the <see cref="IClient" /> decorated by this
        ///     <see cref="IClientInterceptor" /> instance.
        /// </summary>
        new ICharacterInterceptor PlayerCharacter { get; }
    }
}