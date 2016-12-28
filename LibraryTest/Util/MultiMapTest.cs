using Library.Util;
using NUnit.Framework;
using System.Collections.Generic;

namespace LibraryTest.Util
{
    [TestFixture]
    public class MultiMapTest
    {
        const string Key1 = "key1";
        const string Key2 = "key2";
        const string Value1 = "value1";
        const string Value2 = "value2";

        private MultiMap<string,string> map;

        [SetUp]
        public void Initialize()
        {
            map = new MultiMap<string,string>();
        }

        [Test]
        public void Create()
        {
            AssertSize(0);
        }

        [Test]
        public void OneKeyOneValue()
        {
            map.Add(Key1, Value1);
            AssertSize(1);
            Assert.That(map[Key1], Is.EqualTo(new List<string> { Value1 }));
        }

        [Test]
        public void OneKeyMultipleValues()
        {
            map.Add(Key1, Value1);
            map.Add(Key1, Value2);
            AssertSize(1);
            Assert.That(map[Key1], Is.EqualTo(new List<string> { Value1, Value2 }));
        }

        [Test]
        public void Indexing()
        {
            var values = new List<string> {Value1};
            map[Key1] = values;

            Assert.That(map[Key1], Is.EqualTo(values));
        }

        [Test]
        public void CountOfKey()
        {
            map.Add(Key1, Value1);
            Assert.That(map.Count(Key1), Is.EqualTo(1));
            map.Add(Key1, Value2);
            Assert.That(map.Count(Key1), Is.EqualTo(2));
        }

        [Test]
        public void Clear()
        {
            map.Add(Key1, Value1);
            map.Clear();
            AssertSize(0);
        }


        [Test]
        public void MultipleKeys()
        {
            map.Add(Key1, Value1);
            map.Add(Key2, Value2);
            AssertSize(2);
            Assert.That(map[Key1], Is.EqualTo(new List<string> { Value1 }));
            Assert.That(map[Key2], Is.EqualTo(new List<string> { Value2 }));
        }

        [Test]
        public void GatherAllValues()
        {
            map.Add(Key1, Value1);
            map.Add(Key2, Value2);
            var values = map.Values();
            Assert.That(values, Is.EqualTo(new List<string> { Value1, Value2 }));
        }

        [Test]
        public void ReturnsEmptyListWhenKeyNotFound()
        {
            var list = map["nonexistent"];
            Assert.That(list, Is.Empty);
        }

        [Test]
        public void ThrowsExceptionOnNullKey()
        {
            Assert.Throws<IllegalKeyException>(()=>map.Add(null, "anything"));
        }

        private void AssertSize(int expected)
        {
            Assert.That(map.Count(), Is.EqualTo(expected));
            Assert.That(map.IsEmpty(), Is.EqualTo(0 == expected));
        }
    }
}