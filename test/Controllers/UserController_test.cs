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
    public class UserController_test
    {
        private IUserRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            List<User> User = new List<User>(){
                new User {userid = 1, username ="erick roberts"} ,
                new User {userid = 2, username ="Jordan Patrick"} ,
                new User {userid = 3, username ="Anna Leigh"} ,
                new User {userid = 4, username ="Aaron Williams"} ,
            };

            var mockrepo = new Mock<IUserRepo>();

            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<User>()))
            .Callback<User>(a => User.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(User);

            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => User
                        .Where(w => w.userid == i).Single());

            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<User>()))
            .Callback<User>(p => User.Where(w => w.userid == p.userid).First(a =>
            {
                a.userid = p.userid; a.username = p.username; return true;
            }));

            // remove method

            mockrepo.Setup(m => m.Remove(It.IsAny<int>()))
            .Callback<int>(i => User.Remove(User.Where(w => w.userid == i).First()));

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
            var thisUser = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual("erick roberts", thisUser.username);

        }
        [TestMethod]
        public void ShouldAddANewUserToList()
        {
            //arrange
            var newitem = new User
            {
                userid = 12,username = "johnna sunshine"  
            };

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(12);
            //assert
            Assert.AreEqual("johnna sunshine", obj.username);
        }
        [TestMethod]
        public void ShouldUpdateExistingUser()
        {
            //arrange
            var updatedItem = new User
            {
                userid = 4,username = "evey kate"
            };

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(4);
            //assert

            Assert.AreEqual("evey kate", obj.username);

        }
        [TestMethod]
        public void ShouldRemoveTheUserByID()
        {
            //arrange - act
            _mockrepo.Remove(2);


            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() => _mockrepo.FindByID(2));

        }
    }

}