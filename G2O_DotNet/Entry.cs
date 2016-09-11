// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Entry.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Loader
{
    using System;
    using System.Configuration;

    using GothicOnline.G2.DotNet.Character;
    using GothicOnline.G2.DotNet.Loader.Squirrel;
    using GothicOnline.G2.DotNet.Server;
    using GothicOnline.G2.DotNet.Squirrel;

    class Entry
    {
        /// <summary>
        ///     Entry Point of the interface dll
        /// </summary>
        static void Main(IntPtr vm, IntPtr apiPtr)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                ISquirrelApi api = new SquirrelApi(vm, apiPtr);
                IServer server = new Server(api);
                Console.WriteLine(server.Description);
                server.Description = "Hallo";
                Console.WriteLine(server.Description);
                Console.WriteLine(server.World);
                server.Initialize += Server_Initialize;
                server.Clients.ClientConnect += Clients_ClientConnect;
                server.Clients.ClientDisconnect += Clients_ClientDisconnect;
                server.SendMessageToAll(255, 255, 255, "Blabla");
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("[G2ONet]Managed Code loaded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private static void Clients_ClientDisconnect(object sender, Client.ClientDisconnectedEventArgs e)
        {
            Console.WriteLine("Test client disconnected");
        }

        private static void Clients_ClientConnect(object sender, Client.ClientConnectedEventArgs e)
        {
            Console.WriteLine("Test client connected");
        }

        private static void Server_Initialize(object sender, ServerInitializedEventArgs e)
        {
            Console.WriteLine("onInit was called.");
        }
    }
}