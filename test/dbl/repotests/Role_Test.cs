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
using Newtonsoft.Json;
using System.Diagnostics;


namespace test.dbl.repotests
{

    [TestClass]
    public class RoleRepo_Test
    {

        private static RoleRepo _testRepo;
        private int _testid;

        [ClassInitialize]
        public static void ClassStart(TestContext context)
        {
            var config = GeneralHelpers.InitConfiguration();
            RoleRepo a = new RoleRepo(config);
            _testRepo = a;

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            // or Trace.Listeners.Add(new ConsoleTraceListener());
            Trace.WriteLine("Starting Role Repo Test");

           

        }
        [TestMethod]
        public void DoesThePermissionDeserializationReturnJson()
        {
            Role a = new Role()
            {
                roleid = 99,
                title = "some text that came from testing",
                permissions = new List<RolePermissions>(),
            };

            a.permissions.Add(new RolePermissions()
            {
                keyid = "Some Role Name",
                value = "Some value to the role name"
            }
            );
            a.permissions.Add(new RolePermissions()
            {
                keyid = "Some other Role Name",
                value = "Some other value to the role name"
            }
            );

            var expectedJson = "[{\"keyid\":\"Some Role Name\",\"value\":\"Some value to the role name\"},{\"keyid\":\"Some other Role Name\",\"value\":\"Some other value to the role name\"}]";

            var result = _testRepo.ConvertPermissions(a.permissions);
            Assert.AreEqual(expectedJson, result);

        }
        [TestMethod]
        public void DoesRoleRepoClassExists()
        {

            Assert.IsInstanceOfType(_testRepo, typeof(RoleRepo));
        }
        //test each to the crud statements
        [TestMethod]
        public void CanANewRoleBeCreated()
        {
            Role a = new Role()
            {
                roleid = 99,
                title = "some text that came from testing",
                permissions = new List<RolePermissions>(),
            };

            a.permissions.Add(new RolePermissions()
            {
                keyid = "Some Role Name",
                value = "Some value to the role name"
            }
                );
            a.permissions.Add(new RolePermissions()
            {
                keyid = "Some other Role Name",
                value = "Some other value to the role name"
            }
            );

            _testRepo.Add(a);

            var insertedRole = _testRepo.FindAll().Where(w => w.title == a.title).First();
            _testid = insertedRole.roleid;
            Assert.AreEqual(a.title, insertedRole.title);

        }
        [TestMethod]
        public void CanARoleBeUpdated()
        {
            var maxid = _testRepo.FindAll().Min(m => m.roleid);

            Role a = new Role()
            {
                roleid = maxid,
                title = $"Some text from text from test{maxid}",

                permissions = new List<RolePermissions>(),
            };

            a.permissions.Add(new RolePermissions()
            {
                keyid = "Some Role Name",
                value = "Some value to the role name"
            }
               );
            a.permissions.Add(new RolePermissions()
            {
                keyid = "Some other Role Name",
                value = "Some other value to the role name"
            }
            );
            _testRepo.Update(a);


            var updatedRole = _testRepo.FindByID(maxid);

            Assert.AreEqual(a.title, updatedRole.title);

        }
        [TestMethod]
        public void CanARoleBeFoundById()
        {
            var maxid = _testRepo.FindAll().Min(m => m.roleid);
            var Role = _testRepo.FindByID(maxid);

            Assert.AreEqual($"Some text from text from test{maxid}", Role.title);

        }
        [TestMethod]
        public void CanAListOfRolesBeReturned()
        {
            var Roles = _testRepo.FindAll().ToList();

            Assert.IsInstanceOfType(Roles, typeof(List<Role>));
        }

        [TestMethod]
        public void CanTheRoleBeDeleted()
        {
            _testRepo.Remove(_testid);
            var Role = _testRepo.FindByID(_testid);

            var expectedRole = new Role();
            Assert.IsNull(Role);
        }


    }
}
