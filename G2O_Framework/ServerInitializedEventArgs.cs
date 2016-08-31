using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
   public class ServerInitializedEventArgs:EventArgs
    {
       public ServerInitializedEventArgs(IServer server)
       {
           if (server == null)
           {
               throw new ArgumentNullException(nameof(server));
           }
           this.Server = server;
       }

       public  IServer Server { get; }
    }
}
