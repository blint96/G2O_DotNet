using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public class ClientConnectedEventArgs : EventArgs
    {
        private IClient NewClient { get; }

        public ClientConnectedEventArgs(IClient newClient)
        {
            this.NewClient = newClient;
        }
    }
}
