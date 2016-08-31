using System;
using GothicOnline.G2.DotNet.Squirrel;

namespace GothicOnline.G2.DotNet
{
    class Entry
    {
        /// <summary>
        /// Entry Point of the interface dll
        /// </summary>
        static void Main(IntPtr vm, IntPtr apiPtr)
        {
            try
            {

                Console.ForegroundColor = ConsoleColor.Red;
                ISquirrelApi api = new SquirrelApi(vm, apiPtr);


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[G2OMonoLoader]Managed Code loaded!");

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
    }
}
