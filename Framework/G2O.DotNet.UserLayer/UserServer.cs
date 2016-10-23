using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2O.DotNet.UserLayer
{
    using System.ComponentModel.Composition;
    using System.Drawing;

    using G2O.DotNet.Database;
    using G2O.DotNet.ServerApi;
    internal class UserServer : IServer
    {
        private readonly IServer server;

        private readonly IDatabaseContextFactory contextFactory;

        [ImportingConstructor]
        internal UserServer([Import("SquirrelServerAPI")] IServer server,[Import] IDatabaseContextFactory contextFactory)
        {
            if (server == null)
            {
                throw new ArgumentNullException(nameof(server));
            }
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.server = server;
            this.contextFactory = contextFactory;
            server.Initialize += (o, a) => this.Initialize?.Invoke(this, new ServerInitializedEventArgs(this));

            this.Clients= new UserClientList(server.Clients);
        }

        public event EventHandler<ServerInitializedEventArgs> Initialize;



        public IClientList Clients { get; }

        public string Description
        {
            get
            {
                return this.server.Description;
            }
            set
            {
                this.server.Description = value;
            }
        }

        public ServerTime Time
        {
            get
            {
                return this.server.Time;
            }
            set
            {
                this.server.Time = value;
            }
        }

        public string World
        {
            get
            {
                return this.server.World;
            }
            set
            {
                this.server.World = value;
            }
        }


        public void SendMessageToAll(int r, int g, int b, string message)
        {
            this.server.SendMessageToAll(r, g, b, message);
        }

        public void SendPacketToAll(IPacket packet, PacketReliability reliability)
        {
            this.server.SendPacketToAll(packet, reliability);
        }

        public void SendMessageToAll(Color color, string message)
        {
            this.server.SendMessageToAll(color, message);
        }
    }
}
