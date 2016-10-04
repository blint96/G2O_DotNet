// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeaponMode.cs" company="Colony Online Project">
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
// Defines the weapon modes.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.ServerApi
{
    /// <summary>
    /// Defines the weapon modes.
    /// </summary>
    public enum WeaponMode
    {
        /// <summary>
        /// No weapon mode.
        /// </summary>
        NpcWeaponNone, 

        /// <summary>
        /// Weapon mode unarmed.
        /// </summary>
        NpcWeaponFist, 


        NpcWeaponDag, 

        /// <summary>
        /// Weapon mode for one handed weapons
        /// </summary>
        NpcWeapon_1Hs, 

        /// <summary>
        /// Weapon mode for two handed weapons.
        /// </summary>
        NpcWeapon_2Hs, 

        /// <summary>
        /// Weapon mode for bows.
        /// </summary>
        NpcWeaponBow, 

        /// <summary>
        /// Weapon mode for crossbows.
        /// </summary>
        NpcWeaponCbow, 

        /// <summary>
        /// Weapon mode for magic.
        /// </summary>
        NpcWeaponMag, 

        NpcWeaponMax
    }
}