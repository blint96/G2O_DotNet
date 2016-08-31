namespace G2O_Framework
{
    using System;
    using System.Drawing;

    public class ColorChangedEventArgs : EventArgs
    {
        public ColorChangedEventArgs(ICharacter character, Color newColor)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            this.Character = character;
            this.NewColor = newColor;
        }

        public ICharacter Character { get; }

        public Color NewColor { get; }
    }
}