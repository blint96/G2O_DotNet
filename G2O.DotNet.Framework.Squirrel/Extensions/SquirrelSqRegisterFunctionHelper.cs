// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelSqRegisterFunctionHelper.cs" company="Colony Online Project">
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

    /// <summary>
    /// Provides extension methods for the <see cref="ISquirrelApi"/> interface that are related to registering new functions inside the squirrel API.
    /// </summary>
    public static class SquirrelSqRegisterFunctionHelper
    {
        /// <summary>
        /// Registers a new function inside the squirrel VM.
        /// </summary>
        /// <param name="squirrelApi">The used instance of the <see cref="ISquirrelApi"/>.</param>
        /// <param name="functionName">The name of the new function</param>
        /// <param name="function">The delegate for the new function.</param>
        public static void RegisterFunction(this ISquirrelApi squirrelApi, string functionName, SqFunction function)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
            }

            squirrelApi.SqPushRootTable();
            squirrelApi.SqPushString(functionName);
            squirrelApi.SqNewClosure(function, 0);
            squirrelApi.SqNewSlot(-3, false);
            squirrelApi.SqPop(1);
        }
    }
}