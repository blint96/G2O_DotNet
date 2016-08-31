namespace G2O_Framework
{
    using System;

    public class FocusChangedEventArgs : EventArgs
    {
        public FocusChangedEventArgs(ICharacter character, ICharacter oldFocus, ICharacter newFocus)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            this.Character = character;
            this.OldFocus = oldFocus;
            this.NewFocus = newFocus;
        }

        public ICharacter Character { get; }

        public ICharacter NewFocus { get; }

        public ICharacter OldFocus { get; }
    }
}