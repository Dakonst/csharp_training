using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            app.Contacts.Modify(1, newContact);
        }
    }
}