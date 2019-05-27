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
    public class CommentRepo_Test
    {

        private CommentRepo _testRepo;

        [TestInitialize]
        public void Setup()
        {
            var config = GeneralHelpers.InitConfiguration();
            CommentRepo a = new CommentRepo(config);
            _testRepo = a;
        }
        [TestMethod]
        public void DoesCommentRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(CommentRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewCommentBeCreated()
        {
            Comment a = new Comment()
            {
                commentid = 99,
                shorttext = "Some image from test",
                body = "Some Name from test",
                userid = 1,
                contentid = 1
            };
            _testRepo.Add(a);

            var insertedComment = _testRepo.FindAll().Where(w => w.shorttext == a.shorttext).First();

            Assert.AreEqual(a.shorttext, insertedComment.shorttext);

        }
        [TestMethod]
        public void CanAnCommentBeUpdated()
        {
            var maxid = _testRepo.FindAll().Min(m => m.commentid);
            Comment a = new Comment()
            {
                commentid = maxid,
                shorttext = "Some image from test",
                body = "Some Name from test",
                userid = 1,
                contentid = 1
            };
            _testRepo.Update(a);

            var updatedComment = _testRepo.FindByID(maxid);

            Assert.AreEqual(a.shorttext, updatedComment.shorttext);

        }

        [TestMethod]
        public void CanAnCommentBeDeleted()
        {
            var maxid = (int)_testRepo.FindAll().Max(m=>m.commentid);
            _testRepo.Remove(maxid);
            var Comment = _testRepo.FindByID(maxid);
            
            var expectedComment = new Comment();
            Assert.IsNull(Comment);
        }

        [TestMethod]
        public void CanAnCommentBeFoundById()
        {
            var Comment = _testRepo.FindByID(1);

            Assert.AreEqual("Some image from test", Comment.shorttext);

        }

        [TestMethod]
        public void CanAListOfCommentsBeReturned()
        {
            var Comments = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(Comments,typeof(List<Comment>));
        }
    }
}