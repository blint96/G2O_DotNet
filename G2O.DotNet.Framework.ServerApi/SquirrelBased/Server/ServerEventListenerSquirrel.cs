// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerEventListenerSquirrel.cs" company="Colony Online Project">
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
namespace G2O.DotNet.ServerApi.Server
{
    using System;

    using G2O.DotNet.ServerApi.Character;
    using G2O.DotNet.ServerApi.Client;
    using G2O.DotNet.Squirrel;

    internal class ServerEventListenerSquirrel
    {
        private readonly G2OServerSquirrel g2OServerSquirrel;

        /// <summary>
        ///     The used instance of the <see cref="ISquirrelApi" />.
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        private G2OEventCallback onInitFunction;

        private G2OEventCallback onPacketFunction;

        private G2OEventCallback onPlayerChangeColorFunction;

        private G2OEventCallback onPlayerChangeFocusFunction;

        private G2OEventCallback onPlayerChangeHealthFunction;

        private G2OEventCallback onPlayerChangeMaxHealthFunction;

        private G2OEventCallback onPlayerChangeWeaponModeFunction;

        private G2OEventCallback onPlayerChangeWorld;

        private G2OEventCallback onPlayerCommandFunction;

        private G2OEventCallback onPlayerDeadFunction;

        private G2OEventCallback onPlayerDisconnectFunction;

        private G2OEventCallback onPlayerEnterWorld;

        private G2OEventCallback onPlayerEquipArmorFunction;

        private G2OEventCallback onPlayerEquipHandItemFunction;

        private G2OEventCallback onPlayerEquipHelmetFunction;

        private G2OEventCallback onPlayerEquipMeleeWeaponFunction;

        private G2OEventCallback onPlayerEquipRangedWeaponFunction;

        private G2OEventCallback onPlayerEquipShieldFunction;

        private G2OEventCallback onPlayerHitFunction;

        private G2OEventCallback onPlayerJoinFunction;

        private G2OEventCallback onPlayerMessageFunction;

        private G2OEventCallback onPlayerRespawnFunction;

        private G2OEventCallback onPlayerUnconsciousFunction;

        public ServerEventListenerSquirrel(ISquirrelApi squirrelApi, G2OServerSquirrel g2OServerSquirrel)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (g2OServerSquirrel == null)
            {
                throw new ArgumentNullException(nameof(g2OServerSquirrel));
            }

            this.squirrelApi = squirrelApi;
            this.g2OServerSquirrel = g2OServerSquirrel;

            this.onInitFunction = new G2OEventCallback(this.squirrelApi, "onInit", new Action(this.OnInit));

            // onPacketFunction = new G2OEventCallback(this.squirrelApi, "onPacket", new Action<int, IntPtr>(this.OnPacket));
            this.onPlayerCommandFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerCommand", 
                new Action<int, string, string>(this.OnPlayerCommand));
            this.onPlayerMessageFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerMessage", 
                new Action<int, string>(this.OnPlayerMessage));
            this.onPlayerJoinFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerJoin", 
                new Action<int>(this.OnPlayerJoin));
            this.onPlayerDisconnectFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerDisconnect", 
                new Action<int, int>(this.OnPlayerDisconnect));
            this.onPlayerChangeHealthFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerChangeHealth", 
                new Action<int, int, int>(this.OnPlayerChangeHealth));
            this.onPlayerChangeMaxHealthFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerChangeMaxHealth", 
                new Action<int, int, int>(this.OnPlayerChangeMaxHealth));
            this.onPlayerRespawnFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerRespawn", 
                new Action<int>(this.OnPlayerRespawn));
            this.onPlayerDeadFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerDead", 
                new Action<int, int>(this.OnPlayerDead));
            this.onPlayerUnconsciousFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerUnconscious", 
                new Action<int, int>(this.OnPlayerUnconscious));
            this.onPlayerHitFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerHit", 
                new Action<int, int, int, int>(this.OnPlayerHit));
            this.onPlayerChangeWeaponModeFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerChangeWeaponMode", 
                new Action<int, int, int>(this.OnPlayerChangeWeaponMode));
            this.onPlayerChangeFocusFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerChangeFocus", 
                new Action<int, int, int>(this.OnPlayerChangeFocus));
            this.onPlayerChangeColorFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerChangeColor", 
                new Action<int, int, int, int>(this.OnPlayerChangeColor));
            this.onPlayerEquipArmorFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEquipArmor", 
                new Action<int, string>(this.OnPlayerEquipArmor));
            this.onPlayerEquipHelmetFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEquipHelmet", 
                new Action<int, string>(this.OnPlayerEquipHelmet));
            this.onPlayerEquipMeleeWeaponFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEquipMeleeWeapon", 
                new Action<int, string>(this.OnPlayerEquipMeleeWeapon));
            this.onPlayerEquipRangedWeaponFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEquipRangedWeapon", 
                new Action<int, string>(this.OnPlayerEquipRangedWeapon));
            this.onPlayerEquipShieldFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEquipShield", 
                new Action<int, string>(this.OnPlayerEquipShield));
            this.onPlayerEquipHandItemFunction = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEquipHandItem", 
                new Action<int, int, string>(this.OnPlayerEquipHandItem));

            this.onPlayerEnterWorld = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerEnterWorld", 
                new Action<int, string>(this.OnPlayerEnterWorld));
            

            this.onPlayerChangeWorld = new G2OEventCallback(
                this.squirrelApi, 
                "onPlayerChangeWorld", 
                new Action<int, string>(this.OnPlayerChangeWorld));
        }

        void OnInit()
        {
            this.g2OServerSquirrel.OnInitialize(new ServerInitializedEventArgs(this.g2OServerSquirrel));
        }

        void OnPacket(int id, IntPtr packet)
        {
        }

        void OnPlayerChangeColor(int pid, int r, int g, int b)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnNameColorChanged(new NameColorChangedEventArgs(r, g, b));
        }

        void OnPlayerChangeFocus(int pid, int oldFocusId, int currFocusId)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnFocusChanged(
                new FocusChangedEventArgs(
                    this.g2OServerSquirrel.Clients[oldFocusId].PlayerCharacter, 
                    this.g2OServerSquirrel.Clients[currFocusId].PlayerCharacter));
        }

        void OnPlayerChangeHealth(int id, int oldHp, int currHp)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[id].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnHealthChanged(new HealthChangedEventArgs(oldHp, currHp));
        }

        void OnPlayerChangeMaxHealth(int id, int oldMaxHp, int currMaxHp)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[id].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnMaxHealthChanged(new MaxHealthChangedEventArgs(oldMaxHp, currMaxHp));
        }

        void OnPlayerChangeWeaponMode(int pid, int oldWm, int currWm)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnWeaponModeChanged(new ChangeWeaponModeEventArgs(currWm, oldWm));
        }

        private void OnPlayerChangeWorld(int pid, string newWorld)
        {
            var character = this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            character?.OnCharacterWorldChanged(new CharacterWorldChangedEventArgs(newWorld));
        }

        void OnPlayerCommand(int id, string cmd, string parameters)
        {
            ClientSquirrel clientSquirrel = this.g2OServerSquirrel.Clients[id] as ClientSquirrel;
            clientSquirrel?.OnCommandReceived(new CommandReceivedEventArgs(cmd, parameters));
        }

        void OnPlayerDead(int killerId, int id)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[id].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnDied(
                new DeadEventArgs(this.g2OServerSquirrel.Clients[killerId].PlayerCharacter));
        }

        void OnPlayerDisconnect(int id, int reason)
        {
            ClientListSquirrel clientListSquirrel = this.g2OServerSquirrel.Clients as ClientListSquirrel;
            IClient client = this.g2OServerSquirrel.Clients[id];
            clientListSquirrel?.OnClientDisconnect(new ClientDisconnectedEventArgs(client, (DisconnectReason)reason));
            ClientSquirrel realClientSquirrel = client as ClientSquirrel;
            realClientSquirrel?.OnDisconnect(new ClientDisconnectedEventArgs(client, (DisconnectReason)reason));
        }

        private void OnPlayerEnterWorld(int pid, string newWorld)
        {
            var character = this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            character?.OnCharacterEnterWorld(new CharacterWorldChangedEventArgs(newWorld));
        }

        void OnPlayerEquipArmor(int pid, string instance)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnArmorEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipHandItem(int pid, int hand, string instance)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnHandItemEquiped(new HandItemEquipedEventArgs(instance, (Hand)hand));
        }

        void OnPlayerEquipHelmet(int pid, string instance)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnHelmetEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipMeleeWeapon(int pid, string instance)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnMeleeWeaponEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipRangedWeapon(int pid, string instance)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnRangedWeaponEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipShield(int pid, string instance)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[pid].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnShieldEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerHit(int killerId, int id, int dmg, int type)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[id].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnHit(
                new HitEventArgs(this.g2OServerSquirrel.Clients[killerId].PlayerCharacter, dmg, type));
        }

        void OnPlayerJoin(int id)
        {
            ClientListSquirrel clientListSquirrel = this.g2OServerSquirrel.Clients as ClientListSquirrel;
            clientListSquirrel?.OnClientConnect(id);
        }

        void OnPlayerMessage(int id, string message)
        {
            ClientSquirrel realCharacter = this.g2OServerSquirrel.Clients[id] as ClientSquirrel;
            realCharacter?.OnMessageReceived(new MessageReceivedEventArgs(message));
        }

        void OnPlayerRespawn(int id)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[id].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnRespawned();
        }

        void OnPlayerUnconscious(int killerId, int id)
        {
            PlayerCharacterSquirrel realPlayerCharacterSquirrel =
                this.g2OServerSquirrel.Clients[id].PlayerCharacter as PlayerCharacterSquirrel;
            realPlayerCharacterSquirrel?.OnUnconscious(
                new UnconsciousEventArgs(this.g2OServerSquirrel.Clients[killerId].PlayerCharacter));
        }
    }
}