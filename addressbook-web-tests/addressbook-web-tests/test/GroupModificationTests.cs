using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTTest()
        {
            GroupData newData = new GroupData("zzz");
            newData.Header = "qqq";
            newData.Footer = "www";
            app.Groups.Modify(1, newData);
        }

    }
}
