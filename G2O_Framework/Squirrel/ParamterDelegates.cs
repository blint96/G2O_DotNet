// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParamterDelegates.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Squirrel
{
    using System;
    using System.Runtime.InteropServices;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SqWriteFunc(IntPtr p1, IntPtr p2, int intVal);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SqReadFunc(IntPtr p1, IntPtr p2, int intVal);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SqPrintFunction(IntPtr vm, params string[] value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SqFunction(IntPtr vm);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SqLexReadFunc(IntPtr userPtr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SqCompilerError(IntPtr vm, string desc, string source, int line, int column);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int SqReleaseHook(IntPtr userPtr, int size);
}