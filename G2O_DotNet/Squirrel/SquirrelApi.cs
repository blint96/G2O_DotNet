// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelApi.cs" company="Colony Online Project">
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
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using GothicOnline.G2.DotNet.Squirrel;

    internal class SquirrelApi : ISquirrelApi
    {
        /// <summary>
        ///     The squirrel module _api.
        /// </summary>
        private readonly Squirrel _Api;

        private readonly string[] _InvalidFuncs = { "GetFloat", "GetBlob" };

        /// <summary>
        ///     Initializes a new instance of the <see cref="SquirrelApi" /> class.
        /// </summary>
        /// <param name="vm">Pointer to the default vm.</param>
        /// <param name="api">Pointer to the api struct.</param>
        public SquirrelApi(IntPtr vm, IntPtr api)
        {
            this.Vm = vm;

            // Check for function pointers with invalid value and set them to 0.
            // They will then be marshalled to null without causing an exception.
            foreach (FieldInfo field in typeof(Squirrel).GetFields())
            {
                if (field.FieldType.IsSubclassOf(typeof(Delegate)))
                {
                    if (this._InvalidFuncs.Contains(field.Name))
                    {
                        // Set all function addresses with inavlid pointer values to 0, so the struct can be marshaled correcty.
                        IntPtr fieldPtr = IntPtr.Add(api, Marshal.OffsetOf(typeof(Squirrel), field.Name).ToInt32());
                        Console.WriteLine(
                            $"Squirrel function {field.Name}(0x{Marshal.ReadInt32(fieldPtr).ToString("X")}) is not initialized correctly!");
                        Marshal.WriteIntPtr(fieldPtr, IntPtr.Zero);
                    }
                }
            }

            // Marshall the _api struct.
            this._Api = (Squirrel)Marshal.PtrToStructure(api, typeof(Squirrel));
        }

        /// <summary>
        ///     Returns the version number of the squirrel _api.
        /// </summary>
        public int Version => this._Api.Version;

        /// <summary>
        ///     Gets a pointer to the squirrel vm.
        /// </summary>
        public IntPtr Vm { get; }

        public void SqAddRef(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        public void SqAddRef(IntPtr vm, ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayAppend(int index)
        {
            return this._Api.ArrayAppend(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayAppend(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ArrayAppend(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and inserts it in an array at the specified position.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayInsert(int index, int destPos)
        {
            return this._Api.ArrayInsert(this.Vm, index, destPos) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and inserts it in an array at the specified position.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayInsert(IntPtr vm, int index, int destPos)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ArrayInsert(vm, index, destPos) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayPop(int index, bool pushVal)
        {
            return this._Api.ArrayPop(this.Vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayPop(IntPtr vm, int index, bool pushVal)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ArrayPop(vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Removes an item from an array.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayRemove(int index, int itemIndex)
        {
            return this._Api.ArrayRemove(this.Vm, index, itemIndex) == SqResult.SqOk;
        }

        /// <summary>
        ///     Removes an item from an array.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayRemove(IntPtr vm, int index, int itemIndex)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ArrayRemove(vm, index, itemIndex) == SqResult.SqOk;
        }

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
        public bool SqArrayResize(int index, int newSize)
        {
            return this._Api.ArrayResize(this.Vm, index, newSize) == SqResult.SqOk;
        }

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
        public bool SqArrayResize(IntPtr vm, int index, int newSize)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ArrayResize(vm, index, newSize) == SqResult.SqOk;
        }

        /// <summary>
        ///     Reverse an array in place.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayReverse(int index)
        {
            return this._Api.ArrayReverse(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Reverse an array in place.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqArrayReverse(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ArrayReverse(vm, index) == SqResult.SqOk;
        }

        public bool SqBindEnv(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqBindEnv(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SqCall(int parameters, bool retVal, bool raiseError)
        {
            throw new NotImplementedException();
        }

        public bool SqCall(IntPtr vm, int parameters, bool retVal, bool raiseError)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Clears all the element of the table/array at position idx in the stack.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqClear(int index)
        {
            return this._Api.Clear(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clears all the element of the table/array at position idx in the stack.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqClear(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Clear(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqClone(int index)
        {
            return this._Api.Clone(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqClone(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Clone(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Releases a squirrel VM and all related friend VMs.
        /// </summary>
        public void SqClose()
        {
            this._Api.Close(this.Vm);
        }

        /// <summary>
        ///     Releases a squirrel VM and all related friend VMs.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        public void SqClose(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.Close(vm);
        }

        /// <summary>
        ///     Takes 2 object from the stack and compares them.
        /// </summary>
        /// <returns>Result of the comparison as a integer.</returns>
        public int SqCmp()
        {
            return this._Api.Cmp(this.Vm);
        }

        /// <summary>
        ///     Takes 2 object from the stack and compares them.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>Result of the comparison as a integer.</returns>
        public int SqCmp(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Cmp(vm);
        }

        /// <summary>
        ///     Runs the garbage collector and returns the number of reference cycles found(and deleted).
        ///     <para />
        ///     <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        public int SqCollectGarbage()
        {
            return this._Api.CollectGarbage(this.Vm);
        }

        /// <summary>
        ///     Runs the garbage collector and returns the number of reference cycles found(and deleted).
        ///     <para />
        ///     <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        public int SqCollectGarbage(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.CollectGarbage(vm);
        }

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
        public bool SqCompile(SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror)
        {
            return this._Api.Compile(this.Vm, readFunc, p, sourcename, raiseerror) == SqResult.SqOk;
        }

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
        public bool SqCompile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Compile(vm, readFunc, p, sourcename, raiseerror) == SqResult.SqOk;
        }

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
        public bool SqCompile(SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror)
        {
            // Marshall the sourcename to into unmanaged heap.
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourcename);

            SqResult result = this._Api.Compile(this.Vm, readFunc, p, ansiSourceName, raiseerror);

            // Free unmanaged memory.
            Marshal.FreeHGlobal(ansiSourceName);

            return result == SqResult.SqOk;
        }

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
        public bool SqCompile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            // Marshall the sourcename to into unmanaged heap.
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourcename);

            SqResult result = this._Api.Compile(vm, readFunc, p, ansiSourceName, raiseerror);

            // Free unmanaged memory.
            Marshal.FreeHGlobal(ansiSourceName);

            return result == SqResult.SqOk;
        }

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
        public bool SqCompileBuffer(IntPtr script, int size, IntPtr sourceName, bool raiseError)
        {
            return this._Api.CompileBuffer(this.Vm, script, size, sourceName, raiseError) == SqResult.SqOk;
        }

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
        public bool SqCompileBuffer(IntPtr vm, IntPtr script, int size, IntPtr sourceName, bool raiseError)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.CompileBuffer(vm, script, size, sourceName, raiseError) == SqResult.SqOk;
        }

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
        public bool SqCompileBuffer(string script, string sourceName, bool raiseError)
        {
            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("The script argument must not be null or empty.");
            }

            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("The script argument must not be null or empty.");
            }

            // Marshall strings to unmanaged ansi strings.
            IntPtr ansiScript = Marshal.StringToHGlobalAnsi(script);
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourceName);

            SqResult result = this._Api.CompileBuffer(this.Vm, ansiScript, script.Length, ansiSourceName, raiseError);

            // Free unmanaged memory.
            Marshal.FreeHGlobal(ansiScript);
            Marshal.FreeHGlobal(ansiSourceName);

            return result == SqResult.SqOk;
        }

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
        public bool SqCompileBuffer(IntPtr vm, string script, string sourceName, bool raiseError)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("The script argument must not be null or empty.");
            }

            // Marshall strings to unmanaged ansi strings.
            IntPtr ansiScript = Marshal.StringToHGlobalAnsi(script);
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourceName);

            SqResult result = this._Api.CompileBuffer(this.Vm, ansiScript, script.Length, ansiSourceName, raiseError);

            // Free unmanaged memory.
            Marshal.FreeHGlobal(ansiScript);
            Marshal.FreeHGlobal(ansiSourceName);

            return result == SqResult.SqOk;
        }

        public bool SqCreateInstance(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqCreateInstance(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Pops a key from the stack and delete the slot indexed by it
        ///     from the table at position idx in the stack, if the slot does not exits nothing happens.
        ///     <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqDeleteSlot(int index, bool pushVal)
        {
            return this._Api.DeleteSlot(this.Vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and delete the slot indexed by it
        ///     from the table at position idx in the stack, if the slot does not exits nothing happens.
        ///     <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqDeleteSlot(IntPtr vm, int index, bool pushVal)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.DeleteSlot(vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Enable/disable the debug line information generation at compile time.
        ///     <para />
        ///     <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        public void SqEnableDebugInfo(bool enable)
        {
            this._Api.EnableDebugInfo(this.Vm, enable);
        }

        /// <summary>
        ///     Enable/disable the debug line information generation at compile time.
        ///     <para />
        ///     <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        public void SqEnableDebugInfo(IntPtr vm, bool enable)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.EnableDebugInfo(this.Vm, enable);
        }

        /// <summary>
        ///     Frees a previously allocated memory block.
        /// </summary>
        /// <param name="pointer">A Pointer to the previously allocated memory block.</param>
        /// <param name="size">Size of the previously allocated memory block.</param>
        public void SqFree(IntPtr pointer, uint size)
        {
            this._Api.Free(pointer, size);
        }

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
        public bool SqGet(int index)
        {
            return this._Api.Get(this.Vm, index) == SqResult.SqOk;
        }

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
        public bool SqGet(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Get(vm, index) == SqResult.SqOk;
        }

        public bool SqGetAttributes(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetAttributes(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetBase(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetBase(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        public bool SqGetBlob(int index, out IntPtr pointer)
        {
            return this._Api.GetBlob(this.Vm, index, out pointer) == SqResult.SqOk;
        }

        /// <summary>
        ///     Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        public bool SqGetBlob(IntPtr vm, int index, out IntPtr pointer)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetBlob(vm, index, out pointer) == SqResult.SqOk;
        }

        public bool SqGetBool(int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool SqGetBool(IntPtr vm, int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool SqGetClass(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetClass(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetClosureInfo(int index, out uint nParams, out uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public bool SqGetClosureInfo(IntPtr vm, int index, out uint nParams, out uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public bool SqGetDefaultDelegate(SqObjectType objectType)
        {
            throw new NotImplementedException();
        }

        public bool SqGetDefaultDelegate(IntPtr vm, SqObjectType objectType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqGetDelegate(int index)
        {
            return this._Api.GetDelegate(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqGetDelegate(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetDelegate(vm, index) == SqResult.SqOk;
        }

        public bool SqGetFloat(int index, out float value)
        {
            throw new NotImplementedException();
        }

        public bool SqGetFloat(IntPtr vm, int index, out float value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <returns>The current VMs foreign pointer. </returns>
        public IntPtr SqGetForeignPtr()
        {
            return this._Api.GetForeignPtr(this.Vm);
        }

        /// <summary>
        ///     Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The current VMs foreign pointer. </returns>
        public IntPtr SqGetForeignPtr(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetForeignPtr(vm);
        }

        public string SqGetFreeVariable(int idx, uint nval)
        {
            throw new NotImplementedException();
        }

        public string SqGetFreeVariable(IntPtr vm, int idx, uint nval)
        {
            throw new NotImplementedException();
        }

        public bool SqGetInstanceUp(int index, out IntPtr pointer, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqGetInstanceUp(IntPtr vm, int index, out IntPtr pointer, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqGetInteger(int index, out int value)
        {
            throw new NotImplementedException();
        }

        public bool SqGetInteger(IntPtr vm, int index, out int value)
        {
            throw new NotImplementedException();
        }

        public void SqGetLastError()
        {
            throw new NotImplementedException();
        }

        public void SqGetLastError(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        public string SqGetLocal(uint level, uint index)
        {
            throw new NotImplementedException();
        }

        public string SqGetLocal(IntPtr vm, uint level, uint index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetObjectTypeTag(ref SqObject obj, out IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public string SqGetScratchPad(int minSize)
        {
            throw new NotImplementedException();
        }

        public string SqGetScratchPad(IntPtr vm, int minSize)
        {
            throw new NotImplementedException();
        }

        public int SqGetSize(int index)
        {
            throw new NotImplementedException();
        }

        public int SqGetSize(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetStackObj(int index, ref SqObject obj)
        {
            return this._Api.GetStackObj(this.Vm, index, ref obj) == SqResult.SqOk;
        }

        public bool SqGetStackObj(IntPtr vm, int index, ref SqObject obj)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetStackObj(vm, index, ref obj) == SqResult.SqOk;
        }

        public bool SqGetString(int index, out string value)
        {
            return this._Api.GetString(this.Vm, index, out value) == SqResult.SqOk;
        }

        public bool SqGetString(IntPtr vm, int index, out string value)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetString(vm, index, out value) == SqResult.SqOk;
        }

        public bool SqGetThread(int index, out IntPtr threadPtr)
        {
            throw new NotImplementedException();
        }

        public bool SqGetThread(IntPtr vm, int index, out IntPtr threadPtr)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns the index of the top of the stack.
        /// </summary>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        public int SqGetTop()
        {
            return this._Api.GetTop(this.Vm);
        }

        /// <summary>
        ///     Returns the index of the top of the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        public int SqGetTop(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetTop(vm);
        }

        public SqObjectType SqGetType(int index)
        {
            throw new NotImplementedException();
        }

        public SqObjectType SqGetType(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SqGetTypeTag(int index, out IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqGetTypeTag(IntPtr vm, int index, out IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqGetUserData(int index, out IntPtr data, out int typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqGetUserData(IntPtr vm, int index, out IntPtr data, out int typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqGetUserPointer(int index, out IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        public bool SqGetUserPointer(IntPtr vm, int index, out IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns the execution state of a virtual machine.
        /// </summary>
        /// <returns>The state of the vm.</returns>
        public VmState SqGetVmState()
        {
            return this._Api.GetVmState(this.Vm);
        }

        /// <summary>
        ///     Returns the execution state of a virtual machine.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The state of the vm.</returns>
        public VmState SqGetVmState(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetVmState(vm);
        }

        /// <summary>
        ///     Pushes the object pointed by the weak reference at position idx in the stack.
        ///     <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqGetWeakRefVal(int index)
        {
            return this._Api.GetWeakRefVal(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes the object pointed by the weak reference at position idx in the stack.
        ///     <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqGetWeakRefVal(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.GetWeakRefVal(vm, index) == SqResult.SqOk;
        }

        public bool SqInstanceOf()
        {
            throw new NotImplementedException();
        }

        public bool SqInstanceOf(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Allocates a memory new block.
        /// </summary>
        /// <param name="size">Size of the new memory block.</param>
        /// <returns>Pointer to the new memory block.</returns>
        public IntPtr SqMalloc(uint size)
        {
            return this._Api.Malloc(size);
        }

        /// <summary>
        ///     Pushes the object at the position 'idx' of the source vm stack in the destination vm stack.
        /// </summary>
        /// <param name="destVm">Pointer to the destination VM.</param>
        /// <param name="srcVm">Pointer to the source VM.</param>
        /// <param name="idx">The index in the source stack of the value that has to be moved.</param>
        public void SqMove(IntPtr destVm, IntPtr srcVm, int idx)
        {
            if (destVm == IntPtr.Zero)
            {
                throw new ArgumentException("The destVm argument must not be IntPtr.Zero.");
            }

            if (srcVm == IntPtr.Zero)
            {
                throw new ArgumentException("The srcVm argument must not be IntPtr.Zero.");
            }

            this._Api.Move(destVm, srcVm, idx);
        }

        /// <summary>
        ///     Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the array that as to be created.</param>
        public void SqNewArray(int size)
        {
            this._Api.NewArray(this.Vm, size);
        }

        /// <summary>
        ///     Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the array that as to be created.</param>
        public void SqNewArray(IntPtr vm, int size)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.NewArray(vm, size);
        }

        public bool SqNewClass(bool hasBase)
        {
            throw new NotImplementedException();
        }

        public bool SqNewClass(IntPtr vm, bool hasBase)
        {
            throw new NotImplementedException();
        }

        public void SqNewClosure(SqFunction func, uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public void SqNewClosure(IntPtr vm, SqFunction func, uint nFreeVars)
        {
            throw new NotImplementedException();
        }

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
        public bool SqNewSlot(int index, bool bStatic)
        {
            return this._Api.NewSlot(this.Vm, index, bStatic) == SqResult.SqOk;
        }

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
        public bool SqNewSlot(IntPtr vm, int index, bool bStatic)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.NewSlot(vm, index, bStatic) == SqResult.SqOk;
        }

        /// <summary>
        ///     Creates a new table and pushes it in the stack.
        /// </summary>
        public void SqNewTable()
        {
            this._Api.NewTable(this.Vm);
        }

        /// <summary>
        ///     Creates a new table and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void SqNewTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.NewTable(vm);
        }

        /// <summary>
        ///     Creates a new vm friendvm of the one passed as first parmeter and pushes it in its stack as "thread" object.
        /// </summary>
        /// <param name="friendVm">A friend VM.</param>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>A pointer to the new VM. </returns>
        public IntPtr SqNewThread(IntPtr friendVm, int initialStackSize)
        {
            return this._Api.NewThread(friendVm, initialStackSize);
        }

        /// <summary>
        ///     Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        public IntPtr SqNewUserData(uint size)
        {
            return this._Api.NewUserdata(this.Vm, size);
        }

        /// <summary>
        ///     Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        public IntPtr SqNewUserData(IntPtr vm, uint size)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.NewUserdata(this.Vm, size);
        }

        /// <summary>
        ///     Pushes in the stack the next key and value of an array, table or class slot. To start the iteration this function
        ///     expects a null value on top of the stack; at every call the function will substitute the null value with an
        ///     iterator and push key and value of the container slot. Every iteration the application has to pop the previous key
        ///     and value but leave the iterator(that is used as reference point for the next iteration). The function will fail
        ///     when all slots have been iterated(see Tables and arrays manipulation).
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqNext(int index)
        {
            return this._Api.Next(this.Vm, index) == SqResult.SqOk;
        }

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
        public bool SqNext(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Next(vm, index) == SqResult.SqOk;
        }

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
        public void SqNotifyAllExceptions(bool enable)
        {
            this._Api.NotifyAllExceptions(this.Vm, enable);
        }

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
        public void SqNotifyAllExceptions(IntPtr vm, bool enable)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.NotifyAllExceptions(vm, enable);
        }

        public bool SqObjToBool(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        public float SqObjToFloat(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        public int SqObjToInteger(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        public string SqObjToString(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Creates a new instance of a squirrel VM that consists in a new execution stack.
        ///     <remarks>The returned VM has to be released with ReleaseVM.</remarks>
        /// </summary>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>An handle to a squirrel vm.</returns>
        public IntPtr SqOpen(int initialStackSize)
        {
            return this._Api.Open(initialStackSize);
        }

        /// <summary>
        ///     Pops n elements from the stack.
        /// </summary>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        public void SqPop(int elementsToPop)
        {
            this._Api.Pop(this.Vm, elementsToPop);
        }

        /// <summary>
        ///     Pops n elements from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        public void SqPop(IntPtr vm, int elementsToPop)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.Pop(vm, elementsToPop);
        }

        /// <summary>
        ///     Pops 1 object from the stack.
        /// </summary>
        public void SqPopTop()
        {
            this._Api.PopTop(this.Vm);
        }

        /// <summary>
        ///     Pops 1 object from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        public void SqPopTop(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.PopTop(this.Vm);
        }

        /// <summary>
        ///     Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        public void SqPush(int idx)
        {
            this._Api.Push(this.Vm, idx);
        }

        /// <summary>
        ///     Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        public void SqPush(IntPtr vm, int idx)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.Push(vm, idx);
        }

        public void SqPushBool(bool value)
        {
            throw new NotImplementedException();
        }

        public void SqPushBool(IntPtr vm, bool value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Pushes the current const table in the stack.
        /// </summary>
        public void SqPushConstTable()
        {
            this._Api.PushConstTable(this.Vm);
        }

        /// <summary>
        ///     Pushes the current const table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void SqPushConstTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.PushConstTable(vm);
        }

        public void SqPushFloat(float value)
        {
            throw new NotImplementedException();
        }

        public void SqPushFloat(IntPtr vm, float value)
        {
            throw new NotImplementedException();
        }

        public void SqPushInteger(int value)
        {
            throw new NotImplementedException();
        }

        public void SqPushInteger(IntPtr vm, int value)
        {
            throw new NotImplementedException();
        }

        public void SqPushNull()
        {
            throw new NotImplementedException();
        }

        public void SqPushNull(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        public void SqPushObject(SqObject obj)
        {
            this._Api.PushObject(this.Vm, obj);
        }

        public void SqPushObject(IntPtr vm, SqObject obj)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.PushObject(vm, obj);
        }

        /// <summary>
        ///     Pushes the registry table in the stack.
        /// </summary>
        public void SqPushRegistryTable()
        {
            this._Api.PushRegistryTable(this.Vm);
        }

        /// <summary>
        ///     Pushes the registry table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void SqPushRegistryTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.PushRegistryTable(vm);
        }

        /// <summary>
        ///     Pushes the current root table in the stack
        /// </summary>
        public void SqPushRootTable()
        {
            this._Api.PushRootTable(this.Vm);
        }

        /// <summary>
        ///     Pushes the current root table in the stack
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void SqPushRootTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.PushRootTable(vm);
        }

        public void SqPushString(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this._Api.PushString(this.Vm,Marshal.StringToHGlobalAnsi(value),value.Length);
        }

        public void SqPushString(IntPtr vm, string value)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this._Api.PushString(this.Vm, Marshal.StringToHGlobalAnsi(value), value.Length);
        }

        public void SqPushString(IntPtr value, int length)
        {
            if (value == IntPtr.Zero)
            {
                throw new ArgumentException("The value argument must not be IntPtr.Zero.", nameof(value));
            }
            this._Api.PushString(this.Vm, value, length);
        }

        public void SqPushString(IntPtr vm, IntPtr value, int length)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }
            if (value == IntPtr.Zero)
            {
                throw new ArgumentException("The value argument must not be IntPtr.Zero.", nameof(value));
            }
            this._Api.PushString(vm, value, length);
        }

        public void SqPushUserPointer(IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public void SqPushUserPointer(IntPtr vm, IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the
        ///     slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqRawDeleteSlot(int index, bool pushVal)
        {
            return this._Api.RawDeleteSlot(this.Vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the
        ///     slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqRawDeleteSlot(IntPtr vm, int index, bool pushVal)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.RawDeleteSlot(vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at position idx in the stack, without
        ///     employing delegation or metamethods.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqRawGet(int index)
        {
            return this._Api.RawGet(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at position idx in the stack, without
        ///     employing delegation or metamethods.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqRawGet(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.RawGet(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack,
        ///     without employing delegation or metamethods.
        ///     <remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqRawSet(int index)
        {
            return this._Api.RawSet(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack,
        ///     without employing delegation or metamethods.
        ///     <remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target vm.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqRawSet(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.RawSet(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqReadClosure(SqReadFunc readFunction, IntPtr up)
        {
            return this._Api.ReadClosure(this.Vm, readFunction, up) == SqResult.SqOk;
        }

        /// <summary>
        ///     Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqReadClosure(IntPtr vm, SqReadFunc readFunction, IntPtr up)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ReadClosure(vm, readFunction, up) == SqResult.SqOk;
        }

        /// <summary>
        ///     Reallocates a existing memory block.
        /// </summary>
        /// <param name="pointer">A pointer to a previously allocated memory block.</param>
        /// <param name="oldSize">The old size of the memory block.</param>
        /// <param name="newSize">The new size of the memory block.</param>
        /// <returns>Pointer to the reallocated memory block.</returns>
        public IntPtr SqRealloc(IntPtr pointer, uint oldSize, uint newSize)
        {
            return this._Api.Realloc(pointer, oldSize, newSize);
        }

        public bool SqRelease(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        public bool SqRelease(IntPtr vm, ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="idx">Index of the element that has to be removed.</param>
        public void SqRemove(int idx)
        {
            this._Api.Remove(this.Vm, idx);
        }

        /// <summary>
        ///     Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">Index of the element that has to be removed.</param>
        public void SqRemove(IntPtr vm, int idx)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.Remove(vm, idx);
        }

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
        public bool SqReserveStack(int nSize)
        {
            return this._Api.ReserveStack(this.Vm, nSize) >= SqResult.SqOk;
        }

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
        public bool SqReserveStack(IntPtr vm, int nSize)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.ReserveStack(vm, nSize) >= SqResult.SqOk;
        }

        public void SqResetError()
        {
            throw new NotImplementedException();
        }

        public void SqResetError(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        public void SqResetObject(ref SqObject obj)
        {
            throw new NotImplementedException();
        }

        public bool SqResume(bool reVal, bool raiseError)
        {
            throw new NotImplementedException();
        }

        public bool SqResume(IntPtr vm, bool reVal, bool raiseError)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack.
        ///     <remarks>
        ///         This call will invoke the delegation system like a normal assignment, it only works on tables, arrays and
        ///         userdata.
        ///     </remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSet(int index)
        {
            return this._Api.Set(this.Vm, index) == SqResult.SqOk;
        }

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
        public bool SqSet(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.Set(vm, index) == SqResult.SqOk;
        }

        public bool SqSetAttributes(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqSetAttributes(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SqSetClassUdSize(int index, int udSize)
        {
            throw new NotImplementedException();
        }

        public bool SqSetClassUdSize(IntPtr vm, int index, int udSize)
        {
            throw new NotImplementedException();
        }

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
        public void SqSetCompilerErrorHandler(SqCompilerError errorHandler)
        {
            if (errorHandler == null)
            {
                throw new ArgumentNullException(
                    nameof(errorHandler), 
                    $"The {nameof(errorHandler)} argument must not be null.");
            }

            this._Api.SetCompilerErrorHandler(this.Vm, errorHandler);
        }

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
        public void SqSetCompilerErrorHandler(IntPtr vm, SqCompilerError errorHandler)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            if (errorHandler == null)
            {
                throw new ArgumentNullException(
                    nameof(errorHandler), 
                    $"The {nameof(errorHandler)} argument must not be null.");
            }

            this._Api.SetCompilerErrorHandler(vm, errorHandler);
        }

        /// <summary>
        ///     Pops a table from the stack and set it as const table.
        /// </summary>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetConstTable()
        {
            return this._Api.SetConstTable(this.Vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and set it as const table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetConstTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.SetConstTable(this.Vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     pops a closure from the stack an sets it as debug hook.
        ///     <para />
        ///     When a debug hook is set it overrides any previously set native or non native hooks.
        ///     <para />
        ///     If the hook is null the debug hook will be disabled.
        /// </summary>
        public void SqSetDebugHook()
        {
            this._Api.SetDebugHook(this.Vm);
        }

        /// <summary>
        ///     pops a closure from the stack an sets it as debug hook.
        ///     <para />
        ///     When a debug hook is set it overrides any previously set native or non native hooks.
        ///     <para />
        ///     If the hook is null the debug hook will be disabled.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void SqSetDebugHook(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.SetDebugHook(vm);
        }

        /// <summary>
        ///     Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        ///     <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetDelegate(int index)
        {
            return this._Api.SetDelegate(this.Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        ///     <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetDelegate(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.SetDelegate(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops from the stack a closure or native closure and sets it as runtime-error handler.
        ///     <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        public void SqSetErrorHandler()
        {
            this._Api.SetErrorHandler(this.Vm);
        }

        /// <summary>
        ///     Pops from the stack a closure or native closure and sets it as runtime-error handler.
        ///     <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        public void SqSetErrorHandler(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.SetErrorHandler(vm);
        }

        /// <summary>
        ///     Sets the foreign pointer of a certain VM instance.
        ///     <para />
        ///     The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).
        ///     <para />
        ///     This pointer is ignored by the VM.
        /// </summary>
        /// <param name="userPointer">The pointer that has to be set.</param>
        public void SqSetForeignPtr(IntPtr userPointer)
        {
            this._Api.SetForeignPtr(this.Vm, userPointer);
        }

        /// <summary>
        ///     Sets the foreign pointer of a certain VM instance.
        ///     <para />
        ///     The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).
        ///     <para />
        ///     This pointer is ignored by the VM.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="userPointer">The pointer that has to be set.</param>
        public void SqSetForeignPtr(IntPtr vm, IntPtr userPointer)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.SetForeignPtr(vm, userPointer);
        }

        /// <summary>
        ///     Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetFreeVariable(int index, uint nVal)
        {
            return this._Api.SetFreeVariable(this.Vm, index, nVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetFreeVariable(IntPtr vm, int index, uint nVal)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.SetFreeVariable(vm, index, nVal) == SqResult.SqOk;
        }

        public bool SqSetInstanceUp(int index, IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public bool SqSetInstanceUp(IntPtr vm, int index, IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public bool SqSetNativeClosureName(int index, string name)
        {
            throw new NotImplementedException();
        }

        public bool SqSetNativeClosureName(IntPtr vm, int index, string name)
        {
            throw new NotImplementedException();
        }

        public bool SqSetNativeClosureName(int index, IntPtr name)
        {
            throw new NotImplementedException();
        }

        public bool SqSetNativeClosureName(IntPtr vm, int index, IntPtr name)
        {
            throw new NotImplementedException();
        }

        public bool SqSetParamsCheck(int nParamsCheck, string typeMask)
        {
            throw new NotImplementedException();
        }

        public bool SqSetParamsCheck(IntPtr vm, int nParamsCheck, string typeMask)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Sets the print function of the virtual machine.
        ///     <para />
        ///     This function is used by the built-in function '::print()' to output text.
        ///     <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        public void SqSetPrintFunc(SqPrintFunction printFunc, SqPrintFunction errorPrintFunc)
        {
            this._Api.SetPrintFunc(this.Vm, printFunc, errorPrintFunc);
        }

        /// <summary>
        ///     Sets the print function of the virtual machine.
        ///     <para />
        ///     This function is used by the built-in function '::print()' to output text.
        ///     <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        public void SqSetPrintFunc(IntPtr vm, SqPrintFunction printFunc, SqPrintFunction errorPrintFunc)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.SetPrintFunc(vm, printFunc, errorPrintFunc);
        }

        public void SqSetReleaseHook(int index, SqReleaseHook hookFunc)
        {
            throw new NotImplementedException();
        }

        public void SqSetReleaseHook(IntPtr vm, int index, SqReleaseHook hookFunc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Pops a table from the stack and set it as root table.
        /// </summary>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetRootTable()
        {
            return this._Api.SetRootTable(this.Vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and set it as root table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqSetRootTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.SetRootTable(vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="newTop">The new top index.</param>
        public void SqSetTop(int newTop)
        {
            this._Api.SetTop(this.Vm, newTop);
        }

        /// <summary>
        ///     Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="newTop">The new top index.</param>
        public void SqSetTop(IntPtr vm, int newTop)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            this._Api.SetTop(vm, newTop);
        }

        public bool SqSetTypeTag(int index, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqSetTypeTag(IntPtr vm, int index, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SqSqError(string errorText)
        {
            throw new NotImplementedException();
        }

        public bool SqSqError(IntPtr vm, string errorText)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        public bool SqStackInfo(int level, out SqStackInfos stackInfo)
        {
            return this._Api.StackInfos(this.Vm, level, out stackInfo) == SqResult.SqOk;
        }

        /// <summary>
        ///     Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        public bool SqStackInfo(IntPtr vm, int level, out SqStackInfos stackInfo)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.StackInfos(this.Vm, level, out stackInfo) == SqResult.SqOk;
        }

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
        public bool SqSuspendVm()
        {
            return this._Api.SuspendVm(this.Vm) >= SqResult.SqOk;
        }

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
        public bool SqSuspendVm(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.SuspendVm(vm) >= SqResult.SqOk;
        }

        public bool SqThrowError(string errorText)
        {
            throw new NotImplementedException();
        }

        public bool SqThrowError(IntPtr vm, string errorText)
        {
            throw new NotImplementedException();
        }

        public bool SqToBool(int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool SqToBool(IntPtr vm, int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool SqToString(int index)
        {
            throw new NotImplementedException();
        }

        public bool SqToString(IntPtr v, int index)
        {
            throw new NotImplementedException();
        }

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
        public bool SqWakeUpVm(bool resumedRet, bool retVal, bool raiseError, bool throwError)
        {
            return this._Api.WakeupVm(this.Vm, resumedRet, retVal, raiseError, throwError) >= SqResult.SqOk;
        }

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
        public bool SqWakeUpVm(IntPtr vm, bool resumedRet, bool retVal, bool raiseError, bool throwError)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.WakeupVm(vm, resumedRet, retVal, raiseError, throwError) >= SqResult.SqOk;
        }

        public void SqWeakRef(int index)
        {
            throw new NotImplementedException();
        }

        public void SqWeakRef(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqWriteClosure(SqWriteFunc writeFunction, IntPtr up)
        {
            return this._Api.WriteClosure(this.Vm, writeFunction, up) == SqResult.SqOk;
        }

        /// <summary>
        ///     Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was successful, false if not.</returns>
        public bool SqWriteClosure(IntPtr vm, SqWriteFunc writeFunction, IntPtr up)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.", nameof(vm));
            }

            return this._Api.WriteClosure(vm, writeFunction, up) == SqResult.SqOk;
        }
    }
}