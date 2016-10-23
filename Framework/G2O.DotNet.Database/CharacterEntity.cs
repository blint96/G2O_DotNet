// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterEntity.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Database
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Character")]
    public class CharacterEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CharacterId { get; set; }

        public AccountEntity Account { get; set; }

        [Required]
        public string CharacterName { get; set; }

        [Range(1, int.MaxValue)]
        public int Dexterity { get; set; }

        [Range(1, int.MaxValue)]
        public int Health { get; set; }

        [Required]
        public string LastGameWorld { get; set; }

        public float LastPositionX { get; set; }

        public float LastPositionY { get; set; }

        public float LastPositionZ { get; set; }

        [Range(1, int.MaxValue)]
        public int Mana { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxHealth { get; set; }

        [Range(1, int.MaxValue)]
        public int MaxMana { get; }

        [Range(1, int.MaxValue)]
        public int SkillBow { get; set; }

        [Range(1, int.MaxValue)]
        public int SkillCrossBow { get; set; }

        [Range(1, int.MaxValue)]
        public int SkillOneHanded { get; set; }

        [Range(1, int.MaxValue)]
        public int SkillTwoHanded { get; set; }

        [Range(1, int.MaxValue)]
        public int Strength { get; set; }

        public RoleEntity CharacterRole { get; set; }

        public long RoleId { get; set; }

        public ICollection<ItemOwnershipEntity> Items { get; set; }
    }
}