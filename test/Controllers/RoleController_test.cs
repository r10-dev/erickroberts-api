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
    public class RoleController_test
    {
        private IRoleRepo _mockrepo;

        [TestInitialize]
        public void Setup()
        {
            List<Role> Role = new List<Role>(){
                new Role {roleid = 1, title = "body added for Role 1",permissions= new List<RolePermissions>(){
                    new RolePermissions{keyid = "1", value = "json text"},
                    new RolePermissions{keyid = "2", value = "json text for permission 2"},
                }} ,
                new Role {roleid = 2, title = "body added for Role 2",permissions= new List<RolePermissions>(){
                    new RolePermissions{keyid = "3", value = "json text"},
                    new RolePermissions{keyid = "4", value = "json text for permission 4"},
                }},
               new Role {roleid = 3, title = "body added for Role 3",permissions= new List<RolePermissions>(){
                    new RolePermissions{keyid = "5", value = "json text"},
                    new RolePermissions{keyid = "6", value = "json text for permission 6"},
                }},
                new Role {roleid = 4, title = "body added for Role 4",permissions= new List<RolePermissions>(){
                    new RolePermissions{keyid = "7", value = "json text"},
                    new RolePermissions{keyid = "8", value = "json text for permission 8"},
                }},

            };

            var mockrepo = new Mock<IRoleRepo>();

            // add method
            mockrepo.Setup(m => m.Add(It.IsAny<Role>()))
            .Callback<Role>(a => Role.Add(a));
            // find all method
            mockrepo.Setup(m => m.FindAll()).Returns(Role);

            // find by id method
            mockrepo.Setup(m => m.FindByID(It.IsAny<int>()))
                        .Returns((int i) => Role
                        .Where(w => w.roleid == i).Single());

            // update method
            mockrepo.Setup(m => m.Update(It.IsAny<Role>()))
            .Callback<Role>(p => Role.Where(w => w.roleid == p.roleid).First(a =>
            {
                a.roleid = p.roleid; a.title = p.title;
                a.permissions = p.permissions; return true;
            }));

            // remove method

            mockrepo.Setup(m => m.Remove(It.IsAny<int>()))
            .Callback<int>(i => Role.Remove(Role.Where(w => w.roleid == i).First()));

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
            var thisRole = _mockrepo.FindByID(1);

            //act
            Assert.AreEqual("body added for Role 1", thisRole.title);

        }
        [TestMethod]
        public void ShouldAddANewRoleToList()
        {
            //arrange
            var newitem = new Role
            {
                roleid = 12,
                title = "body added for Role 12",
                permissions = new List<RolePermissions>(){
                    new RolePermissions{keyid = "10", value = "json text"},
                    new RolePermissions{keyid = "11", value = "json text for permission 11"},
                }
            };

            //act
            _mockrepo.Add(newitem);
            var obj = _mockrepo.FindByID(12);
            //assert
            Assert.AreEqual("body added for Role 12", obj.title);
        }
        [TestMethod]
        public void ShouldUpdateExistingRole()
        {
            //arrange
            var updatedItem = new Role
            {
                roleid = 4,
                title = "Title has been changed correctly",
                permissions = new List<RolePermissions>(){
                    new RolePermissions{keyid = "7", value = "json text"},
                    new RolePermissions{keyid = "8", value = "json text for permission 8"},
                }
            };

            //act
            _mockrepo.Update(updatedItem);
            var obj = _mockrepo.FindByID(4);
            //assert

            Assert.AreEqual("Title has been changed correctly", obj.title);

        }
        [TestMethod]
        public void ShouldRemoveTheRoleByID()
        {
            //arrange - act
            _mockrepo.Remove(2);


            //assert that there is not an id by the value of 2
            Assert.ThrowsException<InvalidOperationException>(() => _mockrepo.FindByID(2));

        }
    }

}