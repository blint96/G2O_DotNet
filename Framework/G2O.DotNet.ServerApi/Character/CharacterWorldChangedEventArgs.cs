﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterWorldChangedEventArgs.cs" company="Colony Online Project">
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
// EventArgs class for the CharacterWorldChanged event.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi
{
    using System;

    /// <summary>
    ///     <see cref="EventArgs" /> class for the CharacterWorldChanged event.
    /// </summary>
    public class CharacterWorldChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CharacterWorldChangedEventArgs" /> class.
        /// </summary>
        /// <param name="newWorld">The world to which the <see cref="ICharacter" /> has changed.</param>
        public CharacterWorldChangedEventArgs(string newWorld)
        {
            if (string.IsNullOrEmpty(newWorld))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(newWorld));
            }

            this.NewWorld = newWorld;
        }

        /// <summary>
        ///     Gets the world to which the <see cref="ICharacter" /> has changed.
        /// </summary>
        public string NewWorld { get; }
    }
}