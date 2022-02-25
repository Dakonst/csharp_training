using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactDeleteTests : TestBase
    {
        [Test]
        public void ContactDeleteTest()
        {
            app.Contacts.Delete("6");
        }
    }
}