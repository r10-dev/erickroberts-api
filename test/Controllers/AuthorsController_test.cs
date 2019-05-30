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

namespace test.Controllers
{
    [TestClass]
    public class AuthorsController_test
    {
        private IConfiguration _config;
        private ILogger _logger;
        
        public IAuthorRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            _logger = Mock.Of<ILogger<AuthorsController>>();
            _config = Mock.Of<IConfiguration>();
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
            //arrange
            var list = _mockrepo.FindAll();

            Assert.AreEqual(5, list.Count());
            
        }
    }

}