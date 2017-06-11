using Cake.EntityFramework.Interfaces;
using Cake.EntityFramework.Migrator;
using Cake.EntityFramework.TestProject.SqlServer;
using Cake.EntityFramework.Tests.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Cake.EntityFramework.Tests.Migrator.SqlServer
{
    public class RemoteMigrationsFacts : IDisposable
    {
        private readonly ITestOutputHelper _logHelper;

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly ILogger _mockLogger;
        private SchoolContext _DbContext;
        private readonly IEfMigrator _migrator;

        public RemoteMigrationsFacts(ITestOutputHelper logHelper)
        {
            _logHelper = logHelper;
            _mockLogger = new MockLogger(logHelper);
            _migrator = new EfMigrator(SqlServerFactConstants.DdlPath, SqlServerFactConstants.ConfigName, SqlServerFactConstants.AppConfig,
                SqlServerFactConstants.InstanceConnectionString, SqlServerFactConstants.ConnectionProvider, _mockLogger, false);
        }       

        public void Dispose()
        {
        }
    }
}