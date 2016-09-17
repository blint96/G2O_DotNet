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
    using System.ComponentModel;
    using System.Drawing;

    using GothicOnline.G2.DotNet.Client;
    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Character : ICharacter, IDisposable
    {
        // getAniId(int pid)
        private static readonly AnsiString StringGetAniId = "getAniId";

        private static readonly AnsiString StringGetPlayerAngle = "getPlayerAngle";

        /*
         * setPlayerColor(int id, int r, int g, int b)
            getPlayerColor(int id)
         */
        private static readonly AnsiString StringGetPlayerColor = "getPlayerColor";

        private static readonly AnsiString StringGetPlayerDexterity = "getPlayerDexterity";

        // getPlayerFocus(int id)
        private static readonly AnsiString StringGetPlayerFocus = "getPlayerFocus";

        /*
         * setPlayerHealth(int id, int hp)
            getPlayerHealth(int id)
         */
        private static readonly AnsiString StringGetPlayerHealth = "getPlayerHealth";

        private static readonly AnsiString StringGetPlayerMaxHealth = "getPlayerMaxHealth";

        private static readonly AnsiString StringGetPlayerName = "getPlayerName";

        private static readonly AnsiString StringGetPlayerPosition = "getPlayerPosition";

        private static readonly AnsiString StringGetPlayerRespawnTime = "getPlayerRespawnTime";

        // getPlayerSkillWeapon(int id)
        private static readonly AnsiString StringGetPlayerSkillWeapon = "getPlayerSkillWeapon";

        private static readonly AnsiString StringGetPlayerStrength = "getPlayerStrength";

        // getPlayerTalent(id)
        private static readonly AnsiString StringGetPlayerTalent = "getPlayerTalent";

        private static readonly AnsiString StringGetPlayerWeaponMode = "getPlayerWeaponMode";

        private static readonly AnsiString StringIsPlayerDead = "isPlayerDead";

        private static readonly AnsiString StringIsPlayerSpawned = "isPlayerSpawned";

        private static readonly AnsiString StringIsPlayerUnconscious = "isPlayerUnconscious";

        // playAniId(int pid, int id)
        private static readonly AnsiString StringPlayAniId = "playAniId";

        /*
         * setPlayerAngle(int id, int angle)
            getPlayerAngle(int id)
         */
        private static readonly AnsiString StringSetPlayerAngle = "setPlayerAngle";

        private static readonly AnsiString StringSetPlayerColor = "setPlayerColor";

        /*
         * setPlayerDexterity(int id, int dex)
            getPlayerDexterity(int id)
         */
        private static readonly AnsiString StringSetPlayerDexterity = "setPlayerDexterity";

        private static readonly AnsiString StringSetPlayerHealth = "setPlayerHealth";

        /*
         * setPlayerMaxHealth(int id, int maxHp)
getPlayerMaxHealth(int id)
         */
        private static readonly AnsiString StringSetPlayerMaxHealth = "setPlayerMaxHealth";

        /*setPlayerName(int id, string name)
         getPlayerName(int id)*/
        private static readonly AnsiString StringSetPlayerName = "setPlayerName";

        /*
         * 
         * setPlayerPosition(int id, int x, int y, int z)
getPlayerPosition(int id)*/
        private static readonly AnsiString StringSetPlayerPosition = "setPlayerPosition";

        private static readonly AnsiString StringSetPlayerRespawnTime = "setPlayerRespawnTime";

        // setPlayerSkillWeapon(int id, int skill_id, int value)
        private static readonly AnsiString StringSetPlayerSkillWeapon = "setPlayerSkillWeapon";

        /*
         * setPlayerStrength(int id, int str)
            getPlayerStrength(int id)
         */
        private static readonly AnsiString StringSetPlayerStrength = "setPlayerStrength";

        // setPlayerTalent(int id, int talent_id, int value)
        private static readonly AnsiString StringSetPlayerTalent = "setPlayerTalent";

        /*
         * setPlayerWeaponMode(int id, int wm)
            getPlayerWeaponMode(int id)
         */
        private static readonly AnsiString StringSetPlayerWeaponMode = "setPlayerWeaponMode";

        // spawnPlayer(int id)
        private static readonly AnsiString StringSpawnPlayer = "setPlayspawnPlayererTalent";

        // stopAni(int pid)
        private static readonly AnsiString StringStopAni = "stopAni";

        // unspawnPlayer(int id)
        private static readonly AnsiString StringUnspawnPlayer = "unspawnPlayer";

        private readonly IServer server;

        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        ///     Indicates whether this object is disposed or not.
        /// </summary>
        private bool disposed;

        public Character(ISquirrelApi squirrelApi, IClient client, IServer server)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            this.squirrelApi = squirrelApi;
            this.server = server;
            this.Client = client;
        }

        // onPlayerEquipArmor(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> ArmorEquiped;

        // onPlayerDead(int killer_id, int id)
        public event EventHandler<DeadEventArgs> Died;

        // onPlayerChangeFocus(int pid, int oldFocusId, int currFocusId)
        public event EventHandler<FocusChangedEventArgs> FocusChanged;

        // onPlayerEquipHandItem(int pid, int hand, string instance)
        public event EventHandler<HandItemEquipedEventArgs> HandItemEquiped;

        // onPlayerChangeHealth(int id, int oldHp, int currHp)
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        // onPlayerEquipHelmet(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> HelmetEquiped;

        // onPlayerHit(int killer_id, int id, int dmg, int type)
        public event EventHandler<HitEventArgs> Hit;

        // onPlayerChangeMaxHealth(int id, int oldMaxHp, int currMaxHp)
        public event EventHandler<MaxHealthChangedEventArgs> MaxHealthChanged;

        // onPlayerEquipMeleeWeapon(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> MeleeWeaponEquiped;

        // onPlayerEquipRangedWeapon(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> RangedEquiped;

        // onPlayerRespawn(int id)
        public event EventHandler<RespawnEventArgs> Respawned;

        // onPlayerEquipShield(int pid, string instance)
        public event EventHandler<ItemEquipedEventArgs> ShieldEquiped;

        // onPlayerUnconscious(int killer_id, int id)
        public event EventHandler<UnconsciousEventArgs> Unconscious;

        public float Angle
        {
            get
            {
                return this.squirrelApi.Call<int>(StringGetPlayerAngle, this.Client.ClientId);
            }

            set
            {
                // Normalize the angle.
                value %= 360;
                this.squirrelApi.Call(StringSetPlayerAngle, this.Client.ClientId, value);
            }
        }

        public IClient Client { get; }

        public int Dexterity
        {
            get
            {
                return this.squirrelApi.Call<int>(StringGetPlayerDexterity, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.squirrelApi.Call(StringSetPlayerDexterity, this.Client.ClientId, value);
            }
        }

        public ICharacter Focus
        {
            get
            {
                int focusId = this.squirrelApi.Call<int>(StringGetPlayerFocus, this.Client.ClientId);
                if (focusId >= 0)
                {
                    return this.server.Clients[focusId].PlayerCharacter;
                }
                else
                {
                    return null;
                }
            }
        }

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

                this.squirrelApi.Call(StringSetPlayerHealth, this.Client.ClientId, value);
            }
        }

        public IInventory Inventory { get; }

        // isPlayerDead(int id)
        public bool IsDead => this.squirrelApi.Call<bool>(StringIsPlayerDead, this.Client.ClientId);

        // isPlayerSpawned(int id)
        public bool IsSpawned => this.squirrelApi.Call<bool>(StringIsPlayerSpawned, this.Client.ClientId);

        // isPlayerUnconscious(int id)
        public bool IsUnconscious => this.squirrelApi.Call<bool>(StringIsPlayerUnconscious, this.Client.ClientId);

        public int MaxHealth
        {
            get
            {
                return this.squirrelApi.Call<int>(StringGetPlayerMaxHealth, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.squirrelApi.Call(StringSetPlayerMaxHealth, this.Client.ClientId, value);
            }
        }

        public string Name
        {
            get
            {
                return this.squirrelApi.Call<string>(StringGetPlayerName, this.Client.ClientId);
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(value));
                }

                this.squirrelApi.Call(StringSetPlayerName, this.Client.ClientId, value);
            }
        }

        public Color NameColor
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                this.squirrelApi.Call(StringSetPlayerColor, this.Client.ClientId, value.R, value.G, value.B);
            }
        }

        public Point3D Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                this.squirrelApi.Call(StringSetPlayerPosition, this.Client.ClientId, value.X, value.Y, value.Z);
            }
        }

        public int RespawnTime
        {
            get
            {
                return this.squirrelApi.Call<int>(StringGetPlayerRespawnTime, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.squirrelApi.Call(StringSetPlayerRespawnTime, this.Client.ClientId, value);
            }
        }

        public int Strength
        {
            get
            {
                return this.squirrelApi.Call<int>(StringGetPlayerStrength, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                this.squirrelApi.Call(StringSetPlayerStrength, this.Client.ClientId, value);
            }
        }

        public WeaponMode WeaponMode
        {
            get
            {
                return (WeaponMode)this.squirrelApi.Call<int>(StringGetPlayerWeaponMode, this.Client.ClientId);
            }

            set
            {
                if (!Enum.IsDefined(typeof(WeaponMode), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(WeaponMode));
                }

                this.squirrelApi.Call(StringSetPlayerWeaponMode, this.Client.ClientId, (int)value);
            }
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
            }
        }

        public int GetAniId()
        {
            return this.squirrelApi.Call<int>(StringGetAniId, this.Client.ClientId);
        }

        public int GetSkillWeapon(SkillWeapon skill)
        {
            if (!Enum.IsDefined(typeof(SkillWeapon), skill))
            {
                throw new InvalidEnumArgumentException(nameof(skill), (int)skill, typeof(SkillWeapon));
            }

            return this.squirrelApi.Call<int>(StringGetPlayerSkillWeapon, this.Client.ClientId, (int)skill);
        }

        public int GetTalent(Talent talent)
        {
            if (!Enum.IsDefined(typeof(Talent), talent))
            {
                throw new InvalidEnumArgumentException(nameof(talent), (int)talent, typeof(Talent));
            }

            return this.squirrelApi.Call<int>(StringGetPlayerTalent, this.Client.ClientId, (int)talent);
        }

        public void PlayAniId(int aniId)
        {
            if (aniId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aniId));
            }

            this.squirrelApi.Call(StringPlayAniId, this.Client.ClientId, aniId);
        }

        public void SetSkillWeapon(SkillWeapon skill, int value)
        {
            if (!Enum.IsDefined(typeof(SkillWeapon), skill))
            {
                throw new InvalidEnumArgumentException(nameof(skill), (int)skill, typeof(SkillWeapon));
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            this.squirrelApi.Call(StringSetPlayerSkillWeapon, this.Client.ClientId, (int)skill, value);
        }

        public void SetTalent(Talent talent, int value)
        {
            if (!Enum.IsDefined(typeof(Talent), talent))
            {
                throw new InvalidEnumArgumentException(nameof(talent), (int)talent, typeof(Talent));
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            this.squirrelApi.Call(StringSetPlayerTalent, this.Client.ClientId, (int)talent, value);
        }

        public void Spawn()
        {
            this.squirrelApi.Call(StringSpawnPlayer, this.Client.ClientId);
        }

        public void StopAllAnimations()
        {
            this.squirrelApi.Call(StringStopAni, this.Client.ClientId);
        }

        public void UnspawnPlayer()
        {
            this.squirrelApi.Call(StringUnspawnPlayer, this.Client.ClientId);
        }
    }
}