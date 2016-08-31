using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
   public class PacketReceivedEventArgs: EventArgs
   {
       private IPacket Packet { get; }

       public PacketReceivedEventArgs(IPacket packet)
       {
           if (packet == null)
           {
               throw new ArgumentNullException(nameof(packet));
           }
           this.Packet = packet;
       }
   }
}
