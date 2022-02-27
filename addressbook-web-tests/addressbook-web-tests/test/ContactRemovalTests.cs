using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.Delete(1);
        }
    }
}