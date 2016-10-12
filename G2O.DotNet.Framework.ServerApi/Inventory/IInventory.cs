// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInventory.cs" company="Colony Online Project">
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
// Interface for the classes that implement the inventory specific methods of the server API.
// Also contains members for a basic item tracking functionality.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi
{
    using System.Collections.Generic;

    /// <summary>
    ///     Interface for the classes that implement the inventory specific methods of the server API.
    ///     Also contains members for a basic item tracking functionality.
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        ///     Gets all Items that are contained in this instance of <see cref="IInventory" />.
        /// </summary>
        IEnumerable<IItem> Items { get; }

        /// <summary>
        ///     Gets the <see cref="ICharacter" /> that this <see cref="IInventory" /> belongs to.
        /// </summary>
        ICharacter Owner { get; }

        /// <summary>
        ///     Adds a item to the inventory
        /// </summary>
        /// <param name="itemInstance">The instance name of the item.</param>
        /// <param name="amount">The amount of items that should be added.</param>
        void AddItem(string itemInstance, int amount);

        /// <summary>
        ///     Removes all items from the player inventory.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Gets the available data about a item that is contained in this instance of <see cref="IInventory" />.
        ///     <remarks>Throws an exception if the given item instance is not contained in the inventory.</remarks>
        /// </summary>
        /// <param name="itemInstance">The instance name of the item.</param>
        /// <returns>Item data.</returns>
        IItem GetItem(string itemInstance);

        /// <summary>
        ///     Checks if this instance of <see cref="IInventory" /> contains at least one item with the given instance name.
        /// </summary>
        /// <param name="itemInstance">
        ///     The instance name of the item which should be searched.
        /// </param>
        /// <returns>
        ///     True if the <see cref="IInventory" /> contains at least one item with the given instance name./>.
        /// </returns>
        bool HasItem(string itemInstance);

        /// <summary>
        ///     Removes a item from this instance of <see cref="IInventory" />.
        ///     <remarks>Throws an exception if more the <see cref="amount" /> is bigger than the actually available amount.</remarks>
        /// </summary>
        /// <param name="itemInstance">The instance name of the item that should be removed.</param>
        /// <param name="amount">The amount of items that should be removed.</param>
        void RemoveItem(string itemInstance, int amount);
    }
}