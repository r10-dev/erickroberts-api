
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class Author_Test
    {
        [TestMethod]
        public void DoesAuthorClassExists()
        {
            Author a = new Author();
            Assert.IsInstanceOfType(a, typeof(Author));
        }
    }
}
