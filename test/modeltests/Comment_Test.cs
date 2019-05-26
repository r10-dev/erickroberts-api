
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class Comment_Test
    {
        [TestMethod]
        public void DoesCommentClassExists()
        {
            Comment a = new Comment();
            Assert.IsInstanceOfType(a, typeof(Comment));
        }
    }
}
