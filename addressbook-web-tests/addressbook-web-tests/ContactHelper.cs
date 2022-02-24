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
        }
}
