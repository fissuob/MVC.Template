﻿using MvcTemplate.Data.Mapping;
using MvcTemplate.Objects;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcTemplate.Data.Core
{
    public class Context : DbContext
    {
        #region Administration

        protected DbSet<Account> Accounts { get; set; }

        protected DbSet<Role> Roles { get; set; }
        protected DbSet<Privilege> Privileges { get; set; }
        protected DbSet<RolePrivilege> RolePrivileges { get; set; }

        #endregion

        #region System

        protected DbSet<Log> Logs { get; set; }
        protected DbSet<AuditLog> AuditLogs { get; set; }

        #endregion

        static Context()
        {
            ObjectMapper.MapObjects();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
