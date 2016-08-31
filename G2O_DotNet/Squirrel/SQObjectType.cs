
namespace GothicOnline.G2.DotNet.Squirrel
{
    public enum SqObjectType
    {
        OtNull = (SquirrelObjectTypeMask.RtNull | SquirrelObjectTypeMask.SqobjectCanbefalse),
        OtInteger = (SquirrelObjectTypeMask.RtInteger | SquirrelObjectTypeMask.SqobjectNumeric | SquirrelObjectTypeMask.SqobjectCanbefalse),
        OtFloat = (SquirrelObjectTypeMask.RtFloat | SquirrelObjectTypeMask.SqobjectNumeric | SquirrelObjectTypeMask.SqobjectCanbefalse),
        OtBool = (SquirrelObjectTypeMask.RtBool | SquirrelObjectTypeMask.SqobjectCanbefalse),
        OtString = (SquirrelObjectTypeMask.RtString | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtTable = (SquirrelObjectTypeMask.RtTable | SquirrelObjectTypeMask.SqobjectRefCounted | SquirrelObjectTypeMask.SqobjectDelegable),
        OtArray = (SquirrelObjectTypeMask.RtArray | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtUserdata = (SquirrelObjectTypeMask.RtUserdata | SquirrelObjectTypeMask.SqobjectRefCounted | SquirrelObjectTypeMask.SqobjectDelegable),
        OtClosure = (SquirrelObjectTypeMask.RtClosure | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtNativeclosure = (SquirrelObjectTypeMask.RtNativeclosure | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtGenerator = (SquirrelObjectTypeMask.RtGenerator | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtUserpointer = SquirrelObjectTypeMask.RtUserpointer,
        OtThread = (SquirrelObjectTypeMask.RtThread | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtFuncproto = (SquirrelObjectTypeMask.RtFuncproto | SquirrelObjectTypeMask.SqobjectRefCounted), //internal usage only
        OtClass = (SquirrelObjectTypeMask.RtClass | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtInstance = (SquirrelObjectTypeMask.RtInstance | SquirrelObjectTypeMask.SqobjectRefCounted | SquirrelObjectTypeMask.SqobjectDelegable),
        OtWeakref = (SquirrelObjectTypeMask.RtWeakref | SquirrelObjectTypeMask.SqobjectRefCounted),
        OtOuter = (SquirrelObjectTypeMask.RtOuter | SquirrelObjectTypeMask.SqobjectRefCounted) //internal usage only
    }
}
