using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVLCarSystemApp.DataAccessImplementations;
using AVLCarSystemApp.DataContexts;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject
{
    public class InMemoryCarSystemTestBase : IDisposable
    {
        protected readonly CarSystemContext Context;

        public InMemoryCarSystemTestBase()
        {
            var options = new DbContextOptionsBuilder<CarSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new CarSystemContext(options);
            Context.Database.EnsureCreated();
        }


        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
