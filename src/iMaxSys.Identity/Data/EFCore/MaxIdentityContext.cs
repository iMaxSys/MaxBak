
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyModel;

using iMaxSys.Max.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace iMaxSys.Identity.Data.EFCore
{
    /// <summary>
    /// MaxContext
    /// </summary>
    public class MaxIdentityContext : DbContext
    {
        private readonly MaxOption _maxOption;

        //public static readonly LoggerFactory LoggerFactory =
        //new LoggerFactory(new[] { new DebugLoggerProvider() });

        public MaxIdentityContext(IOptions<MaxOption> options)
        {
            _maxOption = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseLoggerFactory(LoggerFactory);
            string connection = string.IsNullOrWhiteSpace(_maxOption.Identity.Connection) ? (string.IsNullOrWhiteSpace(_maxOption.Core.Connection) ? new DesignTimeMaxOptions().Value.Core.Connection : _maxOption.Core.Connection) : _maxOption.Identity.Connection;
            optionsBuilder.UseMySql(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyConfigurations(modelBuilder);
        }

        /// <summary>
        /// 注册映射配置
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        private void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            //映射Core模块配置
            var c = DependencyContext.Default.CompileLibraries.Where(c => c.Name.Contains("iMaxSys.Core")).FirstOrDefault();
            var a = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(c.Name));
            modelBuilder.ApplyConfigurationsFromAssembly(a);

            //映射本模块配置
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
