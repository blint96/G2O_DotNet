using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2O.DotNet.Account
{
    using G2O.DotNet.Plugin;

    internal class CommandCollection
    {
        public bool TryGetCommand(string commandIdentifier, out ICommand cmd)
        {
            cmd = null;
            return false;
        }
    }
}
