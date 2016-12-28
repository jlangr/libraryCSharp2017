using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Text;
using Library.Util;

namespace Library.TestUtil
{
    public static class Asserts
    {
        public static void ContainsAll<T>(IList<T> list, params T[] expected)
        {
            Assert.AreEqual(expected.Length, list.Count);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], list[i]);
        }
    }
}
