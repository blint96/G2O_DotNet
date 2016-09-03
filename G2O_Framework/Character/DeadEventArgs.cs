// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeadEventArgs.cs" company="Colony Online Project">
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

    public class DeadEventArgs : EventArgs
    {
        public DeadEventArgs(ICharacter character, ICharacter killer)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            if (killer == null)
            {
                throw new ArgumentNullException(nameof(killer));
            }

            this.Character = character;
            this.Killer = killer;
        }

        public DeadEventArgs(ICharacter character)
        {
            this.Character = character;
        }

        public ICharacter Character { get; }

        public ICharacter Killer { get; }
    }
}