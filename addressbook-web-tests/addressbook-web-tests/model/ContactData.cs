using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        private string firstname;
        private string lastname;

        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }
        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return ((firstname == other.Firstname) && (lastname == other.Lastname));

        }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }
        public int CompareToF(ContactData other)
        {
            if (object.ReferenceEquals(other, null)) return 1;
            return firstname.CompareTo(other.Firstname);
        }
        public int CompareToL(ContactData other)
        {
            if (object.ReferenceEquals(other, null)) return 1;
            return lastname.CompareTo(other.Lastname);
        }
        public string Firstname
        {
            get
            {
                return firstname;
            }

            set
            {
                firstname = value;
            }
        }
        public string Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
            }
        }

    }
}
