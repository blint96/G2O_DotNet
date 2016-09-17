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
    using Squirrel;

    internal class Character : ICharacter,IDisposable
    {
        private static readonly AnsiString StringGetPlayerRespawnTime = "getPlayerRespawnTime";

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

        //onPlayerEquipArmor(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        //onPlayerDead(int killer_id, int id)
        public event EventHandler<DeadEventArgs> Died;

        //onPlayerChangeFocus(int pid, int oldFocusId, int currFocusId)
        public event EventHandler<FocusChangedEventArgs> FocusChanged;

        //onPlayerEquipHandItem(int pid, int hand, string instance)
        public event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;

        //onPlayerChangeHealth(int id, int oldHp, int currHp)
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        //onPlayerEquipHelmet(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        //onPlayerHit(int killer_id, int id, int dmg, int type)
        public event EventHandler<HitEventArgs> Hit;

        //onPlayerChangeMaxHealth(int id, int oldMaxHp, int currMaxHp)
        public event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        //onPlayerEquipMeleeWeapon(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        //onPlayerEquipRangedWeapon(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> RangedEquiped;

        //onPlayerRespawn(int id)
        public event EventHandler<RespawnEventArgs> Respawned;


        //onPlayerEquipShield(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        //onPlayerUnconscious(int killer_id, int id)
        public event EventHandler<UnconsciousEventArgs> Unconscious;

        /*
         * setPlayerAngle(int id, int angle)
            getPlayerAngle(int id)
         */

        private static readonly AnsiString StringSetPlayerAngle = "getsetPlayerAnglePlayerHealth";
        private static readonly AnsiString StringGetPlayerAngle = "getPlayerAngle";
        public float Angle
        {
            get
            {
                 return this.squirrelApi.Call<int>(StringGetPlayerAngle, this.Client.ClientId);
            }
            set
            {
                //Normalize the angle.
                value %= 360;
                this.squirrelApi.Call(StringSetPlayerAngle, this.Client.ClientId,value);
            }
        }

        public IClient Client { get; }
        /*
         * setPlayerDexterity(int id, int dex)
            getPlayerDexterity(int id)
         */
        public int Dexterity { get; set; }

        //getPlayerFocus(int id)
        public ICharacter Focus { get; }

        /*
         * setPlayerHealth(int id, int hp)
            getPlayerHealth(int id)
         */

        private static readonly AnsiString StringGetPlayerHealth = "getPlayerHealth";
        private static readonly AnsiString StringSetPlayerHealth = "setPlayerHealth";
        public int Health
        {
            get
            {
              return this.squirrelApi.Call<int>(StringGetPlayerHealth, this.Client.ClientId);
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                this.squirrelApi.Call<int>(StringSetPlayerHealth, this.Client.ClientId,value);
            }
        }

        public IInventory Inventory { get; }

        private static readonly AnsiString StringIsPlayerDead = "isPlayerDead";

        //isPlayerDead(int id)
        public bool IsDead => this.squirrelApi.Call<bool>(StringIsPlayerDead, this.Client.ClientId);


        private static readonly AnsiString StringIsPlayerSpawned = "isPlayerSpawned";
        //isPlayerSpawned(int id)
        public bool IsSpawned => this.squirrelApi.Call<bool>(StringIsPlayerSpawned, this.Client.ClientId);

        private static readonly AnsiString StringIsPlayerUnconscious = "isPlayerUnconscious";

        //isPlayerUnconscious(int id)
        public bool IsUnconscious => this.squirrelApi.Call<bool>(StringIsPlayerUnconscious, this.Client.ClientId);

        /*
         * setPlayerMaxHealth(int id, int maxHp)
getPlayerMaxHealth(int id)
         */
        public int MaxHealth { get; set; }

        /*setPlayerName(int id, string name)
         getPlayerName(int id)*/
        public string Name { get; set; }

        /*
         * setPlayerColor(int id, int r, int g, int b)
            getPlayerColor(int id)
         */
        public Color NameColor { get; set; }

        /*
         * 
         * setPlayerPosition(int id, int x, int y, int z)
getPlayerPosition(int id)*/
        public Point3D Position { get; set; }


        public int RespawnTime
        {
            get
            {
                return this.squirrelApi.Call<int>(StringGetPlayerRespawnTime, this.Client.ClientId);
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                this.squirrelApi.Call(StringSetPlayerRespawnTime, this.Client.ClientId, value);
            }
        }

        /*
         * setPlayerStrength(int id, int str)
            getPlayerStrength(int id)
         */
        public int Strength { get; set; }

        /*
         * setPlayerWeaponMode(int id, int wm)
            getPlayerWeaponMode(int id)
         */
        public int WeaponMode { get; set; }


        //getAniId(int pid)
        public int GetAniId()
        {
            throw new NotImplementedException();
        }


        //getPlayerSkillWeapon(int id)
        public int GetSkillWeapon(SkillWeapon skill)
        {
            throw new NotImplementedException();
        }

        //getPlayerTalent(id)
        public int GetTalent(Talent talent)
        {
            throw new NotImplementedException();
        }

        //playAniId(int pid, int id)
        public int PlayAniId(int aniId)
        {
            throw new NotImplementedException();
        }

        //setPlayerSkillWeapon(int id, int skill_id, int value)
        public void SetSkillWeapon(SkillWeapon skill, int value)
        {
            throw new NotImplementedException();
        }

        //setPlayerTalent(int id, int talent_id, int value)
        public void SetTalent(Talent talent, int value)
        {
            throw new NotImplementedException();
        }

        //spawnPlayer(int id)
        public void Spawn()
        {
            throw new NotImplementedException();
        }

        //stopAni(int pid)
        public void StopAniId()
        {
            throw new NotImplementedException();
        }

        //unspawnPlayer(int id)
        public void UnspawnPlayer()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates whether this object is disposed or not.
        /// </summary>
        private bool disposed;

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
            }
        }
    }
}
