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
    public class UserRepo_Test
    {

        private static UserRepo _testRepo;
        private int _testid;

        [ClassInitialize]
        public static void ClassStart(TestContext context)
        {
            var config = GeneralHelpers.InitConfiguration();
            UserRepo a = new UserRepo(config);
            _testRepo = a;

 

        }
       
        [TestMethod]
        public void DoesUserRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(UserRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewUserBeCreated()
        {
            User a = new User()
            {
                userid= 99,
                username = "jordan patrick"           

            };
            _testRepo.Add(a);

            var insertedUser = _testRepo.FindAll().Where(w => w.username == a.username).First();
            _testid = insertedUser.userid;
            Assert.AreEqual(a.username, insertedUser.username);

        }
        [TestMethod]
        public void CanAUserBeUpdated()
        {
            var maxid = _testRepo.FindAll().Min(m => m.userid);
            User a = new User()
            {
                userid = maxid,
                username = $"noobmaster{maxid}",
                

            };
            _testRepo.Update(a);

            
            var updatedUser = _testRepo.FindByID(maxid);

            Assert.AreEqual(a.username, updatedUser.username);

        }
        [TestMethod]
        public void CanAUserBeFoundById()
        {
            var maxid = _testRepo.FindAll().Min(m => m.userid);
            var User = _testRepo.FindByID(maxid);

            Assert.AreEqual($"noobmaster{maxid}", User.username);

        }
        [TestMethod]
        public void CanAListOfUsersBeReturned()
        {
            var Users = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(Users, typeof(List<User>));
        }

        [TestMethod]
        public void CanTheUserBeDeleted()
        {
            _testRepo.Remove(_testid);
            var User = _testRepo.FindByID(_testid);

            var expectedUser = new User();
            Assert.IsNull(User);
        }


    }
}
