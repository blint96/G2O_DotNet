// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Squirrel.cs" company="Colony Online Project">
// Copyright (C) <2016>  <Julian Vogel>
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// </copyright>
// <summary>
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace GothicOnline.G2.DotNet.Loader.Squirrel
{
    using System;
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Squirrel;

    // delegates
    #region vm

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr OpenCallback(int initialstacksize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr NewThreadCallback(IntPtr friendvm, int initialstacksize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetErrorHandlerCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CloseCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetForeignPtrCallback(IntPtr v, IntPtr p);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr GetForeignPtrCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetPrintFuncCallback(IntPtr v, SqPrintFunction printFunc, SqPrintFunction printFunc2);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqPrintFunction GetPrintFuncCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SuspendVmCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult WakeupVmCallback(IntPtr v, bool resumedret, bool retval, bool raiseerror, bool throwerror);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate VmState GetVmStateCallback(IntPtr v);

    #endregion

    #region compiler

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult CompileCallback(IntPtr v, SqLexReadFunc read, IntPtr p, IntPtr sourcename, bool raiseerror);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult CompileBufferCallback(IntPtr v, IntPtr s, int size, IntPtr sourcename, bool raiseerror);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EnableDebugInfoCallback(IntPtr v, bool enable);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NotifyAllExceptionsCallback(IntPtr v, bool enable);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetCompilerErrorHandlerCallback(IntPtr v, SqCompilerError f);

    #endregion

    #region stack operations

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PopCallback(IntPtr v, int nelemstopop);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PopTopCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RemoveCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetTopCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetTopCallback(IntPtr v, int newtop);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ReserveStackCallback(IntPtr v, int nsize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CmpCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void MoveCallback(IntPtr dest, IntPtr src, int idx);

    #endregion

    #region  object creation handling

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr NewUserdataCallback(IntPtr v, uint size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NewTableCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NewArrayCallback(IntPtr v, int size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void NewClosureCallback(IntPtr v, SqFunction func, uint nfreevars);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetParamsCheckCallback(IntPtr v, int nparamscheck, IntPtr typemask);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult BindEnvCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushStringCallback(IntPtr v, IntPtr s, int len);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushFloatCallback(IntPtr v, float f);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushIntegerCallback(IntPtr v, int n);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushBoolCallback(IntPtr v, bool b);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushUserPointerCallback(IntPtr v, IntPtr p);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushNullCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqObjectType GetTypeCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetSizeCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetBaseCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool InstanceOfCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ToStringCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ToBoolCallback(IntPtr v, int idx, out bool b);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetStringCallback(IntPtr v, int idx, out string c);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetIntegerCallback(IntPtr v, int idx, out int i);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetFloatCallback(IntPtr v, int idx, out float f);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetBoolCallback(IntPtr v, int idx, out bool b);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetThreadCallback(IntPtr v, int idx, out IntPtr thread);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetUserPointerCallback(IntPtr v, int idx, out IntPtr p);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetUserDataCallback(IntPtr v, int idx, out IntPtr p, out IntPtr typetag);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetTypeTagCallback(IntPtr v, int idx, IntPtr typetag);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetTypeTagCallback(IntPtr v, int idx, out IntPtr typetag);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetReleaseHookCallback(IntPtr v, int idx, SqReleaseHook hook);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr GetScratchPadCallback(IntPtr v, int minsize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetClosureInfoCallback(IntPtr v, int idx, out uint nparams, out uint nfreevars);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetNativeClosureNameCallback(IntPtr v, int idx, IntPtr name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetInstanceUpCallback(IntPtr v, int idx, IntPtr p);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetInstanceUpCallback(IntPtr v, int idx, out IntPtr p, IntPtr typetag);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetClassUdSizeCallback(IntPtr v, int idx, int udsize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult NewClassCallback(IntPtr v, bool hasbase);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult CreateInstanceCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetAttributesCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetAttributesCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetClassCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void WeakRefCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetDefaultDelegateCallback(IntPtr v, SqObjectType t);

    #endregion

    #region  object manipulation

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushRootTableCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushRegistryTableCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushConstTableCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetRootTableCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetConstTableCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult NewSlotCallback(IntPtr v, int idx, bool bstatic);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult DeleteSlotCallback(IntPtr v, int idx, bool pushval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult RawGetCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult RawSetCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult RawDeleteSlotCallback(IntPtr v, int idx, bool pushval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ArrayAppendCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ArrayPopCallback(IntPtr v, int idx, bool pushval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ArrayResizeCallback(IntPtr v, int idx, int newsize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ArrayReverseCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ArrayRemoveCallback(IntPtr v, int idx, int itemidx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ArrayInsertCallback(IntPtr v, int idx, int destpos);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetDelegateCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetDelegateCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult CloneCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetFreeVariableCallback(IntPtr v, int idx, uint nval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult NextCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetWeakRefValCallback(IntPtr v, int idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ClearCallback(IntPtr v, int idx);

    #endregion

    #region calls

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult CallCallback(IntPtr v, int paramCount, bool retval, bool raiseerror);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ResumeCallback(IntPtr v, bool retval, bool raiseerror);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate string GetLocalCallback(IntPtr v, uint level, uint idx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate string GetFreeVariableCallback(IntPtr v, int idx, uint nval);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SqErrorCallback(IntPtr v, params string[] format);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ThrowErrorCallback(IntPtr v, string err);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ResetErrorCallback(IntPtr v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void GetLastErrorCallback(IntPtr v);

    #endregion

    #region  raw object handling

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetStackObjCallback(IntPtr v, int idx, ref SqObject po);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void PushObjectCallback(IntPtr v, SqObject obj);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void AddRefCallback(IntPtr v, ref SqObject po);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool ReleaseCallback(IntPtr v, ref SqObject po);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ResetObjectCallback(ref SqObject po);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate string ObjToStringCallback(ref SqObject o);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool ObjToBoolCallback(ref SqObject o);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int ObjToIntegerCallback(ref SqObject o);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate float ObjToFloatCallback(ref SqObject o);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetObjTypeTagCallback(ref SqObject o, out IntPtr typetag);

    #endregion

    #region GC

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CollectGarbageCallback(IntPtr v);

    #endregion

    #region  serialization

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult WriteClosureCallback(IntPtr vm, SqWriteFunc writef, IntPtr up);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult ReadClosureCallback(IntPtr vm, SqReadFunc readf, IntPtr up);

    #endregion

    #region  mem allocation

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr MallocCallback(uint size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ReallocCallback(IntPtr p, uint oldsize, uint newsize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeCallback(IntPtr p, uint size);

    #endregion

    #region debug

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult StackInfosCallback(IntPtr v, int level, out SqStackInfos si);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SetDebugHookCallback(IntPtr v);

    #endregion

    #region stdlib

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetBlobCallback(IntPtr v, int idx, out IntPtr ptr);

    #endregion

    /// <summary>
    ///     Encapsulates all information about the squirrel api.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Squirrel
    {
        public readonly AddRefCallback AddRef;

        public readonly ArrayAppendCallback ArrayAppend;

        public readonly ArrayInsertCallback ArrayInsert;

        public readonly ArrayPopCallback ArrayPop;

        public readonly ArrayRemoveCallback ArrayRemove;

        public readonly ArrayResizeCallback ArrayResize;

        public readonly ArrayReverseCallback ArrayReverse;

        public readonly BindEnvCallback BindEnv;

        public readonly CallCallback Call;

        public readonly ClearCallback Clear;

        public readonly CloneCallback Clone;

        public readonly CloseCallback Close;

        public readonly CmpCallback Cmp;

        public readonly CollectGarbageCallback CollectGarbage;

        public readonly CompileCallback Compile;

        public readonly CompileBufferCallback CompileBuffer;

        public readonly CreateInstanceCallback CreateInstance;

        public readonly DeleteSlotCallback DeleteSlot;

        public readonly EnableDebugInfoCallback EnableDebugInfo;

        public readonly FreeCallback Free;

        public readonly GetCallback Get;

        public readonly GetAttributesCallback GetAttributes;

        public readonly GetBaseCallback GetBase;

        public readonly GetBlobCallback GetBlob;

        public readonly GetBoolCallback GetBool;

        public readonly GetClassCallback GetClass;

        public readonly GetClosureInfoCallback GetClosureInfo;

        public readonly GetDefaultDelegateCallback GetDefaultDelegate;

        public readonly GetDelegateCallback GetDelegate;

        public readonly GetFloatCallback GetFloat;

        public readonly GetForeignPtrCallback GetForeignPtr;

        public readonly GetFreeVariableCallback GetFreeVariable;

        public readonly GetInstanceUpCallback GetInstanceUp;

        public readonly GetIntegerCallback GetInteger;

        public readonly GetLastErrorCallback GetLastError;

        public readonly GetLocalCallback GetLocal;

        public readonly GetObjTypeTagCallback GetObjTypeTag;

        public readonly GetPrintFuncCallback GetPrintFunc;

        public readonly GetScratchPadCallback GetScratchPad;

        public readonly GetSizeCallback GetSize;

        public readonly GetStackObjCallback GetStackObj;

        public readonly GetStringCallback GetString;

        public readonly GetThreadCallback GetThread;

        public readonly GetTopCallback GetTop;

        public readonly GetTypeCallback GetType;

        public readonly GetTypeTagCallback GetTypeTag;

        public readonly GetUserDataCallback GetUserData;

        public readonly GetUserPointerCallback GetUserPointer;

        public readonly GetVmStateCallback GetVmState;

        public readonly GetWeakRefValCallback GetWeakRefVal;

        public readonly InstanceOfCallback InstanceOf;

        public readonly MallocCallback Malloc;

        public readonly MoveCallback Move;

        public readonly NewArrayCallback NewArray;

        public readonly NewClassCallback NewClass;

        public readonly NewClosureCallback NewClosure;

        public readonly NewSlotCallback NewSlot;

        public readonly NewTableCallback NewTable;

        public readonly NewThreadCallback NewThread;

        public readonly NewUserdataCallback NewUserdata;

        public readonly NextCallback Next;

        public readonly NotifyAllExceptionsCallback NotifyAllExceptions;

        public readonly ObjToBoolCallback ObjToBool;

        public readonly ObjToFloatCallback ObjToFloat;

        public readonly ObjToIntegerCallback ObjToInteger;

        public readonly ObjToStringCallback ObjToString;

        public readonly OpenCallback Open;

        public readonly PopCallback Pop;

        public readonly PopTopCallback PopTop;

        public readonly PushCallback Push;

        public readonly PushBoolCallback PushBool;

        public readonly PushConstTableCallback PushConstTable;

        public readonly PushFloatCallback PushFloat;

        public readonly PushIntegerCallback PushInteger;

        public readonly PushNullCallback PushNull;

        public readonly PushObjectCallback PushObject;

        public readonly PushRegistryTableCallback PushRegistryTable;

        public readonly PushRootTableCallback PushRootTable;

        public readonly PushStringCallback PushString;

        public readonly PushUserPointerCallback PushUserPointer;

        public readonly RawDeleteSlotCallback RawDeleteSlot;

        public readonly RawGetCallback RawGet;

        public readonly RawSetCallback RawSet;

        public readonly ReadClosureCallback ReadClosure;

        public readonly ReallocCallback Realloc;

        public readonly ReleaseCallback Release;

        public readonly RemoveCallback Remove;

        public readonly ReserveStackCallback ReserveStack;

        public readonly ResetErrorCallback ResetError;

        public readonly ResetObjectCallback ResetObject;

        public readonly ResumeCallback Resume;

        public readonly SetCallback Set;

        public readonly SetAttributesCallback SetAttributes;

        public readonly SetClassUdSizeCallback SetClassUdSize;

        public readonly SetCompilerErrorHandlerCallback SetCompilerErrorHandler;

        public readonly SetConstTableCallback SetConstTable;

        public readonly SetDebugHookCallback SetDebugHook;

        public readonly SetDelegateCallback SetDelegate;

        public readonly SetErrorHandlerCallback SetErrorHandler;

        public readonly SetForeignPtrCallback SetForeignPtr;

        public readonly SetFreeVariableCallback SetFreeVariable;

        public readonly SetInstanceUpCallback SetInstanceUp;

        public readonly SetNativeClosureNameCallback SetNativeClosureName;

        public readonly SetParamsCheckCallback SetParamsCheck;

        public readonly SetPrintFuncCallback SetPrintFunc;

        public readonly SetReleaseHookCallback SetReleaseHook;

        public readonly SetRootTableCallback SetRootTable;

        public readonly SetTopCallback SetTop;

        public readonly SetTypeTagCallback SetTypeTag;

        public readonly SqErrorCallback SqError;

        public readonly StackInfosCallback StackInfos;

        public readonly SuspendVmCallback SuspendVm;

        public readonly ThrowErrorCallback ThrowError;

        public readonly ToBoolCallback ToBool;

        public readonly ToStringCallback ToString;

        public readonly int Version;

        public readonly WakeupVmCallback WakeupVm;

        public readonly WeakRefCallback WeakRef;

        public readonly WriteClosureCallback WriteClosure;

        /// <summary>
        ///     Private constructor.
        /// </summary>
        internal Squirrel()
        {
        }

        // Struct fields
    }
}