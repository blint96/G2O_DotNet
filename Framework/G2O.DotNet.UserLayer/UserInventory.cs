// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserInventory.cs" company="Colony Online Project">
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
namespace G2O.DotNet.UserLayer
{
    using System;
    using System.Collections.Generic;

    using G2O.DotNet.Database;
    using G2O.DotNet.ServerApi;

    internal class UserInventory : IInventory
    {
        private readonly IInventory orgInventory;

        private readonly UserCharacter ownerCharacter;

        private readonly IDatabaseContextFactory contextFactory;

        internal UserInventory(IInventory orgInventory, UserCharacter ownerCharacter,IDatabaseContextFactory contextFactory)
        {
            if (orgInventory == null)
            {
                throw new ArgumentNullException(nameof(orgInventory));
            }

            if (ownerCharacter == null)
            {
                throw new ArgumentNullException(nameof(ownerCharacter));
            }
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.orgInventory = orgInventory;
            this.ownerCharacter = ownerCharacter;
            this.contextFactory = contextFactory;
        }

        public IEnumerable<IItem> Items => this.orgInventory.Items;

        public ICharacter Owner => this.ownerCharacter;

        public void AddItem(string itemInstance, int amount)
        {
            this.orgInventory.AddItem(itemInstance, amount);
        }

        public void Clear()
        {
            this.orgInventory.Clear();
        }

        public IItem GetItem(string itemInstance)
        {
            return this.orgInventory.GetItem(itemInstance);
        }

        public bool HasItem(string itemInstance)
        {
            return this.orgInventory.HasItem(itemInstance);
        }

        public void RemoveItem(string itemInstance, int amount)
        {
            this.orgInventory.RemoveItem(itemInstance, amount);
        }
    }
}