using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {

        private string allPhones;
        private string allEmail;
        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string AllPhones 
        {
            get {
                if (allPhones != null) return allPhones;
                else return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
            }
            set {
                allPhones = value;
            }
        }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string AllEmail
        {
            get
            {
                if (allEmail != null) return allEmail;
                else return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
            }
            set
            {
                allEmail = value;
            }
        }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "") return "";
            return Regex.Replace(phone, "[- ()]", "") + "\r\n";
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null)) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return ((Firstname == other.Firstname) && (Lastname == other.Lastname));

        }
        public override int GetHashCode()
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode() + Id.GetHashCode();
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
        
    }
}
