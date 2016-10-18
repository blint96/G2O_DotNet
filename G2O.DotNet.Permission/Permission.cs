using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2O.DotNet.Permission
{
    class Permission : IPermission
    {
        private readonly List<IPermission> children;

        public string Name { get; }

        public string FullName => this.Parent != null ? $"{this.Parent.FullName}.{this.Name}" : this.Name;

        public IPermission Parent { get; }

        public IEnumerable<IPermission> Children => this.children.AsReadOnly();

        public override string ToString()
        {
            return this.FullName;
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class. The new permission is a direct child of the Root permission
        /// </summary>
        /// <param name="name">The name of the new permission</param>
        public Permission(string name) : this(name, RootPermission) { }


        /// <summary>
        /// Initializes the Root permission.
        /// </summary>
        private Permission()
        {
            this.Name = "*";
        }


        /// <summary>
        /// Gets the Root permission.
        /// <remarks>The Root permission is the parent of all permissions.</remarks>
        /// </summary>
        private static IPermission RootPermission { get; } = new Permission();


        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="name">The name of the new permission</param>
        /// <param name="parent">The parent <see cref="Permission"/> of the new <see cref="Permission"/>.</param>
        public Permission(string name, IPermission parent)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (name.All(char.IsLetterOrDigit))
            {
                throw new ArgumentException("The Permission name must only contain letters and or digits.", nameof(name));
            }
            //Check if a permission with this name was already created.
            if (ExistingPermissions.Contains($"{parent.FullName}.{name}"))
            {
                throw new ArgumentException("A permission with this name does allready exist", nameof(name));
            }

            this.Name = name;
            this.Parent = parent;
            ExistingPermissions.Add(this.FullName);

            //Add a reference to the parents children list.
            var parentPerm = parent as Permission;
            parentPerm?.children?.Add(this);
        }

        /// <summary>
        /// Stores the full names of all <see cref="Permission"/> objects that where created in the active <see cref="AppDomain"/>.
        /// </summary>
        private static readonly HashSet<string> ExistingPermissions = new HashSet<string>();


        public bool IsChildOf(IPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            IPermission current = this.Parent;
            while (current != null)
            {
                if (current == permission)
                {
                    return true;
                }
                current = current.Parent;
            }
            return false;
        }

        /// <summary>
        /// Search for a existing instance of <see cref="IPermission"/> by its name.
        /// <remarks>This method should only be used on initialization because it scanns the whole Permission tree.</remarks>
        /// </summary> 
        /// <param name="permissionName">Name of the permission that should be found</param>
        /// <returns></returns>
        public static IPermission GetNamedPermission(string permissionName)
        {
            if (string.IsNullOrEmpty(permissionName))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(permissionName));
            }
            //Check if a permission with this name was already created.
            if (!ExistingPermissions.Contains(permissionName))
            {
                throw new ArgumentException("A permission with this name does not exist.", nameof(permissionName));
            }

            Queue<IPermission> permissions = new Queue<IPermission>(RootPermission.Children);
            while (permissions.Count > 0)
            {
                IPermission current = permissions.Dequeue();
                if (current.FullName.Equals(permissionName, StringComparison.InvariantCulture))
                {
                    return current;
                }
                //Do not search in branches of the permission tree that do not even share a part oft the name with the searched permission.
                foreach (var permission in current.Children.Where(child => permissionName.Contains(child.FullName)))
                {
                    permissions.Enqueue(permission);
                }
            }
            throw new Exception("Permission object not found");
        }

        public bool IsParentOf(IPermission permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            return permission.IsChildOf(this);
        }
    }
}
