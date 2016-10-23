// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseContextFactory.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Database
{
    /// <summary>
    ///     Interface for classes that can create instances of the <see cref="IServerContext" /> interface.
    /// </summary>
    internal interface IDatabaseContextFactory
    {
        /// <summary>
        ///     Creates a new instance of the server database context of the framework database.
        /// </summary>
        /// <returns>The newly created instance of the <see cref="IServerContext" /></returns>
        IServerContext CreateContext();
    }
}