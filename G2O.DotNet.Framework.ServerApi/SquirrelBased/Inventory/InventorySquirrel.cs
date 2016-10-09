using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace G2O.DotNet.ServerApi.SquirrelBased.Inventory
{
    using G2O.DotNet.ServerApi.Inventory;
    using G2O.DotNet.Squirrel.Interop;

    internal class InventorySquirrel:IInventory
    {
        /// <summary>
        ///     Stores the ansi version of the string "giveItem"
        /// </summary>
        private static AnsiString StringGiveItem = "giveItem";
        /// <summary>
        ///     Stores the ansi version of the string "removeItem"
        /// </summary>
        private static AnsiString StringRemoveItem = "removeItem";


        public IEnumerable<IItem> Items { get; }

        public void AddItem(string itemInstance, int amount)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IItem GetItem(string itemInstance)
        {
            throw new NotImplementedException();
        }

        public void HasItem(string itemInstance)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(string itemInstance, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
