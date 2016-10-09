// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelCompilerHelper.cs" company="Colony Online Project">
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
    /// Provides extension methods for the <see cref="ISquirrelApi"/> interface that are related to the compiler functions of the API.
    /// </summary>
    public static class SquirrelCompilerHelper
    {
        /// <summary>
        /// Compiles and and executes a squirrel script.
        /// </summary>
        /// <param name="squirrelApi">The used squirrel API.</param>
        /// <param name="script">The script that should be compiled and executed.</param>
        /// <param name="scriptName">Name of the script(source name), only for error display.</param>
        public static void CompileAndExecute(this ISquirrelApi squirrelApi, string script, string scriptName)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(scriptName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(scriptName));
            }

            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(script));
            }

            int top = squirrelApi.SqGetTop();
            squirrelApi.SqCompileBuffer(script, scriptName, true);
            squirrelApi.SqPushRootTable();
            squirrelApi.SqCall(1, false, false);
            squirrelApi.SqSetTop(top);
        }
    }
}