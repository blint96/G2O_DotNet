// --------------------------------------------------------------------------------------------------------------------
// <copyright file="G2OEventCallback.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Squirrel
{
    using System;
    using System.Text;

    /// <summary>
    /// Class for encapsulating the registration of managed callbacks to the events of the g2o server.
    /// </summary>
    public class G2OEventCallback
    {
        /// <summary>
        /// Random number generator used to append random numbers to generated function names to prevent name collisions.
        /// <remarks>Allways that with the seed 0 because the values dont need to change on each start of the application.</remarks>
        /// </summary>
        private static readonly Random Random = new Random(0);

        /// <summary>
        /// The managed callback function that is called when the event is called inside the squirrel VM.
        /// </summary>
        private readonly SquirrelFunction managedCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="G2OEventCallback"/> class.
        /// </summary>
        /// <param name="squirrelApi">The instance of the <see cref="ISquirrelApi"/> that should be used to register the event callback.</param>
        /// <param name="eventName">The name of the event for which the callback should be registered.</param>
        /// <param name="callback">The event callback function. The delegate need to have the same parameters that the event has.</param>
        public G2OEventCallback(ISquirrelApi squirrelApi, string eventName, Delegate callback)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(eventName));
            }

            // Register a managed callback function that can be called from inside the squirrel vm.
            this.managedCallback = new SquirrelFunction(squirrelApi, $"G2ONet_{eventName}_{Random.Next()}", callback);

            // Build, compile and register the a proxy function inside the squirrel vm that calls the managed callback.
            string proxyFunctionName = $"G2O_{eventName}_{Random.Next()}";
            var parameterBraces = this.BuildParameterBraces();
            StringBuilder codeBuilder = new StringBuilder();
            codeBuilder.AppendLine($"function {proxyFunctionName}{parameterBraces}");
            codeBuilder.AppendLine("{");
            codeBuilder.AppendLine($"{this.managedCallback.FunctionName}{parameterBraces};");
            codeBuilder.AppendLine("}");
            codeBuilder.AppendLine($"addEventHandler(\"{eventName}\",{proxyFunctionName});");

            // Compile the proxy function
            squirrelApi.CompileAndExecute(codeBuilder.ToString(), $"Register Event Callback for {eventName}");
        }

        /// <summary>
        /// Builds a string which contains the parameter braces for the squirrel proxy function.
        /// </summary>
        /// <returns>A string which contains the parameter braces for the squirrel proxy function.</returns>
        private string BuildParameterBraces()
        {
            // Get the amount of parameters.
            int parameterCount = this.managedCallback.Function.GetType().GetMethod("Invoke").GetParameters().Length;

            // Build the parameter names "(parameter0,parameter1,...)
            StringBuilder paramBuilder = new StringBuilder();
            paramBuilder.Append("(");
            for (int i = 0; i < parameterCount; i++)
            {
                if (i != 0)
                {
                    paramBuilder.Append(",");
                }

                paramBuilder.Append("parameter");
                paramBuilder.Append(i);
            }

            paramBuilder.Append(")");

            return paramBuilder.ToString();
        }
    }
}