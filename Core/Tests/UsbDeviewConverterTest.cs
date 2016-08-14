using NUnit.Framework;
using Usbacc.Core.Converter;

namespace Usbacc.Core.Tests
{
    [TestFixture]
    public class UsbDeviewConverterTest
    {
        [Test]
        public void ConverterTest()
        {
            var converter = new UsbDeviewConverter();
            var result = converter.Convert("c:\\Work\\Import\\106-FEO-03704.xml");

            Assert.Greater(result.Count, 0);
        }
    }
}
