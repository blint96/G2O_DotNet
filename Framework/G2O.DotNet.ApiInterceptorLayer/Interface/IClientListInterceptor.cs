// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientListInterceptor.cs" company="Colony Online Project">
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
    using System.Collections.Generic;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     Interface for the
    /// </summary>
    public interface IClientListInterceptor : IClientList
    {
        /// <summary>
        ///     Gets a enumerable of all <see cref="IClientInterceptor" /> instances in the current
        ///     <see cref="IClientListInterceptor" /> instance.
        /// </summary>
        new IEnumerable<IClientInterceptor> Clients { get; }

        /// <summary>
        ///     Gets the <see cref="IClientInterceptor" /> object that decorates the <see cref="IClient" /> object with the given
        ///     client id.
        /// </summary>
        /// <param name="clientId">
        ///     Client id of the <see cref="IClient" /> object for which the <see cref="IClientInterceptor" />
        ///     object  should be returned.
        /// </param>
        /// <returns>
        ///     The <see cref="IClientInterceptor" /> object that decorates the <see cref="IClient" /> object with the given
        ///     client id
        /// </returns>
        new IClientInterceptor this[int clientId] { get; }
    }
}