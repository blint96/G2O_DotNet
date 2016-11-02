using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2O.DotNet.Account
{
    using G2O.DotNet.ApiInterceptorLayer;
    using G2O.DotNet.Permission;

    internal class Authorizer
    {
        private readonly IClientInterceptor client;

        private readonly CommandCollection commandCollection;

        private IRole role;

        public Authorizer(IClientInterceptor client,CommandCollection commandCollection)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            if (commandCollection == null)
            {
                throw new ArgumentNullException(nameof(commandCollection));
            }

            this.client = client;
            this.commandCollection = commandCollection;
            this.client.BeforeCommandReceived += this.InvokeCommand;
            this.client.Disconnect += this.Client_Disconnect;
        }

        private void Client_Disconnect(object sender, ServerApi.ClientDisconnectedEventArgs e)
        {
            this.client.BeforeCommandReceived -= this.InvokeCommand;
            this.client.Disconnect -= this.Client_Disconnect;
        }

        public void SetRole(IRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }
            this.role = role;
        }

        public void ReseRole()
        {
            this.role = null;
        }

        private void InvokeCommand(object sender, BeforeCommandReceivedEventArgs eventArgs)
        {

        }
    }
}
