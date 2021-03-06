﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelException.cs" company="Colony Online Project">
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
    using System.Runtime.Serialization;

    /// <summary>
    ///     A exception that describes a failure while calling a function through the squirrel api.
    /// </summary>
    public class SquirrelException : Exception
    {
        private readonly ISquirrelApi api;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SquirrelException" /> class.
        /// </summary>
        /// <param name="api">
        ///     The squirrel api. Used to get the last error message.
        /// </param>
        public SquirrelException(ISquirrelApi api)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            this.api = api;
            this.api.SqGetLastError();
            this.InitLastErrorProperty();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SquirrelException" /> class with a specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="api">The squirrel api. Used to get the last error message.</param>
        public SquirrelException(string message, ISquirrelApi api)
            : base(message)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            this.api = api;
            this.InitLastErrorProperty();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SquirrelException" /> class with a specified message
        ///     <para />
        ///     and a reference to a inner exception which caused this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="api">The squirrel api. Used to get the last error message.</param>
        public SquirrelException(string message, Exception innerException, ISquirrelApi api)
            : base(message, innerException)
        {
            if (api == null)
            {
                throw new ArgumentNullException(nameof(api));
            }

            this.api = api;
            this.InitLastErrorProperty();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SquirrelException" /> class.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SquirrelException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///     Gets a text that describes the last squirrel error that occurred. This error is most likely the cause for this
        ///     exception.
        ///     <remarks>Can return null.</remarks>
        /// </summary>
        public string SquirrelLastError { get; private set; }

        /// <summary>
        ///     Returns a string representation of the exception.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{this.Message}{Environment.NewLine}'Last Squirrel error:{this.SquirrelLastError ?? "null"}'";
        }

        /// <summary>
        ///     Initializes the 'SquirrelLastError' property.
        /// </summary>
        private void InitLastErrorProperty()
        {
            string lastError;
            this.SquirrelLastError = this.api.SqGetString(this.api.SqGetTop(), out lastError) ? lastError : "null";
        }
    }
}