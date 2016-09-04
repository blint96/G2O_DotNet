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
    using GothicOnline.G2.DotNet.Squirrel;

    public class Character : ICharacter
    {
        private const string stringGetPlayerRespawnTime = "getPlayerRespawnTime";

        private const string stringSetPlayerRespawnTime = "setPlayerRespawnTime";

        private static readonly IntPtr ansiGetPlayerRespawnTime;

        private static readonly IntPtr ansiSetPlayerRespawnTime;

        private readonly ISquirrelApi squirrelApi;

        static Character()
        {
            ansiSetPlayerRespawnTime = Marshal.StringToHGlobalAnsi(stringSetPlayerRespawnTime);
            ansiGetPlayerRespawnTime = Marshal.StringToHGlobalAnsi(stringGetPlayerRespawnTime);
        }

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
                // Get the stack top index
                int top = this.squirrelApi.SqGetTop();

                // Get the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(ansiGetPlayerRespawnTime, stringGetPlayerRespawnTime.Length);
                if (!this.squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{stringGetPlayerRespawnTime}' could not be found in the root table", 
                        this.squirrelApi);
                }

                // Call the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushInteger(this.Client.ClientId);
                if (!this.squirrelApi.SqCall(2, true, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{stringGetPlayerRespawnTime}' function failed", 
                        this.squirrelApi);
                }

                // Get the result
                int result;
                this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out result);

                // Set back top
                this.squirrelApi.SqSetTop(top);
                return result;
            }

            set
            {
                // Get the stack top index
                int top = this.squirrelApi.SqGetTop();

                // Get the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushString(ansiSetPlayerRespawnTime, stringSetPlayerRespawnTime.Length);
                if (!this.squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{stringSetPlayerRespawnTime}' could not be found in the root table", 
                        this.squirrelApi);
                }

                // Call the function
                this.squirrelApi.SqPushRootTable();
                this.squirrelApi.SqPushInteger(this.Client.ClientId);
                this.squirrelApi.SqPushInteger(value);
                if (!this.squirrelApi.SqCall(2, true, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{stringSetPlayerRespawnTime}' function failed", 
                        this.squirrelApi);
                }

                // Set back top.
                this.squirrelApi.SqSetTop(top);
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