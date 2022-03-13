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
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTTest()
        {
            ContactData newContact = new ContactData("Petrov", "Petr");
            app.Navigator.ReturnHome();
            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                app.Contacts.Create(new ContactData("Default", "Default"));
            }
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Modify(0, newContact);
            
            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = newContact.Firstname;
            oldContacts[0].Lastname = newContact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}