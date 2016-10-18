using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2O.DotNet.Permission
{
    interface IRole
    {
        bool CheckPermission(IPermission permission);

        IEnumerable<IPermission> Permissions { get; }

        IEnumerable<IPermission> ExcludedPermissions { get; }

        IEnumerable<IRole> BaseRoles { get; }

        string Name { get; }
    }

    class Role : IRole
    {
        public bool CheckPermission(IPermission permission)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPermission> Permissions { get; }

        public IEnumerable<IPermission> ExcludedPermissions { get; }

        public IEnumerable<IRole> BaseRoles { get; }

        public string Name { get; }
    }
}
