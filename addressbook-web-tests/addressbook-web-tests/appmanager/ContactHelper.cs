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
            public ContactHelper Modify(int v, ContactData newContact)
            {
                SelectContact(v);
                InitContactModification();
                FillContactForm(newContact);
                SubmitContactModification();
                return this;

        }
            public ContactHelper Delete(int v)
            {
                SelectContact(v);
                InitContactDelete();
                SubmitContactDelete();
                return this;
            }

            public ContactHelper FillContactForm(ContactData contact)
            {
                Type(By.Name("firstname"), contact.Firstname);
                Type(By.Name("lastname"), contact.Lastname);
                driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
                return this;
            }

            private ContactHelper CreateNewContact()
            {
                driver.FindElement(By.LinkText("add new")).Click();
                return this;
            }
            public ContactHelper SelectContact(int index)
            {
                driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
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
        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.ReturnHome();
            //ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr#entry"));
            ICollection<IWebElement> elements1 = driver.FindElements(By.CssSelector("tr[name=entry] td:nth-of-type(2)"));
            ICollection<IWebElement> elements2 = driver.FindElements(By.CssSelector("tr[name=entry] td:nth-of-type(3)"));
            int count = elements1.Count();
            for(int i=0; i<count; i++)
            {
                contacts.Add(new ContactData(elements2.ElementAt(i).Text, elements1.ElementAt(i).Text));
            }
            return contacts;
        }
    }
}
