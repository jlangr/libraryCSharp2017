using System.Collections.Generic;
using Library.Util;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace LibraryTest.Util
{
    [TestFixture]
    public class ListUtilTest
    {
        IList<string> list;

        [SetUp]
        public void Initialize()
        {
            list = new List<string>();
        }

        [Test]
        public void Degenerate()
        {
            Assert.That(ListUtil.ToString(list), Is.EqualTo(""));
        }

        [Test]
        public void SingleElement()
        {
            list.Add("a");
            Assert.That(ListUtil.ToString(list), Is.EqualTo("a"));
        }

        [Test]
        public void MultipleElements()
        {
            list.Add("a");
            list.Add("b");
            Assert.That(ListUtil.ToString(list), Is.EqualTo("a,b"));
        }

        [Test]
        public void Array()
        {
            string[] array = { "a", "b" };
            Assert.That(ListUtil.ToString(array), Is.EqualTo("a,b"));
        }
    }
}
