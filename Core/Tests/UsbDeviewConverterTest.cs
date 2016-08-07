using NUnit.Framework;
using UsbAcc.Core.Converter;

namespace UsbAcc.Core.Tests
{
    [TestFixture]
    public class UsbDeviewConverterTest
    {
        [Test]
        public void ConverterTest()
        {
            var converter = new UsbDeviewReport();
            var result = converter.Convert("C:\\Distributive\\USBDeview\\WORKPC.xml");

            Assert.Greater(result.Count, 0);
        }
    }
}
