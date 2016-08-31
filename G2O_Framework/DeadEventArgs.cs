using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public class DeadEventArgs:EventArgs
    {
        public ICharacter Character { get; }
        public ICharacter Killer { get; }

        public DeadEventArgs(ICharacter character, ICharacter killer)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }
            if (killer == null)
            {
                throw new ArgumentNullException(nameof(killer));
            }
            this.Character = character;
            this.Killer = killer;
        }

        public DeadEventArgs(ICharacter character)
        {
            this.Character = character;
        }
    }
}
