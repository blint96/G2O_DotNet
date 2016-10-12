// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnsiString.cs" company="Colony Online Project">
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
namespace G2O.DotNet.Squirrel
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    ///     Encapsulates the allocation and release of a unmanaged ANSI string from a managed <see cref="string" /> instance.
    /// </summary>
    public class AnsiString : IDisposable
    {
        /// <summary>
        ///     The managed version of the unmanaged ANSI string.
        /// </summary>
        private readonly string value;

        /// <summary>
        ///     Indicates whether the object is disposed or not.
        /// </summary>
        private bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AnsiString" /> class.
        /// </summary>
        /// <param name="value">The <see cref="string" /> for which the unmanaged ANSI string should be created.</param>
        public AnsiString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.value = value;
            this.Unmanaged = Marshal.StringToHGlobalAnsi(value);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="AnsiString" /> class.
        ///     Releases the unmanaged resources if this has not been done before.
        /// </summary>
        ~AnsiString()
        {
            this.Dispose();
        }

        /// <summary>
        ///     Gets the length of the string that is represented by this object.
        /// </summary>
        public int Length => this.value.Length;

        /// <summary>
        ///     Gets the pointer to the unmanaged version of the string.
        /// </summary>
        public IntPtr Unmanaged { get; private set; }

        /// <summary>
        ///     Gets the <see cref="string" /> value of the object.
        /// </summary>
        public string Value => this.value;

        /// <summary>
        ///     Creates a new instance of <see cref="AnsiString" /> from a instance of <see cref="string" />.
        /// </summary>
        /// <param name="value">The <see cref="string" /> for which the unmanaged ANSI string should be created.</param>
        public static implicit operator AnsiString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new AnsiString(value);
        }

        /// <summary>
        ///     Releases all unmanaged resources that are related to the object.
        /// </summary>
        public void Dispose()
        {
            if (!this.disposed)
            {
                Marshal.FreeHGlobal(this.Unmanaged);
                this.Unmanaged = IntPtr.Zero;
                this.disposed = true;
            }
        }

        /// <summary>
        ///     Returns the <see cref="string" /> that is represented by the object.
        /// </summary>
        /// <returns>The <see cref="string" /> that is represented by the object.</returns>
        public override string ToString()
        {
            return this.value;
        }
    }
}