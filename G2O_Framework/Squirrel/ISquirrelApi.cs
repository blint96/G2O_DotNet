// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISquirrelApi.cs" company="Colony Online Project">
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

    /// <summary>
    ///     Provides all functions the squirrel moduel api.
    ///     <remarks>
    ///         Each method has at leat two overloads. One with the vm parameter an one without.
    ///         If the vm is not provided explicitly, the default one is used.
    ///     </remarks>
    /// </summary>
    public interface ISquirrelApi
    {
        /// <summary>
        ///     Gets a pointer to the default squirrel vm.
        /// </summary>
        IntPtr Vm { get; }

        void SqAddRef(ref SqObject obj);

        void SqAddRef(IntPtr vm, ref SqObject obj);

        /// <summary>
        ///     Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayAppend(int index);

        /// <summary>
        ///     Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayAppend(IntPtr vm, int index);

        /// <summary>
        ///     Pops a value from the stack and inserts it in an array at the specified position.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayInsert(int index, int destPos);

        /// <summary>
        ///     Pops a value from the stack and inserts it in an array at the specified position.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayInsert(IntPtr vm, int index, int destPos);

        /// <summary>
        ///     Pops a value from the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayPop(int index, bool pushVal);

        /// <summary>
        ///     Pops a value from the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayPop(IntPtr vm, int index, bool pushVal);

        /// <summary>
        ///     Removes an item from an array.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayRemove(int index, int itemIndex);

        /// <summary>
        ///     Removes an item from an array.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayRemove(IntPtr vm, int index, int itemIndex);

        /// <summary>
        ///     Resizes the array at the position idx in the stack.
        ///     <remarks>
        ///         Only works on arrays.if newsize if greater than the current size the new array slots will be filled with
        ///         nulls.
        ///     </remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="newSize">Requested size of the array.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayResize(int index, int newSize);

        /// <summary>
        ///     Resizes the array at the position idx in the stack.
        ///     <remarks>
        ///         Only works on arrays.if newsize if greater than the current size the new array slots will be filled with
        ///         nulls.
        ///     </remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="newSize">Requested size of the array.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayResize(IntPtr vm, int index, int newSize);

        /// <summary>
        ///     Reverse an array in place.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayReverse(int index);

        /// <summary>
        ///     Reverse an array in place.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqArrayReverse(IntPtr vm, int index);

        bool SqBindEnv(int index);

        bool SqBindEnv(IntPtr vm, int index);

        bool SqCall(int parameters, bool retVal, bool raiseError);

        bool SqCall(IntPtr vm, int parameters, bool retVal, bool raiseError);

        /// <summary>
        ///     Clears all the element of the table/array at position idx in the stack.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqClear(int index);

        /// <summary>
        ///     Clears all the element of the table/array at position idx in the stack.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqClear(IntPtr vm, int index);

        /// <summary>
        ///     Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqClone(int index);

        /// <summary>
        ///     Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqClone(IntPtr vm, int index);

        /// <summary>
        ///     Releases a squirrel VM and all related friend VMs.
        /// </summary>
        void SqClose();

        /// <summary>
        ///     Releases a squirrel VM and all related friend VMs.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        void SqClose(IntPtr vm);

        /// <summary>
        ///     Takes 2 object from the stack and compares them.
        /// </summary>
        /// <returns>Result of the comparison as a integer.</returns>
        int SqCmp();

        /// <summary>
        ///     Takes 2 object from the stack and compares them.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>Result of the comparison as a integer.</returns>
        int SqCmp(IntPtr vm);

        /// <summary>
        ///     Runs the garbage collector and returns the number of reference cycles found(and deleted).
        ///     <para />
        ///     <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        int SqCollectGarbage();

        /// <summary>
        ///     Runs the garbage collector and returns the number of reference cycles found(and deleted).
        ///     <para />
        ///     <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        int SqCollectGarbage(IntPtr vm);

        /// <summary>
        ///     Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">
        ///     The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a
        ///     unmanaged ansi string.
        /// </param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool SqCompile(SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror);

        /// <summary>
        ///     Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">
        ///     The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a
        ///     unmanaged ansi string.
        /// </param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool SqCompile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror);

        /// <summary>
        ///     Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool SqCompile(SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror);

        /// <summary>
        ///     Compiles a squirrel program; if it succeeds, push the compiled script as function in the stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="readFunc">A delegate of a read function that will feed the compiler with the program.</param>
        /// <param name="p">A user defined pointer that will be passed by the compiler to the read function at each invocation.</param>
        /// <param name="sourcename">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseerror">If this value is true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the Compile fails nothing is pushed in the stack).</returns>
        bool SqCompile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror);

        /// <summary>
        ///     Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the
        ///     stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="script">A pointer to the buffer that has to be compiled.</param>
        /// <param name="size">Size in characters of the buffer passed in the parameter 'script'.</param>
        /// <param name="sourceName">
        ///     The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a
        ///     unmanaged ansi string.
        /// </param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool SqCompileBuffer(IntPtr script, int size, IntPtr sourceName, bool raiseError);

        /// <summary>
        ///     Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the
        ///     stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="script">A pointer to the buffer that has to be compiled.</param>
        /// <param name="size">Size in characters of the buffer passed in the parameter 'script'.</param>
        /// <param name="sourceName">
        ///     The symbolic name of the program (used only for more meaningful runtime errors). Pointer to a
        ///     unmanaged ansi string.
        /// </param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool SqCompileBuffer(IntPtr vm, IntPtr script, int size, IntPtr sourceName, bool raiseError);

        /// <summary>
        ///     Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the
        ///     stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="script">The text of the script that has to be compiled.</param>
        /// <param name="sourceName">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool SqCompileBuffer(string script, string sourceName, bool raiseError);

        /// <summary>
        ///     Compiles a squirrel program from a memory buffer; if it succeeds, push the compiled script as function in the
        ///     stack.
        ///     <para />
        ///     <remarks>In case of an error the function will call the function set by sq_setcompilererrorhandler().</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="script">The text of the script that has to be compiled.</param>
        /// <param name="sourceName">The symbolic name of the program (used only for more meaningful runtime errors).</param>
        /// <param name="raiseError">If this value true the compiler error handler will be called in case of an error.</param>
        /// <returns>Returns true if successfully. False if not(If the CompileBuffer fails nothing is pushed in the stack).</returns>
        bool SqCompileBuffer(IntPtr vm, string script, string sourceName, bool raiseError);

        bool SqCreateInstance(int index);

        bool SqCreateInstance(IntPtr vm, int index);

        /// <summary>
        ///     Pops a key from the stack and delete the slot indexed by it
        ///     from the table at position idx in the stack, if the slot does not exits nothing happens.
        ///     <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqDeleteSlot(int index, bool pushVal);

        /// <summary>
        ///     Pops a key from the stack and delete the slot indexed by it
        ///     from the table at position idx in the stack, if the slot does not exits nothing happens.
        ///     <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqDeleteSlot(IntPtr vm, int index, bool pushVal);

        /// <summary>
        ///     Enable/disable the debug line information generation at compile time.
        ///     <para />
        ///     <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        void SqEnableDebugInfo(bool enable);

        /// <summary>
        ///     Enable/disable the debug line information generation at compile time.
        ///     <para />
        ///     <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        void SqEnableDebugInfo(IntPtr vm, bool enable);

        /// <summary>
        ///     Frees a previously allocated memory block.
        /// </summary>
        /// <param name="pointer">A Pointer to the previously allocated memory block.</param>
        /// <param name="size">Size of the previously allocated memory block.</param>
        void SqFree(IntPtr pointer, uint size);

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at the position idx in the stack, and pushes
        ///     the result in the stack.
        ///     <remarks>
        ///         This call will invokes the delegation system like a normal dereference it only works on tables, arrays and
        ///         userdata. if the function fails nothing will be pushed in the stack.
        ///     </remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqGet(int index);

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at the position idx in the stack, and pushes
        ///     the result in the stack.
        ///     <remarks>
        ///         This call will invokes the delegation system like a normal dereference it only works on tables, arrays and
        ///         userdata. if the function fails nothing will be pushed in the stack.
        ///     </remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqGet(IntPtr vm, int index);

        bool SqGetAttributes(int index);

        bool SqGetAttributes(IntPtr vm, int index);

        bool SqGetBase(int index);

        bool SqGetBase(IntPtr vm, int index);

        /// <summary>
        ///     Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        bool SqGetBlob(int index, out IntPtr pointer);

        /// <summary>
        ///     Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        bool SqGetBlob(IntPtr vm, int index, out IntPtr pointer);

        bool SqGetBool(int index, out bool value);

        bool SqGetBool(IntPtr vm, int index, out bool value);

        bool SqGetClass(int index);

        bool SqGetClass(IntPtr vm, int index);

        bool SqGetClosureInfo(int index, out uint nParams, out uint nFreeVars);

        bool SqGetClosureInfo(IntPtr vm, int index, out uint nParams, out uint nFreeVars);

        bool SqGetDefaultDelegate(SqObjectType objectType);

        bool SqGetDefaultDelegate(IntPtr vm, SqObjectType objectType);

        /// <summary>
        ///     Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqGetDelegate(int index);

        /// <summary>
        ///     Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqGetDelegate(IntPtr vm, int index);

        bool SqGetFloat(int index, out float value);

        bool SqGetFloat(IntPtr vm, int index, out float value);

        /// <summary>
        ///     Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <returns>The current VMs foreign pointer. </returns>
        IntPtr SqGetForeignPtr();

        /// <summary>
        ///     Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The current VMs foreign pointer. </returns>
        IntPtr SqGetForeignPtr(IntPtr vm);

        string SqGetFreeVariable(int idx, uint nval);

        string SqGetFreeVariable(IntPtr vm, int idx, uint nval);

        bool SqGetInstanceUp(int index, out IntPtr pointer, IntPtr typeTag);

        bool SqGetInstanceUp(IntPtr vm, int index, out IntPtr pointer, IntPtr typeTag);

        bool SqGetInteger(int index, out int value);

        bool SqGetInteger(IntPtr vm, int index, out int value);

        void SqGetLastError();

        void SqGetLastError(IntPtr vm);

        string SqGetLocal(uint level, uint index);

        string SqGetLocal(IntPtr vm, uint level, uint index);

        bool SqGetObjectTypeTag(ref SqObject obj, out IntPtr typeTag);

        string SqGetScratchPad(int minSize);

        string SqGetScratchPad(IntPtr vm, int minSize);

        int SqGetSize(int index);

        int SqGetSize(IntPtr vm, int index);

        bool SqGetStackObj(int index, ref SqObject obj);

        bool SqGetStackObj(IntPtr vm, int index, ref SqObject obj);

        bool SqGetString(int index, out string value);

        bool SqGetString(IntPtr vm, int index, out string value);

        bool SqGetThread(int index, out IntPtr threadPtr);

        bool SqGetThread(IntPtr vm, int index, out IntPtr threadPtr);

        /// <summary>
        ///     Returns the index of the top of the stack.
        /// </summary>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        int SqGetTop();

        /// <summary>
        ///     Returns the index of the top of the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        int SqGetTop(IntPtr vm);

        SqObjectType SqGetType(int index);

        SqObjectType SqGetType(IntPtr vm, int index);

        bool SqGetTypeTag(int index, out IntPtr typeTag);

        bool SqGetTypeTag(IntPtr vm, int index, out IntPtr typeTag);

        bool SqGetUserData(int index, out IntPtr data, out int typeTag);

        bool SqGetUserData(IntPtr vm, int index, out IntPtr data, out int typeTag);

        bool SqGetUserPointer(int index, out IntPtr userPtr);

        bool SqGetUserPointer(IntPtr vm, int index, out IntPtr userPtr);

        /// <summary>
        ///     Returns the execution state of a virtual machine.
        /// </summary>
        /// <returns>The state of the vm.</returns>
        VmState SqGetVmState();

        /// <summary>
        ///     Returns the execution state of a virtual machine.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The state of the vm.</returns>
        VmState SqGetVmState(IntPtr vm);

        /// <summary>
        ///     Pushes the object pointed by the weak reference at position idx in the stack.
        ///     <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqGetWeakRefVal(int index);

        /// <summary>
        ///     Pushes the object pointed by the weak reference at position idx in the stack.
        ///     <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqGetWeakRefVal(IntPtr vm, int index);

        bool SqInstanceOf();

        bool SqInstanceOf(IntPtr vm);

        /// <summary>
        ///     Allocates a memory new block.
        /// </summary>
        /// <param name="size">Size of the new memory block.</param>
        /// <returns>Pointer to the new memory block.</returns>
        IntPtr SqMalloc(uint size);

        /// <summary>
        ///     Pushes the object at the position 'idx' of the source vm stack in the destination vm stack.
        /// </summary>
        /// <param name="destVm">Pointer to the destination VM.</param>
        /// <param name="srcVm">Pointer to the source VM.</param>
        /// <param name="idx">The index in the source stack of the value that has to be moved.</param>
        void SqMove(IntPtr destVm, IntPtr srcVm, int idx);

        /// <summary>
        ///     Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the array that as to be created.</param>
        void SqNewArray(int size);

        /// <summary>
        ///     Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the array that as to be created.</param>
        void SqNewArray(IntPtr vm, int size);

        bool SqNewClass(bool hasBase);

        bool SqNewClass(IntPtr vm, bool hasBase);

        void SqNewClosure(SqFunction func, uint nFreeVars);

        void SqNewClosure(IntPtr vm, SqFunction func, uint nFreeVars);

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation
        ///     on the table or class that is at position idx in the stack,
        ///     if the slot does not exits it will be created.
        ///     <para />
        ///     <remarks>Invokes the _newslot metamethod in the table delegate. it only works on tables and classes.</remarks>
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="bStatic">If true, creates a static member. This parameter is only used if the target object is a class.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqNewSlot(int index, bool bStatic);

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation
        ///     on the table or class that is at position idx in the stack,
        ///     if the slot does not exits it will be created.
        ///     <para />
        ///     <remarks>Invokes the _newslot metamethod in the table delegate. it only works on tables and classes.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="bStatic">If true, creates a static member. This parameter is only used if the target object is a class.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqNewSlot(IntPtr vm, int index, bool bStatic);

        /// <summary>
        ///     Creates a new table and pushes it in the stack.
        /// </summary>
        void SqNewTable();

        /// <summary>
        ///     Creates a new table and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void SqNewTable(IntPtr vm);

        /// <summary>
        ///     Creates a new vm friendvm of the one passed as first parmeter and pushes it in its stack as "thread" object.
        /// </summary>
        /// <param name="friendVm">A friend VM.</param>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>A pointer to the new VM. </returns>
        IntPtr SqNewThread(IntPtr friendVm, int initialStackSize);

        /// <summary>
        ///     Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        IntPtr SqNewUserData(uint size);

        /// <summary>
        ///     Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        IntPtr SqNewUserData(IntPtr vm, uint size);

        /// <summary>
        ///     Pushes in the stack the next key and value of an array, table or class slot. To start the iteration this function
        ///     expects a null value on top of the stack; at every call the function will substitute the null value with an
        ///     iterator and push key and value of the container slot. Every iteration the application has to pop the previous key
        ///     and value but leave the iterator(that is used as reference point for the next iteration). The function will fail
        ///     when all slots have been iterated(see Tables and arrays manipulation).
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqNext(int index);

        /// <summary>
        ///     Pushes in the stack the next key and value of an array, table or class slot. To start the iteration this function
        ///     expects a null value on top of the stack; at every call the function will substitute the null value with an
        ///     iterator and push key and value of the container slot. Every iteration the application has to pop the previous key
        ///     and value but leave the iterator(that is used as reference point for the next iteration). The function will fail
        ///     when all slots have been iterated(see Tables and arrays manipulation).
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqNext(IntPtr vm, int index);

        /// <summary>
        ///     Enable/disable the error callback notification of handled exceptions.
        ///     <para />
        ///     <remarks>
        ///         By default the VM will invoke the error callback only if an exception is not handled (no try/catch traps are
        ///         present in the call stack).
        ///         <para />
        ///         If notifyallexceptions is enabled, the VM will call the error callback for any exception even if between
        ///         try/catch blocks.
        ///         <para />
        ///         This feature is useful for implementing debuggers.
        ///     </remarks>
        /// </summary>
        /// <param name="enable">If true enables the error callback notification of handled exceptions.</param>
        void SqNotifyAllExceptions(bool enable);

        /// <summary>
        ///     Enable/disable the error callback notification of handled exceptions.
        ///     <para />
        ///     <remarks>
        ///         By default the VM will invoke the error callback only if an exception is not handled (no try/catch traps are
        ///         present in the call stack).
        ///         <para />
        ///         If notifyallexceptions is enabled, the VM will call the error callback for any exception even if between
        ///         try/catch blocks.
        ///         <para />
        ///         This feature is useful for implementing debuggers.
        ///     </remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="enable">If true enables the error callback notification of handled exceptions.</param>
        void SqNotifyAllExceptions(IntPtr vm, bool enable);

        bool SqObjToBool(ref SqObject obj);

        float SqObjToFloat(ref SqObject obj);

        int SqObjToInteger(ref SqObject obj);

        string SqObjToString(ref SqObject obj);

        /// <summary>
        ///     Creates a new instance of a squirrel VM that consists in a new execution stack.
        ///     <remarks>The returned VM has to be released with ReleaseVM.</remarks>
        /// </summary>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>An handle to a squirrel vm.</returns>
        IntPtr SqOpen(int initialStackSize);

        /// <summary>
        ///     Pops n elements from the stack.
        /// </summary>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        void SqPop(int elementsToPop);

        /// <summary>
        ///     Pops n elements from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        void SqPop(IntPtr vm, int elementsToPop);

        /// <summary>
        ///     Pops 1 object from the stack.
        /// </summary>
        void SqPopTop();

        /// <summary>
        ///     Pops 1 object from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        void SqPopTop(IntPtr vm);

        /// <summary>
        ///     Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        void SqPush(int idx);

        /// <summary>
        ///     Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        void SqPush(IntPtr vm, int idx);

        void SqPushBool(bool value);

        void SqPushBool(IntPtr vm, bool value);

        /// <summary>
        ///     Pushes the current const table in the stack.
        /// </summary>
        void SqPushConstTable();

        /// <summary>
        ///     Pushes the current const table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void SqPushConstTable(IntPtr vm);

        void SqPushFloat(float value);

        void SqPushFloat(IntPtr vm, float value);

        void SqPushInteger(int value);

        void SqPushInteger(IntPtr vm, int value);

        void SqPushNull();

        void SqPushNull(IntPtr vm);

        void SqPushObject(SqObject obj);

        void SqPushObject(IntPtr vm, SqObject obj);

        /// <summary>
        ///     Pushes the registry table in the stack.
        /// </summary>
        void SqPushRegistryTable();

        /// <summary>
        ///     Pushes the registry table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void SqPushRegistryTable(IntPtr vm);

        /// <summary>
        ///     Pushes the current root table in the stack
        /// </summary>
        void SqPushRootTable();

        /// <summary>
        ///     Pushes the current root table in the stack
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void SqPushRootTable(IntPtr vm);

        void SqPushString(string value);

        void SqPushString(IntPtr vm, string value);

        void SqPushString(IntPtr value, int length);

        void SqPushString(IntPtr vm, IntPtr value, int length);

        void SqPushUserPointer(IntPtr pointer);

        void SqPushUserPointer(IntPtr vm, IntPtr pointer);

        /// <summary>
        ///     Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the
        ///     slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqRawDeleteSlot(int index, bool pushVal);

        /// <summary>
        ///     Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the
        ///     slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqRawDeleteSlot(IntPtr vm, int index, bool pushVal);

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at position idx in the stack, without
        ///     employing delegation or metamethods.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqRawGet(int index);

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at position idx in the stack, without
        ///     employing delegation or metamethods.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqRawGet(IntPtr vm, int index);

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack,
        ///     without employing delegation or metamethods.
        ///     <remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqRawSet(int index);

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack,
        ///     without employing delegation or metamethods.
        ///     <remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target vm.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqRawSet(IntPtr vm, int index);

        /// <summary>
        ///     Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqReadClosure(SqReadFunc readFunction, IntPtr up);

        /// <summary>
        ///     Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqReadClosure(IntPtr vm, SqReadFunc readFunction, IntPtr up);

        /// <summary>
        ///     Reallocates a existing memory block.
        /// </summary>
        /// <param name="pointer">A pointer to a previously allocated memory block.</param>
        /// <param name="oldSize">The old size of the memory block.</param>
        /// <param name="newSize">The new size of the memory block.</param>
        /// <returns>Pointer to the reallocated memory block.</returns>
        IntPtr SqRealloc(IntPtr pointer, uint oldSize, uint newSize);

        bool SqRelease(ref SqObject obj);

        bool SqRelease(IntPtr vm, ref SqObject obj);

        /// <summary>
        ///     Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="idx">Index of the element that has to be removed.</param>
        void SqRemove(int idx);

        /// <summary>
        ///     Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">Index of the element that has to be removed.</param>
        void SqRemove(IntPtr vm, int idx);

        /// <summary>
        ///     Ensure that the stack space left is at least of a specified size.
        ///     <para />
        ///     If the stack is smaller it will automatically grow.
        ///     <para />
        ///     If there's a memtamethod currently running the function will fail and the stack will not be resized, this
        ///     situatuation has to be considered a "stack overflow".
        /// </summary>
        /// <param name="nSize">Required stack size.</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SqReserveStack(int nSize);

        /// <summary>
        ///     Ensure that the stack space left is at least of a specified size.
        ///     <para />
        ///     If the stack is smaller it will automatically grow.
        ///     <para />
        ///     If there's a memtamethod currently running the function will fail and the stack will not be resized, this
        ///     situatuation has to be considered a "stack overflow".
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="nSize">Required stack size.</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SqReserveStack(IntPtr vm, int nSize);

        void SqResetError();

        void SqResetError(IntPtr vm);

        void SqResetObject(ref SqObject obj);

        bool SqResume(bool reVal, bool raiseError);

        bool SqResume(IntPtr vm, bool reVal, bool raiseError);

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack.
        ///     <remarks>
        ///         This call will invoke the delegation system like a normal assignment, it only works on tables, arrays and
        ///         userdata.
        ///     </remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSet(int index);

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack.
        ///     <remarks>
        ///         This call will invoke the delegation system like a normal assignment, it only works on tables, arrays and
        ///         userdata.
        ///     </remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSet(IntPtr vm, int index);

        bool SqSetAttributes(int index);

        bool SqSetAttributes(IntPtr vm, int index);

        bool SqSetClassUdSize(int index, int udSize);

        bool SqSetClassUdSize(IntPtr vm, int index, int udSize);

        /// <summary>
        ///     Sets the compiler error handler function.
        ///     <para />
        ///     <remarks>
        ///         if the parameter errorHandler is NULL no function will be called when a compiler error occurs.
        ///         <para />
        ///         The compiler error handler is shared between friend VMs.
        ///     </remarks>
        /// </summary>
        /// <param name="errorHandler">Delegate of the error handler function.</param>
        void SqSetCompilerErrorHandler(SqCompilerError errorHandler);

        /// <summary>
        ///     Sets the compiler error handler function.
        ///     <para />
        ///     <remarks>
        ///         if the parameter errorHandler is NULL no function will be called when a compiler error occurs.
        ///         <para />
        ///         The compiler error handler is shared between friend VMs.
        ///     </remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="errorHandler">Delegate of the error handler function.</param>
        void SqSetCompilerErrorHandler(IntPtr vm, SqCompilerError errorHandler);

        /// <summary>
        ///     Pops a table from the stack and set it as const table.
        /// </summary>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetConstTable();

        /// <summary>
        ///     Pops a table from the stack and set it as const table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetConstTable(IntPtr vm);

        /// <summary>
        ///     pops a closure from the stack an sets it as debug hook.
        ///     <para />
        ///     When a debug hook is set it overrides any previously set native or non native hooks.
        ///     <para />
        ///     If the hook is null the debug hook will be disabled.
        /// </summary>
        void SqSetDebugHook();

        /// <summary>
        ///     pops a closure from the stack an sets it as debug hook.
        ///     <para />
        ///     When a debug hook is set it overrides any previously set native or non native hooks.
        ///     <para />
        ///     If the hook is null the debug hook will be disabled.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        void SqSetDebugHook(IntPtr vm);

        /// <summary>
        ///     Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        ///     <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetDelegate(int index);

        /// <summary>
        ///     Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        ///     <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetDelegate(IntPtr vm, int index);

        /// <summary>
        ///     Pops from the stack a closure or native closure and sets it as runtime-error handler.
        ///     <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        void SqSetErrorHandler();

        /// <summary>
        ///     Pops from the stack a closure or native closure and sets it as runtime-error handler.
        ///     <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        void SqSetErrorHandler(IntPtr vm);

        /// <summary>
        ///     Sets the foreign pointer of a certain VM instance.
        ///     <para />
        ///     The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).
        ///     <para />
        ///     This pointer is ignored by the VM.
        /// </summary>
        /// <param name="userPointer">The pointer that has to be set.</param>
        void SqSetForeignPtr(IntPtr userPointer);

        /// <summary>
        ///     Sets the foreign pointer of a certain VM instance.
        ///     <para />
        ///     The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).
        ///     <para />
        ///     This pointer is ignored by the VM.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="userPointer">The pointer that has to be set.</param>
        void SqSetForeignPtr(IntPtr vm, IntPtr userPointer);

        /// <summary>
        ///     Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetFreeVariable(int index, uint nVal);

        /// <summary>
        ///     Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetFreeVariable(IntPtr vm, int index, uint nVal);

        bool SqSetInstanceUp(int index, IntPtr pointer);

        bool SqSetInstanceUp(IntPtr vm, int index, IntPtr pointer);

        bool SqSetNativeClosureName(int index, string name);

        bool SqSetNativeClosureName(IntPtr vm, int index, string name);

        bool SqSetNativeClosureName(int index, IntPtr name);

        bool SqSetNativeClosureName(IntPtr vm, int index, IntPtr name);

        bool SqSetParamsCheck(int nParamsCheck, string typeMask);

        bool SqSetParamsCheck(IntPtr vm, int nParamsCheck, string typeMask);

        /// <summary>
        ///     Sets the print function of the virtual machine.
        ///     <para />
        ///     This function is used by the built-in function '::print()' to output text.
        ///     <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        void SqSetPrintFunc(SqPrintFunction printFunc, SqPrintFunction errorPrintFunc);

        /// <summary>
        ///     Sets the print function of the virtual machine.
        ///     <para />
        ///     This function is used by the built-in function '::print()' to output text.
        ///     <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        void SqSetPrintFunc(IntPtr vm, SqPrintFunction printFunc, SqPrintFunction errorPrintFunc);

        void SqSetReleaseHook(int index, SqReleaseHook hookFunc);

        void SqSetReleaseHook(IntPtr vm, int index, SqReleaseHook hookFunc);

        /// <summary>
        ///     Pops a table from the stack and set it as root table.
        /// </summary>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetRootTable();

        /// <summary>
        ///     Pops a table from the stack and set it as root table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqSetRootTable(IntPtr vm);

        /// <summary>
        ///     Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="newTop">The new top index.</param>
        void SqSetTop(int newTop);

        /// <summary>
        ///     Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="newTop">The new top index.</param>
        void SqSetTop(IntPtr vm, int newTop);

        bool SqSetTypeTag(int index, IntPtr typeTag);

        bool SqSetTypeTag(IntPtr vm, int index, IntPtr typeTag);

        bool SqSqError(string errorText);

        bool SqSqError(IntPtr vm, string errorText);

        /// <summary>
        ///     Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        bool SqStackInfo(int level, out SqStackInfos stackInfo);

        /// <summary>
        ///     Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        bool SqStackInfo(IntPtr vm, int level, out SqStackInfos stackInfo);

        /// <summary>
        ///     Suspends the execution of the specified vm.
        ///     <para />
        ///     <remarks>
        ///         sq_result can only be called as return expression of a C function.
        ///         <para />
        ///         The function will fail is the suspension is done through more C calls or in a metamethod.
        ///     </remarks>
        /// </summary>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SqSuspendVm();

        /// <summary>
        ///     Suspends the execution of the specified vm.
        ///     <para />
        ///     <remarks>
        ///         sq_result can only be called as return expression of a C function.
        ///         <para />
        ///         The function will fail is the suspension is done through more C calls or in a metamethod.
        ///     </remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SqSuspendVm(IntPtr vm);

        bool SqThrowError(string errorText);

        bool SqThrowError(IntPtr vm, string errorText);

        bool SqToBool(int index, out bool value);

        bool SqToBool(IntPtr vm, int index, out bool value);

        bool SqToString(int index);

        bool SqToString(IntPtr v, int index);

        /// <summary>
        ///     Wake up the execution a previously suspended virtual machine.
        /// </summary>
        /// <param name="resumedRet">
        ///     If true the function will pop a value from the stack and use it as return value for the
        ///     function that has previously suspended the virtual machine.
        /// </param>
        /// <param name="retVal">
        ///     If true the function will push the return value of the function that suspend the excution or the
        ///     main function one.
        /// </param>
        /// <param name="raiseError">
        ///     If true, if a runtime error occurs during the execution of the call, the vm will invoke the
        ///     error handler.
        /// </param>
        /// <param name="throwError">
        ///     If true, the vm will thow an exception as soon as is resumed. the exception payload must be
        ///     set beforehand invoking sq_thowerror().
        /// </param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SqWakeUpVm(bool resumedRet, bool retVal, bool raiseError, bool throwError);

        /// <summary>
        ///     Wake up the execution a previously suspended virtual machine.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="resumedRet">
        ///     If true the function will pop a value from the stack and use it as return value for the
        ///     function that has previously suspended the virtual machine.
        /// </param>
        /// <param name="retVal">
        ///     If true the function will push the return value of the function that suspend the excution or the
        ///     main function one.
        /// </param>
        /// <param name="raiseError">
        ///     If true, if a runtime error occurs during the execution of the call, the vm will invoke the
        ///     error handler.
        /// </param>
        /// <param name="throwError">
        ///     If true, the vm will thow an exception as soon as is resumed. the exception payload must be
        ///     set beforehand invoking sq_thowerror().
        /// </param>
        /// <returns>True if the function was execuded successfully. False if an error occured.</returns>
        bool SqWakeUpVm(IntPtr vm, bool resumedRet, bool retVal, bool raiseError, bool throwError);

        void SqWeakRef(int index);

        void SqWeakRef(IntPtr vm, int index);

        /// <summary>
        ///     Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqWriteClosure(SqWriteFunc writeFunction, IntPtr up);

        /// <summary>
        ///     Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        bool SqWriteClosure(IntPtr vm, SqWriteFunc writeFunction, IntPtr up);
    }
}