// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventorySquirrel.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  Julian Vogel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// -
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// -
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// -
// </copyright>
// <summary>
// The implementation of the IInventory interface that uses the squirrel API to call the server functions.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Inventory
{
    using System;
    using System.Collections.Generic;

    using G2O.DotNet.ServerApi.Character;
    using G2O.DotNet.Squirrel;
    using G2O.DotNet.Squirrel.Interop;

    /// <summary>
    /// The implementation of the <see cref="IInventory"/> interface that uses the squirrel API to call the server functions.
    /// </summary>
    internal class InventorySquirrel : IInventory
    {
        /// <summary>
        ///     Stores the ANSI version of the string "giveItem"
        /// </summary>
        private static readonly AnsiString StringGiveItem = "giveItem";

        /// <summary>
        ///     Stores the ANSI version of the string "removeItem"
        /// </summary>
        private static readonly AnsiString StringRemoveItem = "removeItem";

        /// <summary>
        ///     The character that this inventory belongs to.
        /// </summary>
        private readonly ICharacter character;

        /// <summary>
        ///     Internal storage for the inventory content.
        /// </summary>
        private readonly Dictionary<string, int> items = new Dictionary<string, int>();

        /// <summary>
        ///     The used instance of the <see cref="ISquirrelApi" />.
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InventorySquirrel" /> class.
        /// </summary>
        /// <param name="squirrelApi">The instance of the squirrel API that should be used to call the related server functions.</param>
        /// <param name="character">The character that this inventory belongs to.</param>
        public InventorySquirrel(ISquirrelApi squirrelApi, ICharacter character)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            this.squirrelApi = squirrelApi;
            this.character = character;
        }

        /// <summary>
        ///     Gets all Items that are contained in this instance of <see cref="IInventory" />.
        /// </summary>
        public IEnumerable<IItem> Items
        {
            get
            {
                List<IItem> list = new List<IItem>();
                foreach (var instance in this.items.Keys)
                {
                    list.Add(new Item(this.items[instance], instance));
                }

                return list;
            }
        }

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> that this <see cref="IInventory" /> belongs to.
        /// </summary>
        public ICharacter Owner => this.character;

        /// <summary>
        ///     Adds a item to the inventory
        /// </summary>
        /// <param name="itemInstance">The instance name of the item.</param>
        /// <param name="amount">The amount of items that should be added.</param>
        public void AddItem(string itemInstance, int amount)
        {
            if (string.IsNullOrEmpty(itemInstance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(itemInstance));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            this.squirrelApi.Call(StringGiveItem, this.character.Client.ClientId, itemInstance, amount);

            // Add the items to the internal storage dictionary.
            if (this.items.ContainsKey(itemInstance))
            {
                this.items[itemInstance] += amount;
            }
            else
            {
                this.items.Add(itemInstance, amount);
            }
        }

        /// <summary>
        ///     Removes all items from the player inventory.
        /// </summary>
        public void Clear()
        {
            foreach (var itemInstance in this.items.Keys)
            {
                this.RemoveItem(itemInstance, this.items[itemInstance]);
            }
        }

        /// <summary>
        ///     Gets the available data about a item that is contained in this instance of <see cref="IInventory" />.
        ///     <remarks>Throws an exception if the given item instance is not contained in the inventory.</remarks>
        /// </summary>
        /// <param name="itemInstance">The instance name of the item.</param>
        /// <returns>Item data.</returns>
        public IItem GetItem(string itemInstance)
        {
            if (string.IsNullOrEmpty(itemInstance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(itemInstance));
            }

            int amount;
            if (this.items.TryGetValue(itemInstance, out amount))
            {
                return new Item(amount, itemInstance);
            }
            else
            {
                throw new ArgumentException(
                    "The inventory does not contain a item with the given instance name.", 
                    nameof(itemInstance));
            }
        }

        /// <summary>
        ///     Checks if this instance of <see cref="IInventory" /> contains at least one item with the given instance name.
        /// </summary>
        /// <param name="itemInstance">
        ///     The instance name of the item which should be searched.
        /// </param>
        /// <returns>
        ///     True if the <see cref="IInventory" /> contains at least one item with the given instance name./>.
        /// </returns>
        public bool HasItem(string itemInstance)
        {
            if (string.IsNullOrEmpty(itemInstance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(itemInstance));
            }

            return this.items.ContainsKey(itemInstance);
        }

        /// <summary>
        ///     Removes a item from this instance of <see cref="IInventory" />.
        ///     <remarks>Throws an exception if more the <see cref="amount" /> is bigger than the actually available amount.</remarks>
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be removed.</param>
        /// <param name="amount">The amount of items that should be removed.</param>
        public void RemoveItem(string itemInstance, int amount)
        {
            if (string.IsNullOrEmpty(itemInstance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(itemInstance));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            // Check if the items can be removed(do not allow to remove more items than are available.
            int currentAmount;
            if (!this.items.TryGetValue(itemInstance, out currentAmount))
            {
                throw new ArgumentException(
                    "The inventory does not contain items of the with the given instance name", 
                    nameof(itemInstance));
            }

            if (currentAmount < amount)
            {
                throw new ArgumentException(
                    "The inventory does not contain enough items of the with the given instance name", 
                    nameof(amount));
            }

            // Subtract the item count of completely remove the key if the amount has reached 0.
            if (currentAmount == amount)
            {
                this.items.Remove(itemInstance);
            }
            else
            {
                this.items[itemInstance] -= amount;
            }

            this.squirrelApi.Call(StringRemoveItem, this.character.Client.ClientId, itemInstance, amount);
        }
    }
}