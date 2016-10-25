// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterInterceptor.cs" company="Colony Online Project">
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
    ///     A class that decorates the <see cref="ICharacter" /> interface with events that allow listeners to be informed
    ///     about calls that change the method.
    /// </summary>
    internal class CharacterInterceptor : ICharacterInterceptor
    {
        /// <summary>
        ///     Stores a reference to the <see cref="ClientListInterceptor" /> object.
        /// </summary>
        private readonly ClientListInterceptor clientListInterceptor;

        /// <summary>
        ///     Stores a reference to the original instance of <see cref="ICharacter" /> that is intercepted by the current
        ///     instance of <see cref="CharacterInterceptor" />
        /// </summary>
        private readonly ICharacter orgCharacter;

        /// <summary>
        ///     Stores a reference to the  parent <see cref="ClientInterceptor" /> instance.
        /// </summary>
        private readonly ClientInterceptor parentClient;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CharacterInterceptor" />
        /// </summary>
        /// <param name="orgCharacter">The original <see cref="ICharacter" /> instance that should be intercepted.</param>
        /// <param name="parentClient">The the parent <see cref="ClientInterceptor" /> instance.</param>
        /// <param name="clientListInterceptor">reference to the <see cref="ClientListInterceptor" /></param>
        internal CharacterInterceptor(
            ICharacter orgCharacter, 
            ClientInterceptor parentClient, 
            ClientListInterceptor clientListInterceptor)
        {
            if (orgCharacter == null)
            {
                throw new ArgumentNullException(nameof(orgCharacter));
            }

            if (parentClient == null)
            {
                throw new ArgumentNullException(nameof(parentClient));
            }

            if (clientListInterceptor == null)
            {
                throw new ArgumentNullException(nameof(clientListInterceptor));
            }

            this.orgCharacter = orgCharacter;
            this.parentClient = parentClient;
            this.clientListInterceptor = clientListInterceptor;
            this.Inventory = new InventoryInterceptor(orgCharacter.Inventory, this);
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a body armor.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> ArmorEquiped
        {
            add
            {
                this.orgCharacter.ArmorEquiped += value;
            }

            remove
            {
                this.orgCharacter.ArmorEquiped -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> joins a game world.
        /// </summary>
        public event EventHandler<CharacterWorldChangedEventArgs> CharacterEnterWorld
        {
            add
            {
                this.orgCharacter.CharacterEnterWorld += value;
            }

            remove
            {
                this.orgCharacter.CharacterEnterWorld -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes the game world that it is in.
        /// </summary>
        public event EventHandler<CharacterWorldChangedEventArgs> CharacterWorldChanged
        {
            add
            {
                this.orgCharacter.CharacterWorldChanged += value;
            }

            remove
            {
                this.orgCharacter.CharacterWorldChanged -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> dies.
        /// </summary>
        public event EventHandler<DeadEventArgs> Died
        {
            add
            {
                this.orgCharacter.Died += value;
            }

            remove
            {
                this.orgCharacter.Died -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its focus.
        /// </summary>
        public event EventHandler<FocusChangedEventArgs> FocusChanged
        {
            add
            {
                this.orgCharacter.FocusChanged += value;
            }

            remove
            {
                this.orgCharacter.FocusChanged -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a hand item.
        /// </summary>
        public event EventHandler<HandItemEquipedEventArgs> HandItemEquiped
        {
            add
            {
                this.orgCharacter.HandItemEquiped += value;
            }

            remove
            {
                this.orgCharacter.HandItemEquiped -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its health value.
        /// </summary>
        public event EventHandler<HealthChangedEventArgs> HealthChanged
        {
            add
            {
                this.orgCharacter.HealthChanged += value;
            }

            remove
            {
                this.orgCharacter.HealthChanged -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a helmet.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> HelmetEquiped
        {
            add
            {
                this.orgCharacter.HelmetEquiped += value;
            }

            remove
            {
                this.orgCharacter.HelmetEquiped -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> is hit by something(most likely another
        ///     <see cref="ICharacter" />).
        /// </summary>
        public event EventHandler<HitEventArgs> Hit
        {
            add
            {
                this.orgCharacter.Hit += value;
            }

            remove
            {
                this.orgCharacter.Hit -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its maximum health value.
        /// </summary>
        public event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged
        {
            add
            {
                this.orgCharacter.MaxHealthChanged += value;
            }

            remove
            {
                this.orgCharacter.MaxHealthChanged -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a melee weapon.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped
        {
            add
            {
                this.orgCharacter.MeleeWeaponEquiped += value;
            }

            remove
            {
                this.orgCharacter.MeleeWeaponEquiped -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its name
        ///     <see cref="System.Drawing.Color" />.
        /// </summary>
        public event EventHandler<NameColorChangedEventArgs> NameColorChanged
        {
            add
            {
                this.orgCharacter.NameColorChanged += value;
            }

            remove
            {
                this.orgCharacter.NameColorChanged -= value;
            }
        }

        /// <summary>
        ///     Invokes all registered handlers if the "EquipItem" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string>> OnEquipItem;

        /// <summary>
        ///     Invokes all registered handlers if the "PlayAniId" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int>> OnPlayAniId;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "MaxHealth" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int>> OnSetMaxHealth;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "Name" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string>> OnSetName;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "NameColor" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<Color>> OnSetNameColor;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "Position" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<Point3D>> OnSetPosition;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "respawnTime" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int>> OnSetRespawnTime;

        /// <summary>
        ///     Invokes all registered handlers if the "SetSkillWeapon" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<WeaponSkill, int>> OnSetSkillWeapon;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "Strength" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<int>> OnSetStrength;

        /// <summary>
        ///     Invokes all registered handlers if the "SetTalent" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<Talent, int>> OnSetTalent;

        /// <summary>
        ///     Invokes all registered handlers if the value of the "WeaponMode" property is set.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<WeaponMode>> OnSetWeaponMode;

        /// <summary>
        ///     Invokes all registered handlers if the "Spawn" method is called.
        /// </summary>
        public event EventHandler<EventArgs> OnSpawn;

        /// <summary>
        ///     Invokes all registered handlers if the "StopAllAnimations" method is called.
        /// </summary>
        public event EventHandler<EventArgs> OnStopAllAnimations;

        /// <summary>
        ///     Invokes all registered handlers if the "UnequipItem" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string>> OnUnequipItem;

        /// <summary>
        ///     Invokes all registered handlers if the "Unspawn" method is called.
        /// </summary>
        public event EventHandler<EventArgs> OnUnspawn;

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a ranged weapon
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> RangedWeaponEquiped
        {
            add
            {
                this.orgCharacter.RangedWeaponEquiped += value;
            }

            remove
            {
                this.orgCharacter.RangedWeaponEquiped -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> respawns.
        /// </summary>
        public event EventHandler<EventArgs> Respawned
        {
            add
            {
                this.orgCharacter.Respawned += value;
            }

            remove
            {
                this.orgCharacter.Respawned -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> equips a shield.
        /// </summary>
        public event EventHandler<ItemEquipedEventArgs> ShieldEquiped
        {
            add
            {
                this.orgCharacter.ShieldEquiped += value;
            }

            remove
            {
                this.orgCharacter.ShieldEquiped -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> becomes unconscious.
        /// </summary>
        public event EventHandler<UnconsciousEventArgs> Unconscious
        {
            add
            {
                this.orgCharacter.Unconscious += value;
            }

            remove
            {
                this.orgCharacter.Unconscious -= value;
            }
        }

        /// <summary>
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its <see cref="ICharacter.WeaponMode" />.
        /// </summary>
        public event EventHandler<ChangeWeaponModeEventArgs> WeaponModeChanged
        {
            add
            {
                this.orgCharacter.WeaponModeChanged += value;
            }

            remove
            {
                this.orgCharacter.WeaponModeChanged -= value;
            }
        }

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

        /// <summary>
        ///     Gets the client two which the <see cref="ICharacter" /> belongs to.
        ///     <remarks>Returns null if this is not a client(player) character.</remarks>
        /// </summary>
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
                    return this.clientListInterceptor[orgFocus.Client.ClientId].PlayerCharacter;
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
                this.OnSetMaxHealth?.Invoke(this, new NotifyAboutCallEventArgs<int>(value));
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
                this.OnSetName?.Invoke(this, new NotifyAboutCallEventArgs<string>(value));
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
                this.OnSetNameColor?.Invoke(this, new NotifyAboutCallEventArgs<Color>(value));
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
                this.OnSetPosition?.Invoke(this, new NotifyAboutCallEventArgs<Point3D>(value));
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
                this.OnSetRespawnTime?.Invoke(this, new NotifyAboutCallEventArgs<int>(value));
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
                this.OnSetStrength?.Invoke(this, new NotifyAboutCallEventArgs<int>(value));
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
                this.OnSetWeaponMode?.Invoke(this, new NotifyAboutCallEventArgs<WeaponMode>(value));
            }
        }

        /// <summary>
        ///     Gets the <see cref="IInventoryInterceptor" /> instance that decorates the <see cref="IInventory" />
        ///     of the <see cref="ICharacter" /> that is decorated by the current <see cref="ICharacterInterceptor" /> instance.
        /// </summary>
        IInventoryInterceptor ICharacterInterceptor.Inventory => this.Inventory as IInventoryInterceptor;

        /// <summary>
        ///     Equips a item on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be equipped on this <see cref="ICharacter" />.</param>
        public void EquipItem(string itemInstance)
        {
            this.orgCharacter.EquipItem(itemInstance);
            this.OnEquipItem?.Invoke(this, new NotifyAboutCallEventArgs<string>(itemInstance));
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
            this.OnPlayAniId?.Invoke(this, new NotifyAboutCallEventArgs<int>(aniId));
        }

        /// <summary>
        ///     Sets the skill value for a weapon skill of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <param name="value">The new weapon skill value.</param>
        public void SetSkillWeapon(WeaponSkill weaponSkill, int value)
        {
            this.orgCharacter.SetSkillWeapon(weaponSkill, value);
            this.OnSetSkillWeapon?.Invoke(this, new NotifyAboutCallEventArgs<WeaponSkill, int>(weaponSkill, value));
        }

        /// <summary>
        ///     Sets the value for a talent of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <param name="value">The new talent value.</param>
        public void SetTalent(Talent talent, int value)
        {
            this.orgCharacter.SetTalent(talent, value);
            this.OnSetTalent?.Invoke(this, new NotifyAboutCallEventArgs<Talent, int>(talent, value));
        }

        /// <summary>
        ///     Spawns this <see cref="ICharacter" /> if it is not already spawned.
        /// </summary>
        public void Spawn()
        {
            this.orgCharacter.Spawn();
            this.OnSpawn?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Stops all animations that are currently played on this <see cref="ICharacter" />.
        /// </summary>
        public void StopAllAnimations()
        {
            this.orgCharacter.StopAllAnimations();
            this.OnStopAllAnimations?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Unequips a item that is currently equipped by this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be unequipped by this <see cref="ICharacter" />.</param>
        public void UnequipItem(string itemInstance)
        {
            this.orgCharacter.UnequipItem(itemInstance);
            this.OnUnequipItem?.Invoke(this, new NotifyAboutCallEventArgs<string>(itemInstance));
        }

        /// <summary>
        ///     Unspawns this <see cref="ICharacter" /> if it is spawned.
        /// </summary>
        public void Unspawn()
        {
            this.orgCharacter.Unspawn();
            this.OnUnspawn?.Invoke(this, new EventArgs());
        }
    }
}