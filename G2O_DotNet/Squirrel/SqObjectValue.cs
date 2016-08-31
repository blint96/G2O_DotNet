using System;
using System.Runtime.InteropServices;

namespace GothicOnline.G2.DotNet.Squirrel
{
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
