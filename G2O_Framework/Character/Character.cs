// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Character.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Character
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Client;
    using GothicOnline.G2.DotNet.Exceptions;
    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Character : ICharacter
    {
        private static  readonly AnsiString StringGetPlayerRespawnTime = "getPlayerRespawnTime";

        private static readonly AnsiString StringSetPlayerRespawnTime = "setPlayerRespawnTime";


        private readonly ISquirrelApi squirrelApi;


        public Character(ISquirrelApi squirrelApi, IClient client)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.squirrelApi = squirrelApi;
            this.Client = client;
        }

        public event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        public event EventHandler<DeadEventArgs> Died;

        public event EventHandler<FocusChangedEventArgs> FocusChanged;

        public event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;

        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        public event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        public event EventHandler<HitEventArgs> Hit;

        public event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        public event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        public event EventHandler<ItemEquipedEventArgs> RangedEquiped;

        public event EventHandler<RespawnEventArgs> Respawned;

        public event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        public event EventHandler<UnconsciousEventArgs> Unconscious;

        public float Angle { get; set; }

        public IClient Client { get; }

        public int Dexterity { get; set; }

        public ICharacter Focus { get; }

        public int Health { get; set; }

        public IInventory Inventory { get; }

        public bool IsDead { get; }

        public bool IsSpawned { get; }

        public bool IsUnconscious { get; }

        public int MaxHealth { get; set; }

        public string Name { get; set; }

        public Color NameColor { get; set; }

        public Point3D Position { get; set; }

        public int RespawnTime
        {
            get
            {
                return this.squirrelApi.Call<int, int>(StringGetPlayerRespawnTime, this.Client.ClientId);        
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                this.squirrelApi.CallWithParameter<int, int>(StringSetPlayerRespawnTime, this.Client.ClientId, value);
            }
        }

        public int Strength { get; set; }

        public int WeaponMode { get; set; }

        public int GetAniId()
        {
            throw new NotImplementedException();
        }

        public int GetSkillWeapon(SkillWeapon skill)
        {
            throw new NotImplementedException();
        }

        public int GetTalent(Talent talent)
        {
            throw new NotImplementedException();
        }

        public int PlayAniId(int aniId)
        {
            throw new NotImplementedException();
        }

        public void SetSkillWeapon(SkillWeapon skill, int value)
        {
            throw new NotImplementedException();
        }

        public void SetTalent(Talent talent, int value)
        {
            throw new NotImplementedException();
        }

        public void Spawn()
        {
            throw new NotImplementedException();
        }

        public void StopAniId()
        {
            throw new NotImplementedException();
        }

        public void UnspawnPlayer()
        {
            throw new NotImplementedException();
        }
    }
}