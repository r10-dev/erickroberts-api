
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class UserRole_Test
    {
        [TestMethod]
        public void DoesUserRoleClassExists()
        {
            UserRole a = new UserRole();
            Assert.IsInstanceOfType(a, typeof(UserRole));
        }
    }
}
