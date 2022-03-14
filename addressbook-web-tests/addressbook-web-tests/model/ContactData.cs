using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
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
            return ((Firstname == other.Firstname) && (Lastname == other.Lastname));

        }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }
        public int CompareTo(ContactData other)
        {
            if (object.ReferenceEquals(other, null)) return 1;
            if (Lastname.CompareTo(other.Lastname) == 0) return (Firstname.CompareTo(other.Firstname));
            return Lastname.CompareTo(other.Lastname);
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
