// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountEntity.cs" company="Colony Online Project">
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Account")]
    public class AccountEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AccountId { get; set; }

        [Required]
        public string AccountName { get; set; }

        public DateTime LastDisconnectTime { get; set; }

        public string LastIpAddress { get; set; }

        public DateTime LastLoginTime { get; set; }

        public string LastMACAddress { get; set; }

        public string MailAddress { get; set; }

        [Required]
        public int MaxCharacters { get; set; }

        [Required]
        public string PasswordHash { get; set; }


        public ICollection<CharacterEntity> PlayerCharacter { get; set; }
    }
}