using Library.Util;
using NUnit.Framework;

namespace LibraryTest.Util
{
    [TestFixture]
    public class MultiMapNewTest
    {
        const string Key1 = "key1";
        const string Value1 = "value1";
        private MultiMap<string, string> map;

        [SetUp]
        public void CreateMap()
        {
            map = new MultiMap<string, string>();
        }

        [Test]
        public void IsEmptyWhenCreated()
        {
            Assert.That(map.IsEmpty());
        }
        [Test]
        public void IncrementsCountWhenKeyAdded()
        {
            map.Add(Key1, Value1);
            Assert.That(map.Count(Key1), Is.EqualTo(1));
        }

        [Test]
        public void ThrowsExceptionOnNullKey()
        {
            Assert.Throws<IllegalKeyException>(() => map.Add(null, "anything"));
        }
    }
}
