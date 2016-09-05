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
namespace GothicOnline.G2.DotNet.Squirrel
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Exceptions;
    using GothicOnline.G2.DotNet.Interop;

    public static class SquirrelCallHelper
    {
        public static void CallNoReturn(this ISquirrelApi squirrelApi, string functionName)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
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
                if (!squirrelApi.SqCall(1, false, false))
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

        public static void CallNoReturn(this ISquirrelApi squirrelApi, IntPtr functionName, int functionNameLength)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentException("The Argument must not be IntPtr.Zero", nameof(functionName));
            }

            if (functionNameLength <= 0)
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
                        $"The gothic online server function '{Marshal.PtrToStringAnsi(functionName)}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                if (!squirrelApi.SqCall(1, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{Marshal.PtrToStringAnsi(functionName)}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallNoReturn(this ISquirrelApi squirrelApi, AnsiString functionName)
        {
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
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
                if (!squirrelApi.SqCall(1, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{functionName}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static TRet CallWithReturn<TRet>(this ISquirrelApi squirrelApi, string functionName)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
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
                if (!squirrelApi.SqCall(1, true, false))
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

        public static TRet CallWithReturn<TRet>(
            this ISquirrelApi squirrelApi,
            IntPtr functionName,
            int functionNameLength)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentException("The Argument must not be IntPtr.Zero", nameof(functionName));
            }

            if (functionNameLength <= 0)
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
                        $"The gothic online server function '{Marshal.PtrToStringAnsi(functionName)}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                if (!squirrelApi.SqCall(1, true, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{Marshal.PtrToStringAnsi(functionName)}' function failed",
                        squirrelApi);
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

        public static TRet CallWithReturn<TRet>(
           this ISquirrelApi squirrelApi, AnsiString functionName)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
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
                if (!squirrelApi.SqCall(1, true, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{functionName}' function failed",
                        squirrelApi);
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


        public static void CallWithParameter<TParam>(this ISquirrelApi squirrelApi, string functionName, TParam param)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
            }
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
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
                PushValue<TParam>(squirrelApi, param);

                if (!squirrelApi.SqCall(2, false, false))
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

        public static void CallWithParameter<TParam>(
            this ISquirrelApi squirrelApi,
            IntPtr functionName,
            int functionNameLength,
            TParam param)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentException("The Argument must not be IntPtr.Zero", nameof(functionName));
            }

            if (functionNameLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(functionNameLength));
            }
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
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
                        $"The gothic online server function '{Marshal.PtrToStringAnsi(functionName)}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                PushValue<TParam>(squirrelApi, param);

                if (!squirrelApi.SqCall(2, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{Marshal.PtrToStringAnsi(functionName)}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam>(this ISquirrelApi squirrelApi, AnsiString functionName, TParam param)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
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
                PushValue<TParam>(squirrelApi, param);

                if (!squirrelApi.SqCall(2, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{functionName}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam1,TParam2>(this ISquirrelApi squirrelApi, string functionName, TParam1 param1, TParam2 param2)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
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
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);

                if (!squirrelApi.SqCall(3, false, false))
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

        public static void CallWithParameter<TParam1, TParam2>(
            this ISquirrelApi squirrelApi,
            IntPtr functionName,
            int functionNameLength,
            TParam1 param1, TParam2 param2)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentException("The Argument must not be IntPtr.Zero", nameof(functionName));
            }

            if (functionNameLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(functionNameLength));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
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
                        $"The gothic online server function '{Marshal.PtrToStringAnsi(functionName)}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);

                if (!squirrelApi.SqCall(3, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{Marshal.PtrToStringAnsi(functionName)}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam1, TParam2>(this ISquirrelApi squirrelApi, AnsiString functionName, TParam1 param1, TParam2 param2)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
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
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);

                if (!squirrelApi.SqCall(3, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{functionName}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam1, TParam2, TParam3>(this ISquirrelApi squirrelApi, string functionName, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }
            if (param3 == null)
            {
                throw new ArgumentNullException(nameof(param3));
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
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);
                PushValue<TParam3>(squirrelApi, param3);

                if (!squirrelApi.SqCall(4, false, false))
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

        public static void CallWithParameter<TParam1, TParam2, TParam3>(
            this ISquirrelApi squirrelApi,
            IntPtr functionName,
            int functionNameLength,
            TParam1 param1, TParam2 param2, TParam3 param3)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentException("The Argument must not be IntPtr.Zero", nameof(functionName));
            }

            if (functionNameLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(functionNameLength));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }
            if (param3 == null)
            {
                throw new ArgumentNullException(nameof(param3));
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
                        $"The gothic online server function '{Marshal.PtrToStringAnsi(functionName)}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);
                PushValue<TParam3>(squirrelApi, param3);

                if (!squirrelApi.SqCall(4, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{Marshal.PtrToStringAnsi(functionName)}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam1, TParam2, TParam3>(this ISquirrelApi squirrelApi, AnsiString functionName, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }
            if (param3 == null)
            {
                throw new ArgumentNullException(nameof(param3));
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
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);
                PushValue<TParam3>(squirrelApi, param3);

                if (!squirrelApi.SqCall(4, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{functionName}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam1, TParam2, TParam3, TParam4>(this ISquirrelApi squirrelApi, string functionName, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (string.IsNullOrEmpty(functionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(functionName));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }
            if (param3 == null)
            {
                throw new ArgumentNullException(nameof(param3));
            }
            if (param4 == null)
            {
                throw new ArgumentNullException(nameof(param4));
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
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);
                PushValue<TParam3>(squirrelApi, param3);
                PushValue<TParam4>(squirrelApi, param4);

                if (!squirrelApi.SqCall(5, false, false))
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

        public static void CallWithParameter<TParam1, TParam2, TParam3, TParam4>(
            this ISquirrelApi squirrelApi,
            IntPtr functionName,
            int functionNameLength,
            TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }

            if (functionName == IntPtr.Zero)
            {
                throw new ArgumentException("The Argument must not be IntPtr.Zero", nameof(functionName));
            }

            if (functionNameLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(functionNameLength));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }
            if (param3 == null)
            {
                throw new ArgumentNullException(nameof(param3));
            }
            if (param4 == null)
            {
                throw new ArgumentNullException(nameof(param4));
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
                        $"The gothic online server function '{Marshal.PtrToStringAnsi(functionName)}' could not be found in the root table",
                        squirrelApi);
                }

                // Call the function
                squirrelApi.SqPushRootTable();
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);
                PushValue<TParam3>(squirrelApi, param3);
                PushValue<TParam4>(squirrelApi, param4);

                if (!squirrelApi.SqCall(5, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{Marshal.PtrToStringAnsi(functionName)}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        public static void CallWithParameter<TParam1, TParam2, TParam3, TParam4>(this ISquirrelApi squirrelApi, AnsiString functionName, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            if (squirrelApi == null)
            {
                throw new ArgumentNullException(nameof(squirrelApi));
            }
            if (functionName == null)
            {
                throw new ArgumentNullException(nameof(functionName));
            }
            if (param1 == null)
            {
                throw new ArgumentNullException(nameof(param1));
            }
            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }
            if (param3 == null)
            {
                throw new ArgumentNullException(nameof(param3));
            }
            if (param4 == null)
            {
                throw new ArgumentNullException(nameof(param4));
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
                PushValue<TParam1>(squirrelApi, param1);
                PushValue<TParam2>(squirrelApi, param2);
                PushValue<TParam3>(squirrelApi, param3);
                PushValue<TParam4>(squirrelApi, param4);

                if (!squirrelApi.SqCall(5, false, false))
                {
                    throw new SquirrelException(
                        $"The call to the '{functionName}' function failed",
                        squirrelApi);
                }
            }
            finally
            {
                // Set back top
                squirrelApi.SqSetTop(top);
            }
        }

        private static void PushValue<TParam>(ISquirrelApi squirrelApi, TParam value)
        {
            if (typeof(TParam) == typeof(string))
            {
                squirrelApi.SqPushString((string)(object)value);
            }
            else if (typeof(TParam) == typeof(int))
            {
                squirrelApi.SqPushInteger((int)(object)value);
            }
            else if (typeof(TParam) == typeof(float))
            {
                squirrelApi.SqPushFloat((float)(object)value);
            }
            else if (typeof(TParam) == typeof(bool))
            {
                squirrelApi.SqPushBool((bool)(object)value);
            }
            else if (typeof(TParam) == typeof(IntPtr))
            {
                squirrelApi.SqPushUserPointer((IntPtr)(object)value);
            }
            else
            {
                throw new NotSupportedException(
                    $"The given type argument '{typeof(TParam).Name}' is not a supported parameter of a squirrel function");
            }
        }
    }
}