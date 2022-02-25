using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
        public class ContactHelper : HelperBase
        {
            public ContactHelper(ApplicationManager manager)
            : base(manager)
            {
            }
            public ContactHelper Create(ContactData contact)
            {
                CreateNewContact();
                FillContactForm(contact);
                manager.Navigator.ReturnHome();
                return this;
            }
            public ContactHelper Modify(string v, ContactData newContact)
            {
                manager.Navigator.ReturnHome();
                SelectContact(v);
                InitContactModification();
                FillContactForm(newContact);
                SubmitContactModification();
                return this;
            }
            public ContactHelper Delete(string v)
            {
                manager.Navigator.ReturnHome();
                SelectContact(v);
                InitContactDelete();
                SubmitContactDelete();
                return this;
            }

            public ContactHelper FillContactForm(ContactData contact)
            {
                driver.FindElement(By.Name("firstname")).Click();
                driver.FindElement(By.Name("firstname")).Clear();
                driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
                driver.FindElement(By.Name("lastname")).Click();
                driver.FindElement(By.Name("lastname")).Clear();
                driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
                driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
                return this;
            }

            private ContactHelper CreateNewContact()
            {
                driver.FindElement(By.LinkText("add new")).Click();
                return this;
            }
            public ContactHelper SelectContact(string index)
            {
            driver.FindElement(By.Id(index)).Click(); ;
                return this;
            }
            private ContactHelper InitContactModification()
            {
                driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
                return this;
            }
            private ContactHelper SubmitContactModification()
            {
                driver.FindElement(By.Name("update")).Click(); ;
                return this;
            }
            private ContactHelper InitContactDelete()
            {
                driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
                return this;
            }
            private ContactHelper SubmitContactDelete()
            {
                driver.SwitchTo().Alert().Accept();
                return this;
            }
    }
}
