using System.Collections.Generic;
using Library.Util;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace LibraryTest.Util
{
    [TestFixture]
    public class CollectionUtilTest // TODO ?? NEEDED
    {
        [Test]
        public void ToArrayDegenerate()
        {
            var collection = new List<string>();
            var array = CollectionUtil.ToArray(collection);
            Assert.That(array.Length, Is.EqualTo(0));
        }

        [Test]
        public void ToArray()
        {
            var collection = new List<string> {"a", "b"};
            var array = CollectionUtil.ToArray(collection);
            Assert.That(array, Is.EqualTo(new[] { "a", "b" }));
        }

        [Test]
        public void Find()
        {
            var a = new X("a"); 
            var b = new X("b");
            var collection = new List<object> {a, b};

            Assert.That(CollectionUtil.Find(collection, XFinderMethod, "b"), Is.EqualTo(b));
        }

        class X
        {
            internal readonly object Value;
            internal X(object value)
            {
                Value = value;
            }
        }

        static bool XFinderMethod(object obj, object value)
        {
            return ((X)obj).Value == value;
        }
    }
}
