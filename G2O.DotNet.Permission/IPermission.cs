using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2O.DotNet.Permission
{
    interface IPermission
    {
        string Name { get; }

        string FullName { get; }

        IPermission Parent { get; }

        IEnumerable<IPermission> Children { get; }


        bool IsChildOf(IPermission permission);


        bool IsParentOf(IPermission permission);

    }
}
