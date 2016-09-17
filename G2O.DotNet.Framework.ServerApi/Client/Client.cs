namespace GothicOnline.G2.DotNet.ServerApi.Client
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;

    using GothicOnline.G2.DotNet.Character;
    using GothicOnline.G2.DotNet.Client;
    using GothicOnline.G2.DotNet.Interop;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Client : IClient, IDisposable
    {
        private readonly ISquirrelApi squirrelApi;

        private readonly IServer server;

        public event EventHandler<ClientDisconnectedEventArgs> Disconnect;

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        public event EventHandler<CommandReceivedEventArgs> CommandReceived;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private static readonly AnsiString StringGetPlayerIP = "getPlayerIP";
        private static readonly AnsiString StringGetPlayerMacAddr = "getPlayerMacAddr";
        private static readonly AnsiString StringGetPlayerSerial = "getPlayerSerial";
        private static readonly AnsiString StringSendMessageToPlayer = "sendMessageToPlayer";
        private static readonly AnsiString StringSendPlayerMessageToAll = "sendPlayerMessageToAll";
        private static readonly AnsiString StringSendPlayerMessageToPlayer = "sendPlayerMessageToPlayer";


        public Client(ISquirrelApi squirrelApi, int clientId, IServer server)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }
            if (clientId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(clientId));
            }
            this.squirrelApi = squirrelApi;
            this.server = server;
            this.ClientId = clientId;

            this.IpAddress = IPAddress.Parse(squirrelApi.Call<string>(StringGetPlayerIP, clientId));
            this.MacAddress = PhysicalAddress.Parse(squirrelApi.Call<string>(StringGetPlayerMacAddr, clientId));
            this.Serial = squirrelApi.Call<string>(StringGetPlayerSerial, clientId);
        }

        public int ClientId { get; }

        //getPlayerIP(int pid)
        public IPAddress IpAddress { get; }

        public bool IsConnected { get; }

        //getPlayerMacAddr(int pid)
        public PhysicalAddress MacAddress { get; }

        public string Nickname { get; }

        public ICharacter PlayerCharacter { get; }

        //getPlayerSerial(int pid)
        public string Serial { get; }

        //sendMessageToPlayer(int id, int r, int g, int b, string message)
        public void SendMessage(int r, int g, int b, string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(Client), "The object is disposed because the client is no longer connected to the server.");
            }

            this.squirrelApi.Call(StringSendMessageToPlayer, this.ClientId, r, g, b, message);
        }

        //sendPlayerMessageToAll(int id, int r, int g, int b, string message)
        public void SendMessageToAll(int r, int g, int b, string message)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(Client), "The object is disposed because the client is no longer connected to the server.");
            }

            this.squirrelApi.Call(StringSendPlayerMessageToAll, this.ClientId, r, g, b, message);
        }


        //sendPlayerMessageToPlayer
        public void SendMessageToClient(IClient receiver, int r, int g, int b, string message)
        {

            if (this.disposed)
            {
                throw new ObjectDisposedException(nameof(Client), "The object is disposed because the client is no longer connected to the server.");
            }
            this.squirrelApi.Call(StringSendPlayerMessageToPlayer, this.ClientId, receiver.ClientId, r, g, b, message);
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                Disconnect = null;
                PacketReceived = null;
            }
        }

        /// <summary>
        /// Indicates whether this object is disposed or not.
        /// </summary>
        private bool disposed;
    }
}