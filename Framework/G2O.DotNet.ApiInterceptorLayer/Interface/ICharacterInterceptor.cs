// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICharacterInterceptor.cs" company="Colony Online Project">
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
    using System.Drawing;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     Interface for the <see cref="ICharacter" /> interceptor.
    /// </summary>
    public interface ICharacterInterceptor : ICharacter
    {
        /// <summary>
        ///     Invokes all registered handlers if the "EquipItem" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string>> OnEquipItem;

        /// <summary>
        ///     Invokes all registered handlers if the "PlayAniId" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int>> OnPlayAniId;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "MaxHealth" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int>> OnSetMaxHealth;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "Name" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string>> OnSetName;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "NameColor" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<Color>> OnSetNameColor;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "Position" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<Point3D>> OnSetPosition;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "respawnTime" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int>> OnSetRespawnTime;

        /// <summary>
        ///     Invokes all registered handlers if the "SetSkillWeapon" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<WeaponSkill, int>> OnSetSkillWeapon;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "Strength" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<int>> OnSetStrength;

        /// <summary>
        ///     Invokes all registered handlers if the "SetTalent" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<Talent, int>> OnSetTalent;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "WeaponMode" property is set.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<WeaponMode>> OnSetWeaponMode;

        /// <summary>
        ///     Invokes all registered handlers if the "Spawn" method is called.
        /// </summary>
        event EventHandler<EventArgs> OnSpawn;

        /// <summary>
        ///     Invokes all registered handlers if the "StopAllAnimations" method is called.
        /// </summary>
        event EventHandler<EventArgs> OnStopAllAnimations;

        /// <summary>
        ///     Invokes all registered handlers if the "UnequipItem" method is called.
        /// </summary>
        event EventHandler<NotifyAboutCallEventArgs<string>> OnUnequipItem;

        /// <summary>
        ///     Invokes all registered handlers if the "Unspawn" method is called.
        /// </summary>
        event EventHandler<EventArgs> OnUnspawn;

        /// <summary>
        ///     Gets the <see cref="IInventoryInterceptor" /> instance that decorates the <see cref="IInventory" />
        ///     of the <see cref="ICharacter" /> that is decorated by the current <see cref="ICharacterInterceptor" /> instance.
        /// </summary>
        new IInventoryInterceptor Inventory { get; }
    }
}