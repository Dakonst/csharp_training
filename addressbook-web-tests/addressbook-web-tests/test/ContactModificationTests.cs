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
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTTest()
        {
            ContactData newContact = new ContactData("Petr", "Petrov");
            app.Navigator.ReturnHome();
            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                app.Contacts.Create(new ContactData("Default", "Default"));
            }
            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData oldData = oldContacts[0];
            app.Contacts.Modify(oldData.Id, newContact);
            
            List<ContactData> newContacts = ContactData.GetAll();
            oldData.Firstname = newContact.Firstname;
            oldData.Lastname = newContact.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}