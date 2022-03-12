using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTTest()
        {
            GroupData newData = new GroupData("zzz");
            newData.Header = "qqq";
            newData.Footer = "www";
            app.Navigator.GoToGroupsPage();
            if (!app.Groups.IsElementPresent(By.Name("selected[]")))
            {
                app.Groups.Create(new GroupData("Default"));
            }
            List<GroupData> oldGgroups = app.Groups.GetGroupList();

            app.Groups.Modify(0, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGgroups[0].Name = newData.Name;
            oldGgroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGgroups, newGroups);
        }

    }
}
