// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServerContext.cs" company="Colony Online Project">
// -
// Copyright (C) 2016  Julian Vogel
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// -
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// -
// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.
// -
// </copyright>
// <summary>
// </summary>
//  -------------------------------------------------------------------------------------------------------------------
namespace G2O.DotNet.Database
{

    using Microsoft.Data.Entity;
    using Microsoft.Data.Sqlite;

    internal class ServerContext : DbContext, IServerContext
    {
        private const string DbVersionPragma = "PRAGMA user_version";

        public DbSet<AccountEntity> Accounts { get; set; }

        public DbSet<CharacterEntity> Characters { get; set; }

        /// <summary>
        ///     Gets or sets the database UserVersion.
        /// </summary>
        public int DbVersion
        {
            get
            {
                using (var connection = this.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = DbVersionPragma;
                        return (int)command.ExecuteScalar();
                    }
                }
            }

            set
            {
                using (var connection = this.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"{DbVersionPragma}={value}";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public DbSet<ItemOwnershipEntity> ItemOwnership { get; set; }

        public DbSet<PermissionOwnershipEntity> PermissionOwnerships { get; set; }

        public DbSet<RoleInheritanceEntity> RoleInheritances { get; set; }

        public DbSet<RoleEntity> Roles { get; set; }

        public override void Dispose()
        {
            this.SaveChanges();
            base.Dispose();
        }

        // This method connects the context with the database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Framework.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}