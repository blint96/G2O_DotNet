// ReSharper disable UnassignedReadonlyField

namespace GothicOnline.G2.DotNet.Loader.Squirrel
{
    using DotNet.Squirrel.Squirrel;
    using System;
    using System.Runtime.InteropServices;

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
    public delegate SqResult GetThreadCallback(IntPtr v, int idx,out  IntPtr thread);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetUserPointerCallback(IntPtr v, int idx,out IntPtr p);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetUserDataCallback(IntPtr v, int idx,out IntPtr p,out IntPtr typetag);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult SetTypeTagCallback(IntPtr v, int idx, IntPtr typetag);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SqResult GetTypeTagCallback(IntPtr v, int idx,out IntPtr typetag);
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
    public delegate SqResult GetInstanceUpCallback(IntPtr v, int idx,out IntPtr p, IntPtr typetag);
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
    /// Encapsulates all information about the squirrel api.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Squirrel
    {
        /// <summary>
        /// Private constructor.
        /// </summary>
        internal Squirrel() { }

        //Struct fields
        #region vm
        public readonly int Version;
        public readonly OpenCallback Open;
        public readonly NewThreadCallback NewThread;
        public readonly SetErrorHandlerCallback SetErrorHandler;
        public readonly CloseCallback Close;
        public readonly SetForeignPtrCallback SetForeignPtr;
        public readonly GetForeignPtrCallback GetForeignPtr;
        public readonly SetPrintFuncCallback SetPrintFunc;
        public readonly GetPrintFuncCallback GetPrintFunc;
        public readonly SuspendVmCallback SuspendVm;
        public readonly WakeupVmCallback WakeupVm;
        public readonly GetVmStateCallback GetVmState;
        #endregion

        #region compiler
        public readonly CompileCallback Compile;
        public readonly CompileBufferCallback CompileBuffer;
        public readonly EnableDebugInfoCallback EnableDebugInfo;
        public readonly NotifyAllExceptionsCallback NotifyAllExceptions;
        public readonly SetCompilerErrorHandlerCallback SetCompilerErrorHandler;
        #endregion

        #region  stack operations
        public readonly PushCallback Push;
        public readonly PopCallback Pop;
        public readonly PopTopCallback PopTop;
        public readonly RemoveCallback Remove;
        public readonly GetTopCallback GetTop;
        public readonly SetTopCallback SetTop;
        public readonly ReserveStackCallback ReserveStack;
        public readonly CmpCallback Cmp;
        public readonly MoveCallback Move;
        #endregion

        #region  object creation handling
        public readonly NewUserdataCallback NewUserdata;
        public readonly NewTableCallback NewTable;
        public readonly NewArrayCallback NewArray;
        public readonly NewClosureCallback NewClosure;
        public readonly SetParamsCheckCallback SetParamsCheck;
        public readonly BindEnvCallback BindEnv;
        public readonly PushStringCallback PushString;
        public readonly PushFloatCallback PushFloat;
        public readonly PushIntegerCallback PushInteger;
        public readonly PushBoolCallback PushBool;
        public readonly PushUserPointerCallback PushUserPointer;
        public readonly PushNullCallback PushNull;
        public readonly GetTypeCallback GetType;
        public readonly GetSizeCallback GetSize;
        public readonly GetBaseCallback GetBase;
        public readonly InstanceOfCallback InstanceOf;
        public readonly ToStringCallback ToString;
        public readonly ToBoolCallback ToBool;
        public readonly GetStringCallback GetString;
        public readonly GetIntegerCallback GetInteger;
        public readonly GetFloatCallback GetFloat;
        public readonly GetBoolCallback GetBool;
        public readonly GetThreadCallback GetThread;
        public readonly GetUserPointerCallback GetUserPointer;
        public readonly GetUserDataCallback GetUserData;
        public readonly SetTypeTagCallback SetTypeTag;
        public readonly GetTypeTagCallback GetTypeTag;
        public readonly SetReleaseHookCallback SetReleaseHook;
        public readonly GetScratchPadCallback GetScratchPad;
        public readonly GetClosureInfoCallback GetClosureInfo;
        public readonly SetNativeClosureNameCallback SetNativeClosureName;
        public readonly SetInstanceUpCallback SetInstanceUp;
        public readonly GetInstanceUpCallback GetInstanceUp;
        public readonly SetClassUdSizeCallback SetClassUdSize;
        public readonly NewClassCallback NewClass;
        public readonly CreateInstanceCallback CreateInstance;
        public readonly SetAttributesCallback SetAttributes;
        public readonly GetAttributesCallback GetAttributes;
        public readonly GetClassCallback GetClass;
        public readonly WeakRefCallback WeakRef;
        public readonly GetDefaultDelegateCallback GetDefaultDelegate;
        #endregion

        #region  object manipulation
        public readonly PushRootTableCallback PushRootTable;
        public readonly PushRegistryTableCallback PushRegistryTable;
        public readonly PushConstTableCallback PushConstTable;
        public readonly SetRootTableCallback SetRootTable;
        public readonly SetConstTableCallback SetConstTable;
        public readonly NewSlotCallback NewSlot;
        public readonly DeleteSlotCallback DeleteSlot;
        public readonly SetCallback Set;
        public readonly GetCallback Get;
        public readonly RawGetCallback RawGet;
        public readonly RawSetCallback RawSet;
        public readonly RawDeleteSlotCallback RawDeleteSlot;
        public readonly ArrayAppendCallback ArrayAppend;
        public readonly ArrayPopCallback ArrayPop;
        public readonly ArrayResizeCallback ArrayResize;
        public readonly ArrayReverseCallback ArrayReverse;
        public readonly ArrayRemoveCallback ArrayRemove;
        public readonly ArrayInsertCallback ArrayInsert;
        public readonly SetDelegateCallback SetDelegate;
        public readonly GetDelegateCallback GetDelegate;
        public readonly CloneCallback Clone;
        public readonly SetFreeVariableCallback SetFreeVariable;
        public readonly NextCallback Next;
        public readonly GetWeakRefValCallback GetWeakRefVal;
        public readonly ClearCallback Clear;
        #endregion

        #region calls
        public readonly CallCallback Call;
        public readonly ResumeCallback Resume;
        public readonly GetLocalCallback GetLocal;
        public readonly GetFreeVariableCallback GetFreeVariable;
        public readonly SqErrorCallback SqError;
        public readonly ThrowErrorCallback ThrowError;
        public readonly ResetErrorCallback ResetError;
        public readonly GetLastErrorCallback GetLastError;
        #endregion

        #region  raw object handling
        public readonly GetStackObjCallback GetStackObj;
        public readonly PushObjectCallback PushObject;
        public readonly AddRefCallback AddRef;
        public readonly ReleaseCallback Release;
        public readonly ResetObjectCallback ResetObject;
        public readonly ObjToStringCallback ObjToString;
        public readonly ObjToBoolCallback ObjToBool;
        public readonly ObjToIntegerCallback ObjToInteger;
        public readonly ObjToFloatCallback ObjToFloat;
        public readonly GetObjTypeTagCallback GetObjTypeTag;
        #endregion

        #region GC
        public readonly CollectGarbageCallback CollectGarbage;
        #endregion

        #region  serialization
        public readonly WriteClosureCallback WriteClosure;
        public readonly ReadClosureCallback ReadClosure;
        #endregion

        #region  mem allocation
        public readonly MallocCallback Malloc;
        public readonly ReallocCallback Realloc;
        public readonly FreeCallback Free;
        #endregion

        #region debug
        public readonly StackInfosCallback StackInfos;
        public readonly SetDebugHookCallback SetDebugHook;
        #endregion

        #region stdlib
        public readonly GetBlobCallback GetBlob;
        #endregion
    }
}
