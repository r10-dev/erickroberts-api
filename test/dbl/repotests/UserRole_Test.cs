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
    public class UserRoleRepo_Test
    {

        private static UserRoleRepo _testRepo;
        private int _testid;

        [ClassInitialize]
        public static void ClassStart(TestContext context)
        {
            var config = GeneralHelpers.InitConfiguration();
            UserRoleRepo a = new UserRoleRepo(config);
            _testRepo = a;

 

        }
       
        [TestMethod]
        public void DoesUserRoleRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(UserRoleRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewUserRoleBeCreated()
        {
            UserRole a = new UserRole()
            {
                userroleid= 99,
                userid = 10          

            };
            _testRepo.Add(a);

            var insertedUserRole = _testRepo.FindAll().Where(w => w.userid == a.userid).First();
            _testid = insertedUserRole.userroleid;
            Assert.AreEqual(a.userid, insertedUserRole.userid);

        }
        [TestMethod]
        public void CanAUserRoleBeUpdated()
        {
            var maxid = _testRepo.FindAll().Min(m => m.userroleid);
            UserRole a = new UserRole()
            {
                userroleid = maxid,
                userid = 100 + maxid,
                

            };
            _testRepo.Update(a);

            
            var updatedUserRole = _testRepo.FindByID(maxid);

            Assert.AreEqual(a.userid, updatedUserRole.userid);

        }
        [TestMethod]
        public void CanAUserRoleBeFoundById()
        {
            var maxid = _testRepo.FindAll().Min(m => m.userroleid);
            var UserRole = _testRepo.FindByID(maxid);

            Assert.AreEqual(100+maxid, UserRole.userid);

        }
        [TestMethod]
        public void CanAListOfUserRolesBeReturned()
        {
            var UserRoles = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(UserRoles, typeof(List<UserRole>));
        }

        [TestMethod]
        public void CanTheUserRoleBeDeleted()
        {
            _testRepo.Remove(_testid);
            var UserRole = _testRepo.FindByID(_testid);

            var expectedUserRole = new UserRole();
            Assert.IsNull(UserRole);
        }


    }
}
