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
    public class ContentRepo_Test
    {

        private static ContentRepo _testRepo;
        private int _testid;

        [ClassInitialize]
        public static void ClassStart(TestContext context)
        {
            var config = GeneralHelpers.InitConfiguration();
            ContentRepo a = new ContentRepo(config);
            _testRepo = a;

            var insert_value = a.FindAll().Where(w => w.slug == "new-post-from-test");
            if (insert_value.Count() > 0)
            {
                var id = insert_value.Where(w => w.slug == "new-post-from-test").First().contentid;

                a.Remove(id);
            }

            Content con = new Content()
            {
                contentid = 6,
                headerimage = "Some image from test _ updated",
                tabimage = "Some Name from test _ updated",
                slug = "new-post-from-test _ updated",
                body = "some awesome tesx _ updated",
                title = "some title added from test _ updated",
                authorid = 1,
                views = 1,
                stars = (float)4.5,
                published = true,
                staged = true,
                draft = true,
                created_on = DateTime.Now,
                published_on = DateTime.Now

            };
            _testRepo.Update(con);

        }
       
        [TestMethod]
        public void DoesContentRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(ContentRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewContentBeCreated()
        {
            Content a = new Content()
            {
                contentid = 99,
                headerimage = "Some image from test",
                tabimage = "Some Name from test",
                slug = "new-post-from-test",
                body = "some awesome tesx",
                title = "some title added from test",
                authorid = 1,

            };
            _testRepo.Add(a);

            var insertedContent = _testRepo.FindAll().Where(w => w.slug == a.slug).First();
            _testid = insertedContent.contentid;
            Assert.AreEqual(a.slug, insertedContent.slug);

        }
        [TestMethod]
        public void CanAContentBeUpdated()
        {
            var maxid = _testRepo.FindAll().Min(m => m.contentid);
            Content a = new Content()
            {
                contentid = maxid,
                headerimage = "Some image from test _ updated",
                tabimage = "Some Name from test _ updated",
                slug = $"new-post-from-test _ updated{maxid}",
                body = "some awesome tesx _ updated",
                title = "some title added from test _ updated",
                authorid = 1,
                views = 1,
                stars = (float)4.5,
                published = true,
                staged = true,
                draft = true,
                created_on = DateTime.Now,
                published_on = DateTime.Now

            };
            _testRepo.Update(a);

            
            var updatedContent = _testRepo.FindByID(maxid);

            Assert.AreEqual(a.slug, updatedContent.slug);

        }
        [TestMethod]
        public void CanAContentBeFoundById()
        {
            var Content = _testRepo.FindByID(6);

            Assert.AreEqual("new-post-from-test _ updated6", Content.slug);

        }
        [TestMethod]
        public void CanAListOfContentsBeReturned()
        {
            var Contents = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(Contents, typeof(List<Content>));
        }

        [TestMethod]
        public void CanTheContentBeDeleted()
        {
            _testRepo.Remove(_testid);
            var Content = _testRepo.FindByID(_testid);

            var expectedContent = new Content();
            Assert.IsNull(Content);
        }


    }
}