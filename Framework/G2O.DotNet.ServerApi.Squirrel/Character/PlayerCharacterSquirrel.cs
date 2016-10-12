// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerCharacterSquirrel.cs" company="Colony Online Project">
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
// Implementation of the <see cref="ICharacter"/> interface using the squirrel api.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Squirrel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    using G2O.DotNet.Squirrel;

    using System.Drawing;

    /// <summary>
    ///     Implementation of the <see cref="ICharacter" /> interface using the squirrel api.
    /// </summary>
    internal class PlayerCharacterSquirrel : ICharacter, IDisposable
    {
        /// <summary>
        ///     Stores the ANSI version of the string "b"
        /// </summary>
        private static readonly AnsiString StringB = "b";

        /// <summary>
        ///     Stores the ANSI version of the string "equipItem"
        /// </summary>
        private static readonly AnsiString StringEquipItem = "equipItem";

        /// <summary>
        ///     Stores the ANSI version of the string "g"
        /// </summary>
        private static readonly AnsiString StringG = "g";

        /// <summary>
        ///     Stores the ANSI version of the string "getAniId"
        /// </summary>
        private static readonly AnsiString StringGetAniId = "getAniId";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerAngle"
        /// </summary>
        private static readonly AnsiString StringGetPlayerAngle = "getPlayerAngle";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerColor"
        /// </summary>
        private static readonly AnsiString StringGetPlayerColor = "getPlayerColor";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerDexterity"
        /// </summary>
        private static readonly AnsiString StringGetPlayerDexterity = "getPlayerDexterity";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerFocus"
        /// </summary>
        private static readonly AnsiString StringGetPlayerFocus = "getPlayerFocus";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerHealth"
        /// </summary>
        private static readonly AnsiString StringGetPlayerHealth = "getPlayerHealth";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerMaxHealth"
        /// </summary>
        private static readonly AnsiString StringGetPlayerMaxHealth = "getPlayerMaxHealth";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerName"
        /// </summary>
        private static readonly AnsiString StringGetPlayerName = "getPlayerName";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerPosition"
        /// </summary>
        private static readonly AnsiString StringGetPlayerPosition = "getPlayerPosition";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerRespawnTime"
        /// </summary>
        private static readonly AnsiString StringGetPlayerRespawnTime = "getPlayerRespawnTime";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerSkillWeapon"
        /// </summary>
        private static readonly AnsiString StringGetPlayerSkillWeapon = "getPlayerSkillWeapon";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerStrength"
        /// </summary>
        private static readonly AnsiString StringGetPlayerStrength = "getPlayerStrength";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerTalent"
        /// </summary>
        private static readonly AnsiString StringGetPlayerTalent = "getPlayerTalent";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerWeaponMode"
        /// </summary>
        private static readonly AnsiString StringGetPlayerWeaponMode = "getPlayerWeaponMode";

        /// <summary>
        ///     Stores the ANSI version of the string "getPlayerWorld"
        /// </summary>
        private static readonly AnsiString StringGetPlayerWorld = "getPlayerWorld";

        /// <summary>
        ///     Stores the ANSI version of the string "isPlayerDead"
        /// </summary>
        private static readonly AnsiString StringIsPlayerDead = "isPlayerDead";

        /// <summary>
        ///     Stores the ANSI version of the string "isPlayerSpawned"
        /// </summary>
        private static readonly AnsiString StringIsPlayerSpawned = "isPlayerSpawned";

        /// <summary>
        ///     Stores the ANSI version of the string "isPlayerUnconscious"
        /// </summary>
        private static readonly AnsiString StringIsPlayerUnconscious = "isPlayerUnconscious";

        /// <summary>
        ///     Stores the ANSI version of the string "playAniId"
        /// </summary>
        private static readonly AnsiString StringPlayAniId = "playAniId";

        /// <summary>
        ///     Stores the ANSI version of the string "r"
        /// </summary>
        private static readonly AnsiString StringR = "r";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerAngle"
        /// </summary>
        private static readonly AnsiString StringSetPlayerAngle = "setPlayerAngle";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerColor"
        /// </summary>
        private static readonly AnsiString StringSetPlayerColor = "setPlayerColor";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerDexterity"
        /// </summary>
        private static readonly AnsiString StringSetPlayerDexterity = "setPlayerDexterity";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerHealth"
        /// </summary>
        private static readonly AnsiString StringSetPlayerHealth = "setPlayerHealth";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerMaxHealth"
        /// </summary>
        private static readonly AnsiString StringSetPlayerMaxHealth = "setPlayerMaxHealth";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerName"
        /// </summary>
        private static readonly AnsiString StringSetPlayerName = "setPlayerName";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerPosition"
        /// </summary>
        private static readonly AnsiString StringSetPlayerPosition = "setPlayerPosition";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerRespawnTime"
        /// </summary>
        private static readonly AnsiString StringSetPlayerRespawnTime = "setPlayerRespawnTime";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerSkillWeapon"
        /// </summary>
        private static readonly AnsiString StringSetPlayerSkillWeapon = "setPlayerSkillWeapon";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerStrength"
        /// </summary>
        private static readonly AnsiString StringSetPlayerStrength = "setPlayerStrength";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerTalent"
        /// </summary>
        private static readonly AnsiString StringSetPlayerTalent = "setPlayerTalent";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerWeaponMode"
        /// </summary>
        private static readonly AnsiString StringSetPlayerWeaponMode = "setPlayerWeaponMode";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayerWorld"
        /// </summary>
        private static readonly AnsiString StringSetPlayerWorld = "setPlayerWorld";

        /// <summary>
        ///     Stores the ANSI version of the string "setPlayspawnPlayererTalent"
        /// </summary>
        private static readonly AnsiString StringSpawnPlayer = "setPlayspawnPlayererTalent";

        /// <summary>
        ///     Stores the ANSI version of the string "stopAni"
        /// </summary>
        private static readonly AnsiString StringStopAni = "stopAni";

        /// <summary>
        ///     Stores the ANSI version of the string "unequipItem"
        /// </summary>
        private static readonly AnsiString StringUnequipItem = "unequipItem";

        /// <summary>
        ///     Stores the ANSI version of the string "unspawnPlayer"
        /// </summary>
        private static readonly AnsiString StringUnspawnPlayer = "unspawnPlayer";

        /// <summary>
        ///     Key for the player position on the Z axis in the return table of the getPlayerPosition function.
        /// </summary>
        private static readonly AnsiString StringX = "x";

        /// <summary>
        ///     Key for the player position on the Y axis in the return table of the getPlayerPosition function.
        /// </summary>
        private static readonly AnsiString StringY = "y";

        /// <summary>
        ///     Key for the player position on the Z axis in the return table of the getPlayerPosition function.
        /// </summary>
        private static readonly AnsiString StringZ = "z";

        /// <summary>
        ///     The used instance of the server API.
        /// </summary>
        private readonly IServer server;

        /// <summary>
        ///     The used instance of the squirrel API.
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        ///     Indicates whether this object is disposed or not.
        /// </summary>
        private bool disposed;

        /// <summary>
        ///     Stores the value of all readable values after the object was disposed.
        /// </summary>
        private Dictionary<string, object> stateCopy;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PlayerCharacterSquirrel" /> class.
        /// </summary>
        /// <param name="squirrelApi">The instance of the squirrel api that should be used by the new instance.</param>
        /// <param name="client">The <see cref="IClient" /> that this <see cref="PlayerCharacterSquirrel" /> belongs to.</param>
        /// <param name="server">The instance of the server that should be used.</param>
        public PlayerCharacterSquirrel(ISquirrelApi squirrelApi, IClient client, IServer server)
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

            // Instantiate a new instance of the inventory class for this character.
            this.Inventory = new InventorySquirrel(squirrelApi, this);
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
        ///     Calls all registered handlers when this <see cref="ICharacter" /> changes its name <see cref="Color" />.
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
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (float)this.stateCopy[nameof(this.Angle)];
                }

                return this.squirrelApi.Call<int>(StringGetPlayerAngle, this.Client.ClientId);
            }

            set
            {
                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                // Normalize the angle.
                value %= 360;
                this.squirrelApi.Call(StringSetPlayerAngle, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets or sets the world that the <see cref="PlayerCharacterSquirrel" /> is in.
        /// </summary>
        public string CharacterWorld
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (string)this.stateCopy[nameof(this.CharacterWorld)];
                }

                return this.squirrelApi.Call<string>(StringGetPlayerWorld, this.Client.ClientId);
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerWorld, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets the client two which the <see cref="ICharacter" /> belongs to.
        ///     <remarks>Returns null if this is not a client(player) character.</remarks>
        /// </summary>
        public IClient Client { get; }

        /// <summary>
        ///     Gets or sets the Dexterity value of the <see cref="ICharacter" />.
        /// </summary>
        public int Dexterity
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (int)this.stateCopy[nameof(this.Dexterity)];
                }

                return this.squirrelApi.Call<int>(StringGetPlayerDexterity, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerDexterity, this.Client.ClientId, value);
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
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                int focusId = this.squirrelApi.Call<int>(StringGetPlayerFocus, this.Client.ClientId);
                return focusId >= 0 ? this.server.Clients[focusId].PlayerCharacter : null;
            }
        }

        /// <summary>
        ///     Gets or sets the the current health value of this <see cref="ICharacter" />.
        /// </summary>
        public int Health
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (int)this.stateCopy[nameof(this.Health)];
                }

                return this.squirrelApi.Call<int>(StringGetPlayerHealth, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerHealth, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets the <see cref="IInventory" /> of this <see cref="ICharacter" />.
        /// </summary>
        public IInventory Inventory { get; }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is dead.
        /// </summary>
        public bool IsDead
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (bool)this.stateCopy[nameof(this.IsDead)];
                }

                return this.squirrelApi.Call<bool>(StringIsPlayerDead, this.Client.ClientId);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is spawned.
        /// </summary>
        public bool IsSpawned
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (bool)this.stateCopy[nameof(this.IsSpawned)];
                }

                return this.squirrelApi.Call<bool>(StringIsPlayerSpawned, this.Client.ClientId);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="ICharacter" /> is unconscious.
        /// </summary>
        public bool IsUnconscious
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (bool)this.stateCopy[nameof(this.IsUnconscious)];
                }

                return this.squirrelApi.Call<bool>(StringIsPlayerUnconscious, this.Client.ClientId);
            }
        }

        /// <summary>
        ///     Gets or sets the maximum health value of this <see cref="ICharacter" />.
        /// </summary>
        public int MaxHealth
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (int)this.stateCopy[nameof(this.MaxHealth)];
                }

                return this.squirrelApi.Call<int>(StringGetPlayerMaxHealth, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerMaxHealth, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets or sets the name of this <see cref="ICharacter" />.
        /// </summary>
        public string Name
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (string)this.stateCopy[nameof(this.Name)];
                }

                return this.squirrelApi.Call<string>(StringGetPlayerName, this.Client.ClientId);
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerName, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets or sets the name color of this <see cref="ICharacter" />.
        /// </summary>
        public Color NameColor
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (Color)this.stateCopy[nameof(this.NameColor)];
                }

                int top = this.squirrelApi.SqGetTop();
                try
                {
                    this.squirrelApi.SqPushRootTable();
                    this.squirrelApi.SqPushString(StringGetPlayerColor.Unmanaged, StringGetPlayerColor.Length);
                    if (!this.squirrelApi.SqGet(-2))
                    {
                        throw new SquirrelException(
                            $"The gothic online server function '{StringGetPlayerColor}' could not be found in the root table", 
                            this.squirrelApi);
                    }

                    // Call the function
                    this.squirrelApi.SqPushRootTable();
                    this.squirrelApi.SqPushInteger(this.Client.ClientId);
                    if (!this.squirrelApi.SqCall(2, true, false))
                    {
                        throw new SquirrelException(
                            $"The call to the '{StringGetPlayerColor}' function failed", 
                            this.squirrelApi);
                    }

                    int resultTop = this.squirrelApi.SqGetTop();

                    // Color value R
                    this.squirrelApi.SqPushString(StringR.Unmanaged, StringR.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringR} value from the result of the '{StringGetPlayerColor}' function", 
                            this.squirrelApi);
                    }

                    int valueR;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out valueR);

                    // Color value G
                    this.squirrelApi.SqPushString(StringG.Unmanaged, StringG.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringG} value from the result of the '{StringGetPlayerPosition}' function", 
                            this.squirrelApi);
                    }

                    int valueG;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out valueG);

                    // Color value B
                    this.squirrelApi.SqPushString(StringB.Unmanaged, StringB.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringB} value from the result of the '{StringGetPlayerPosition}' function", 
                            this.squirrelApi);
                    }

                    int valueB;
                    this.squirrelApi.SqGetInteger(this.squirrelApi.SqGetTop(), out valueB);
                    return Color.FromArgb(valueR, valueG, valueB);
                }
                finally
                {
                    // Reset the stack top if a exception occures.
                    this.squirrelApi.SqSetTop(top);
                }
            }

            set
            {
                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerColor, this.Client.ClientId, value.R, value.G, value.B);
            }
        }

        /// <summary>
        ///     Gets or sets position of this <see cref="ICharacter" /> in the 3d game world.
        /// </summary>
        public Point3D Position
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (Point3D)this.stateCopy[nameof(this.Position)];
                }

                int top = this.squirrelApi.SqGetTop();
                try
                {
                    this.squirrelApi.SqPushRootTable();
                    this.squirrelApi.SqPushString(StringGetPlayerPosition.Unmanaged, StringGetPlayerPosition.Length);
                    if (!this.squirrelApi.SqGet(-2))
                    {
                        throw new SquirrelException(
                            $"The gothic online server function '{StringGetPlayerPosition}' could not be found in the root table", 
                            this.squirrelApi);
                    }

                    // Call the function
                    this.squirrelApi.SqPushRootTable();
                    this.squirrelApi.SqPushInteger(this.Client.ClientId);
                    if (!this.squirrelApi.SqCall(2, true, false))
                    {
                        throw new SquirrelException(
                            $"The call to the '{StringGetPlayerPosition}' function failed", 
                            this.squirrelApi);
                    }

                    int resultTop = this.squirrelApi.SqGetTop();

                    // Position x
                    this.squirrelApi.SqPushString(StringX.Unmanaged, StringX.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringX} value from the result of the '{StringGetPlayerPosition}' function", 
                            this.squirrelApi);
                    }

                    float posX;
                    this.squirrelApi.SqGetFloat(this.squirrelApi.SqGetTop(), out posX);

                    // Position Y
                    this.squirrelApi.SqPushString(StringY.Unmanaged, StringY.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringY} value from the result of the '{StringGetPlayerPosition}' function", 
                            this.squirrelApi);
                    }

                    float posY;
                    this.squirrelApi.SqGetFloat(this.squirrelApi.SqGetTop(), out posY);

                    // Position Z
                    this.squirrelApi.SqPushString(StringZ.Unmanaged, StringZ.Length);
                    if (!this.squirrelApi.SqGet(resultTop))
                    {
                        throw new SquirrelException(
                            $"Could not get the {StringZ} value from the result of the '{StringGetPlayerPosition}' function", 
                            this.squirrelApi);
                    }

                    float posZ;
                    this.squirrelApi.SqGetFloat(this.squirrelApi.SqGetTop(), out posZ);
                    return new Point3D(posX, posY, posZ);
                }
                finally
                {
                    // Reset the stack top if a exception occures.
                    this.squirrelApi.SqSetTop(top);
                }
            }

            set
            {
                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerPosition, this.Client.ClientId, value.X, value.Y, value.Z);
            }
        }

        /// <summary>
        ///     Gets or sets the respawn-time for this <see cref="ICharacter" />.
        /// </summary>
        public int RespawnTime
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (int)this.stateCopy[nameof(this.RespawnTime)];
                }

                return this.squirrelApi.Call<int>(StringGetPlayerRespawnTime, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerRespawnTime, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets or sets the strength value of this <see cref="ICharacter" />.
        /// </summary>
        public int Strength
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (int)this.stateCopy[nameof(this.Strength)];
                }

                return this.squirrelApi.Call<int>(StringGetPlayerStrength, this.Client.ClientId);
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerStrength, this.Client.ClientId, value);
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="ICharacter.WeaponMode" /> of this <see cref="ICharacter" />.
        /// </summary>
        public WeaponMode WeaponMode
        {
            get
            {
                // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
                if (this.disposed)
                {
                    return (WeaponMode)this.stateCopy[nameof(this.WeaponMode)];
                }

                return (WeaponMode)this.squirrelApi.Call<int>(StringGetPlayerWeaponMode, this.Client.ClientId);
            }

            set
            {
                if (!Enum.IsDefined(typeof(WeaponMode), value))
                {
                    throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(WeaponMode));
                }

                // Check if the object is disposed.
                if (this.disposed)
                {
                    throw new ObjectDisposedException(
                        nameof(PlayerCharacterSquirrel), 
                        "The object is already disposed(The related client has disconnected from the server");
                }

                this.squirrelApi.Call(StringSetPlayerWeaponMode, this.Client.ClientId, (int)value);
            }
        }

        /// <summary>
        ///     Releases all unmanaged resources that are related to this instance of <see cref="PlayerCharacterSquirrel" />
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                // Store all readable values, so they can later be read even if a new client connects and uses up the slot that was released by client that disconnected.
                this.stateCopy = new Dictionary<string, object>
                                     {
                                         { nameof(this.Angle), this.Angle }, 
                                         { nameof(this.CharacterWorld), this.CharacterWorld }, 
                                         { nameof(this.Dexterity), this.Dexterity }, 
                                         { nameof(this.Health), this.Health }, 
                                         { nameof(this.MaxHealth), this.MaxHealth }, 
                                         { nameof(this.Name), this.Name }, 
                                         { nameof(this.NameColor), this.NameColor }, 
                                         { nameof(this.Position), this.Position }, 
                                         { nameof(this.RespawnTime), this.RespawnTime }, 
                                         { nameof(this.Strength), this.Strength }, 
                                         { nameof(this.WeaponMode), this.WeaponMode }, 
                                         { nameof(this.IsDead), this.IsDead }, 
                                         { nameof(this.IsSpawned), this.IsSpawned }, 
                                         { nameof(this.IsUnconscious), this.IsUnconscious }
                                     };

                // Get all weapon skills an store them
                foreach (WeaponSkill weaponskill in Enum.GetValues(typeof(WeaponSkill)))
                {
                    this.stateCopy.Add($"{nameof(this.GetSkillWeapon)}{weaponskill}", this.GetSkillWeapon(weaponskill));
                }

                // Get all talents and store them.
                foreach (Talent talent in Enum.GetValues(typeof(Talent)))
                {
                    this.stateCopy.Add($"{nameof(this.GetTalent)}{talent}", this.GetTalent(talent));
                }

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Equips a item on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be equipped on this <see cref="ICharacter" />.</param>
        public void EquipItem(string itemInstance)
        {
            if (string.IsNullOrEmpty(itemInstance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(itemInstance));
            }

            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringEquipItem, this.Client.ClientId, itemInstance);
        }

        /// <summary>
        ///     Gets the id of the animation that is currently played by this <see cref="ICharacter" />.
        /// </summary>
        /// <returns>The id of the animation that is currently played by this <see cref="ICharacter" />.</returns>
        public int GetAniId()
        {
            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            return this.squirrelApi.Call<int>(StringGetAniId, this.Client.ClientId);
        }

        /// <summary>
        ///     Gets the weaponSkill value of a specified <see cref="WeaponSkill" /> of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <returns>The weapons skill value.</returns>
        public int GetSkillWeapon(WeaponSkill weaponSkill)
        {
            if (!Enum.IsDefined(typeof(WeaponSkill), weaponSkill))
            {
                throw new InvalidEnumArgumentException(nameof(weaponSkill), (int)weaponSkill, typeof(WeaponSkill));
            }

            // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
            if (this.disposed)
            {
                return (int)this.stateCopy[$"{nameof(this.GetSkillWeapon)}{weaponSkill}"];
            }

            return this.squirrelApi.Call<int>(StringGetPlayerSkillWeapon, this.Client.ClientId, (int)weaponSkill);
        }

        /// <summary>
        ///     Gets the value of a <see cref="Talent" /> of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <returns>The talent value.</returns>
        public int GetTalent(Talent talent)
        {
            if (!Enum.IsDefined(typeof(Talent), talent))
            {
                throw new InvalidEnumArgumentException(nameof(talent), (int)talent, typeof(Talent));
            }

            // Return a buffered copy of the last state if the object is allready disposed(the client that this character belongs to has disconnected).
            if (this.disposed)
            {
                return (int)this.stateCopy[$"{nameof(this.GetTalent)}{talent}"];
            }

            return this.squirrelApi.Call<int>(StringGetPlayerTalent, this.Client.ClientId, (int)talent);
        }

        /// <summary>
        ///     Starts playing a animation on this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="aniId">The id of the animation that should be played on the <see cref="ICharacter" />.</param>
        public void PlayAniId(int aniId)
        {
            if (aniId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aniId));
            }

            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringPlayAniId, this.Client.ClientId, aniId);
        }

        /// <summary>
        ///     Sets the skill value for a weapon skill of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="weaponSkill">The weapon skill type.</param>
        /// <param name="value">The new weapon skill value.</param>
        public void SetSkillWeapon(WeaponSkill weaponSkill, int value)
        {
            if (!Enum.IsDefined(typeof(WeaponSkill), weaponSkill))
            {
                throw new InvalidEnumArgumentException(nameof(weaponSkill), (int)weaponSkill, typeof(WeaponSkill));
            }

            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringSetPlayerSkillWeapon, this.Client.ClientId, (int)weaponSkill, value);
        }

        /// <summary>
        ///     Sets the value for a talent of this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="talent">The talent type.</param>
        /// <param name="value">The new talent value.</param>
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

            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringSetPlayerTalent, this.Client.ClientId, (int)talent, value);
        }

        /// <summary>
        ///     Spawns this <see cref="ICharacter" /> if it is not already spawned.
        /// </summary>
        public void Spawn()
        {
            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringSpawnPlayer, this.Client.ClientId);
        }

        /// <summary>
        ///     Stops all animations that are currently played on this <see cref="ICharacter" />.
        /// </summary>
        public void StopAllAnimations()
        {
            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringStopAni, this.Client.ClientId);
        }

        /// <summary>
        ///     Returns a string representation of the object.
        ///     <remarks>This is thought to be mainly used for debugging.</remarks>
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return $"Character[{this.Name}]";
        }

        /// <summary>
        ///     Unequips a item that is currently equipped by this <see cref="ICharacter" />.
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be unequipped by this <see cref="ICharacter" />.</param>
        public void UnequipItem(string itemInstance)
        {
            if (string.IsNullOrEmpty(itemInstance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(itemInstance));
            }

            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringUnequipItem, this.Client.ClientId, itemInstance);
        }

        /// <summary>
        ///     Unspawns this <see cref="ICharacter" /> if it is spawned.
        /// </summary>
        public void UnspawnPlayer()
        {
            // Check if the object is disposed.
            if (this.disposed)
            {
                throw new ObjectDisposedException(
                    nameof(PlayerCharacterSquirrel), 
                    "The object is already disposed(The related client has disconnected from the server");
            }

            this.squirrelApi.Call(StringUnspawnPlayer, this.Client.ClientId);
        }

        /// <summary>
        ///     Invokes the <see cref="ArmorEquiped" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ItemEquipedEventArgs" />
        /// </param>
        internal void OnArmorEquiped(ItemEquipedEventArgs e)
        {
            this.ArmorEquiped?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="CharacterEnterWorld" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="CharacterWorldChangedEventArgs" />
        /// </param>
        internal void OnCharacterEnterWorld(CharacterWorldChangedEventArgs e)
        {
            this.CharacterEnterWorld?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="CharacterWorldChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="CharacterWorldChangedEventArgs" />
        /// </param>
        internal void OnCharacterWorldChanged(CharacterWorldChangedEventArgs e)
        {
            this.CharacterWorldChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="Died" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="DeadEventArgs" />
        /// </param>
        internal void OnDied(DeadEventArgs e)
        {
            this.Died?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="FocusChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="FocusChangedEventArgs" />
        /// </param>
        internal void OnFocusChanged(FocusChangedEventArgs e)
        {
            this.FocusChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="HandItemEquiped" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="HandItemEquipedEventArgs" />
        /// </param>
        internal void OnHandItemEquiped(HandItemEquipedEventArgs e)
        {
            this.HandItemEquiped?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="HealthChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="HealthChangedEventArgs" />
        /// </param>
        internal void OnHealthChanged(HealthChangedEventArgs e)
        {
            this.HealthChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="HelmetEquiped" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ItemEquipedEventArgs" />
        /// </param>
        internal void OnHelmetEquiped(ItemEquipedEventArgs e)
        {
            this.HelmetEquiped?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="Hit" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="HitEventArgs" />
        /// </param>
        internal void OnHit(HitEventArgs e)
        {
            this.Hit?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="MaxHealthChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="MaxHealthChangedEventArgs" />
        /// </param>
        internal void OnMaxHealthChanged(MaxHealthChangedEventArgs e)
        {
            this.MaxHealthChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="MeleeWeaponEquiped" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ItemEquipedEventArgs" />
        /// </param>
        internal void OnMeleeWeaponEquiped(ItemEquipedEventArgs e)
        {
            this.MeleeWeaponEquiped?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="NameColorChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="NameColorChangedEventArgs" />
        /// </param>
        internal void OnNameColorChanged(NameColorChangedEventArgs e)
        {
            this.NameColorChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="RangedWeaponEquiped" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ItemEquipedEventArgs" />
        /// </param>
        internal void OnRangedWeaponEquiped(ItemEquipedEventArgs e)
        {
            this.RangedWeaponEquiped?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="Respawned" /> event.
        /// </summary>
        internal void OnRespawned()
        {
            this.Respawned?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Invokes the <see cref="ShieldEquiped" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ItemEquipedEventArgs" />
        /// </param>
        internal void OnShieldEquiped(ItemEquipedEventArgs e)
        {
            this.ShieldEquiped?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="Unconscious" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="UnconsciousEventArgs" />
        /// </param>
        internal void OnUnconscious(UnconsciousEventArgs e)
        {
            this.Unconscious?.Invoke(this, e);
        }

        /// <summary>
        ///     Invokes the <see cref="WeaponModeChanged" /> event.
        /// </summary>
        /// <param name="e">
        ///     <see cref="ChangeWeaponModeEventArgs" />
        /// </param>
        internal void OnWeaponModeChanged(ChangeWeaponModeEventArgs e)
        {
            this.WeaponModeChanged?.Invoke(this, e);
        }
    }
}