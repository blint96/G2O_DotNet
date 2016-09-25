﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="Colony Online Project">
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
namespace G2OColony.DemoPlugin
{
    using System;
    using System.ComponentModel.Composition;

    using GothicOnline.G2.DotNet.Framework.Plugin;
    using GothicOnline.G2.DotNet.ServerApi.Server;

    [Export(typeof(IPlugin))]
    public class DemoPlugin : IPlugin
    {
        [ImportingConstructor]
        public DemoPlugin([Import]IServer server)
        {
            server.Initialize += this.ServerInitialize;
            server.Clients.ClientConnect += Clients_ClientConnect;
        }

        private void Clients_ClientConnect(object sender, GothicOnline.G2.DotNet.ServerApi.Client.ClientConnectedEventArgs e)
        {
            e.NewClient.CommandReceived += this.NewClient_CommandReceived;
        }

        private void NewClient_CommandReceived(object sender, GothicOnline.G2.DotNet.ServerApi.Client.CommandReceivedEventArgs e)
        {
          Console.WriteLine($"Command was send {e.Command}");
        }

        private void ServerInitialize(object sender, ServerInitializedEventArgs e)
        {
            Console.WriteLine("DemoPlugin loaded");
            Console.WriteLine(e.Server.Description);
            Console.WriteLine(e.Server.Time);
        }

        public void ServerShutdown()
        {
            Console.WriteLine("Shuting down demo plugin");
        }
    }
}