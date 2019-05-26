
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class Role_Test
    {
        [TestMethod]
        public void DoesRoleClassExists()
        {
            Role a = new Role();
            Assert.IsInstanceOfType(a, typeof(Role));
        }
    }
}
