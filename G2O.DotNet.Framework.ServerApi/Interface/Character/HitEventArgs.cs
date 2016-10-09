// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HitEventArgs.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ServerApi.Character
{
    using System;

    public class HitEventArgs : EventArgs
    {
        public HitEventArgs(ICharacter attacker, int damage, int type)
        {
          if (attacker == null)
            {
                throw new ArgumentNullException(nameof(attacker));
            }
            if (damage <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }
            if (type < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            this.Attacker = attacker;
            this.Damage = damage;
            this.Type = type;
        }

        public HitEventArgs(int damage, int type)
        {
            this.Damage = damage;
            this.Type = type;
        }

        public ICharacter Attacker { get; }

        public int Damage { get; }
        public int Type { get; }
    }
}