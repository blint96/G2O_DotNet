using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
   public class HitEventArgs:EventArgs
    {
        public ICharacter Character { get; }
        public ICharacter Attacker { get; }

        public HitEventArgs(ICharacter character, ICharacter attacker)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }
            if (attacker == null)
            {
                throw new ArgumentNullException(nameof(attacker));
            }
            this.Character = character;
            this.Attacker = attacker;
        }

        public HitEventArgs(ICharacter character)
        {
            this.Character = character;
        }
    }
}
