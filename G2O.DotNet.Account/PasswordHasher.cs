// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasswordHasher.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Account
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// A class that provides methods for calculating password hashes and salts.
    /// </summary>
    internal class PasswordHasher
    {
        /// <summary>
        /// Generates a new password salt.
        /// </summary>
        /// <returns>The password salt string</returns>
        public string GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Calculates the hash of a password in combination with a password salt.
        /// </summary>
        /// <param name="password">The password that should be hashed.</param>
        /// <param name="salt">The password salt.</param>
        /// <returns>Password hash.</returns>
        public string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(salt));
            }

            byte[] saltByte = Convert.FromBase64String(salt);
            var hasher = new Rfc2898DeriveBytes(password, saltByte, 10000);
            byte[] hash = hasher.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(saltByte, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}