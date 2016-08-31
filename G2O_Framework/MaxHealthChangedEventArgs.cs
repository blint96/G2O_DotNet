using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public class MaxHealthChangedEventArgs : EventArgs
    {
        public MaxHealthChangedEventArgs(ICharacter character, int oldMaxHp, int newMaxHp)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }
            this.Character = character;
            this.OldMaxHp = oldMaxHp;
            this.NewMaxHp = newMaxHp;
        }

        public ICharacter Character { get; }
        public int OldMaxHp { get; }
        public int NewMaxHp { get; }
    }
}
