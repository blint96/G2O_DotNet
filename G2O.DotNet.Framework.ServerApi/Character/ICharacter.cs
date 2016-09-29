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
// The interface for the classes that implement the object oriented wrapper of the character specific functions of the G2O server api.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.ServerApi
{
    using System;
    using System.Drawing;

    using GothicOnline.G2.DotNet.ServerApi.Character;
    using GothicOnline.G2.DotNet.ServerApi.Client;

    /// <summary>
    /// The interface for the classes that implement the object oriented wrapper of the character specific functions of the G2O server api.
    /// </summary>
    public interface ICharacter
    {
        event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        event EventHandler<DeadEventArgs> Died;

        event EventHandler<FocusChangedEventArgs> FocusChanged;

        event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;

        event EventHandler<HealthChangedEventArgs> HealthChanged;

        event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        event EventHandler<HitEventArgs> Hit;

        event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        event EventHandler<ItemEquipedEventArgs> RangedWeaponEquiped;

        event EventHandler<System.EventArgs> Respawned;

        event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        event EventHandler<UnconsciousEventArgs> Unconscious;

        event EventHandler<ChangeWeaponModeEventArgs> WeaponModeChanged;

        event EventHandler<NameColorChangedEventArgs> NameColorChanged;

        event EventHandler<CharacterWorldChangedEventArgs> OnCharacterWorldChanged;

        event EventHandler<CharacterWorldChangedEventArgs> OnCharacterJoinWorld;

        /// <summary>
        /// Gets or set the angle of the character in the game world.
        /// </summary>
        float Angle { get; set; }

        /// <summary>
        /// Gets the client two which the <see cref="ICharacter"/> belongs to.
        /// <remarks>Returns null if this is not a client(player) character.</remarks>
        /// </summary>
        IClient Client { get; }

        /// <summary>
        /// Gets or sets the Dexterity value of the <see cref="ICharacter"/>.
        /// </summary>
        int Dexterity { get; set; }

        ICharacter Focus { get; }

        int Health { get; set; }

        IInventory Inventory { get; }

        bool IsDead { get; }

        bool IsSpawned { get; }

        bool IsUnconscious { get; }

        int MaxHealth { get; set; }

        string Name { get; set; }

        Color NameColor { get; set; }

        Point3D Position { get; set; }

        int RespawnTime { get; set; }

        int Strength { get; set; }

        WeaponMode WeaponMode { get; set; }

        int GetAniId();

        int GetSkillWeapon(SkillWeapon skill);

        int GetTalent(Talent talent);

        void PlayAniId(int aniId);

        void SetSkillWeapon(SkillWeapon skill, int value);

        void SetTalent(Talent talent, int value);

        void Spawn();

        void StopAllAnimations();

        void UnspawnPlayer();

        /// <summary>
        /// Gets or sets the world that the <see cref="PlayerCharacter"/> is in.
        /// </summary>
        string CharacterWorld { get; set; }

        void EquipItem(string itemInstance);

        void UnequipItem(string itemInstance);
    }
}