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
    public class AuthorRepo_Test
    {

        private AuthorRepo _testRepo;

        [TestInitialize]
        public void Setup()
        {
            var config = GeneralHelpers.InitConfiguration();
            AuthorRepo a = new AuthorRepo(config);
            _testRepo = a;
        }
        [TestMethod]
        public void DoesAuthorRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(AuthorRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewAuthorBeCreated()
        {
            Author a = new Author()
            {
                authorid = 99,
                authorimage = "Some image from test",
                authorname = "Some Name from test",
                roleid = 1
            };
            _testRepo.Add(a);

            var insertedAuthor = _testRepo.FindAll().Where(w => w.authorname == a.authorname).First();

            Assert.AreEqual(a.authorname, insertedAuthor.authorname);

        }
        [TestMethod]
        public void CanAnAuthorBeUpdated()
        {
            Author a = new Author()
            {
                authorid = 1,
                authorimage = "jordans picture",
                authorname = "jordan roberts",
                roleid = 10
            };
            _testRepo.Update(a);

            var updatedAuthor = _testRepo.FindByID(1);

            Assert.AreEqual(a.authorname, updatedAuthor.authorname);

        }

        [TestMethod]
        public void CanAnAuthorBeDeleted()
        {
            _testRepo.Remove(12);
            var author = _testRepo.FindByID(12);
            
            var expectedAuthor = new Author();
            Assert.IsNull(author);
        }

        [TestMethod]
        public void CanAnAuthorBeFoundById()
        {
            var author = _testRepo.FindByID(1);

            Assert.AreEqual("jordan roberts", author.authorname);

        }

        [TestMethod]
        public void CanAListOfAuthorsBeReturned()
        {
            var authors = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(authors,typeof(List<Author>));
        }
    }
}