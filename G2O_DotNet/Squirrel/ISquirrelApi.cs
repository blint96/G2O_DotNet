using System;

namespace GothicOnline.G2.DotNet.Squirrel
{
    /// <summary>
    /// Provides all functions the squirrel moduel api.
    /// <remarks>Each method has at leat two overloads. One with the vm parameter an one without. 
    /// If the vm is not provided explicitely, the default one is used.</remarks>
    /// </summary>
    public interface ISquirrelApi
    {
        #region properties
        /// <summary>
        /// Gets a pointer to the default squirrel vm.
        /// </summary>
        IntPtr Vm { get; }

        #endregion

        #region vm

        /// <summary>
        /// Creates a new instance of a squirrel VM that consists in a new execution stack.
        /// <remarks>The returned VM has to be released with ReleaseVM.</remarks>
        /// </summary>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>An handle to a squirrel vm.</returns>
        IntPtr Open(int initialStackSize);

        /// <summary>
        /// Creates a new vm friendvm of the one passed as first parmeter and pushes it in its stack as "thread" object.
        /// </summary>
        /// <param name="friendVm">A friend VM.</param>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>A pointer to the new VM. </returns>
        IntPtr NewThread(IntPtr friendVm, int initialStackSize);

        /// <summary>
        /// Pops from the stack a closure or native closure and sets it as runtime-error handler.
        /// <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        void SetErrorHandler();

        /// <summary>
        /// Pops from the stack a closure or native closure and sets it as runtime-error handler.
        /// <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        void SetErrorHandler(IntPtr vm);

        /// <summary>
        /// Releases a squirrel VM and all related friend VMs.
        /// </summary>
        void Close();

        /// <summary>
        /// Releases a squirrel VM and all related friend VMs.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        void Close(IntPtr vm);

        /// <summary>
        /// Sets the foreign pointer of a certain VM instance.<para/>
        /// The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).<para/>
        ///  This pointer is ignored by the VM.
        /// </summary>
        /// <param name="userPointer">The pointer that has to be set.</param>
        void SetForeignPtr(IntPtr userPointer);

        /// <summary>
        /// Sets the foreign pointer of a certain VM instance.<para/>
        /// The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).<para/>
        ///  This pointer is ignored by the VM.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="userPointer">The pointer that has to be set.</param>
        void SetForeignPtr(IntPtr vm, IntPtr userPointer);

        /// <summary>
        /// Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <returns>The current VMs foreign pointer. </returns>
        IntPtr GetForeignPtr();

        /// <summary>
        /// Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The current VMs foreign pointer. </returns>
        IntPtr GetForeignPtr(IntPtr vm);

        /// <summary>
        /// Sets the print function of the virtual machine.<para/>
        /// This function is used by the built-in function '::print()' to output text.
        /// <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        void SetPrintFunc(SqPrintFunction printFunc, SqPrintFunction errorPrintFunc);

        /// <summary>
        /// Sets the print function of the virtual machine.<para/>
        /// This function is used by the built-in function '::print()' to output text.
        /// <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        void SetPrintFunc(IntPtr vm, SqPrintFunction printFunc, SqPrintFunction errorPrintFunc);

        /// <summary>
        /// Suspends the execution of the specified vm. <para/>
        /// <remarks>sq_result can only be called as return expression of a C function. <para/>
        /// The function will fail is the suspension is done through more C calls or in a metamethod.</remarks>
        /// </summary>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SuspendVm();

        /// <summary>
        /// Suspends the execution of the specified vm. <para/>
        /// <remarks>sq_result can only be called as return expression of a C function. <para/>
        /// The function will fail is the suspension is done through more C calls or in a metamethod.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SuspendVm(IntPtr vm);

        /// <summary>
        /// Wake up the execution a previously suspended virtual machine.
        /// </summary>
        /// <param name="resumedRet">If true the function will pop a value from the stack and use it as return value for the function that has previously suspended the virtual machine.</param>
        /// <param name="retVal">If true the function will push the return value of the function that suspend the excution or the main function one.</param>
        /// <param name="raiseError">If true, if a runtime error occurs during the execution of the call, the vm will invoke the error handler.</param>
        /// <param name="throwError">If true, the vm will thow an exception as soon as is resumed. the exception payload must be set beforehand invoking sq_thowerror().</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool WakeUpVm(bool resumedRet, bool retVal, bool raiseError, bool throwError);

        /// <summary>
        /// Wake up the execution a previously suspended virtual machine.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="resumedRet">If true the function will pop a value from the stack and use it as return value for the function that has previously suspended the virtual machine.</param>
        /// <param name="retVal">If true the function will push the return value of the function that suspend the excution or the main function one.</param>
        /// <param name="raiseError">If true, if a runtime error occurs during the execution of the call, the vm will invoke the error handler.</param>
        /// <param name="throwError">If true, the vm will thow an exception as soon as is resumed. the exception payload must be set beforehand invoking sq_thowerror().</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool WakeUpVm(IntPtr vm, bool resumedRet, bool retVal, bool raiseError, bool throwError);

        /// <summary>
        /// Returns the execution state of a virtual machine.
        /// </summary>
        /// <returns>The state of the vm.</returns>
        VmState GetVmState();

        /// <summary>
        /// Returns the execution state of a virtual machine.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The state of the vm.</returns>
        VmState GetVmState(IntPtr vm);

        #endregion

        #region compiler

        /// <summary>
        /// Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a unmanaged ansi string.</param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool Compile(SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror);

        /// <summary>
        /// Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a unmanaged ansi string.</param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool Compile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror);

        /// <summary>
        /// Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool Compile(SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror);

        /// <summary>
        /// Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool Compile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror);

        /// <summary>
        /// Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="script">A pointer to the buffer that has to be compiled.</param>
        /// <param name="size">Size in characters of the buffer passed in the parameter 'script'.</param>
        /// <param name="sourceName">The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a unmanaged ansi string.</param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool CompileBuffer(IntPtr script, int size, IntPtr sourceName, bool raiseError);

        /// <summary>
        /// Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="script">A pointer to the buffer that has to be compiled.</param>
        /// <param name="size">Size in characters of the buffer passed in the parameter 'script'.</param>
        /// <param name="sourceName">The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a unmanaged ansi string.</param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool CompileBuffer(IntPtr vm, IntPtr script, int size, IntPtr sourceName, bool raiseError);

        /// <summary>
        /// Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="script">The text of the script that has to be compiled.</param>
        /// <param name="sourceName">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool CompileBuffer(string script, string sourceName, bool raiseError);

        /// <summary>
        /// Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the stack.<para/>
        /// <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="script">The text of the script that has to be compiled.</param>
        /// <param name="sourceName">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool CompileBuffer(IntPtr vm, string script, string sourceName, bool raiseError);

        /// <summary>
        /// Enable/disable the debug line information generation at compile time.<para/>
        /// <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        void EnableDebugInfo(bool enable);

        /// <summary>
        /// Enable/disable the debug line information generation at compile time.<para/>
        /// <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        void EnableDebugInfo(IntPtr vm, bool enable);

        /// <summary>
        /// Enable/disable the error callback notification of handled exceptions.<para/>
        /// <remarks>By default the VM will invoke the error callback only if an exception is not handled (no try/catch traps are present in the call stack).<para/>
        ///  If notifyallexceptions is enabled, the VM will call the error callback for any exception even if between try/catch blocks.<para/>
        ///  This feature is useful for implementing debuggers.</remarks>
        /// </summary>
        /// <param name="enable">If true enables the error callback notification of handled exceptions.</param>
        void NotifyAllExceptions(bool enable);

        /// <summary>
        /// Enable/disable the error callback notification of handled exceptions.<para/>
        /// <remarks>By default the VM will invoke the error callback only if an exception is not handled (no try/catch traps are present in the call stack).<para/>
        ///  If notifyallexceptions is enabled, the VM will call the error callback for any exception even if between try/catch blocks.<para/>
        ///  This feature is useful for implementing debuggers.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="enable">If true enables the error callback notification of handled exceptions.</param>
        void NotifyAllExceptions(IntPtr vm, bool enable);

        /// <summary>
        /// Sets the compiler error handler function.<para/>
        /// <remarks>if the parameter errorHandler is NULL no function will be called when a compiler error occurs.<para/>
        /// The compiler error handler is shared between friend VMs.</remarks>
        /// </summary>
        /// <param name="errorHandler">Delegate of the error handler function.</param>
        void SetCompilerErrorHandler(SqCompilerError errorHandler);

        /// <summary>
        /// Sets the compiler error handler function.<para/>
        /// <remarks>if the parameter errorHandler is NULL no function will be called when a compiler error occurs.<para/>
        /// The compiler error handler is shared between friend VMs.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="errorHandler">Delegate of the error handler function.</param>
        void SetCompilerErrorHandler(IntPtr vm, SqCompilerError errorHandler);

        #endregion

        #region  stack operations

        /// <summary>
        /// Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        void Push(int idx);

        /// <summary>
        /// Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        void Push(IntPtr vm, int idx);

        /// <summary>
        /// Pops n elements from the stack.
        /// </summary>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        void Pop(int elementsToPop);

        /// <summary>
        /// Pops n elements from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        void Pop(IntPtr vm, int elementsToPop);

        /// <summary>
        /// Pops 1 object from the stack.
        /// </summary>
        void PopTop();

        /// <summary>
        /// Pops 1 object from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        void PopTop(IntPtr vm);

        /// <summary>
        /// Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="idx">Index of the element that has to be removed.</param>
        void Remove(int idx);

        /// <summary>
        /// Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">Index of the element that has to be removed.</param>
        void Remove(IntPtr vm, int idx);

        /// <summary>
        /// Returns the index of the top of the stack.
        /// </summary>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        int GetTop();

        /// <summary>
        /// Returns the index of the top of the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        int GetTop(IntPtr vm);

        /// <summary>
        /// Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="newTop">The new top index.</param>
        void SetTop(int newTop);

        /// <summary>
        /// Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="newTop">The new top index.</param>
        void SetTop(IntPtr vm, int newTop);

        /// <summary>
        /// Ensure that the stack space left is at least of a specified size.<para/>
        /// If the stack is smaller it will automatically grow.<para/>
        /// If there's a memtamethod currently running the function will fail and the stack will not be resized, this situatuation has to be considered a "stack overflow".
        /// </summary>
        /// <param name="nSize">Required stack size.</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool ReserveStack(int nSize);

        /// <summary>
        /// Ensure that the stack space left is at least of a specified size.<para/>
        /// If the stack is smaller it will automatically grow.<para/>
        /// If there's a memtamethod currently running the function will fail and the stack will not be resized, this situatuation has to be considered a "stack overflow".
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="nSize">Required stack size.</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool ReserveStack(IntPtr vm, int nSize);

        /// <summary>
        /// Takes 2 object from the stack and compares them.
        /// </summary>
        /// <returns>Result of the comparison as a integer.</returns>
        int Cmp();

        /// <summary>
        /// Takes 2 object from the stack and compares them.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>Result of the comparison as a integer.</returns>
        int Cmp(IntPtr vm);

        /// <summary>
        /// Pushes the object at the position 'idx' of the source vm stack in the destination vm stack.
        /// </summary>
        /// <param name="destVm">Pointer to the destination VM.</param>
        /// <param name="srcVm">Pointer to the source VM.</param>
        /// <param name="idx">The index in the source stack of the value that has to be moved.</param>
        void Move(IntPtr destVm, IntPtr srcVm, int idx);

        #endregion

        #region object creation handling

        /// <summary>
        /// Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        IntPtr NewUserData(uint size);

        /// <summary>
        /// Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        IntPtr NewUserData(IntPtr vm, uint size);

        /// <summary>
        /// Creates a new table and pushes it in the stack.
        /// </summary>
        void NewTable();

        /// <summary>
        /// Creates a new table and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void NewTable(IntPtr vm);

        /// <summary>
        /// Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the array that as to be created.</param>
        void NewArray(int size);

        /// <summary>
        /// Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the array that as to be created.</param>
        void NewArray(IntPtr vm, int size);

        void NewClosure(SqFunction func, uint nFreeVars);

        void NewClosure(IntPtr vm, SqFunction func, uint nFreeVars);

        bool SetParamsCheck(int nParamsCheck, string typeMask);

        bool SetParamsCheck(IntPtr vm, int nParamsCheck, string typeMask);

        bool BindEnv(int index);

        bool BindEnv(IntPtr vm, int index);

        void PushString(string value);

        void PushString(IntPtr vm, string value);

        void PushString(IntPtr value, int length);

        void PushString(IntPtr vm, IntPtr value, int length);

        void PushFloat(float value);

        void PushFloat(IntPtr vm, float value);

        void PushInteger(int value);

        void PushInteger(IntPtr vm, int value);

        void PushBool(bool value);

        void PushBool(IntPtr vm, bool value);

        void PushUserPointer(IntPtr pointer);

        void PushUserPointer(IntPtr vm, IntPtr pointer);

        void PushNull();

        void PushNull(IntPtr vm);

        SqObjectType GetType(int index);

        SqObjectType GetType(IntPtr vm, int index);

        int GetSize(int index);

        int GetSize(IntPtr vm, int index);

        bool GetBase(int index);

        bool GetBase(IntPtr vm, int index);

        bool InstanceOf();

        bool InstanceOf(IntPtr vm);

        bool ToString(int index);

        bool ToString(IntPtr v, int index);

        bool ToBool(int index, out bool value);

        bool ToBool(IntPtr vm, int index, out bool value);

        bool GetString(int index, out string value);

        bool GetString(IntPtr vm, int index, out string value);

        bool GetInteger(int index, out int value);

        bool GetInteger(IntPtr vm, int index, out int value);

        bool GetFloat(int index, out float value);

        bool GetFloat(IntPtr vm, int index, out float value);

        bool GetBool(int index, out bool value);

        bool GetBool(IntPtr vm, int index, out bool value);

        bool GetThread(int index, out IntPtr threadPtr);

        bool GetThread(IntPtr vm, int index, out IntPtr threadPtr);

        bool GetUserPointer(int index, out IntPtr userPtr);

        bool GetUserPointer(IntPtr vm, int index, out IntPtr userPtr);

        bool GetUserData(int index, out IntPtr data, out int typeTag);

        bool GetUserData(IntPtr vm, int index, out IntPtr data, out int typeTag);

        bool SetTypeTag(int index, IntPtr typeTag);

        bool SetTypeTag(IntPtr vm, int index, IntPtr typeTag);

        bool GetTypeTag(int index, out IntPtr typeTag);

        bool GetTypeTag(IntPtr vm, int index, out IntPtr typeTag);

        void SetReleaseHook(int index, SqReleaseHook hookFunc);

        void SetReleaseHook(IntPtr vm, int index, SqReleaseHook hookFunc);

        string GetScratchPad(int minSize);

        string GetScratchPad(IntPtr vm, int minSize);

        bool GetClosureInfo(int index, out uint nParams, out uint nFreeVars);

        bool GetClosureInfo(IntPtr vm, int index, out uint nParams, out uint nFreeVars);

        bool SetNativeClosureName(int index, string name);

        bool SetNativeClosureName(IntPtr vm, int index, string name);

        bool SetNativeClosureName(int index, IntPtr name);

        bool SetNativeClosureName(IntPtr vm, int index, IntPtr name);

        bool SetInstanceUp(int index, IntPtr pointer);

        bool SetInstanceUp(IntPtr vm, int index, IntPtr pointer);

        bool GetInstanceUp(int index, out IntPtr pointer, IntPtr typeTag);

        bool GetInstanceUp(IntPtr vm, int index, out IntPtr pointer, IntPtr typeTag);

        bool SetClassUdSize(int index, int udSize);

        bool SetClassUdSize(IntPtr vm, int index, int udSize);

        bool NewClass(bool hasBase);

        bool NewClass(IntPtr vm, bool hasBase);

        bool CreateInstance(int index);

        bool CreateInstance(IntPtr vm, int index);

        bool SetAttributes(int index);

        bool SetAttributes(IntPtr vm, int index);

        bool GetAttributes(int index);

        bool GetAttributes(IntPtr vm, int index);

        bool GetClass(int index);

        bool GetClass(IntPtr vm, int index);

        void WeakRef(int index);

        void WeakRef(IntPtr vm, int index);

        bool GetDefaultDelegate(SqObjectType objectType);

        bool GetDefaultDelegate(IntPtr vm, SqObjectType objectType);

        #endregion

        #region Object Manipulation

        /// <summary>
        /// Pushes the current root table in the stack
        /// </summary>
        void PushRootTable();

        /// <summary>
        /// Pushes the current root table in the stack
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void PushRootTable(IntPtr vm);

        /// <summary>
        /// Pushes the registry table in the stack.
        /// </summary>
        void PushRegistryTable();

        /// <summary>
        /// Pushes the registry table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void PushRegistryTable(IntPtr vm);

        /// <summary>
        /// Pushes the current const table in the stack.
        /// </summary>
        void PushConstTable();

        /// <summary>
        /// Pushes the current const table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void PushConstTable(IntPtr vm);

        /// <summary>
        /// Pops a table from the stack and set it as root table.
        /// </summary>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetRootTable();

        /// <summary>
        /// Pops a table from the stack and set it as root table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetRootTable(IntPtr vm);

        /// <summary>
        ///Pops a table from the stack and set it as const table.
        /// </summary>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetConstTable();

        /// <summary>
        ///Pops a table from the stack and set it as const table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetConstTable(IntPtr vm);

        /// <summary>
        /// Pops a key and a value from the stack and performs a set operation
        /// on the table or class that is at position idx in the stack,
        /// if the slot does not exits it will be created.<para/>
        /// <remarks>Invokes the _newslot metamethod in the table delegate. it only works on tables and classes.</remarks>
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="bStatic">If true, creates a static member. This parameter is only used if the target object is a class.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool NewSlot(int index, bool bStatic);

        /// <summary>
        /// Pops a key and a value from the stack and performs a set operation
        /// on the table or class that is at position idx in the stack,
        /// if the slot does not exits it will be created.<para/>
        /// <remarks>Invokes the _newslot metamethod in the table delegate. it only works on tables and classes.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="bStatic">If true, creates a static member. This parameter is only used if the target object is a class.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool NewSlot(IntPtr vm, int index, bool bStatic);

        /// <summary>
        /// Pops a key from the stack and delete the slot indexed by it
        /// from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool DeleteSlot(int index, bool pushVal);

        /// <summary>
        /// Pops a key from the stack and delete the slot indexed by it
        /// from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool DeleteSlot(IntPtr vm, int index, bool pushVal);

        /// <summary>
        /// Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack.
        /// <remarks>This call will invoke the delegation system like a normal assignment, it only works on tables, arrays and userdata.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Set(int index);

        /// <summary>
        /// Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack.
        /// <remarks>This call will invoke the delegation system like a normal assignment, it only works on tables, arrays and userdata.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Set(IntPtr vm, int index);

        /// <summary>
        /// Pops a key from the stack and performs a get operation on the object at the position idx in the stack, and pushes the result in the stack.
        /// <remarks>This call will invokes the delegation system like a normal dereference it only works on tables, arrays and userdata. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Get(int index);

        /// <summary>
        /// Pops a key from the stack and performs a get operation on the object at the position idx in the stack, and pushes the result in the stack.
        /// <remarks>This call will invokes the delegation system like a normal dereference it only works on tables, arrays and userdata. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Get(IntPtr vm, int index);

        /// <summary>
        /// Pops a key from the stack and performs a get operation on the object at position idx in the stack, without employing delegation or metamethods.
        /// <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool RawGet(int index);

        /// <summary>
        /// Pops a key from the stack and performs a get operation on the object at position idx in the stack, without employing delegation or metamethods.
        /// <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool RawGet(IntPtr vm, int index);

        /// <summary>
        /// Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack, without employing delegation or metamethods.
        ///<remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool RawSet(int index);

        /// <summary>
        /// Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack, without employing delegation or metamethods.
        ///<remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target vm.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool RawSet(IntPtr vm, int index);

        /// <summary>
        /// Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool RawDeleteSlot(int index, bool pushVal);

        /// <summary>
        /// Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool RawDeleteSlot(IntPtr vm, int index, bool pushVal);

        /// <summary>
        /// Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayAppend(int index);

        /// <summary>
        /// Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayAppend(IntPtr vm, int index);

        /// <summary>
        /// Pops a value from the back of the array at the position idx in the stack.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayPop(int index, bool pushVal);

        /// <summary>
        /// Pops a value from the back of the array at the position idx in the stack.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayPop(IntPtr vm, int index, bool pushVal);

        /// <summary>
        /// Resizes the array at the position idx in the stack.
        /// <remarks>Only works on arrays.if newsize if greater than the current size the new array slots will be filled with nulls.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="newSize">Requested size of the array.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayResize(int index, int newSize);

        /// <summary>
        /// Resizes the array at the position idx in the stack.
        /// <remarks>Only works on arrays.if newsize if greater than the current size the new array slots will be filled with nulls.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="newSize">Requested size of the array.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayResize(IntPtr vm, int index, int newSize);

        /// <summary>
        /// Reverse an array in place.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayReverse(int index);

        /// <summary>
        /// Reverse an array in place.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayReverse(IntPtr vm, int index);

        /// <summary>
        /// Removes an item from an array.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayRemove(int index, int itemIndex);

        /// <summary>
        /// Removes an item from an array.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayRemove(IntPtr vm, int index, int itemIndex);

        /// <summary>
        /// Pops a value from the stack and inserts it in an array at the specified position.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayInsert(int index, int destPos);

        /// <summary>
        /// Pops a value from the stack and inserts it in an array at the specified position.
        /// <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ArrayInsert(IntPtr vm, int index, int destPos);

        /// <summary>
        /// Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        /// <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetDelegate(int index);

        /// <summary>
        /// Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        /// <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetDelegate(IntPtr vm, int index);

        /// <summary>
        /// Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool GetDelegate(int index);

        /// <summary>
        /// Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool GetDelegate(IntPtr vm, int index);

        /// <summary>
        /// Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Clone(int index);

        /// <summary>
        /// Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Clone(IntPtr vm, int index);

        /// <summary>
        /// Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetFreeVariable(int index, uint nVal);

        /// <summary>
        /// Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool SetFreeVariable(IntPtr vm, int index, uint nVal);

        /// <summary>
        /// Pushes in the stack the next key and value of an array, table or class slot. To start the iteration this function expects a null value on top of the stack; at every call the function will substitute the null value with an iterator and push key and value of the container slot. Every iteration the application has to pop the previous key and value but leave the iterator(that is used as reference point for the next iteration). The function will fail when all slots have been iterated(see Tables and arrays manipulation).
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Next(int index);

        /// <summary>
        /// Pushes in the stack the next key and value of an array, table or class slot. To start the iteration this function expects a null value on top of the stack; at every call the function will substitute the null value with an iterator and push key and value of the container slot. Every iteration the application has to pop the previous key and value but leave the iterator(that is used as reference point for the next iteration). The function will fail when all slots have been iterated(see Tables and arrays manipulation).
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Next(IntPtr vm, int index);

        /// <summary>
        /// Pushes the object pointed by the weak reference at position idx in the stack.
        /// <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool GetWeakRefVal(int index);

        /// <summary>
        /// Pushes the object pointed by the weak reference at position idx in the stack.
        /// <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool GetWeakRefVal(IntPtr vm, int index);

        /// <summary>
        /// Clears all the element of the table/array at position idx in the stack.
        /// <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Clear(int index);

        /// <summary>
        /// Clears all the element of the table/array at position idx in the stack.
        /// <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool Clear(IntPtr vm, int index);

        #endregion

        #region  calls

        bool Call(int parameters, bool retVal, bool raiseError);

        bool Call(IntPtr vm, int parameters, bool retVal, bool raiseError);

        bool Resume(bool reVal, bool raiseError);

        bool Resume(IntPtr vm, bool reVal, bool raiseError);

        string GetLocal(uint level, uint index);

        string GetLocal(IntPtr vm, uint level, uint index);

        string GetFreeVariable(int idx, uint nval);

        string GetFreeVariable(IntPtr vm, int idx, uint nval);

        bool SqError(string errorText);

        bool SqError(IntPtr vm, string errorText);

        bool ThrowError(string errorText);

        bool ThrowError(IntPtr vm, string errorText);

        void ResetError();

        void ResetError(IntPtr vm);

        void GetLastError();

        void GetLastError(IntPtr vm);

        #endregion

        #region  raw object handling

        bool GetStackObj(int index, ref Sqobject obj);

        bool GetStackObj(IntPtr vm, int index, ref Sqobject obj);

        void PushObject(Sqobject obj);

        void PushObject(IntPtr vm, Sqobject obj);

        void AddRef(ref Sqobject obj);

        void AddRef(IntPtr vm, ref Sqobject obj);

        bool Release(ref Sqobject obj);

        bool Release(IntPtr vm, ref Sqobject obj);

        void ResetObject(ref Sqobject obj);

        string ObjToString(ref Sqobject obj);

        bool ObjToBool(ref Sqobject obj);

        int ObjToInteger(ref Sqobject obj);

        float ObjToFloat(ref Sqobject obj);

        bool GetObjectTypeTag(ref Sqobject obj, out IntPtr typeTag);

        #endregion

        #region GC

        /// <summary>
        /// Runs the garbage collector and returns the number of reference cycles found(and deleted).<para/>
        /// <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        int CollectGarbage();

        /// <summary>
        /// Runs the garbage collector and returns the number of reference cycles found(and deleted).<para/>
        /// <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        int CollectGarbage(IntPtr vm);

        #endregion

        #region serialization

        /// <summary>
        /// Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool WriteClosure(SqWriteFunc writeFunction, IntPtr up);

        /// <summary>
        /// Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool WriteClosure(IntPtr vm, SqWriteFunc writeFunction, IntPtr up);

        /// <summary>
        /// Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ReadClosure(SqReadFunc readFunction, IntPtr up);

        /// <summary>
        /// Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        bool ReadClosure(IntPtr vm, SqReadFunc readFunction, IntPtr up);

        #endregion

        #region mem allocation

        /// <summary>
        /// Allocates a memory new block.
        /// </summary>
        /// <param name="size">Size of the new memory block.</param>
        /// <returns>Pointer to the new memory block.</returns>
        IntPtr Malloc(uint size);

        /// <summary>
        /// Reallocates a existing memory block.
        /// </summary>
        /// <param name="pointer">A pointer to a previously allocated memory block.</param>
        /// <param name="oldSize">The old size of the memory block.</param>
        /// <param name="newSize">The new size of the memory block.</param>
        /// <returns>Pointer to the reallocated memory block.</returns>
        IntPtr Realloc(IntPtr pointer, uint oldSize, uint newSize);

        /// <summary>
        /// Frees a previously allocated memory block.
        /// </summary>
        /// <param name="pointer">A Pointer to the previously allocated memory block.</param>
        /// <param name="size">Size of the previously allocated memory block.</param>
        void Free(IntPtr pointer, uint size);

        #endregion

        #region debug

        /// <summary>
        /// Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        bool StackInfo(int level, out SqStackInfos stackInfo);

        /// <summary>
        /// Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        bool StackInfo(IntPtr vm, int level, out SqStackInfos stackInfo);

        /// <summary>
        /// pops a closure from the stack an sets it as debug hook.<para/>
        /// When a debug hook is set it overrides any previously set native or non native hooks.<para/>
        /// If the hook is null the debug hook will be disabled.
        /// </summary>
        void SetDebugHook();

        /// <summary>
        /// pops a closure from the stack an sets it as debug hook.<para/>
        /// When a debug hook is set it overrides any previously set native or non native hooks.<para/>
        /// If the hook is null the debug hook will be disabled.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void SetDebugHook(IntPtr vm);

        #endregion

        #region  stdlib

        /// <summary>
        /// Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        bool GetBlob(int index, out IntPtr pointer);

        /// <summary>
        /// Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        bool GetBlob(IntPtr vm, int index, out IntPtr pointer);

        #endregion
    }
}
