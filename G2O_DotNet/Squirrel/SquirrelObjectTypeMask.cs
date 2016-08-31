﻿namespace GothicOnline.G2.DotNet.Squirrel
{
    public enum SquirrelObjectTypeMask
    {
        SqobjectRefCounted = 0x08000000,
        SqobjectNumeric = 0x04000000,
        SqobjectDelegable = 0x02000000,
        SqobjectCanbefalse = 0x01000000,
        RtNull = 0x00000001,
        RtInteger = 0x00000002,
        RtFloat = 0x00000004,
        RtBool = 0x00000008,
        RtString = 0x00000010,
        RtTable = 0x00000020,
        RtArray = 0x00000040,
        RtUserdata = 0x00000080,
        RtClosure = 0x00000100,
        RtNativeclosure = 0x00000200,
        RtGenerator = 0x00000400,
        RtUserpointer = 0x00000800,
        RtThread = 0x00001000,
        RtFuncproto = 0x00002000,
        RtClass = 0x00004000,
        RtInstance = 0x00008000,
        RtWeakref = 0x00010000,
        RtOuter = 0x00020000,
    }
}
