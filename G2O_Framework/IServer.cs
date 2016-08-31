
namespace G2O_Framework
{
    using System;

    public interface IServer
    {
        IClientList Clients{ get;}

        void SendMessageToAll(int r, int g, int b, string message);

        string Description { get; set; }

        string World { get; set; }

        ServerTime Time { get; set; }

        void SendPacketToAll(IPacket packet, int reliability);

        int RespawnTime { get; set; }

        event EventHandler<ClientConnectedEventArgs> Initialize;
    }
}
