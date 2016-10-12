// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelCallHelper.cs" company="Colony Online Project">
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

    public static class SquirrelCallHelper
    {
        public static void PushValue(this ISquirrelApi squirrelApi, object value)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value is string)
            {
                squirrelApi.SqPushString((string)(object)value);
            }
            else if (value is int)
            {
                squirrelApi.SqPushInteger((int)(object)value);
            }
            else if (value is float)
            {
                squirrelApi.SqPushFloat((float)(object)value);
            }
            else if (value is bool)
            {
                squirrelApi.SqPushBool((bool)(object)value);
            }
            else if (value is IntPtr)
            {
                squirrelApi.SqPushUserPointer((IntPtr)(object)value);
            }
            else
            {
                throw new NotSupportedException(
                    $"The given type argument '{value.GetType().Name}' is not a supported parameter of a squirrel function");
            }
        }

        public static TRet Call<TRet>(this ISquirrelApi squirrelApi, AnsiString functionName, params object[] parameters)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            // Get the stack top index
            int top = squirrelApi.SqGetTop();

            try
            {
                // Get the function
                squirrelApi.SqPushRootTable();
                squirrelApi.SqPushString(functionName.Unmanaged, functionName.Length);
                if (!squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{functionName}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                foreach (var parameter in parameters)
                {
                    squirrelApi.PushValue(parameter);
                }

                if (!squirrelApi.SqCall(parameters.Length+1, true, false))
                {
                    throw new SquirrelException($"The call to the '{functionName}' function failed", squirrelApi);
                }
                if (typeof(TRet) == typeof(string))
                {
                    string value;
                    squirrelApi.SqGetString(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(int))
                {
                    int value;
                    squirrelApi.SqGetInteger(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(float))
                {
                    float value;
                    squirrelApi.SqGetFloat(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(bool))
                {
                    bool value;
                    squirrelApi.SqGetBool(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(IntPtr))
                {
                    IntPtr value;
                    squirrelApi.SqGetUserPointer(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else
                {
                    throw new NotSupportedException(
                        $"The given type argument '{typeof(TRet).Name}' is not supported as the return value of a squirrel function.");
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static TRet Call<TRet>(this ISquirrelApi squirrelApi, string functionName, params object[] parameters)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            // Get the stack top index
            int top = squirrelApi.SqGetTop();

            try
            {
                // Get the function
                squirrelApi.SqPushRootTable();
                squirrelApi.SqPushString(functionName);
                if (!squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{functionName}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                foreach (var parameter in parameters)
                {
                    squirrelApi.PushValue(parameter);
                }

                if (!squirrelApi.SqCall(parameters.Length + 1, true, false))
                {
                    throw new SquirrelException($"The call to the '{functionName}' function failed", squirrelApi);
                }
                if (typeof(TRet) == typeof(string))
                {
                    string value;
                    squirrelApi.SqGetString(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(int))
                {
                    int value;
                    squirrelApi.SqGetInteger(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(float))
                {
                    float value;
                    squirrelApi.SqGetFloat(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(bool))
                {
                    bool value;
                    squirrelApi.SqGetBool(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(IntPtr))
                {
                    IntPtr value;
                    squirrelApi.SqGetUserPointer(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else
                {
                    throw new NotSupportedException(
                        $"The given type argument '{typeof(TRet).Name}' is not supported as the return value of a squirrel function.");
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static TRet Call<TRet>(this ISquirrelApi squirrelApi, IntPtr functionName,int functionNameLength, params object[] parameters)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            if (functionNameLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(functionNameLength));
            }

            // Get the stack top index
            int top = squirrelApi.SqGetTop();

            try
            {
                // Get the function
                squirrelApi.SqPushRootTable();
                squirrelApi.SqPushString(functionName,functionNameLength);
                if (!squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{functionName}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                foreach (var parameter in parameters)
                {
                    squirrelApi.PushValue(parameter);
                }

                if (!squirrelApi.SqCall(parameters.Length + 1, true, false))
                {
                    throw new SquirrelException($"The call to the '{functionName}' function failed", squirrelApi);
                }
                if (typeof(TRet) == typeof(string))
                {
                    string value;
                    squirrelApi.SqGetString(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(int))
                {
                    int value;
                    squirrelApi.SqGetInteger(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(float))
                {
                    float value;
                    squirrelApi.SqGetFloat(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(bool))
                {
                    bool value;
                    squirrelApi.SqGetBool(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else if (typeof(TRet) == typeof(IntPtr))
                {
                    IntPtr value;
                    squirrelApi.SqGetUserPointer(squirrelApi.SqGetTop(), out value);
                    return (TRet)(object)value;
                }
                else
                {
                    throw new NotSupportedException(
                        $"The given type argument '{typeof(TRet).Name}' is not supported as the return value of a squirrel function.");
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void Call(this ISquirrelApi squirrelApi, AnsiString functionName, params object[] parameters)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            // Get the stack top index
            int top = squirrelApi.SqGetTop();

            try
            {
                // Get the function
                squirrelApi.SqPushRootTable();
                squirrelApi.SqPushString(functionName.Unmanaged, functionName.Length);
                if (!squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{functionName}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                foreach (var parameter in parameters)
                {
                    squirrelApi.PushValue(parameter);
                }

                if (!squirrelApi.SqCall(parameters.Length + 1, false, false))
                {
                    throw new SquirrelException($"The call to the '{functionName}' function failed", squirrelApi);
                }              
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void Call(this ISquirrelApi squirrelApi, string functionName, params object[] parameters)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            // Get the stack top index
            int top = squirrelApi.SqGetTop();

            try
            {
                // Get the function
                squirrelApi.SqPushRootTable();
                squirrelApi.SqPushString(functionName);
                if (!squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{functionName}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                foreach (var parameter in parameters)
                {
                    squirrelApi.PushValue(parameter);
                }

                if (!squirrelApi.SqCall(parameters.Length + 1, false, false))
                {
                    throw new SquirrelException($"The call to the '{functionName}' function failed", squirrelApi);
                }             
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void Call(this ISquirrelApi squirrelApi, IntPtr functionName, int functionNameLength, params object[] parameters)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            if (functionNameLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(functionNameLength));
            }

            // Get the stack top index
            int top = squirrelApi.SqGetTop();

            try
            {
                // Get the function
                squirrelApi.SqPushRootTable();
                squirrelApi.SqPushString(functionName, functionNameLength);
                if (!squirrelApi.SqGet(-2))
                {
                    throw new SquirrelException(
                        $"The gothic online server function '{functionName}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                foreach (var parameter in parameters)
                {
                    squirrelApi.PushValue(parameter);
                }
                if (!squirrelApi.SqCall(parameters.Length + 1, false, false))
                {
                    throw new SquirrelException($"The call to the '{functionName}' function failed", squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }
    }
}