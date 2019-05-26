
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class Content_Test
    {
        [TestMethod]
        public void DoesContentClassExists()
        {
            Content a = new Content();
            Assert.IsInstanceOfType(a, typeof(Content));
        }
    }
}
