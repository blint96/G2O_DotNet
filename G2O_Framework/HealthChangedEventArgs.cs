using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    public class HealthChangedEventArgs : EventArgs
    {
        public HealthChangedEventArgs(ICharacter character, int oldHp, int newHp)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }
            this.Character = character;
            this.OldHp = oldHp;
            this.NewHp = newHp;
        }

        public ICharacter Character { get; }
        public int OldHp { get; }
        public int NewHp { get; }
    }
}
