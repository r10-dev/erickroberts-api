
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.models;
namespace test.modeltests
{
    [TestClass]
    public class ContentTags_Test
    {
        [TestMethod]
        public void DoesContentTagsClassExists()
        {
            ContentTags a = new ContentTags();
            Assert.IsInstanceOfType(a, typeof(ContentTags));
        }
    }
}
