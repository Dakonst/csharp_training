using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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

        internal void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGtoup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }
        internal void DeleteContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SetGroupFilter(group.Name);
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitDeletingContactFromGtoup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }


        /*public ContactHelper Modify(int v, ContactData newContact)
        {
            SelectContact(v);
            InitContactModification();
            FillContactForm(newContact);
            SubmitContactModification();
            return this;

        }*/

        public ContactHelper Modify(int v, ContactData newContact)
        {
            InitContactModification(v);
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
        public ContactHelper Delete(ContactData contact)
        {
            SelectContact(contact.Id);
            InitContactDelete();
            SubmitContactDelete();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
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
        public ContactHelper SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
            return this;
        }
        private ContactHelper InitContactModification(int contactId)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (contactId + 2) + "]/td[8]/a/img")).Click();
            return this;
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SetGroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        private ContactHelper InitWatchContactProperties()
        {
            driver.FindElement(By.XPath("//img[@alt='Details']")).Click();
            return this;
        }
        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }
        private ContactHelper InitContactDelete()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }
        private ContactHelper SubmitContactDelete()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        private void CommitAddingContactToGtoup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void CommitDeletingContactFromGtoup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.ReturnHome();
                ICollection<IWebElement> elements1 = driver.FindElements(By.CssSelector("tr[name=entry] td:nth-of-type(2)"));
                ICollection<IWebElement> elements2 = driver.FindElements(By.CssSelector("tr[name=entry] td:nth-of-type(3)"));
                int count = elements1.Count();
                for (int i = 0; i < count; i++)
                {
                    contactCache.Add(new ContactData(elements2.ElementAt(i).Text, elements1.ElementAt(i).Text));
                }
            }
            return new List<ContactData>(contactCache);
        }
        public ContactData GetContactInformationFromEditForm(int v)
        {
            manager.Navigator.OpenHomePage();
            /*SelectContact(index);*/
            InitContactModification(v);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };

        }

        public string GetStringInfoFromForm(ContactData info)
        {
            string part1 = info.Firstname + " " + info.Lastname ;
            if (info.Address != null) part1 = part1 + "\r\n" + info.Address;

            string part2 = null;
            if (info.HomePhone != "") part2 = "H: " + info.HomePhone + "\r\n";
            if (info.MobilePhone != "") part2 = part2 + "M: " + info.MobilePhone + "\r\n";
            if (info.WorkPhone != "") part2 = part2 + "W: " + info.WorkPhone + "\r\n";

            string part3 = null;
            if (info.Email != "") part3 = part3 + info.Email + "\r\n";
            if (info.Email2 != "") part3 = part3 + info.Email2 + "\r\n";
            if (info.Email3 != "") part3 = part3 + info.Email3 + "\r\n";

            return (part1 +"\r\n" + "\r\n" + part2 + "\r\n" + part3).
                Replace("\r\n\r\n\r\n", "\r\n\r\n").Trim(new Char[] { ' ', '\r', '\n' }); 
        }
        public ContactData GetContactInformationFromTable(int index)
        {
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmail = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmail = allEmail
            };
        }


        public string GetContactInformationFromProperties(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(index);
            InitWatchContactProperties();
            return driver.FindElement(By.CssSelector("div#content")).Text;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
