// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginManager.cs" company="Colony Online Project">
// Copyright (C) <2016>  <Julian Vogel>
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// </copyright>
// <summary>
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.Squirrel
{
    public interface IPluginManager
    {
        int PluginCount { get; }

        /// <summary>
        ///     Gets the directory of the calling plugin.
        /// </summary>
        /// <returns> The directory of the calling plugin.</returns>
        string GetLocalPluginDirectory();

        string GetPluginFilePath();

        /// <summary>
        ///     Gets the public interface of another plugin.
        ///     <remarks>Can return null if the given interface is not available.</remarks>
        /// </summary>
        /// <typeparam name="TPlugin">The type of the interface that should be returned.</typeparam>
        /// <returns>The instance of the interface that was provided by the other plugin</returns>
        TPlugin GetPluginInterface<TPlugin>();
    }
}