using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "bbb";
            group.Footer = "ccc";

            List<GroupData> oldGgroups = app.Groups.GetGroupList();
            
            app.Groups.Create(group);

            Assert.AreEqual(oldGgroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGgroups.Add(group);
            oldGgroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGgroups, newGroups);
        }
        
        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGgroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGgroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGgroups.Add(group);
            oldGgroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGgroups, newGroups);
        }
        [Test]
        public void BadNameEmptyGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGgroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGgroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGgroups.Add(group);
            oldGgroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGgroups, newGroups);
        }
    }
}
