// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyResolver.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  Julian Vogel
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
namespace G2O.DotNet.EntryPoint
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class DependencyResolver
    {
        static bool is64BitProcess = IntPtr.Size == 8;

        private readonly List<string> ManagedDependencyDirs = new List<string>();

        private readonly List<string> NativeDependencies32Bit = new List<string>();

        private readonly List<string> NativeDependencies64Bit = new List<string>();

        private readonly List<IntPtr> NativeLibraries = new List<IntPtr>();

        Dictionary<string, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();

        public DependencyResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += this.CurrentDomain_AssemblyResolve;
        }


        public void AddManagedDependencyDir(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            this.ManagedDependencyDirs.Add(path);
        }

        public void AddNativeDependency32Bit(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given native x86 binary does not exist.", path);
            }

            if (is64BitProcess)
            {
                return;
            }

            this.NativeDependencies32Bit.Add(path);
            IntPtr ptr = LoadLibrary(path);
            if (ptr != IntPtr.Zero)
            {
                this.NativeLibraries.Add(ptr);
            }
        }

        public void AddNativeDependency64Bit(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(path));
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("The given native x86 binary does not exist.", path);
            }

            if (!is64BitProcess)
            {
                return;
            }

            this.NativeDependencies64Bit.Add(path);
            IntPtr ptr = LoadLibrary(path);
            if (ptr != IntPtr.Zero)
            {
                this.NativeLibraries.Add(ptr);
            }
        }

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly;
            if (this.loadedAssemblies.TryGetValue(args.Name, out assembly))
            {
                return assembly;
            }

            string dllName = args.Name.Split(',').First() + ".dll";
            foreach (var managedDependencyDir in this.ManagedDependencyDirs)
            {
                if (Directory.Exists(managedDependencyDir))
                {
                    foreach (var enumerateFile in Directory.EnumerateFiles(managedDependencyDir, "*.dll"))
                    {
                        if (enumerateFile.EndsWith(dllName))
                        {
                            var asm = Assembly.LoadFrom(enumerateFile);
                            this.loadedAssemblies.Add(args.Name, asm);
                            return asm;
                        }
                    }
                }
            }

            return null;
        }
    }
}