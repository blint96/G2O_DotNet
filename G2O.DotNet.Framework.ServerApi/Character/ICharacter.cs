// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICharacter.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.ServerApi
{
    using System;
    using System.Drawing;

    using GothicOnline.G2.DotNet.ServerApi.Character;
    using GothicOnline.G2.DotNet.ServerApi.Client;

    /// <summary>
    ///     The interface for the classes that implement the object oriented wrapper of the character specific functions of the
    ///     G2O server API.
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a body armor.
        /// </summary>
        event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> joins a game world.
        /// </summary>
        event EventHandler<CharacterWorldChangedEventArgs> CharacterJoinWorld;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes the game world that it is in.
        /// </summary>
        event EventHandler<CharacterWorldChangedEventArgs> CharacterWorldChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> dies.
        /// </summary>
        event EventHandler<DeadEventArgs> Died;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its focus.
        /// </summary>
        event EventHandler<FocusChangedEventArgs> FocusChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a hand item.
        /// </summary>
        event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its health value.
        /// </summary>
        event EventHandler<HealthChangedEventArgs> HealthChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a helmet.
        /// </summary>
        event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> is hit by something(most likely another
        ///     <see cref="ICharacter" />).
        /// </summary>
        event EventHandler<HitEventArgs> Hit;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its maximum health value.
        /// </summary>
        event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a melee weapon.
        /// </summary>
        event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its name <see cref="Color" />.
        /// </summary>
        event EventHandler<NameColorChangedEventArgs> NameColorChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a ranged weapon
        /// </summary>
        event EventHandler<ItemEquipedEventArgs> RangedWeaponEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> respawns.
        /// </summary>
        event EventHandler<EventArgs> Respawned;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a shield.
        /// </summary>
        event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> becomes unconscious.
        /// </summary>
        event EventHandler<UnconsciousEventArgs> Unconscious;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its <see cref="WeaponMode" />.
        /// </summary>
        event EventHandler<ChangeWeaponModeEventArgs> WeaponModeChanged;

        /// <summary>
        ///     Gets or sets the angle of the character in the game world.
        /// </summary>
        float Angle { get; set; }

        /// <summary>
        ///     Gets or sets the world that the <see cref="PlayerCharacterSquirrel" /> is in.
        /// </summary>
        string CharacterWorld { get; set; }

        /// <summary>
        ///     Gets the client two which the <see cref="ICharacter" /> belongs to.
        ///     <remarks>Returns null if this is not a client(player) character.</remarks>
        /// </summary>
        IClient Client { get; }

        /// <summary>
        ///     Gets or sets the Dexterity value of the <see cref="ICharacter" />.
        /// </summary>
        int Dexterity { get; set; }

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> that is currently focused by this <see cref="ICharacter" />.
        ///     <remarks>Returns null if no <see cref="ICharacter" /> is focused.</remarks>
        /// </summary>
        ICharacter Focus { get; }

        /// <summary>
        ///     Gets or sets the the current health value of this <see cref="ICharacter" />.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        ///     Gets the <see cref="IInventory" /> of this <see cref="ICharacter" />.
        /// </summary>
        IInventory Inventory { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is dead.
        /// </summary>
        bool IsDead { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is spawned.
        /// </summary>
        bool IsSpawned { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is unconscious.
        /// </summary>
        bool IsUnconscious { get; }

        /// <summary>
        ///     Gets or sets the maximum health value of this <see cref="ICharacter" />.
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        ///     Gets or sets the name of this <see cref="ICharacter" />.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name color of this <see cref="ICharacter" />.
        /// </summary>
        Color NameColor { get; set; }

        /// <summary>
        ///     Gets or sets position of this <see cref="ICharacter" /> in the 3d game world.
        /// </summary>
        Point3D Position { get; set; }

        /// <summary>
        ///     Gets or sets the respawn-time for this <see cref="ICharacter" />.
        /// </summary>
        int RespawnTime { get; set; }

        /// <summary>
        ///     Gets or sets the strength value of this <see cref="ICharacter" />.
        /// </summary>
        int Strength { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="WeaponMode" /> of this <see cref="ICharacter" />.
        /// </summary>
        WeaponMode WeaponMode { get; set; }

        /// <summary>
        ///     Equips a item on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be equipped on this <see cref="ICharacter" />.</param>
        void EquipItem(string itemInstance);

        /// <summary>
        ///     Gets the id of the animation that is currently played by this <see cref="ICharacter" />.
        /// </summary>
        /// <returns>The id of the animation that is currently played by this <see cref="ICharacter" />.</returns>
        int GetAniId();

        /// <summary>
        ///     Gets the weaponSkill value of a specified <see cref="WeaponSkill" /> of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <returns>The weapons skill value.</returns>
        int GetSkillWeapon(WeaponSkill weaponSkill);

        /// <summary>
        ///     Gets the value of a <see cref="Talent" /> of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <returns>The talent value.</returns>
        int GetTalent(Talent talent);

        /// <summary>
        ///     Starts playing a animation on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="aniId">The id of the animation that should be played on the <see cref="ICharacter" />.</param>
        void PlayAniId(int aniId);

        /// <summary>
        ///     Sets the skill value for a weapon skill of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <param name="value">The new weapon skill value.</param>
        void SetSkillWeapon(WeaponSkill weaponSkill, int value);

        /// <summary>
        ///     Sets the value for a talent of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <param name="value">The new talent value.</param>
        void SetTalent(Talent talent, int value);

        /// <summary>
        ///     Spawns this <see cref="ICharacter" /> if it is not already spawned.
        /// </summary>
        void Spawn();

        /// <summary>
        ///     Stops all animations that are currently played on this <see cref="ICharacter" />.
        /// </summary>
        void StopAllAnimations();

        /// <summary>
        ///     Unequips a item that is currently equipped by this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be unequipped by this <see cref="ICharacter" />.</param>
        void UnequipItem(string itemInstance);

        /// <summary>
        ///     Unspawns this <see cref="ICharacter" /> if it is spawned.
        /// </summary>
        void UnspawnPlayer();
    }
}