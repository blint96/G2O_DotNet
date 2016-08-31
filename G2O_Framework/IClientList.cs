using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public interface IClientList
    {
        int MaxSlots { get; }

        int Count { get; }

        event EventHandler<ClientConnectedEventArgs> ClientConnect;

        event EventHandler<ClientDisconnectedEventArgs> ClientDisconnect;

    }
}
