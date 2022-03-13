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
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Navigator.ReturnHome();
            if (!app.Contacts.IsElementPresent(By.Name("selected[]")))
            {
                app.Contacts.Create(new ContactData("Default", "Default"));
            }
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Delete(0);

            Thread.Sleep(1000);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}