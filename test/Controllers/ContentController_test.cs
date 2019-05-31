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
    public class ContentController_test
    {
        private IContentRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            List<Content> content = new List<Content>(){
                new Content {contentid = 1, body = "body added for content 1",authorid = 1, slug = "url-3-1",  title= "title-1", headerimage = "hi-1", tabimage = "ti-1", published=true, staged=true, draft=false, created_on=DateTime.Parse("05/11/2019"), published_on = DateTime.Parse("06/11/2019"), views=3, stars=3.5F },

                new Content {contentid = 2, body = "body added for content 2",authorid = 2, slug = "url-3-2",  title= "title-2", headerimage = "hi-2", tabimage = "ti-2", published=true, staged=true, draft=false, created_on=DateTime.Parse("05/12/2019"), published_on = DateTime.Parse("06/12/2019"), views=3, stars=3.5F},


                new Content {contentid = 3, body = "body added for content 3",authorid = 3, slug = "url-3-3",  title= "title-3", headerimage = "hi-3", tabimage = "ti-3", published=true, staged=true, draft=false, created_on=DateTime.Parse("05/13/2019"), published_on = DateTime.Parse("06/13/2019") , views=3, stars=3.5F},

                new Content {contentid = 4, body = "body added for content 4",authorid = 4, slug = "url-3-4",  title= "title-4", headerimage = "hi-4", tabimage = "ti-4", published=true, staged=true, draft=false, created_on=DateTime.Parse("05/14/2019"), published_on = DateTime.Parse("06/14/2019"), views=3, stars=3.5F }

            };

            var mockrepo = new Mock<IContentRepo>();

            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<Content>()))
            .Callback<Content>(a => content.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(content);

            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => content
                        .Where(w => w.contentid == i).Single());

            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<Content>()))
            .Callback<Content>(p => content.Where(w => w.contentid == p.contentid).First(a =>
            {
                a.slug = p.slug; a.tabimage = p.tabimage;
                a.views = p.views; a.stars = p.stars;
                a.published = p.published; a.created_on = p.created_on; a.published_on = p.published_on; a.draft = p.draft;
                a.headerimage = p.headerimage; a.staged = p.staged; a.contentid = p.contentid; a.body = p.body; return true;
            }));

            // remove method

            mockrepo.Setup(m => m.Remove(It.IsAny<int>()))
            .Callback<int>(i => content.Remove(content.Where(w => w.contentid == i).First()));

            _mockrepo = mockrepo.Object;


        }

        [TestMethod]
        public void ShouldReturnAListFromFindAll()
        {
            //arrange - act
            var list = _mockrepo.FindAll();

            //assert
            Assert.AreEqual(4, list.Count());

        }
        [TestMethod]
        public void ShouldReturnAuthorNameForID()
        {
            //arrange - act
            var thisContent = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual("url-3-1", thisContent.slug);

        }
        [TestMethod]
        public void ShouldAddANewContentToList()
        {
            //arrange
            var newitem = new Content
            { contentid = 5, body = "body added for content 5", authorid = 5, slug = "url-3-5", title = "title-5", headerimage = "hi-5", tabimage = "ti-5", published = true, staged = true, draft = false, created_on = DateTime.Parse("05/15/2019"), published_on = DateTime.Parse("06/15/2019"), views = 44, stars = 4.5F };

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(5);
            //assert
            Assert.AreEqual("url-3-5", obj.slug);
        }
        [TestMethod]
        public void ShouldUpdateExistingContent()
        {
            //arrange
            var updatedItem = new Content {contentid = 4, body = "body updated for content 4",authorid = 4, slug = "url-updated-4",  title= "title-4", headerimage = "hi-4", tabimage = "ti-4", published=true, staged=true, draft=false, created_on=DateTime.Parse("05/14/2019"), published_on = DateTime.Parse("06/14/2019"), views=3, stars=3.5F };

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(4);
            //assert

            Assert.AreEqual("url-updated-4", obj.slug);

        }
        [TestMethod]
        public void ShouldRemoveTheContentByID()
        {
            //arrange - act
            _mockrepo.Remove(2);


            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() => _mockrepo.FindByID(2));

        }
    }

}