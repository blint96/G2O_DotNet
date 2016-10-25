// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServerInterceptor.cs" company="Colony Online Project">
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
    ///     Interface for the <see cref="IServer" /> interceptor class.
    /// </summary>
    internal interface IServerInterceptor : IServer
    {
        /// <summary>
        ///     Invokes all registered handlers when the "SendMessageToAll" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int, int, int, string>> OnSendMessageToAll;

        /// <summary>
        ///     Invokes all registered handlers when the "SendPacketToAll" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<IPacket, PacketReliability>> OnSendPacketToAll;

        /// <summary>
        ///     Invokes all registered handlers when the value of the "Description" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string>> OnSetDescription;

        /// <summary>
        ///     Invokes all registered handlers when the value of the "Time" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<ServerTime>> OnSetTime;

        /// <summary>
        ///     Invokes all registered handlers when the value of the "World" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string>> OnSetWorld;
    }
}