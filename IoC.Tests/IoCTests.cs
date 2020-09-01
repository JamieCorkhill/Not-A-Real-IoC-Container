using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IoC.Tests
{
    [TestClass]
    public class IoCTests
    {
        [TestMethod]
        public void CanResolveType()
        {
            // Arrange
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();

            // Act
            var logger = ioc.Resolve<ILogger>();

            // Assert
            Assert.AreEqual(typeof(SqlServerLogger), logger.GetType());
        }

        [TestMethod]
        public void CanResolveTypesWithoutDefaultCtor()
        {
            // Arrange
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            // Act
            var repository = ioc.Resolve<IRepository<Employee>>();

            // Assert
            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void CanResolveConcreteType()
        {
            // Arrange
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlServerLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>));

            // Act
            var service = ioc.Resolve<InvoiceService>();

            // Assert
            Assert.IsNotNull(service);
        }
    }
}
