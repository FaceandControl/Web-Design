using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.IO;
using Microsoft.Extensions.DependencyModel;

namespace DAL
{
    public class AccountingSystemDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagDescription> TagDesctriptions { get; set; }

        public AccountingSystemDbContext(DbContextOptions<AccountingSystemDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AccountingSystemDbContext).Assembly);
        }

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(AccountingSystemDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");
        private static IList<Type> _entityTypeCache;

        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(BaseEntity)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }
    }
}
