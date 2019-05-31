/* 
    This test conducts unit tests....
*/


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
    public class AuthorsController_test
    {
        public IAuthorRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            
            IList<Author> author = new List<Author>{
               new Author{authorid = 1, authorimage = "ericks image", authorname="erick roberts", userid = 1},
               new Author{authorid = 2, authorimage = "sams image", authorname="Sam roberts", userid = 2},
               new Author{authorid = 3, authorimage = "Annas image", authorname="anna roberts", userid = 3},
               new Author{authorid = 4, authorimage = "anna image", authorname="aaron roberts", userid = 4},
               new Author{authorid = 5, authorimage = "luke image", authorname="luke roberts", userid = 5}
                };

            var mockrepo = new Mock<IAuthorRepo>();
            
            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<Author>()))
            .Callback<Author>(a => author.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(author);
            
            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => author
                        .Where(w=>w.authorid == i).Single());
            
            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<Author>()))
            .Callback<Author>(p => author.Where(w=>w.authorid == p.authorid).First(a => {a.authorimage = p.authorimage; a.authorname = p.authorname; return true;}));

            // remove method

            mockrepo.Setup(m=>m.Remove(It.IsAny<int>()))
            .Callback<int>(i => author.Remove(author.Where(w=>w.authorid == i).First()));
            
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
        public void ShouldReturnAuthorNameForID()
        {
           //arrange - act
            var thisAuthor = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual("erick roberts", thisAuthor.authorname);

        }
        [TestMethod]
        public void ShouldAddANewAuthorToList()
        {
            //arrange
            var newitem = new Author{authorid = 150, authorimage = "test image from add new", authorname="newly added author", userid = 150};

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(150);
            //assert
            Assert.AreEqual("newly added author", obj.authorname);
        }
        [TestMethod]
        public void ShouldUpdateExistingAuthor()
        {
            //arrange
            var updatedItem = new Author{authorid = 2, authorimage = "test image from update author", authorname="updated author from update", userid = 2};

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(2);
            //assert

            Assert.AreEqual("updated author from update", obj.authorname);

        }
        [TestMethod]
        public void ShouldRemoveTheAuthorByID()
        {
            //arrange - act
            _mockrepo.Remove(2);
            
            
            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() =>_mockrepo.FindByID(2));
           
        }
    }

}