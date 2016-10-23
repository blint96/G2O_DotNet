// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCharacter.cs" company="Colony Online Project">
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
namespace G2O.DotNet.UserLayer
{
    using System;
    using System.Drawing;

    using G2O.DotNet.Database;
    using G2O.DotNet.ServerApi;

    internal class UserCharacter : ICharacter
    {
        private readonly UserClientList clientList;

        private readonly IDatabaseContextFactory contextFactory;

        private readonly ICharacter orgCharacter;

        private readonly UserClient parentClient;

        internal UserCharacter(ICharacter orgCharacter, UserClient parentClient, UserClientList clientList, IDatabaseContextFactory contextFactory)
        {
            if (orgCharacter == null)
            {
                throw new ArgumentNullException(nameof(orgCharacter));
            }

            if (parentClient == null)
            {
                throw new ArgumentNullException(nameof(parentClient));
            }

            if (clientList == null)
            {
                throw new ArgumentNullException(nameof(clientList));
            }
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.orgCharacter = orgCharacter;
            this.parentClient = parentClient;
            this.clientList = clientList;
            this.contextFactory = contextFactory;
            this.Inventory = new UserInventory(orgCharacter.Inventory, this, contextFactory);

            // Redirect the events of the original api object.
            this.orgCharacter.ArmorEquiped += this.OrgCharacterArmorEquiped;
            this.orgCharacter.Died += this.OrgCharacterDied;
            this.orgCharacter.FocusChanged += this.OrgCharacterFocusChanged;
            this.orgCharacter.HandItemEquiped += this.OrgCharacterHandItemEquiped;
            this.orgCharacter.HealthChanged += this.OrgCharacterHealthChanged;
            this.orgCharacter.HelmetEquiped += this.OrgCharacterHelmetEquiped;
            this.orgCharacter.Hit += this.OrgCharacterHit;
            this.orgCharacter.MaxHealthChanged += this.OrgCharacterMaxHealthChanged;
            this.orgCharacter.CharacterEnterWorld += this.OrgCharacterCharacterEnterWorld;
            this.orgCharacter.MeleeWeaponEquiped += this.OrgCharacterMeleeWeaponEquiped;
            this.orgCharacter.CharacterWorldChanged += this.OrgCharacterCharacterWorldChanged;
            this.orgCharacter.NameColorChanged += this.OrgCharacterNameColorChanged;
            this.orgCharacter.RangedWeaponEquiped += this.OrgCharacterRangedWeaponEquiped;
            this.orgCharacter.ShieldEquiped += this.OrgCharacterShieldEquiped;
            this.orgCharacter.Respawned += this.OrgCharacterRespawned;
            this.orgCharacter.Unconscious += this.OrgCharacterUnconscious;
            this.orgCharacter.WeaponModeChanged += this.OrgCharacterWeaponModeChanged;
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a body armor.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> joins a game world.
        /// </summary>
        public event EventHandler<CharacterWorldChangedEventArgs> CharacterEnterWorld;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes the game world that it is in.
        /// </summary>
        public event EventHandler<CharacterWorldChangedEventArgs> CharacterWorldChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> dies.
        /// </summary>
        public event EventHandler<DeadEventArgs> Died;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its focus.
        /// </summary>
        public event EventHandler<FocusChangedEventArgs> FocusChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a hand item.
        /// </summary>
        public event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its health value.
        /// </summary>
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a helmet.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> is hit by something(most likely another
        ///     <see cref="ICharacter" />).
        /// </summary>
        public event EventHandler<HitEventArgs> Hit;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its maximum health value.
        /// </summary>
        public event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a melee weapon.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its name
        ///     <see cref="System.Drawing.Color" />.
        /// </summary>
        public event EventHandler<NameColorChangedEventArgs> NameColorChanged;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a ranged weapon
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> RangedWeaponEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> respawns.
        /// </summary>
        public event EventHandler<EventArgs> Respawned;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a shield.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> becomes unconscious.
        /// </summary>
        public event EventHandler<UnconsciousEventArgs> Unconscious;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its <see cref="ICharacter.WeaponMode" />.
        /// </summary>
        public event EventHandler<ChangeWeaponModeEventArgs> WeaponModeChanged;

        /// <summary>
        ///     Gets or sets the angle of the character in the game world.
        /// </summary>
        public float Angle
        {
            get
            {
                return this.orgCharacter.Angle;
            }

            set
            {
                this.orgCharacter.Angle = value;
            }
        }

        /// <summary>
        ///     Gets or sets the world that the <see cref="ICharacter" /> is in.
        /// </summary>
        public string CharacterWorld
        {
            get
            {
                return this.orgCharacter.CharacterWorld;
            }

            set
            {
                this.orgCharacter.CharacterWorld = value;
            }
        }

        public IClient Client => this.parentClient;

        /// <summary>
        ///     Gets or sets the Dexterity value of the <see cref="ICharacter" />.
        /// </summary>
        public int Dexterity
        {
            get
            {
                return this.orgCharacter.Dexterity;
            }

            set
            {
                this.orgCharacter.Dexterity = value;
            }
        }

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> that is currently focused by this <see cref="ICharacter" />.
        ///     <remarks>Returns null if no <see cref="ICharacter" /> is focused.</remarks>
        /// </summary>
        public ICharacter Focus
        {
            get
            {
                var orgFocus = this.orgCharacter.Focus;
                if (orgFocus != null)
                {
                    return this.clientList[orgFocus.Client.ClientId].PlayerCharacter;
                }

                return null;
            }
        }

        /// <summary>
        ///     Gets or sets the the current health value of this <see cref="ICharacter" />.
        /// </summary>
        public int Health
        {
            get
            {
                return this.orgCharacter.Health;
            }

            set
            {
                this.orgCharacter.Health = value;
            }
        }

        /// <summary>
        ///     Gets the <see cref="IInventory" /> of this <see cref="ICharacter" />.
        /// </summary>
        public IInventory Inventory { get; }



        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is dead.
        /// </summary>
        public bool IsDead => this.orgCharacter.IsDead;

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is spawned.
        /// </summary>
        public bool IsSpawned => this.orgCharacter.IsSpawned;

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is unconscious.
        /// </summary>
        public bool IsUnconscious => this.orgCharacter.IsUnconscious;

        /// <summary>
        ///     Gets or sets the maximum health value of this <see cref="ICharacter" />.
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return this.orgCharacter.MaxHealth;
            }

            set
            {
                this.orgCharacter.MaxHealth = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name of this <see cref="ICharacter" />.
        /// </summary>
        public string Name
        {
            get
            {
                return this.orgCharacter.Name;
            }

            set
            {
                this.orgCharacter.Name = value;
            }
        }

        /// <summary>
        ///     Gets or sets the name color of this <see cref="ICharacter" />.
        /// </summary>
        public Color NameColor
        {
            get
            {
                return this.orgCharacter.NameColor;
            }

            set
            {
                this.orgCharacter.NameColor = value;
            }
        }

        /// <summary>
        ///     Gets or sets position of this <see cref="ICharacter" /> in the 3d game world.
        /// </summary>
        public Point3D Position
        {
            get
            {
                return this.orgCharacter.Position;
            }

            set
            {
                this.orgCharacter.Position = value;
            }
        }

        /// <summary>
        ///     Gets or sets the respawn-time for this <see cref="ICharacter" />.
        /// </summary>
        public int RespawnTime
        {
            get
            {
                return this.orgCharacter.RespawnTime;
            }

            set
            {
                this.orgCharacter.RespawnTime = value;
            }
        }

        /// <summary>
        ///     Gets or sets the strength value of this <see cref="ICharacter" />.
        /// </summary>
        public int Strength
        {
            get
            {
                return this.orgCharacter.Strength;
            }

            set
            {
                this.orgCharacter.Strength = value;
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="ICharacter.WeaponMode" /> of this <see cref="ICharacter" />.
        /// </summary>
        public WeaponMode WeaponMode
        {
            get
            {
                return this.orgCharacter.WeaponMode;
            }

            set
            {
                this.orgCharacter.WeaponMode = value;
            }
        }

        /// <summary>
        ///     Equips a item on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be equipped on this <see cref="ICharacter" />.</param>
        public void EquipItem(string itemInstance)
        {
            this.orgCharacter.EquipItem(itemInstance);
        }

        /// <summary>
        ///     Gets the id of the animation that is currently played by this <see cref="ICharacter" />.
        /// </summary>
        /// <returns>The id of the animation that is currently played by this <see cref="ICharacter" />.</returns>
        public int GetAniId()
        {
            return this.orgCharacter.GetAniId();
        }

        /// <summary>
        ///     Gets the weaponSkill value of a specified <see cref="WeaponSkill" /> of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <returns>The weapons skill value.</returns>
        public int GetSkillWeapon(WeaponSkill weaponSkill)
        {
            return this.orgCharacter.GetSkillWeapon(weaponSkill);
        }

        /// <summary>
        ///     Gets the value of a <see cref="Talent" /> of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <returns>The talent value.</returns>
        public int GetTalent(Talent talent)
        {
            return this.orgCharacter.GetTalent(talent);
        }

        /// <summary>
        ///     Starts playing a animation on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="aniId">The id of the animation that should be played on the <see cref="ICharacter" />.</param>
        public void PlayAniId(int aniId)
        {
            this.orgCharacter.PlayAniId(aniId);
        }

        /// <summary>
        ///     Sets the skill value for a weapon skill of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <param name="value">The new weapon skill value.</param>
        public void SetSkillWeapon(WeaponSkill weaponSkill, int value)
        {
            this.orgCharacter.SetSkillWeapon(weaponSkill, value);
        }

        /// <summary>
        ///     Sets the value for a talent of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <param name="value">The new talent value.</param>
        public void SetTalent(Talent talent, int value)
        {
            this.orgCharacter.SetTalent(talent, value);
        }

        /// <summary>
        ///     Spawns this <see cref="ICharacter" /> if it is not already spawned.
        /// </summary>
        public void Spawn()
        {
            this.orgCharacter.Spawn();
        }

        /// <summary>
        ///     Stops all animations that are currently played on this <see cref="ICharacter" />.
        /// </summary>
        public void StopAllAnimations()
        {
            this.orgCharacter.StopAllAnimations();
        }

        /// <summary>
        ///     Unequips a item that is currently equipped by this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be unequipped by this <see cref="ICharacter" />.</param>
        public void UnequipItem(string itemInstance)
        {
            this.orgCharacter.UnequipItem(itemInstance);
        }

        /// <summary>
        ///     Unspawns this <see cref="ICharacter" /> if it is spawned.
        /// </summary>
        public void UnspawnPlayer()
        {
            this.orgCharacter.UnspawnPlayer();
        }

        private void OrgCharacterArmorEquiped(object sender, ItemEquipedEventArgs e)
        {
            this.ArmorEquiped?.Invoke(this, e);
        }

        private void OrgCharacterCharacterEnterWorld(object sender, CharacterWorldChangedEventArgs e)
        {
            this.CharacterEnterWorld?.Invoke(this, e);
        }

        private void OrgCharacterCharacterWorldChanged(object sender, CharacterWorldChangedEventArgs e)
        {
            this.CharacterWorldChanged?.Invoke(this, e);
        }

        private void OrgCharacterDied(object sender, DeadEventArgs e)
        {
            ICharacter attacker = this.clientList[e.Killer.Client.ClientId].PlayerCharacter;
            this.Died?.Invoke(this, new DeadEventArgs(attacker));
        }

        private void OrgCharacterFocusChanged(object sender, FocusChangedEventArgs e)
        {
            ICharacter newFocus = this.clientList[e.NewFocus.Client.ClientId].PlayerCharacter;
            ICharacter oldFocus = this.clientList[e.OldFocus.Client.ClientId].PlayerCharacter;
            this.FocusChanged?.Invoke(this, new FocusChangedEventArgs(oldFocus, newFocus));
        }

        private void OrgCharacterHandItemEquiped(object sender, HandItemEquipedEventArgs e)
        {
            this.HandItemEquiped?.Invoke(this, e);
        }

        private void OrgCharacterHealthChanged(object sender, HealthChangedEventArgs e)
        {
            this.HealthChanged?.Invoke(this, e);
        }

        private void OrgCharacterHelmetEquiped(object sender, ItemEquipedEventArgs e)
        {
            this.HelmetEquiped?.Invoke(this, e);
        }

        private void OrgCharacterHit(object sender, HitEventArgs e)
        {
            ICharacter attacker = this.clientList[e.Attacker.Client.ClientId].PlayerCharacter;
            this.Hit?.Invoke(this, new HitEventArgs(attacker, e.Damage, e.Type));
        }

        private void OrgCharacterMaxHealthChanged(object sender, MaxHealthChangedEventArgs e)
        {
            this.MaxHealthChanged?.Invoke(this, e);
        }

        private void OrgCharacterMeleeWeaponEquiped(object sender, ItemEquipedEventArgs e)
        {
            this.MeleeWeaponEquiped?.Invoke(this, e);
        }

        private void OrgCharacterNameColorChanged(object sender, NameColorChangedEventArgs e)
        {
            this.NameColorChanged?.Invoke(this, e);
        }

        private void OrgCharacterRangedWeaponEquiped(object sender, ItemEquipedEventArgs e)
        {
            this.RangedWeaponEquiped?.Invoke(this, e);
        }

        private void OrgCharacterRespawned(object sender, EventArgs e)
        {
            this.Respawned?.Invoke(this, e);
        }

        private void OrgCharacterShieldEquiped(object sender, ItemEquipedEventArgs e)
        {
            this.ShieldEquiped?.Invoke(this, e);
        }

        private void OrgCharacterUnconscious(object sender, UnconsciousEventArgs e)
        {
            ICharacter attacker = this.clientList[e.Attacker.Client.ClientId].PlayerCharacter;
            this.Unconscious?.Invoke(this, new UnconsciousEventArgs(attacker));
        }

        private void OrgCharacterWeaponModeChanged(object sender, ChangeWeaponModeEventArgs e)
        {
            this.WeaponModeChanged?.Invoke(this, e);
        }
    }
}