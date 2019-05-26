
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class User_Test
    {
        [TestMethod]
        public void DoesUserClassExists()
        {
            User a = new User();
            Assert.IsInstanceOfType(a, typeof(User));
        }
    }
}
