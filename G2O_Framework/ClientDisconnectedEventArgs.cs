namespace G2O_Framework
{
    using System;
    using System.ComponentModel;

    public class ClientDisconnectedEventArgs : EventArgs
    {
        public ClientDisconnectedEventArgs(IClient client, DisconnectReason reason)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!Enum.IsDefined(typeof(DisconnectReason), reason))
            {
                throw new InvalidEnumArgumentException(nameof(reason), (int)reason, typeof(DisconnectReason));
            }

            this.Client = client;
            this.Reason = reason;
        }

        public IClient Client { get; }

        public DisconnectReason Reason { get; }
    }
}