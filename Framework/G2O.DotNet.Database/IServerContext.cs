using Microsoft.Data.Entity;

namespace G2O.DotNet.Database
{
    using System;
    internal interface IServerContext : IDisposable
    {
        DbSet<AccountEntity> Accounts { get; set; }
        DbSet<CharacterEntity> Characters { get; set; }
        int DbVersion { get; set; }
        DbSet<ItemOwnershipEntity> ItemOwnership { get; set; }
        DbSet<PermissionOwnershipEntity> PermissionOwnerships { get; set; }
        DbSet<RoleInheritanceEntity> RoleInheritances { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
    }
}