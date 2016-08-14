using NUnit.Framework;
using Usbacc.Core.Domain;
using Usbacc.Core.Repository;

namespace Usbacc.Core.Tests
{
    [TestFixture]
    public class ReportTests
    {
        [Test]
        public void GetAllReports()
        {
            var repository = new Repository<Report>();
            var result = repository.GetAll();

            Assert.Greater(result.Count, 0);
        }
    }
}
