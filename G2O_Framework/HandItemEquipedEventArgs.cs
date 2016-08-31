namespace G2O_Framework
{
    using System;
    using System.ComponentModel;

    public class HandItemEquipedEventArgs : ItemEquipedEventArgs
    {
        public HandItemEquipedEventArgs(ICharacter character, IItemInstance instance, Hand hand)
            : base(character, instance)
        {
            if (!Enum.IsDefined(typeof(Hand), hand))
            {
                throw new InvalidEnumArgumentException(nameof(hand), (int)hand, typeof(Hand));
            }

            this.Hand = hand;
        }

        public Hand Hand { get;}
    }
}