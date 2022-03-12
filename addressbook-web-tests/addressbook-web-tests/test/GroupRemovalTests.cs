using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToGroupsPage();
            if (!app.Groups.IsElementPresent(By.Name("selected[]")))
            {
                app.Groups.Create(new GroupData("Default"));
            }
            List<GroupData> oldGgroups = app.Groups.GetGroupList();
            
            app.Groups.Remove(0);
            
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGgroups.RemoveAt(0);
            Assert.AreEqual(oldGgroups, newGroups);
        }
    }
}
