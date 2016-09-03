// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorChangedEventArgs.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  <ulian Vogel
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
namespace GothicOnline.G2.DotNet.Character
{
    using System;
    using System.Drawing;

    public class ColorChangedEventArgs : EventArgs
    {
        public ColorChangedEventArgs(ICharacter character, Color newColor)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            this.Character = character;
            this.NewColor = newColor;
        }

        public ICharacter Character { get; }

        public Color NewColor { get; }
    }
}