using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTTest()
        {
            ContactData newContact = new ContactData("Petrov", "Petr");
            app.Contacts.Modify(1, newContact);
        }
    }
}