using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O_Framework
{
    using System.Security.Cryptography.X509Certificates;

    public interface IInventory
    {
        void HasItem(IItemInstance itemInstance);

        IItem GetItem(IItemInstance itemInstance);

        void AddItem(IItemInstance itemInstance, int amount);

        void RemoveItem(IItemInstance itemInstance, int amount);

        void Clear();

        IEnumerable<IItem> Items { get; }


    }
}
