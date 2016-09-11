namespace GothicOnline.G2.DotNet.Client
{
    using System;
    using System.Net;
    using System.Net.NetworkInformation;

    using GothicOnline.G2.DotNet.Character;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Client : IClient
    {
        private readonly ISquirrelApi squirrelApi;

        public event EventHandler<ClientDisconnectedEventArgs> Disconnect;

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        public Client(ISquirrelApi squirrelApi,int clientId)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (clientId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(clientId));
            }
            this.squirrelApi = squirrelApi;
            this.ClientId = clientId;
        }

        public int ClientId { get; }

        public IPAddress IpAddress { get; }

        public bool IsConnected { get; }

        public PhysicalAddress MacAddress { get; }

        public string Nickname { get; }

        public ICharacter PlayerCharacter { get; }

        public void SendMessage(int r, int g, int b, string message)
        {
            throw new NotImplementedException();
        }

        public void SendMessageToAll(int r, int g, int b, string message)
        {
            throw new NotImplementedException();
        }

        public void SendMessageToClient(IClient receiver, int r, int g, int b, string message)
        {
            throw new NotImplementedException();
        }
    }
}