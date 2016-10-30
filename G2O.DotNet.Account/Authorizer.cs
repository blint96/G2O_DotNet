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

        private IRole role;

        public Authorizer(IClientInterceptor client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            this.client = client;
            this.client.BeforeCommandReceived += this.InvokeCommand;
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
