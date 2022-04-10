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
            if (GroupData.GetAll() == null)
            {
                List<GroupData> groups = new List<GroupData>();
                groups.Add(new GroupData("NewGroupName"));
            }
            
            GroupData group = GroupData.GetAll()[0];
            
            if (ContactData.GetAll() == null)
            {
                List<ContactData> contacts = new List<ContactData>();
                contacts.Add(new ContactData("ImyaNew", "FamiliyaNew"));
            }
            int iGroup;
            for (iGroup = 0; iGroup < GroupData.GetAll().Count(); iGroup++)
            {
                GroupData group2 = GroupData.GetAll()[iGroup];
                List<ContactData> oldList = group2.GetContacts();
                ContactData contact = ContactData.GetAll().Except(oldList).First();

                if (contact != null)
                {
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
                ContactData contactNew = new ContactData("Name1", "LastName1");
                contacts.Add(contactNew);

                List<ContactData> oldList2 = group.GetContacts();

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

                if (oldList != null)
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
                ContactData contactNew = new ContactData("Name1", "LastName1");
                contacts.Add(contactNew);

                GroupData group = GroupData.GetAll()[0];

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
