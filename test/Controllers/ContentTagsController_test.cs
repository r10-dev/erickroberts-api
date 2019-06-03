using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using api.dbl.repo;
using test.helpers;
using api.models;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using api.Controllers;
using api.dbl.repo.interfaces;


namespace test.Controllers
{
    [TestClass]
    public class ContentTagsController_test
    {
        private IContentTagsRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            List<ContentTags> ContentTags = new List<ContentTags>(){
            new ContentTags {contenttagid = 1, tags = "body added for ContentTags 1",
                    url="sql-tags-temp - 1", contentid=1, description = "some description" },
            new ContentTags {contenttagid = 2, tags = "body added for ContentTags 2",
                    url="sql-tags-temp", contentid=2, description = "some description" },
            new ContentTags {contenttagid = 3, tags = "body added for ContentTags 3",
                    url="sql-tags-temp", contentid=3, description = "some description" },
            new ContentTags {contenttagid = 4, tags = "body added for ContentTags 4",
                    url="sql-tags-temp", contentid=4, description = "some description" },
            new ContentTags {contenttagid = 5, tags = "body added for ContentTags 5",
                    url="sql-tags-temp", contentid=5, description = "some description" }
            };

            var mockrepo = new Mock<IContentTagsRepo>();

            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<ContentTags>()))
            .Callback<ContentTags>(a => ContentTags.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(ContentTags);

            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => ContentTags
                        .Where(w => w.contenttagid == i).Single());

            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<ContentTags>()))
            .Callback<ContentTags>(p => ContentTags.Where(w => w.contenttagid == p.contenttagid).First(a => { a.url = p.url; a.contentid = p.contentid; a.description = p.description; return true; }));

            // remove method

            mockrepo.Setup(m => m.Remove(It.IsAny<int>()))
            .Callback<int>(i => ContentTags.Remove(ContentTags.Where(w => w.contenttagid == i).First()));

            _mockrepo = mockrepo.Object;


        }

        [TestMethod]
        public void ShouldReturnAListFromFindAll()
        {
            //arrange - act
            var list = _mockrepo.FindAll();

            //assert
            Assert.AreEqual(5, list.Count());

        }
        [TestMethod]
        public void ShouldReturnContentTagsNameForID()
        {
            //arrange - act
            var thisContentTags = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual("sql-tags-temp - 1", thisContentTags.url);

        }
        [TestMethod]
        public void ShouldAddANewContentTagsToList()
        {
            //arrange
            var newitem = new ContentTags {contenttagid = 11, url = "url slug for new tags 11",
                    contentid = 11, description = "slug-11" };

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(11);
            //assert
            Assert.AreEqual("slug-11", obj.description);
        }
        [TestMethod]
        public void ShouldUpdateExistingContentTags()
        {
            //arrange
            var updatedItem = new ContentTags { contenttagid = 2, url = "test url from update ContentTags", description = "updated ContentTags from update", contentid = 2};

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(2);
            //assert

            Assert.AreEqual("test url from update ContentTags", obj.url);

        }
        [TestMethod]
        public void ShouldRemoveTheContentTagsByID()
        {
            //arrange - act
            _mockrepo.Remove(2);


            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() => _mockrepo.FindByID(2));

        }
    }

}