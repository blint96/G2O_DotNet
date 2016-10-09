// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Item.cs" company="Colony Online Project">
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
//  Default implementation of the <see cref="IItem" /> interface.
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.ServerApi.Inventory
{
    using System;

    /// <summary>
    ///     Default implementation of the <see cref="IItem" /> interface.
    ///     <remarks>This is a data class so its independent of the currently used server API implementation.</remarks>
    /// </summary>
    internal class Item : IItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        /// <param name="amount">Item amount.</param>
        /// <param name="instance">Item instance name.</param>
        public Item(int amount, string instance)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            if (string.IsNullOrEmpty(instance))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(instance));
            }

            this.Amount = amount;
            this.Instance = instance;
        }

        /// <summary>
        ///     Gets the amount of items.
        /// </summary>
        public int Amount { get; }

        /// <summary>
        ///     Gets the instance name of the item.
        /// </summary>
        public string Instance { get; }
    }
}