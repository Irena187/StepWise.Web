using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
namespace StepWise.Services.Tests
{
    public static class TestHelpers
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();

            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            dbSetMock.Setup(d => d.AddAsync(It.IsAny<T>(), default))
                .ReturnsAsync((T t, System.Threading.CancellationToken _) =>
                {
                    var list = data as IList<T>;
                    list?.Add(t);
                    return new Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<T>(null!); 
                });

            return dbSetMock;
        }
    }

}