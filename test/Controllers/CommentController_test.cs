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
    public class CommentController_test
    {
        private ICommentRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            List<Comment> comment = new List<Comment>(){
            new Comment {commentid = 1, body = "body added for comment 1",
                    userid = 1, contentid = 1, shorttext = "slug-1" },
            new Comment {commentid = 2, body = "body added for comment 2",
                    userid = 2, contentid = 2, shorttext = "slug-2" },
            new Comment {commentid = 3, body = "body added for comment 3",
                    userid = 3, contentid = 3, shorttext = "slug-3" },
            new Comment {commentid = 4, body = "body added for comment 4",
                    userid = 4, contentid = 4, shorttext = "slug-4" },
            new Comment {commentid = 4, body = "body added for comment 5",
                    userid = 4, contentid = 5, shorttext = "slug-5" }
            };

            var mockrepo = new Mock<ICommentRepo>();

            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<Comment>()))
            .Callback<Comment>(a => comment.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(comment);

            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => comment
                        .Where(w => w.commentid == i).Single());

            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<Comment>()))
            .Callback<Comment>(p => comment.Where(w => w.commentid == p.commentid).First(a => { a.shorttext = p.shorttext; a.userid = p.userid; a.contentid = p.contentid; a.body = p.body; return true; }));

            // remove method

            mockrepo.Setup(m => m.Remove(It.IsAny<int>()))
            .Callback<int>(i => comment.Remove(comment.Where(w => w.commentid == i).First()));

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
        public void ShouldReturnCommentNameForID()
        {
            //arrange - act
            var thisComment = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual("slug-1", thisComment.shorttext);

        }
        [TestMethod]
        public void ShouldAddANewCommentToList()
        {
            //arrange
            var newitem = new Comment {commentid = 11, body = "body added for comment 11",
                    userid = 11, contentid = 11, shorttext = "slug-11" };

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(11);
            //assert
            Assert.AreEqual("slug-11", obj.shorttext);
        }
        [TestMethod]
        public void ShouldUpdateExistingComment()
        {
            //arrange
            var updatedItem = new Comment { commentid = 2, body = "test body from update comment", shorttext = "updated comment from update", userid = 2, contentid = 2 };

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(2);
            //assert

            Assert.AreEqual("updated comment from update", obj.shorttext);

        }
        [TestMethod]
        public void ShouldRemoveTheCommentByID()
        {
            //arrange - act
            _mockrepo.Remove(2);


            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() => _mockrepo.FindByID(2));

        }
    }

}