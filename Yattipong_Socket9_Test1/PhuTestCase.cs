using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yattipong_Socket9_Test1
{
    [TestFixture]
    class PhuTestCase
    {
        [TestCase]
        public void DoFindWeekDay()
        {
            FindWeekday test1 = new FindWeekday();
            // dd,MM,yyyy
            Assert.AreEqual("monday",   test1.DoFindWeekDay( 1,  1, 1900));   // not leap year
            Assert.AreEqual("tuesday",  test1.DoFindWeekDay(01, 03, 1904));   // leap year
            Assert.AreEqual("wednesday",test1.DoFindWeekDay(01,  3, 1905));   // not leap year
            Assert.AreEqual("thursday", test1.DoFindWeekDay(31, 12, 1908));   // leap year
            Assert.AreEqual("friday",   test1.DoFindWeekDay(19, 12, 1986));   // not leap year
            Assert.AreEqual("saturday", test1.DoFindWeekDay( 4, 03, 2000));   // leap year
            Assert.AreEqual("sunday",   test1.DoFindWeekDay(28,  2, 2100));    // not leap year

            Assert.AreEqual(null,       test1.DoFindWeekDay(29,  2, 2100));    // test error

        }
    }
}
