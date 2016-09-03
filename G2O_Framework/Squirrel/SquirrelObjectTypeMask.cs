// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquirrelObjectTypeMask.cs" company="Colony Online Project">
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
namespace GothicOnline.G2.DotNet.Squirrel.Squirrel
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