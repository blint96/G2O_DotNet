using System.Runtime.InteropServices;

namespace GothicOnline.G2.DotNet.Squirrel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SqStackInfos
    {
        public string funcname;
        public string source;
        public int line;
    }
}
