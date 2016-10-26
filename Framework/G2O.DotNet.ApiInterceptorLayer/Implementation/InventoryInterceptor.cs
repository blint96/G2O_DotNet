// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InventoryInterceptor.cs" company="Colony Online Project">
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
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ApiInterceptorLayer
{
    using System;
    using System.Collections.Generic;

    using G2O.DotNet.ServerApi;

    /// <summary>
    ///     A class that decorates the <see cref="IInventory" /> interface with events that allow listeners to be informed
    ///     about calls that change the method.
    /// </summary>
    internal class InventoryInterceptor : IInventoryInterceptor
    {
        /// <summary>
        ///     Stores a reference to original <see cref="IInventory" /> instance that is decorated by the current
        ///     <see cref="InventoryInterceptor" /> object.
        /// </summary>
        private readonly IInventory orgInventory;

        /// <summary>
        ///     Stores a reference to the <see cref="CharacterInterceptor" /> that intercepts the <see cref="ICharacter" />
        ///     instance that owns the decorated <see cref="IInventory" /> instance.
        /// </summary>
        private readonly CharacterInterceptor ownerCharacter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InventoryInterceptor" /> class.
        /// </summary>
        /// <param name="orgInventory">
        ///     The original <see cref="IInventory" /> instance that should be decorated by the new
        ///     <see cref="InventoryInterceptor" /> object.
        /// </param>
        /// <param name="ownerCharacter">
        ///     The interceptor of the owner <see cref="ICharacter" /> of the original
        ///     <see cref="IInventory" /> instance .
        /// </param>
        internal InventoryInterceptor(IInventory orgInventory, CharacterInterceptor ownerCharacter)
        {
            if (orgInventory == null)
            {
                throw new ArgumentNullException(nameof(orgInventory));
            }

            if (ownerCharacter == null)
            {
                throw new ArgumentNullException(nameof(ownerCharacter));
            }

            this.orgInventory = orgInventory;
            this.ownerCharacter = ownerCharacter;
        }

        /// <summary>
        ///     Invokes all registered handlers when the "AddItem" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string, int>> OnAddItem;

        /// <summary>
        ///     Invokes all registered handlers when the "Clear" method is called.
        /// </summary>
        public event EventHandler<EventArgs> OnClear;

        /// <summary>
        ///     Invokes all registered handlers when the "RemoveItem" method is called.
        /// </summary>
        public event EventHandler<NotifyAboutCallEventArgs<string, int>> OnRemoveItem;

        /// <summary>
        ///     Gets all Items that are contained in this instance of <see cref="IInventory" />.
        /// </summary>
        public IEnumerable<IItem> Items => this.orgInventory.Items;

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> that this <see cref="IInventory" /> belongs to.
        /// </summary>
        public ICharacter Owner => this.ownerCharacter;

        /// <summary>
        ///     Gets the <see cref="ICharacterInterceptor" /> that decorates the <see cref="ICharacter" /> instance that owns the
        ///     inventory.
        /// </summary>
        ICharacterInterceptor IInventoryInterceptor.Owner => this.ownerCharacter;

        /// <summary>
        ///     Adds a item to the inventory
        /// </summary>
        /// <param name="itemInstance">The instance name of the item.</param>
        /// <param name="amount">The amount of items that should be added.</param>
        public void AddItem(string itemInstance, int amount)
        {
            this.orgInventory.AddItem(itemInstance, amount);
            this.OnAddItem?.Invoke(this, new NotifyAboutCallEventArgs<string, int>(itemInstance, amount));
        }

        /// <summary>
        ///     Removes all items from the player inventory.
        /// </summary>
        public void Clear()
        {
            this.orgInventory.Clear();
            this.OnClear?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Gets the available data about a item that is contained in this instance of <see cref="IInventory" />.
        ///     <remarks>Throws an exception if the given item instance is not contained in the inventory.</remarks>
        /// </summary>
        /// <param name="itemInstance">The instance name of the item.</param>
        /// <returns>Item data.</returns>
        public IItem GetItem(string itemInstance)
        {
            return this.orgInventory.GetItem(itemInstance);
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
            return this.orgInventory.HasItem(itemInstance);
        }

        /// <summary>
        ///     Removes a item from this instance of <see cref="IInventory" />.
        ///     <remarks>Throws an exception if more the amount is bigger than the actually available amount.</remarks>
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be removed.</param>
        /// <param name="amount">The amount of items that should be removed.</param>
        public void RemoveItem(string itemInstance, int amount)
        {
            this.orgInventory.RemoveItem(itemInstance, amount);
            this.OnRemoveItem?.Invoke(this, new NotifyAboutCallEventArgs<string, int>(itemInstance, amount));
        }
    }
}