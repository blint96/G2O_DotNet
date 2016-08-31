using System;
using System.Runtime.InteropServices;

namespace GothicOnline.G2.DotNet.Squirrel
{
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
