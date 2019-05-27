/* 
    This test conducts integration testing.
    Integration test are prefixed with comment.
*/


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.dbl.repo;
using test.helpers;
using api.models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace test.dbl.repotests
{

    [TestClass]
    public class ContentTagsRepo_Test
    {

        private static ContentTagsRepo _testRepo;
        private int _testid;

        [ClassInitialize]
        public static void ClassStart(TestContext context)
        {
            var config = GeneralHelpers.InitConfiguration();
            ContentTagsRepo a = new ContentTagsRepo(config);
            _testRepo = a;

 

        }
       
        [TestMethod]
        public void DoesContentTagsRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(ContentTagsRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewContentTagsBeCreated()
        {
            ContentTags a = new ContentTags()
            {
                contenttagid = 99,
                url = "Some image from test",
                contentid = 3,
                description = "new-post-from-test",
                tags = "some awesome tesx",           

            };
            _testRepo.Add(a);

            var insertedContentTags = _testRepo.FindAll().Where(w => w.url == a.url).First();
            _testid = insertedContentTags.contenttagid;
            Assert.AreEqual(a.url, insertedContentTags.url);

        }
        [TestMethod]
        public void CanAContentTagsBeUpdated()
        {
            var maxid = _testRepo.FindAll().Min(m => m.contenttagid);
            ContentTags a = new ContentTags()
            {
                contenttagid = maxid,
                url = $"Some image from test{maxid}",
                contentid = 3,
                description = "new-post-from-test",
                tags = "some awesome tesx", 

            };
            _testRepo.Update(a);

            
            var updatedContentTags = _testRepo.FindByID(maxid);

            Assert.AreEqual(a.url, updatedContentTags.url);

        }
        [TestMethod]
        public void CanAContentTagsBeFoundById()
        {
            var maxid = _testRepo.FindAll().Min(m => m.contenttagid);
            var ContentTags = _testRepo.FindByID(maxid);

            Assert.AreEqual($"Some image from test{maxid}", ContentTags.url);

        }
        [TestMethod]
        public void CanAListOfContentTagssBeReturned()
        {
            var ContentTagss = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(ContentTagss, typeof(List<ContentTags>));
        }

        [TestMethod]
        public void CanTheContentTagsBeDeleted()
        {
            _testRepo.Remove(_testid);
            var ContentTags = _testRepo.FindByID(_testid);

            var expectedContentTags = new ContentTags();
            Assert.IsNull(ContentTags);
        }


    }
}
