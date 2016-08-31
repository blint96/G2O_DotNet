using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    using System.Net;
    using System.Net.NetworkInformation;

    public interface IClient
    {
        /// <summary>
        /// Gets the nickname that was specified by the user before connecting.
        /// </summary>
        string Nickname { get; }

        ICharacter PlayerCharacter { get; }

        int ClientId { get; }

        void SendMessage(int r, int g, int b, string message);

        void SendMessageToClient(IClient receiver, int r, int g, int b, string message);

        void SendMessageToAll(int r, int g, int b, string message);

        bool IsConnected { get; }

        IPAddress IpAddress { get; }

        PhysicalAddress MacAddress { get; }

        event EventHandler<PacketReceivedEventArgs> PacketReceived;

        event EventHandler<ClientDisconnectedEventArgs> Disconnect;
    }
}
