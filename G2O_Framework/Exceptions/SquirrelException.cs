// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelException.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  <ulian Vogel
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
namespace GothicOnline.G2.DotNet.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// A exception that describes a failure while calling a function through the squirrel api.
    /// </summary>
    public class SquirrelException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SquirrelException"/> class.
        /// </summary>
        public SquirrelException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquirrelException"/> class with a specified message.
        /// </summary>
        /// <param name="message"></param>
        public SquirrelException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquirrelException"/> class with a specified message<para/>
        ///  and a reference to a inner exception which caused this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SquirrelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SquirrelException"/> class.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SquirrelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}