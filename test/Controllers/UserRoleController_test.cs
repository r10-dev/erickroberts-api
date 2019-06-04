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
    public class UserRoleController_test
    {
        private IUserRoleRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            List<UserRole> UserRole = new List<UserRole>(){
                new UserRole {userroleid = 1, userid = 1} ,
                new UserRole {userroleid = 2, userid = 2} ,
                new UserRole {userroleid = 3, userid = 3} ,
                new UserRole {userroleid = 4, userid = 4} ,
            };

            var mockrepo = new Mock<IUserRoleRepo>();

            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<UserRole>()))
            .Callback<UserRole>(a => UserRole.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(UserRole);

            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => UserRole
                        .Where(w => w.userroleid == i).Single());

            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<UserRole>()))
            .Callback<UserRole>(p => UserRole.Where(w => w.userroleid == p.userroleid).First(a =>
            {
                a.userroleid = p.userroleid; a.userid = p.userid; return true;
            }));

            // remove method

            mockrepo.Setup(m => m.Remove(It.IsAny<int>()))
            .Callback<int>(i => UserRole.Remove(UserRole.Where(w => w.userroleid == i).First()));

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
            var thisUserRole = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual(1, thisUserRole.userid);

        }
        [TestMethod]
        public void ShouldAddANewUserRoleToList()
        {
            //arrange
            var newitem = new UserRole
            {
                userroleid = 12,userid = 12  
            };

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(12);
            //assert
            Assert.AreEqual(12, obj.userid);
        }
        [TestMethod]
        public void ShouldUpdateExistingUserRole()
        {
            //arrange
            var updatedItem = new UserRole
            {
                userroleid = 4,userid = 8
            };

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(4);
            //assert

            Assert.AreEqual(8, obj.userid);

        }
        [TestMethod]
        public void ShouldRemoveTheUserRoleByID()
        {
            //arrange - act
            _mockrepo.Remove(2);


            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() => _mockrepo.FindByID(2));

        }
    }

}