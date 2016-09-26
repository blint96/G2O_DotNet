// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerEventListener.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.ServerApi
{
    using System;

    using GothicOnline.G2.DotNet.ServerApi.Character;
    using GothicOnline.G2.DotNet.ServerApi.Client;
    using GothicOnline.G2.DotNet.ServerApi.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class ServerEventListener
    {
        private readonly Server.Server server;

        private readonly ISquirrelApi squirrelApi;

        private G2OEventCallback onInitFunction;

        private G2OEventCallback onPacketFunction;

        private G2OEventCallback onPlayerChangeColorFunction;

        private G2OEventCallback onPlayerChangeFocusFunction;

        private G2OEventCallback onPlayerChangeHealthFunction;

        private G2OEventCallback onPlayerChangeMaxHealthFunction;

        private G2OEventCallback onPlayerChangeWeaponModeFunction;

        private G2OEventCallback onPlayerCommandFunction;

        private G2OEventCallback onPlayerDeadFunction;

        private G2OEventCallback onPlayerDisconnectFunction;

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

        public ServerEventListener(ISquirrelApi squirrelApi, Server.Server server)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }

            this.squirrelApi = squirrelApi;
            this.server = server;

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
        }

        void OnInit()
        {
            this.server.OnInitialize(new ServerInitializedEventArgs(this.server));
        }

        void OnPacket(int id, IntPtr packet)
        {
        }

        void OnPlayerChangeColor(int pid, int r, int g, int b)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnNameColorChanged(new NameColorChangedEventArgs(r, g, b));
        }

        void OnPlayerChangeFocus(int pid, int oldFocusId, int currFocusId)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnFocusChanged(
                new FocusChangedEventArgs(
                    this.server.Clients[oldFocusId].PlayerCharacter, 
                    this.server.Clients[currFocusId].PlayerCharacter));
        }

        void OnPlayerChangeHealth(int id, int oldHp, int currHp)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[id].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnHealthChanged(new HealthChangedEventArgs(oldHp, currHp));
        }

        void OnPlayerChangeMaxHealth(int id, int oldMaxHp, int currMaxHp)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[id].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnMaxHealthChanged(new MaxHealthChangedEventArgs(oldMaxHp, currMaxHp));
        }

        void OnPlayerChangeWeaponMode(int pid, int oldWm, int currWm)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnWeaponModeChanged(new ChangeWeaponModeEventArgs(currWm, oldWm));
        }

        void OnPlayerCommand(int id, string cmd, string parameters)
        {
            Client.Client client=   this.server.Clients[id] as Client.Client;
            client?.OnCommandReceived(new CommandReceivedEventArgs(cmd,parameters));
        }

        void OnPlayerDead(int killerId, int id)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[id].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnDied(new DeadEventArgs(this.server.Clients[killerId].PlayerCharacter));
        }

        void OnPlayerDisconnect(int id, int reason)
        {
            ClientList clientList = this.server.Clients as ClientList;
            IClient client = this.server.Clients[id];
            clientList?.OnClientDisconnect(new ClientDisconnectedEventArgs(client, (DisconnectReason)reason));
            Client.Client realClient = client as Client.Client;
            realClient?.OnDisconnect(new ClientDisconnectedEventArgs(client, (DisconnectReason)reason));
        }

        void OnPlayerEquipArmor(int pid, string instance)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnArmorEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipHandItem(int pid, int hand, string instance)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnHandItemEquiped(new HandItemEquipedEventArgs(instance, (Hand)hand));
        }

        void OnPlayerEquipHelmet(int pid, string instance)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnHelmetEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipMeleeWeapon(int pid, string instance)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnMeleeWeaponEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipRangedWeapon(int pid, string instance)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnRangedWeaponEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipShield(int pid, string instance)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[pid].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnShieldEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerHit(int killerId, int id, int dmg, int type)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[id].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnHit(new HitEventArgs(this.server.Clients[killerId].PlayerCharacter, dmg, type));
        }

        void OnPlayerJoin(int id)
        {
            ClientList clientList = this.server.Clients as ClientList;
            clientList?.OnClientConnect(id);
        }

        void OnPlayerMessage(int id, string message)
        {
            Client.Client realCharacter = this.server.Clients[id] as Client.Client;
            realCharacter?.OnMessageReceived(new MessageReceivedEventArgs(message));
        }

        void OnPlayerRespawn(int id)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[id].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnRespawned();
        }

        void OnPlayerUnconscious(int killerId, int id)
        {
            Character.PlayerCharacter realPlayerCharacter = this.server.Clients[id].PlayerCharacter as Character.PlayerCharacter;
            realPlayerCharacter?.OnUnconscious(new UnconsciousEventArgs(this.server.Clients[killerId].PlayerCharacter));
        }
    }
}