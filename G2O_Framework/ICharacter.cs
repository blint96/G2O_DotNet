// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICharacter.cs" company="Colony Online Project">
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
    using System;
    using System.Drawing;

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

        event EventHandler<ItemEquipedEventArgs> RangedEquiped;

        event EventHandler<RespawnEventArgs> Respawned;

        event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        event EventHandler<UnconsciousEventArgs> Unconscious;

        float Angle { get; set; }

        IClient Client { get; }

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

        int Strength { get; set; }

        int WeaponMode { get; set; }

        int GetAniId();

        int GetSkillWeapon(SkillWeapon skill);

        int GetTalent(Talent talent);

        int PlayAniId(int aniId);

        void SetSkillWeapon(SkillWeapon skill, int value);

        void SetTalent(Talent talent, int value);

        void Spawn();

        void StopAniId();

        void UnspawnPlayer();
    }
}