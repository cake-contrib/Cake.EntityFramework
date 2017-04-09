using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Cake.Core;
using Cake.EntityFramework6.CakeTranslation;

using FluentAssertions;

using NSubstitute;

using Ploeh.AutoFixture;

using Xunit;

namespace Cake.EntityFramework6.Tests.CakeAliases
{
    public class Ef6AliasesFacts
    {
        private static readonly Fixture AutoFixture = new Fixture();
        private readonly ICakeContext _context;

        public Ef6AliasesFacts()
        {
            _context = Substitute.For<ICakeContext>();
        }

        public static List<object[]> Expressions => new List<object[]>
        {
            new object[] {Expression(x => x.AppConfigPath)},
            new object[] {Expression(x => x.AssemblyPath)},
            new object[] {Expression(x => x.ConfigurationClass)},
            new object[] {Expression(x => x.ConnectionProvider)},
            new object[] {Expression(x => x.ConnectionString)},
        };

        [Theory, MemberData(nameof(Expressions))]
        public void Empty_data_should_throw(Expression<Func<EfMigratorSettings, object>> expression)
        {
            var settings = AutoFixture.Build<EfMigratorSettings>()
                                      .With(expression, null)
                                      .Create();

            // Act
            Action action = () => _context.CreateEfMigrator(settings);

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Empty_settings_arg_throws()
        {
            EfMigratorSettings settings = null;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            Action action = () => _context.CreateEfMigrator(settings);

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Empty_context_should_throw()
        {
            ICakeContext context = null;

            // Act
            // ReSharper disable once ExpressionIsAlwaysNull
            Action action = () => context.CreateEfMigrator(null);

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        private static Expression<Func<EfMigratorSettings, object>> Expression(Expression<Func<EfMigratorSettings, object>> expression)
        {
            return expression;
        }
    }
}