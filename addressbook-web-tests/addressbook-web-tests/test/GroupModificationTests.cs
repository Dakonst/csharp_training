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
    public class GroupModificationTests : GroupTestBase
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
            List<GroupData> oldGroups = GroupData.GetAll();

            GroupData oldData = oldGroups[0];
            app.Groups.Modify(oldData, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                if (group.Id == oldData.Id) Assert.AreEqual(newData.Name, group.Name);
            }
        }

    }
}
