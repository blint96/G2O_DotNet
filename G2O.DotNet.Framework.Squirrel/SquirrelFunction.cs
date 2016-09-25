// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelFunction.cs" company="Colony Online Project">
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
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Encapsulates the registration of a squirrel function.
    /// </summary>
    public class SquirrelFunction
    {
        /// <summary>
        /// Stores information about the parameters types of the function.
        /// </summary>
        private readonly ParameterInfo[] parameterTypes;

        /// <summary>
        /// The used instance of the <see cref="ISquirrelApi"/>
        /// </summary>
        private readonly ISquirrelApi squirrelApi;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquirrelFunction"/> class.
        /// Creates a new function inside the squirrel vm with a specified name.
        ///  The parameters and return type are taken from the provided callback function.
        /// </summary>
        /// <param name="squirrelApi">The squirrel api that should be used to create the function.</param>
        /// <param name="functionName">The name of the new squirrel function.</param>
        /// <param name="function">The callback delegate that gets called when the function is called from the squirrel vm.</param>
        public SquirrelFunction(ISquirrelApi squirrelApi, string functionName, Delegate function)
        {
            this.squirrelApi = squirrelApi;
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }

            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            this.FunctionName = functionName;
            this.Function = function;

            Type returnType = function.GetType().GetMethod("Invoke").ReturnType;
            if (returnType != typeof(void) && !this.IsSquirrelCompatibleType(returnType))
            {
                throw new ArgumentException(
                    "The return type of the given function delegate is no squirrel compatible type.", 
                    nameof(function));
            }

            this.parameterTypes = function.GetType().GetMethod("Invoke").GetParameters();
            if (this.parameterTypes.Any(parameterInfo => !this.IsSquirrelCompatibleType(parameterInfo.ParameterType)))
            {
                throw new ArgumentException(
                    "A parameter type of the given function delegate is no squirrel compatible type.", 
                    nameof(function));
            }

            squirrelApi.RegisterFunction(functionName, this.OnCalled);
        }

        /// <summary>
        /// Gets the callback delegate of this <see cref="SquirrelFunction"/>.
        /// </summary>
        public Delegate Function { get; }

        /// <summary>
        /// Gets the function name of this <see cref="SquirrelFunction"/>.
        /// </summary>
        public string FunctionName { get; }

        /// <summary>
        /// Checks if the given type has a equivalent in the squirrel API.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that should be checked.</param>
        /// <returns></returns>
        private bool IsSquirrelCompatibleType(Type type)
        {
            if (type == typeof(string))
            {
                return true;
            }

            if (type == typeof(int))
            {
                return true;
            }

            if (type == typeof(float))
            {
                return true;
            }

            if (type == typeof(bool))
            {
                return true;
            }

            if (type == typeof(string))
            {
                return true;
            }

            if (type == typeof(IntPtr))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets called when the function is called from the squirrel vm.
        /// Maps the parameters and return value to the callback delegate and invokes it.
        /// </summary>
        /// <param name="vm">The vm that invoke the function.</param>
        /// <returns>Return value for the invoking VM. Indicates whether the call to the function was successfull.</returns>
        private SqResult OnCalled(IntPtr vm)
        {
            if (this.squirrelApi.SqGetTop() != this.parameterTypes.Length + 1)
            {
                string error =
                    $"Invalid parameter count for the function '{this.FunctionName}' expecting {this.parameterTypes.Length}(given {this.squirrelApi.SqGetTop() - 1})";
                this.squirrelApi.SqThrowError(error);
                return SqResult.SqError;
            }

            object[] parameterValues = new object[this.parameterTypes.Length];
            for (int i = 0; i < this.parameterTypes.Length; i++)
            {
                SqObjectType type = this.squirrelApi.SqGetType(i+2);
                
                // String
                if (this.parameterTypes[i].ParameterType == typeof(string) && type != SqObjectType.OtString)
                {
                    string error =
                        $"Invalid parameter  for the function '{this.FunctionName}' expecting string (given {type}";
                    this.squirrelApi.SqThrowError(error);
                    return SqResult.SqError;
                }
                else if(this.parameterTypes[i].ParameterType == typeof(string))
                {
                    string value;
                    this.squirrelApi.SqGetString(i, out value);
                    //Never return null for string values. If the value is empty or not available return string.empty
                    parameterValues[i] = value?? string.Empty;
                }

                // Int
                if (this.parameterTypes[i].ParameterType == typeof(int) && type != SqObjectType.OtInteger)
                {
                    string error =
                        $"Invalid parameter  for the function '{this.FunctionName}' expecting integer (given {type}";
                    this.squirrelApi.SqThrowError(error);
                    return SqResult.SqError;
                }
                else if (this.parameterTypes[i].ParameterType == typeof(int))
                {
                    int value;
                    this.squirrelApi.SqGetInteger(i, out value);
                    parameterValues[i] = value;
                }

                // Float
                if (this.parameterTypes[i].ParameterType == typeof(float) && type != SqObjectType.OtFloat)
                {
                    string error =
                        $"Invalid parameter  for the function '{this.FunctionName}' expecting float (given {type}";
                    this.squirrelApi.SqThrowError(error);
                    return SqResult.SqError;
                }
                else if(this.parameterTypes[i].ParameterType == typeof(float))
                {
                    float value;
                    this.squirrelApi.SqGetFloat(i, out value);
                    parameterValues[i] = value;
                }

                // Bool
                if (this.parameterTypes[i].ParameterType == typeof(bool) && type != SqObjectType.OtBool)
                {
                    string error =
                        $"Invalid parameter  for the function '{this.FunctionName}' expecting bool (given {type}";
                    this.squirrelApi.SqThrowError(error);
                    return SqResult.SqError;
                }
                else if(this.parameterTypes[i].ParameterType == typeof(bool))
                {
                    bool value;
                    this.squirrelApi.SqGetBool(i, out value);
                    parameterValues[i] = value;
                }

                // Userpointer
                if (this.parameterTypes[i].ParameterType == typeof(IntPtr) && type != SqObjectType.OtBool)
                {
                    string error =
                        $"Invalid parameter  for the function '{this.FunctionName}' expecting user pointer (given {type}";
                    this.squirrelApi.SqThrowError(error);
                    return SqResult.SqError;
                }
                else if(this.parameterTypes[i].ParameterType == typeof(IntPtr))
                {
                    IntPtr value;
                    this.squirrelApi.SqGetUserPointer(i, out value);
                    parameterValues[i] = value;
                }
            }

            object result = this.Function.DynamicInvoke(parameterValues);
            if (result != null)
            {
                if (result is string)
                {
                    this.squirrelApi.SqPushString((string)result);
                }
                else if (result is int)
                {
                    this.squirrelApi.SqPushInteger((int)result);
                }
                else if (result is float)
                {
                    this.squirrelApi.SqPushFloat((float)result);
                }
                else if (result is bool)
                {
                    this.squirrelApi.SqPushBool((bool)result);
                }
                else if (result is IntPtr)
                {
                    this.squirrelApi.SqPushUserPointer((IntPtr)result);
                }
            }

            return SqResult.SqOk;
        }
    }
}