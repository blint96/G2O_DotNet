using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GothicOnline.G2.DotNet.Squirrel
{
    internal class SquirrelApi : ISquirrelApi
    {

        #region constants
        private  readonly string[] _InvalidFuncs = { "GetFloat", "GetBlob" };

        #endregion

        #region fields

        /// <summary>
        /// The squirrel module _api.
        /// </summary>
        private readonly Squirrel _Api;

        #endregion

        #region properties

        /// <summary>
        ///     Gets a pointer to the squirrel vm.
        /// </summary>
        public IntPtr Vm { get; }

        /// <summary>
        ///     Returns the version number of the squirrel _api.
        /// </summary>
        public int Version => _Api.Version;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SquirrelApi"/> class.
        /// </summary>
        /// <param name="vm">Pointer to the default vm.</param>
        /// <param name="api">Pointer to the api struct.</param>
        public SquirrelApi(IntPtr vm, IntPtr api)
        {
            Vm = vm;

            //Check for function pointers with invalid value and set them to 0.
            //They will then be marshalled to null without causing an exception.
            foreach (FieldInfo field in typeof(Squirrel).GetFields())
            {
                if (field.FieldType.IsSubclassOf(typeof(Delegate)))
                {
                    if (_InvalidFuncs.Contains(field.Name))
                    {
                        //Set all function addresses with inavlid pointer values to 0, so the struct can be marshaled correcty.
                        IntPtr fieldPtr = IntPtr.Add(api, Marshal.OffsetOf(typeof(Squirrel), field.Name).ToInt32());
                        Console.WriteLine($"Squirrel function {field.Name}(0x{Marshal.ReadInt32(fieldPtr).ToString("X")}) is not initialized correctly!");
                        Marshal.WriteIntPtr(fieldPtr, IntPtr.Zero);
                    }
                }
            }
            //Marshall the _api struct.
            _Api = (Squirrel)Marshal.PtrToStructure(api, typeof(Squirrel));
        }

        #region VM

        /// <summary>
        ///     Creates a new instance of a squirrel VM that consists in a new execution stack.
        ///     <remarks>The returned VM has to be released with ReleaseVM.</remarks>
        /// </summary>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>An handle to a squirrel vm.</returns>
        public IntPtr Open(int initialStackSize)
        {
            return _Api.Open(initialStackSize);
        }

        /// <summary>
        ///     Creates a new vm friendvm of the one passed as first parmeter and pushes it in its stack as "thread" object.
        /// </summary>
        /// <param name="friendVm">A friend VM.</param>
        /// <param name="initialStackSize">The size of the stack in slots(number of objects).</param>
        /// <returns>A pointer to the new VM. </returns>
        public IntPtr NewThread(IntPtr friendVm, int initialStackSize)
        {
            return _Api.NewThread(friendVm, initialStackSize);
        }

        /// <summary>
        ///     Pops from the stack a closure or native closure and sets it as runtime-error handler.
        ///     <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        public void SetErrorHandler()
        {
            _Api.SetErrorHandler(Vm);
        }

        /// <summary>
        ///     Pops from the stack a closure or native closure and sets it as runtime-error handler.
        ///     <remarks>The error handler is shared by friend VM.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        public void SetErrorHandler(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.SetErrorHandler(vm);
        }

        /// <summary>
        ///     Releases a squirrel VM and all related friend VMs.
        /// </summary>
        public void Close()
        {
            _Api.Close(Vm);
        }

        /// <summary>
        ///     Releases a squirrel VM and all related friend VMs.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        public void Close(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.Close(vm);
        }

        /// <summary>
        ///     Sets the foreign pointer of a certain VM instance.
        ///     <para />
        ///     The foreign pointer is an arbitrary user defined pointer associated to a VM (by default is value id 0).
        ///     <para />
        ///     This pointer is ignored by the VM.
        /// </summary>
        /// <param name="userPointer">The pointer that has to be set.</param>
        public void SetForeignPtr(IntPtr userPointer)
        {
            _Api.SetForeignPtr(Vm, userPointer);
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
        public void SetForeignPtr(IntPtr vm, IntPtr userPointer)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.SetForeignPtr(vm, userPointer);
        }

        /// <summary>
        ///     Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <returns>The current VMs foreign pointer. </returns>
        public IntPtr GetForeignPtr()
        {
            return _Api.GetForeignPtr(Vm);
        }

        /// <summary>
        ///     Returns the foreign pointer of a VM instance.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The current VMs foreign pointer. </returns>
        public IntPtr GetForeignPtr(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetForeignPtr(vm);
        }

        /// <summary>
        ///     Sets the print function of the virtual machine.
        ///     <para />
        ///     This function is used by the built-in function '::print()' to output text.
        ///     <remarks>Make sure to prevent the delegates from being garbadge collected.</remarks>
        /// </summary>
        /// <param name="printFunc">A pointer to the print func or IntPtr.Zero to disable the output.</param>
        /// <param name="errorPrintFunc">A pointer to the error func or IntPtr.Zero to disable the output.</param>
        public void SetPrintFunc(SqPrintFunction printFunc, SqPrintFunction errorPrintFunc)
        {
            _Api.SetPrintFunc(Vm, printFunc, errorPrintFunc);
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
        public void SetPrintFunc(IntPtr vm, SqPrintFunction printFunc, SqPrintFunction errorPrintFunc)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.SetPrintFunc(vm, printFunc, errorPrintFunc);
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
        public bool SuspendVm()
        {
            return _Api.SuspendVm(Vm) >= SqResult.SqOk;
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
        public bool SuspendVm(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.SuspendVm(vm) >= SqResult.SqOk;
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
        public bool WakeUpVm(bool resumedRet, bool retVal, bool raiseError, bool throwError)
        {
            return _Api.WakeupVm(Vm, resumedRet, retVal, raiseError, throwError) >= SqResult.SqOk;
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
        public bool WakeUpVm(IntPtr vm, bool resumedRet, bool retVal, bool raiseError, bool throwError)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.WakeupVm(vm, resumedRet, retVal, raiseError, throwError) >=
                   SqResult.SqOk;
        }

        /// <summary>
        ///     Returns the execution state of a virtual machine.
        /// </summary>
        /// <returns>The state of the vm.</returns>
        public VmState GetVmState()
        {
            return _Api.GetVmState(Vm);
        }

        /// <summary>
        ///     Returns the execution state of a virtual machine.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>The state of the vm.</returns>
        public VmState GetVmState(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetVmState(vm);
        }

        #endregion

        #region Compiler

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
        public bool Compile(SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror)
        {
            return _Api.Compile(Vm, readFunc, p, sourcename, raiseerror) == SqResult.SqOk;
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
        public bool Compile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, IntPtr sourcename, bool raiseerror)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Compile(vm, readFunc, p, sourcename, raiseerror) == SqResult.SqOk;
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
        public bool Compile(SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror)
        {
            //Marshall the sourcename to into unmanaged heap.
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourcename);

            SqResult result = _Api.Compile(Vm, readFunc, p, ansiSourceName, raiseerror);

            //Free unmanaged memory.
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
        public bool Compile(IntPtr vm, SqLexReadFunc readFunc, IntPtr p, string sourcename, bool raiseerror)
        {
            if (vm == IntPtr.Zero)
            {
                throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            //Marshall the sourcename to into unmanaged heap.
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourcename);

            SqResult result = _Api.Compile(vm, readFunc, p, ansiSourceName, raiseerror);

            //Free unmanaged memory.
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
        public bool CompileBuffer(IntPtr script, int size, IntPtr sourceName, bool raiseError)
        {
            return _Api.CompileBuffer(Vm, script, size, sourceName, raiseError) == SqResult.SqOk;
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
        public bool CompileBuffer(IntPtr vm, IntPtr script, int size, IntPtr sourceName, bool raiseError)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.CompileBuffer(vm, script, size, sourceName, raiseError) == SqResult.SqOk;
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
        public bool CompileBuffer(string script, string sourceName, bool raiseError)
        {
            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("The script argument must not be null or empty.");
            }
            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("The script argument must not be null or empty.");
            }

            //Marshall strings to unmanaged ansi strings.
            IntPtr ansiScript = Marshal.StringToHGlobalAnsi(script);
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourceName);

            SqResult result = _Api.CompileBuffer(Vm, ansiScript, script.Length, ansiSourceName, raiseError);

            //Free unmanaged memory.
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
        public bool CompileBuffer(IntPtr vm, string script, string sourceName, bool raiseError)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }
            if (string.IsNullOrEmpty(script))
            {
                throw new ArgumentException("The script argument must not be null or empty.");
            }

            //Marshall strings to unmanaged ansi strings.
            IntPtr ansiScript = Marshal.StringToHGlobalAnsi(script);
            IntPtr ansiSourceName = Marshal.StringToHGlobalAnsi(sourceName);

            SqResult result = _Api.CompileBuffer(Vm, ansiScript, script.Length, ansiSourceName, raiseError);

            //Free unmanaged memory.
            Marshal.FreeHGlobal(ansiScript);
            Marshal.FreeHGlobal(ansiSourceName);

            return result == SqResult.SqOk;
        }

        /// <summary>
        ///     Enable/disable the debug line information generation at compile time.
        ///     <para />
        ///     <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        public void EnableDebugInfo(bool enable)
        {
            _Api.EnableDebugInfo(Vm, enable);
        }

        /// <summary>
        ///     Enable/disable the debug line information generation at compile time.
        ///     <para />
        ///     <remarks>The function affects all threads as well.</remarks>
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="enable">If true enables the debug info generation, if false disables it.</param>
        public void EnableDebugInfo(IntPtr vm, bool enable)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.EnableDebugInfo(Vm, enable);
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
        public void NotifyAllExceptions(bool enable)
        {
            _Api.NotifyAllExceptions(Vm, enable);
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
        public void NotifyAllExceptions(IntPtr vm, bool enable)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.NotifyAllExceptions(vm, enable);
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
        public void SetCompilerErrorHandler(SqCompilerError errorHandler)
        {
            if (errorHandler == null)
            {
                throw new ArgumentNullException(nameof(errorHandler), $"The {nameof(errorHandler)} argument must not be null.");
            }

            _Api.SetCompilerErrorHandler(Vm, errorHandler);
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
        public void SetCompilerErrorHandler(IntPtr vm, SqCompilerError errorHandler)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }
            if (errorHandler == null)
            {
                throw new ArgumentNullException(nameof(errorHandler),$"The {nameof(errorHandler)} argument must not be null.");
            }

            _Api.SetCompilerErrorHandler(vm, errorHandler);
        }

        #endregion

        #region  stack operations

        /// <summary>
        ///     Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        public void Push(int idx)
        {
            _Api.Push(Vm, idx);
        }

        /// <summary>
        ///     Pushes in the stack the value at the index idx.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">The index in the stack of the value that has to be pushed.</param>
        public void Push(IntPtr vm, int idx)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.Push(vm, idx);
        }

        /// <summary>
        ///     Pops n elements from the stack.
        /// </summary>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        public void Pop(int elementsToPop)
        {
            _Api.Pop(Vm, elementsToPop);
        }

        /// <summary>
        ///     Pops n elements from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="elementsToPop">The number of elements to pop.</param>
        public void Pop(IntPtr vm, int elementsToPop)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.Pop(vm, elementsToPop);
        }

        /// <summary>
        ///     Pops 1 object from the stack.
        /// </summary>
        public void PopTop()
        {
            _Api.PopTop(Vm);
        }

        /// <summary>
        ///     Pops 1 object from the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        public void PopTop(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.PopTop(Vm);
        }

        /// <summary>
        ///     Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="idx">Index of the element that has to be removed.</param>
        public void Remove(int idx)
        {
            _Api.Remove(Vm, idx);
        }

        /// <summary>
        ///     Removes an element from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="idx">Index of the element that has to be removed.</param>
        public void Remove(IntPtr vm, int idx)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.Remove(vm, idx);
        }

        /// <summary>
        ///     Returns the index of the top of the stack.
        /// </summary>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        public int GetTop()
        {
            return _Api.GetTop(Vm);
        }

        /// <summary>
        ///     Returns the index of the top of the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>An integer representing the index of the top of the stack .</returns>
        public int GetTop(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetTop(vm);
        }

        /// <summary>
        ///     Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="newTop">The new top index.</param>
        public void SetTop(int newTop)
        {
            _Api.SetTop(Vm, newTop);
        }

        /// <summary>
        ///     Resize the stack, if new top is bigger then the current top the function will push nulls.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="newTop">The new top index.</param>
        public void SetTop(IntPtr vm, int newTop)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.SetTop(vm, newTop);
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
        public bool ReserveStack(int nSize)
        {
            return _Api.ReserveStack(Vm, nSize) >= SqResult.SqOk;
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
        public bool ReserveStack(IntPtr vm, int nSize)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ReserveStack(vm, nSize) >= SqResult.SqOk;
        }

        /// <summary>
        ///     Takes 2 object from the stack and compares them.
        /// </summary>
        /// <returns>Result of the comparison as a integer.</returns>
        public int Cmp()
        {
            return _Api.Cmp(Vm);
        }

        /// <summary>
        ///     Takes 2 object from the stack and compares them.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <returns>Result of the comparison as a integer.</returns>
        public int Cmp(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Cmp(vm);
        }

        /// <summary>
        ///     Pushes the object at the position 'idx' of the source vm stack in the destination vm stack.
        /// </summary>
        /// <param name="destVm">Pointer to the destination VM.</param>
        /// <param name="srcVm">Pointer to the source VM.</param>
        /// <param name="idx">The index in the source stack of the value that has to be moved.</param>
        public void Move(IntPtr destVm, IntPtr srcVm, int idx)
        {
            if (destVm == IntPtr.Zero)
            {
                throw new ArgumentException("The destVm argument must not be IntPtr.Zero.");
            }
            if (srcVm == IntPtr.Zero)
            {
                throw new ArgumentException("The srcVm argument must not be IntPtr.Zero.");
            }

            _Api.Move(destVm, srcVm, idx);
        }

        #endregion

        #region object creation handling

        /// <summary>
        ///     Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        public IntPtr NewUserData(uint size)
        {
            return _Api.NewUserdata(Vm, size);
        }

        /// <summary>
        ///     Creates a new userdata and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the userdata that as to be created in bytes.</param>
        /// <returns>Pointer to the new userdata.</returns>
        public IntPtr NewUserData(IntPtr vm, uint size)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.NewUserdata(Vm, size);
        }

        /// <summary>
        ///     Creates a new table and pushes it in the stack.
        /// </summary>
        public void NewTable()
        {
            _Api.NewTable(Vm);
        }

        /// <summary>
        ///     Creates a new table and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void NewTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.NewTable(vm);
        }

        /// <summary>
        ///     Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="size">The size of the array that as to be created.</param>
        public void NewArray(int size)
        {
            _Api.NewArray(Vm, size);
        }

        /// <summary>
        ///     Creates a new array and pushes it in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="size">The size of the array that as to be created.</param>
        public void NewArray(IntPtr vm, int size)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.NewArray(vm, size);
        }

        public void NewClosure(SqFunction func, uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public void NewClosure(IntPtr vm, SqFunction func, uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public bool SetParamsCheck(int nParamsCheck, string typeMask)
        {
            throw new NotImplementedException();
        }

        public bool SetParamsCheck(IntPtr vm, int nParamsCheck, string typeMask)
        {
            throw new NotImplementedException();
        }

        public bool BindEnv(int index)
        {
            throw new NotImplementedException();
        }

        public bool BindEnv(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public void PushString(string value)
        {
            throw new NotImplementedException();
        }

        public void PushString(IntPtr vm, string value)
        {
            throw new NotImplementedException();
        }

        public void PushString(IntPtr value, int length)
        {
            throw new NotImplementedException();
        }

        public void PushString(IntPtr vm, IntPtr value, int length)
        {
            throw new NotImplementedException();
        }

        public void PushFloat(float value)
        {
            throw new NotImplementedException();
        }

        public void PushFloat(IntPtr vm, float value)
        {
            throw new NotImplementedException();
        }

        public void PushInteger(int value)
        {
            throw new NotImplementedException();
        }

        public void PushInteger(IntPtr vm, int value)
        {
            throw new NotImplementedException();
        }

        public void PushBool(bool value)
        {
            throw new NotImplementedException();
        }

        public void PushBool(IntPtr vm, bool value)
        {
            throw new NotImplementedException();
        }

        public void PushUserPointer(IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public void PushUserPointer(IntPtr vm, IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public void PushNull()
        {
            throw new NotImplementedException();
        }

        public void PushNull(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        public SqObjectType GetType(int index)
        {
            throw new NotImplementedException();
        }

        public SqObjectType GetType(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public int GetSize(int index)
        {
            throw new NotImplementedException();
        }

        public int GetSize(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool GetBase(int index)
        {
            throw new NotImplementedException();
        }

        public bool GetBase(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool InstanceOf()
        {
            throw new NotImplementedException();
        }

        public bool InstanceOf(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        public bool ToString(int index)
        {
            throw new NotImplementedException();
        }

        public bool ToString(IntPtr v, int index)
        {
            throw new NotImplementedException();
        }

        public bool ToBool(int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool ToBool(IntPtr vm, int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool GetString(int index, out string value)
        {
            throw new NotImplementedException();
        }

        public bool GetString(IntPtr vm, int index, out string value)
        {
            throw new NotImplementedException();
        }

        public bool GetInteger(int index, out int value)
        {
            throw new NotImplementedException();
        }

        public bool GetInteger(IntPtr vm, int index, out int value)
        {
            throw new NotImplementedException();
        }

        public bool GetFloat(int index, out float value)
        {
            throw new NotImplementedException();
        }

        public bool GetFloat(IntPtr vm, int index, out float value)
        {
            throw new NotImplementedException();
        }

        public bool GetBool(int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool GetBool(IntPtr vm, int index, out bool value)
        {
            throw new NotImplementedException();
        }

        public bool GetThread(int index, out IntPtr threadPtr)
        {
            throw new NotImplementedException();
        }

        public bool GetThread(IntPtr vm, int index, out IntPtr threadPtr)
        {
            throw new NotImplementedException();
        }

        public bool GetUserPointer(int index, out IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        public bool GetUserPointer(IntPtr vm, int index, out IntPtr userPtr)
        {
            throw new NotImplementedException();
        }

        public bool GetUserData(int index, out IntPtr data, out int typeTag)
        {
            throw new NotImplementedException();
        }

        public bool GetUserData(IntPtr vm, int index, out IntPtr data, out int typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SetTypeTag(int index, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SetTypeTag(IntPtr vm, int index, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool GetTypeTag(int index, out IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool GetTypeTag(IntPtr vm, int index, out IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public void SetReleaseHook(int index, SqReleaseHook hookFunc)
        {
            throw new NotImplementedException();
        }

        public void SetReleaseHook(IntPtr vm, int index, SqReleaseHook hookFunc)
        {
            throw new NotImplementedException();
        }

        public string GetScratchPad(int minSize)
        {
            throw new NotImplementedException();
        }

        public string GetScratchPad(IntPtr vm, int minSize)
        {
            throw new NotImplementedException();
        }

        public bool GetClosureInfo(int index, out uint nParams, out uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public bool GetClosureInfo(IntPtr vm, int index, out uint nParams, out uint nFreeVars)
        {
            throw new NotImplementedException();
        }

        public bool SetNativeClosureName(int index, string name)
        {
            throw new NotImplementedException();
        }

        public bool SetNativeClosureName(IntPtr vm, int index, string name)
        {
            throw new NotImplementedException();
        }

        public bool SetNativeClosureName(int index, IntPtr name)
        {
            throw new NotImplementedException();
        }

        public bool SetNativeClosureName(IntPtr vm, int index, IntPtr name)
        {
            throw new NotImplementedException();
        }

        public bool SetInstanceUp(int index, IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public bool SetInstanceUp(IntPtr vm, int index, IntPtr pointer)
        {
            throw new NotImplementedException();
        }

        public bool GetInstanceUp(int index, out IntPtr pointer, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool GetInstanceUp(IntPtr vm, int index, out IntPtr pointer, IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        public bool SetClassUdSize(int index, int udSize)
        {
            throw new NotImplementedException();
        }

        public bool SetClassUdSize(IntPtr vm, int index, int udSize)
        {
            throw new NotImplementedException();
        }

        public bool NewClass(bool hasBase)
        {
            throw new NotImplementedException();
        }

        public bool NewClass(IntPtr vm, bool hasBase)
        {
            throw new NotImplementedException();
        }

        public bool CreateInstance(int index)
        {
            throw new NotImplementedException();
        }

        public bool CreateInstance(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool SetAttributes(int index)
        {
            throw new NotImplementedException();
        }

        public bool SetAttributes(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool GetAttributes(int index)
        {
            throw new NotImplementedException();
        }

        public bool GetAttributes(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool GetClass(int index)
        {
            throw new NotImplementedException();
        }

        public bool GetClass(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public void WeakRef(int index)
        {
            throw new NotImplementedException();
        }

        public void WeakRef(IntPtr vm, int index)
        {
            throw new NotImplementedException();
        }

        public bool GetDefaultDelegate(SqObjectType objectType)
        {
            throw new NotImplementedException();
        }

        public bool GetDefaultDelegate(IntPtr vm, SqObjectType objectType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Object Manipulation

        /// <summary>
        ///     Pushes the current root table in the stack
        /// </summary>
        public void PushRootTable()
        {
            _Api.PushRootTable(Vm);
        }

        /// <summary>
        ///     Pushes the current root table in the stack
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void PushRootTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.PushRootTable(vm);
        }

        /// <summary>
        ///     Pushes the registry table in the stack.
        /// </summary>
        public void PushRegistryTable()
        {
            _Api.PushRegistryTable(Vm);
        }

        /// <summary>
        ///     Pushes the registry table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void PushRegistryTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.PushRegistryTable(vm);
        }

        /// <summary>
        ///     Pushes the current const table in the stack.
        /// </summary>
        public void PushConstTable()
        {
            _Api.PushConstTable(Vm);
        }

        /// <summary>
        ///     Pushes the current const table in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void PushConstTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.PushConstTable(vm);
        }

        /// <summary>
        ///     Pops a table from the stack and set it as root table.
        /// </summary>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetRootTable()
        {
            return _Api.SetRootTable(Vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and set it as root table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetRootTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.SetRootTable(vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and set it as const table.
        /// </summary>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetConstTable()
        {
            return _Api.SetConstTable(Vm) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and set it as const table.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetConstTable(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.SetConstTable(Vm) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool NewSlot(int index, bool bStatic)
        {
            return _Api.NewSlot(Vm, index, bStatic) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool NewSlot(IntPtr vm, int index, bool bStatic)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.NewSlot(vm, index, bStatic) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and delete the slot indexed by it
        ///     from the table at position idx in the stack, if the slot does not exits nothing happens.
        ///     <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool DeleteSlot(int index, bool pushVal)
        {
            return _Api.DeleteSlot(Vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and delete the slot indexed by it
        ///     from the table at position idx in the stack, if the slot does not exits nothing happens.
        ///     <remarks>Invoke the _delslot metamethod in the table delegate. It only works on tables.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool DeleteSlot(IntPtr vm, int index, bool pushVal)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.DeleteSlot(vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack.
        ///     <remarks>
        ///         This call will invoke the delegation system like a normal assignment, it only works on tables, arrays and
        ///         userdata.
        ///     </remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Set(int index)
        {
            return _Api.Set(Vm, index) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Set(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Set(vm, index) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Get(int index)
        {
            return _Api.Get(Vm, index) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Get(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Get(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at position idx in the stack, without
        ///     employing delegation or metamethods.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool RawGet(int index)
        {
            return _Api.RawGet(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key from the stack and performs a get operation on the object at position idx in the stack, without
        ///     employing delegation or metamethods.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool RawGet(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.RawGet(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack,
        ///     without employing delegation or metamethods.
        ///     <remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool RawSet(int index)
        {
            return _Api.RawSet(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a key and a value from the stack and performs a set operation on the object at position idx in the stack,
        ///     without employing delegation or metamethods.
        ///     <remarks>It only works on tables and arrays. if the function fails nothing will be pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target vm.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool RawSet(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.RawSet(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the
        ///     slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool RawDeleteSlot(int index, bool pushVal)
        {
            return _Api.RawDeleteSlot(Vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Deletes a slot from a table without employing the _delslot metamethod. pops a key from the stack and delete the
        ///     slot indexed by it from the table at position idx in the stack, if the slot does not exits nothing happens.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target table in the stack.</param>
        /// <param name="pushVal">If this param is true the function will push the value of the deleted slot.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool RawDeleteSlot(IntPtr vm, int index, bool pushVal)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.RawDeleteSlot(vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayAppend(int index)
        {
            return _Api.ArrayAppend(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and pushes it in the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayAppend(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ArrayAppend(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayPop(int index, bool pushVal)
        {
            return _Api.ArrayPop(Vm, index, pushVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the back of the array at the position idx in the stack.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="pushVal">If true the poped value is pushed on the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayPop(IntPtr vm, int index, bool pushVal)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ArrayPop(vm, index, pushVal) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayResize(int index, int newSize)
        {
            return _Api.ArrayResize(Vm, index, newSize) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayResize(IntPtr vm, int index, int newSize)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ArrayResize(vm, index, newSize) == SqResult.SqOk;
        }

        /// <summary>
        ///     Reverse an array in place.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayReverse(int index)
        {
            return _Api.ArrayReverse(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Reverse an array in place.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayReverse(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ArrayReverse(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Removes an item from an array.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayRemove(int index, int itemIndex)
        {
            return _Api.ArrayRemove(Vm, index, itemIndex) == SqResult.SqOk;
        }

        /// <summary>
        ///     Removes an item from an array.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="itemIndex">The index of the item in the array that has to be removed.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayRemove(IntPtr vm, int index, int itemIndex)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ArrayRemove(vm, index, itemIndex) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and inserts it in an array at the specified position.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayInsert(int index, int destPos)
        {
            return _Api.ArrayInsert(Vm, index, destPos) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and inserts it in an array at the specified position.
        ///     <remarks>Only works on arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target array in the stack.</param>
        /// <param name="destPos">The postion in the array where the item has to be inserted.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ArrayInsert(IntPtr vm, int index, int destPos)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ArrayInsert(vm, index, destPos) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        ///     <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetDelegate(int index)
        {
            return _Api.SetDelegate(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a table from the stack and sets it as delegate of the object at the position idx in the stack.
        ///     <remarks>To remove the delgate from an object is necessary to use null as delegate instead of a table.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetDelegate(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.SetDelegate(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool GetDelegate(int index)
        {
            return _Api.GetDelegate(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes the current delegate of the object at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool GetDelegate(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetDelegate(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Clone(int index)
        {
            return _Api.Clone(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clones the table, array or class instance at the position idx, clones it and pushes the new object in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Clone(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Clone(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetFreeVariable(int index, uint nVal)
        {
            return _Api.SetFreeVariable(Vm, index, nVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pops a value from the stack and sets it as free variable of the closure at the position idx in the stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <param name="nVal">0 based index of the free variable(relative to the closure).</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool SetFreeVariable(IntPtr vm, int index, uint nVal)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.SetFreeVariable(vm, index, nVal) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes in the stack the next key and value of an array, table or class slot. To start the iteration this function
        ///     expects a null value on top of the stack; at every call the function will substitute the null value with an
        ///     iterator and push key and value of the container slot. Every iteration the application has to pop the previous key
        ///     and value but leave the iterator(that is used as reference point for the next iteration). The function will fail
        ///     when all slots have been iterated(see Tables and arrays manipulation).
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Next(int index)
        {
            return _Api.Next(Vm, index) == SqResult.SqOk;
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
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Next(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Next(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes the object pointed by the weak reference at position idx in the stack.
        ///     <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool GetWeakRefVal(int index)
        {
            return _Api.GetWeakRefVal(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Pushes the object pointed by the weak reference at position idx in the stack.
        ///     <remarks>If the function fails, nothing is pushed in the stack.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target weak reference</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool GetWeakRefVal(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetWeakRefVal(vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clears all the element of the table/array at position idx in the stack.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Clear(int index)
        {
            return _Api.Clear(Vm, index) == SqResult.SqOk;
        }

        /// <summary>
        ///     Clears all the element of the table/array at position idx in the stack.
        ///     <remarks>Only works on tables and arrays.</remarks>
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="index">Index of the target object in the stack.</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool Clear(IntPtr vm, int index)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.Clear(vm, index) == SqResult.SqOk;
        }

        #endregion

        #region  calls

        public bool Call(int parameters, bool retVal, bool raiseError)
        {
            throw new NotImplementedException();
        }


        public bool Call(IntPtr vm, int parameters, bool retVal, bool raiseError)
        {
            throw new NotImplementedException();
        }

        public bool Resume(bool reVal, bool raiseError)
        {
            throw new NotImplementedException();
        }

        public bool Resume(IntPtr vm, bool reVal, bool raiseError)

        {
            throw new NotImplementedException();
        }

        public string GetLocal(uint level, uint index)

        {
            throw new NotImplementedException();
        }

        public string GetLocal(IntPtr vm, uint level, uint index)
        {
            throw new NotImplementedException();
        }

        public string GetFreeVariable(int idx, uint nval)
        {
            throw new NotImplementedException();
        }

        public string GetFreeVariable(IntPtr vm, int idx, uint nval)
        {
            throw new NotImplementedException();
        }

        public bool SqError(string errorText)
        {
            throw new NotImplementedException();
        }

        public bool SqError(IntPtr vm, string errorText)
        {
            throw new NotImplementedException();
        }

        public bool ThrowError(string errorText)
        {
            throw new NotImplementedException();
        }

        public bool ThrowError(IntPtr vm, string errorText)
        {
            throw new NotImplementedException();
        }

        public void ResetError()
        {
            throw new NotImplementedException();
        }

        public void ResetError(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        public void GetLastError()
        {
            throw new NotImplementedException();
        }

        public void GetLastError(IntPtr vm)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region  raw object handling

        public bool GetStackObj(int index, ref Sqobject obj)
        {
            return _Api.GetStackObj(Vm, index, ref obj) == SqResult.SqOk;
        }

        public bool GetStackObj(IntPtr vm, int index, ref Sqobject obj)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetStackObj(vm, index, ref obj) == SqResult.SqOk;
        }

        public void PushObject(Sqobject obj)
        {
            _Api.PushObject(Vm, obj);
        }

        public void PushObject(IntPtr vm, Sqobject obj)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.PushObject(vm, obj);
        }

        public void AddRef(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public void AddRef(IntPtr vm, ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public bool Release(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public bool Release(IntPtr vm, ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public void ResetObject(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public string ObjToString(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public bool ObjToBool(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public int ObjToInteger(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public float ObjToFloat(ref Sqobject obj)
        {
            throw new NotImplementedException();
        }

        public bool GetObjectTypeTag(ref Sqobject obj, out IntPtr typeTag)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GC

        /// <summary>
        ///     Runs the garbage collector and returns the number of reference cycles found(and deleted).
        ///     <para />
        ///     <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        public int CollectGarbage()
        {
            return _Api.CollectGarbage(Vm);
        }

        /// <summary>
        ///     Runs the garbage collector and returns the number of reference cycles found(and deleted).
        ///     <para />
        ///     <remarks>This function only works on garbage collector builds.</remarks>
        /// </summary>
        /// <returns>The number of reference cycles found(and deleted)</returns>
        public int CollectGarbage(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.CollectGarbage(vm);
        }

        #endregion

        #region Serialization

        /// <summary>
        ///     Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool WriteClosure(SqWriteFunc writeFunction, IntPtr up)
        {
            return _Api.WriteClosure(Vm, writeFunction, up) == SqResult.SqOk;
        }

        /// <summary>
        ///     Serialize(write) the closure on top of the stack, the desination is user defined through a write callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="writeFunction">Delegate of a write function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool WriteClosure(IntPtr vm, SqWriteFunc writeFunction, IntPtr up)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.WriteClosure(vm, writeFunction, up) == SqResult.SqOk;
        }

        /// <summary>
        ///     Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ReadClosure(SqReadFunc readFunction, IntPtr up)
        {
            return _Api.ReadClosure(Vm, readFunction, up) == SqResult.SqOk;
        }

        /// <summary>
        ///     Serialize (read) a closure and pushes it on top of the stack, the source is user defined through a read callback.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="readFunction">Delegate of a read function that will be invoked by the vm during the serialization.</param>
        /// <param name="up">Pointer that will be passed to each call to the write function</param>
        /// <returns>True if the call was succesfull, false if not.</returns>
        public bool ReadClosure(IntPtr vm, SqReadFunc readFunction, IntPtr up)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.ReadClosure(vm, readFunction, up) == SqResult.SqOk;
        }

        #endregion

        #region Mem Allocation

        /// <summary>
        ///     Allocates a memory new block.
        /// </summary>
        /// <param name="size">Size of the new memory block.</param>
        /// <returns>Pointer to the new memory block.</returns>
        public IntPtr Malloc(uint size)
        {
            return _Api.Malloc(size);
        }

        /// <summary>
        ///     Reallocates a existing memory block.
        /// </summary>
        /// <param name="pointer">A pointer to a previously allocated memory block.</param>
        /// <param name="oldSize">The old size of the memory block.</param>
        /// <param name="newSize">The new size of the memory block.</param>
        /// <returns>Pointer to the reallocated memory block.</returns>
        public IntPtr Realloc(IntPtr pointer, uint oldSize, uint newSize)
        {
            return _Api.Realloc(pointer, oldSize, newSize);
        }

        /// <summary>
        ///     Frees a previously allocated memory block.
        /// </summary>
        /// <param name="pointer">A Pointer to the previously allocated memory block.</param>
        /// <param name="size">Size of the previously allocated memory block.</param>
        public void Free(IntPtr pointer, uint size)
        {
            _Api.Free(pointer, size);
        }

        #endregion

        #region Debug

        /// <summary>
        ///     Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        public bool StackInfo(int level, out SqStackInfos stackInfo)
        {
            return _Api.StackInfos(Vm, level, out stackInfo) == SqResult.SqOk;
        }

        /// <summary>
        ///     Retrieve the calls stack informations of a ceratain level in the calls stack.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        /// <param name="level">Calls stack level.</param>
        /// <param name="stackInfo">The stack info.</param>
        /// <returns>True if the call was successfull, false if it failed.</returns>
        public bool StackInfo(IntPtr vm, int level, out SqStackInfos stackInfo)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.StackInfos(Vm, level, out stackInfo) == SqResult.SqOk;
        }

        /// <summary>
        ///     pops a closure from the stack an sets it as debug hook.
        ///     <para />
        ///     When a debug hook is set it overrides any previously set native or non native hooks.
        ///     <para />
        ///     If the hook is null the debug hook will be disabled.
        /// </summary>
        public void SetDebugHook()
        {
            _Api.SetDebugHook(Vm);
        }

        /// <summary>
        ///     pops a closure from the stack an sets it as debug hook.
        ///     <para />
        ///     When a debug hook is set it overrides any previously set native or non native hooks.
        ///     <para />
        ///     If the hook is null the debug hook will be disabled.
        /// </summary>
        /// <param name="vm">The target VM.</param>
        public void SetDebugHook(IntPtr vm)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            _Api.SetDebugHook(vm);
        }

        #endregion

        #region  stdlib

        /// <summary>
        ///     Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        public bool GetBlob(int index, out IntPtr pointer)
        {
            return _Api.GetBlob(Vm, index, out pointer) == SqResult.SqOk;
        }

        /// <summary>
        ///     Retrieve the pointer of a blob's payload from an arbitrary position in the stack.
        /// </summary>
        /// <param name="vm">A pointer to the target VM.</param>
        /// <param name="index">An index in the stack.</param>
        /// <param name="pointer">A pointer that will point to the blob's payload</param>
        /// <returns>True if successfull, false if not.</returns>
        public bool GetBlob(IntPtr vm, int index, out IntPtr pointer)
        {
            if (vm == IntPtr.Zero)
            {
              throw new ArgumentException("The vm argument must not be IntPtr.Zero.",nameof(vm));
            }

            return _Api.GetBlob(vm, index, out pointer) == SqResult.SqOk;
        }

        #endregion
    }
}