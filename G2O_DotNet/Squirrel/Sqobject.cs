using System.Runtime.InteropServices;

namespace GothicOnline.G2.DotNet.Squirrel
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Sqobject
    {
        public SqObjectType Type;
        public SqObjectValue UnValue;
    }
}
