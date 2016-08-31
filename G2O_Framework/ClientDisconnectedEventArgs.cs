using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public class ClientDisconnectedEventArgs : EventArgs
    {
        public ClientDisconnectedEventArgs(IClient client, int reason)
        {
            this.Client = client;
            this.Reason = reason;
        }

        public IClient Client { get; }
        public int Reason { get; }
    }
}
