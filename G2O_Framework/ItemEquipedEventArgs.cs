namespace G2O_Framework
{
    using System;

    public class ItemEquipedEventArgs : EventArgs
    {
        public ItemEquipedEventArgs(ICharacter character, IItemInstance instance)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            this.Character = character;
            this.Instance = instance;
        }

        public ICharacter Character { get; }

        public IItemInstance Instance { get; }
    }
}