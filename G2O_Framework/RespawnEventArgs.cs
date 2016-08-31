using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public class RespawnEventArgs:EventArgs
    {
        public RespawnEventArgs(ICharacter character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }
            this.Character = character;
        }

        public  ICharacter Character { get; }
    }
}
