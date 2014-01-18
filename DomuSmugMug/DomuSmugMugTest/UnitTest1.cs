using System;
using NUnit.Framework;

namespace DomuSmugMugTest
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void PingSmugMug()
        {
            var result = DomuSmugMug.MainWindow.PingSmugMug();
            Assert.IsTrue(DomuSmugMug.MainWindow.PingSmugMug());
        }
    }
}
