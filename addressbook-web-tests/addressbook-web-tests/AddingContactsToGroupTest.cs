using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTest : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            if (GroupData.GetAll().Count() == 0)
            {
                List<GroupData> groups = new List<GroupData>();
                app.Groups.Create(new GroupData("NewGroupName")
                {
                    Header = "New Header",
                    Footer = "New Footer"
                });
            }
            
            GroupData group = GroupData.GetAll().ElementAt(0);
            
            if (ContactData.GetAll().Count() == 0)
            {
                List<ContactData> contacts = new List<ContactData>();
                app.Contacts.Create(new ContactData("ImyaNew", "FamiliyaNew"));
            }
            int iGroup;
            for (iGroup = 0; iGroup < GroupData.GetAll().Count(); iGroup++)
            {
                GroupData group2 = GroupData.GetAll()[iGroup];
                List<ContactData> oldList = group2.GetContacts();
                int n = ContactData.GetAll().Except(oldList).Count();

                if (ContactData.GetAll().Except(oldList).Count() != 0)
                {
                    ContactData contact = ContactData.GetAll().Except(oldList).First();
                    app.Contacts.AddContactToGroup(contact, group2);
                    List<ContactData> newList = group2.GetContacts();
                    oldList.Add(contact);
                    oldList.Sort();
                    newList.Sort();
                    Assert.AreEqual(oldList, newList);
                    break;
                }
            }
            if (iGroup == GroupData.GetAll().Count())
            {
                List<ContactData> contacts = new List<ContactData>();
                app.Contacts.Create(new ContactData("Name1", "LastName1"));

                List<ContactData> oldList2 = group.GetContacts();

                ContactData contactNew = ContactData.GetAll().Except(oldList2).First();

                app.Contacts.AddContactToGroup(contactNew, GroupData.GetAll()[0]);
                    
                List<ContactData> newList = group.GetContacts();
                oldList2.Add(contactNew);
                oldList2.Sort();
                newList.Sort();
                Assert.AreEqual(oldList2, newList);

            }
               
        }

        [Test]
        public void TestDeletingContactFromGroup()
        {

            int iGroup;
            for (iGroup = 0; iGroup < GroupData.GetAll().Count(); iGroup++)
            {
                GroupData group = GroupData.GetAll()[iGroup];
                List<ContactData> oldList = group.GetContacts();

                if (oldList.Count() != 0)
                {
                    ContactData contact = oldList.First();

                    app.Contacts.DeleteContactFromGroup(contact, group);

                    List<ContactData> newList = group.GetContacts();
                    oldList.Remove(contact);

                    oldList.Sort();
                    newList.Sort();
                    Assert.AreEqual(oldList, newList);
                    break;
                }

            }
            if (iGroup == GroupData.GetAll().Count())
            {
              List<ContactData> contacts = new List<ContactData>();
                app.Contacts.Create(new ContactData("Name1", "LastName1"));

                GroupData group = GroupData.GetAll()[0];
                ContactData contactNew = ContactData.GetAll().First();

                app.Contacts.AddContactToGroup(contactNew, GroupData.GetAll()[0]);
                List<ContactData> oldList2 = group.GetContacts();

                app.Contacts.DeleteContactFromGroup(contactNew, GroupData.GetAll()[0]);

                List<ContactData> newList = group.GetContacts();
                oldList2.Remove(contactNew);
                oldList2.Sort();
                newList.Sort();
                Assert.AreEqual(oldList2, newList);

            }

        }
    }
}
