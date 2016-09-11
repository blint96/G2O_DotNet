﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Composition.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Loader
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.Reflection;

    using GothicOnline.G2.DotNet.Framework.Plugin;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    internal class Composition
    {
        private readonly Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

        private CompositionContainer _container;

        [Import(typeof(ISquirrelApi))]
        private ISquirrelApi apiImport;

        [Import(typeof(IServer))]
        private IServer server;

        [ImportMany]
        private IEnumerable<Lazy<IPlugin>> plugins;

        public Composition(IntPtr vm, IntPtr apiPtr)
        {
            AppDomain.CurrentDomain.AssemblyResolve += this.CurrentDomain_AssemblyResolve;

            // An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            // Components
            string componentsFolder = Path.Combine(Environment.CurrentDirectory, "FrameworkComponents");
            Directory.CreateDirectory(componentsFolder);
            catalog.Catalogs.Add(new DirectoryCatalog(componentsFolder));
            foreach (
                var assemblyFile in Directory.EnumerateFiles(componentsFolder, "*.dll", SearchOption.AllDirectories))
            {
                var asm = Assembly.LoadFrom(assemblyFile);
                if (!this.assemblies.ContainsKey(asm.FullName))
                {
                    this.assemblies.Add(asm.FullName, asm);                     
                }
            }

            // Plugins
            string pluginFolder = Path.Combine(Environment.CurrentDirectory, "ManagedPlugins");
            Directory.CreateDirectory(pluginFolder);
            catalog.Catalogs.Add(new DirectoryCatalog(pluginFolder));
            foreach (var assemblyFile in Directory.EnumerateFiles(pluginFolder, "*.dll", SearchOption.AllDirectories))
            {
                var asm = Assembly.LoadFrom(assemblyFile);
                if (!this.assemblies.ContainsKey(asm.FullName))
                {
                    this.assemblies.Add(asm.FullName, asm);
                }
            }

            // Create the CompositionContainer with the parts in the catalog
            this._container = new CompositionContainer(catalog);
            this._container.ComposeExportedValue<IntPtr>("SquirrelVmHandle", vm);
            this._container.ComposeExportedValue<IntPtr>("SquirrelAPIHandle", apiPtr);

            this._container.ComposeParts(this);


        }

        public ISquirrelApi ApiImport => this.apiImport;

        public IServer Server => this.server;

        public IEnumerable<Lazy<IPlugin>> Plugins => this.plugins;

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly result;
            this.assemblies.TryGetValue(args.Name, out result);
            return result;
        }
    }
}