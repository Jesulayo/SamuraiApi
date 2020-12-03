using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SamuraiTest
{
    [TestClass()]
    public class InMemoryTest
    {

        [TestMethod()]
        public void CanInsertSamuraiIntoDatabase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using (var context = new SamuraiContext(builder.Options))
            {
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                
                Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
            }
        }

    }
}
