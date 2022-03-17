using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTest : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
           ContactData fromTable =  app.Contacts.GetContactInformationFromTable(0);
           ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]
        public void TestContactInformation2()
        {
            string fromProperties = app.Contacts.GetContactInformationFromProperties(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            string fromFormUnited = fromForm.Firstname + fromForm.Lastname + fromForm.Address + fromForm.AllPhones;
            
            fromProperties = Regex.Replace(fromProperties, "[ \r\nHWM:]", "");
            fromFormUnited = Regex.Replace(fromFormUnited, "[\r\n]", "");

            Assert.AreEqual(fromProperties, fromFormUnited);
        }
    }
}
