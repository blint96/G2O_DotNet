// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqObjectValue.cs" company="Colony Online Project">
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

    [StructLayout(LayoutKind.Explicit)]
    public struct SqObjectValue
    {
        [FieldOffset(0)]
        public IntPtr pTable;

        [FieldOffset(0)]
        public IntPtr pArray;

        [FieldOffset(0)]
        public IntPtr pClosure;

        [FieldOffset(0)]
        public IntPtr pOuter;

        [FieldOffset(0)]
        public IntPtr pGenerator;

        [FieldOffset(0)]
        public IntPtr pNativeClosure;

        [FieldOffset(0)]
        public IntPtr pString;

        [FieldOffset(0)]
        public IntPtr pUserData;

        [FieldOffset(0)]
        public int nInteger;

        [FieldOffset(0)]
        public float fFloat;

        [FieldOffset(0)]
        public IntPtr pUserPointer;

        [FieldOffset(0)]
        public IntPtr pFunctionProto;

        [FieldOffset(0)]
        public IntPtr pRefCounted;

        [FieldOffset(0)]
        public IntPtr pDelegable;

        [FieldOffset(0)]
        public IntPtr pThread;

        [FieldOffset(0)]
        public IntPtr pClass;

        [FieldOffset(0)]
        public IntPtr pInstance;

        [FieldOffset(0)]
        public IntPtr pWeakRef;

        [FieldOffset(0)]
        public uint raw;
    }
}