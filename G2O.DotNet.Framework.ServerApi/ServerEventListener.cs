using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GothicOnline.G2.DotNet.ServerApi
{
    using GothicOnline.G2.DotNet.ServerApi.Character;
    using GothicOnline.G2.DotNet.ServerApi.Client;
    using GothicOnline.G2.DotNet.ServerApi.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class ServerEventListener
    {
        private G2OEventCallback onInitFunction;
        private G2OEventCallback onPacketFunction;
        private G2OEventCallback onPlayerCommandFunction;
        private G2OEventCallback onPlayerMessageFunction;
        private G2OEventCallback onPlayerJoinFunction;
        private G2OEventCallback onPlayerDisconnectFunction;
        private G2OEventCallback onPlayerChangeHealthFunction;
        private G2OEventCallback onPlayerChangeMaxHealthFunction;
        private G2OEventCallback onPlayerRespawnFunction;
        private G2OEventCallback onPlayerDeadFunction;
        private G2OEventCallback onPlayerUnconsciousFunction;
        private G2OEventCallback onPlayerHitFunction;
        private G2OEventCallback onPlayerChangeWeaponModeFunction;
        private G2OEventCallback onPlayerChangeFocusFunction;
        private G2OEventCallback onPlayerChangeColorFunction;
        private G2OEventCallback onPlayerEquipArmorFunction;
        private G2OEventCallback onPlayerEquipHelmetFunction;
        private G2OEventCallback onPlayerEquipMeleeWeaponFunction;
        private G2OEventCallback onPlayerEquipRangedWeaponFunction;
        private G2OEventCallback onPlayerEquipShieldFunction;
        private G2OEventCallback onPlayerEquipHandItemFunction;







        private readonly ISquirrelApi squirrelApi;

        private readonly Server.Server server;



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
            // onPlayerCommandFunction = new G2OEventCallback(this.squirrelApi, "onPlayerCommand", new Action(() => this.OnInit()));
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


        void OnPacket(int id, IntPtr packet) { }
        void OnPlayerCommand(int id, string cmd) { }
        void OnPlayerMessage(int id, string message) { }

        void OnPlayerJoin(int id)
        {
            ClientList clientList = this.server.Clients as ClientList;
            clientList?.OnClientConnect(new ClientConnectedEventArgs(this.server.Clients[id]));
        }

        void OnPlayerDisconnect(int id, int reason)
        {
            ClientList clientList = this.server.Clients as ClientList;
            IClient client = this.server.Clients[id];
            clientList?.OnClientDisconnect(new ClientDisconnectedEventArgs(client, (DisconnectReason)reason));
            Client.Client realClient = client as Client.Client;
            realClient?.OnDisconnect(new ClientDisconnectedEventArgs(client, (DisconnectReason)reason));
        }

        void OnPlayerChangeHealth(int id, int oldHp, int currHp)
        {
            Character.Character realCharacter = this.server.Clients[id].PlayerCharacter as Character.Character;
            realCharacter?.OnHealthChanged(new HealthChangedEventArgs(oldHp, currHp));
        }

        void OnPlayerChangeMaxHealth(int id, int oldMaxHp, int currMaxHp)
        {
            Character.Character realCharacter = this.server.Clients[id].PlayerCharacter as Character.Character;
            realCharacter?.OnMaxHealthChanged(new MaxHealthChangedEventArgs(oldMaxHp, currMaxHp));
        }

        void OnPlayerRespawn(int id)
        {
            Character.Character realCharacter = this.server.Clients[id].PlayerCharacter as Character.Character;
            realCharacter?.OnRespawned();
        }

        void OnPlayerDead(int killerId, int id)
        {
            Character.Character realCharacter = this.server.Clients[id].PlayerCharacter as Character.Character;
            realCharacter?.OnDied(new DeadEventArgs(this.server.Clients[killerId].PlayerCharacter));
        }

        void OnPlayerUnconscious(int killerId, int id)
        {
            Character.Character realCharacter = this.server.Clients[id].PlayerCharacter as Character.Character;
            realCharacter?.OnUnconscious(new UnconsciousEventArgs(this.server.Clients[killerId].PlayerCharacter));
        }

        void OnPlayerHit(int killerId, int id, int dmg, int type)
        {
            Character.Character realCharacter = this.server.Clients[id].PlayerCharacter as Character.Character;
            realCharacter?.OnHit(new HitEventArgs(this.server.Clients[killerId].PlayerCharacter, dmg, type));
        }

        void OnPlayerChangeWeaponMode(int pid, int oldWm, int currWm)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnWeaponModeChanged(new ChangeWeaponModeEventArgs(currWm, oldWm));
        }

        void OnPlayerChangeFocus(int pid, int oldFocusId, int currFocusId)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnFocusChanged(new FocusChangedEventArgs(this.server.Clients[oldFocusId].PlayerCharacter, this.server.Clients[currFocusId].PlayerCharacter));
        }

        void OnPlayerChangeColor(int pid, int r, int g, int b)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnNameColorChanged(new NameColorChangedEventArgs(r, g, b));
        }

        void OnPlayerEquipArmor(int pid, string instance)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnArmorEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipHelmet(int pid, string instance)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnHelmetEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipMeleeWeapon(int pid, string instance)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnMeleeWeaponEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipRangedWeapon(int pid, string instance)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnRangedWeaponEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipShield(int pid, string instance)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnShieldEquiped(new ItemEquipedEventArgs(instance));
        }

        void OnPlayerEquipHandItem(int pid, int hand, string instance)
        {
            Character.Character realCharacter = this.server.Clients[pid].PlayerCharacter as Character.Character;
            realCharacter?.OnHandItemEquiped(new HandItemEquipedEventArgs(instance, (Hand)hand));
        }
    }
}
