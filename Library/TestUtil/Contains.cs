using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Library.Util;

namespace Library.TestUtil
{
    public class ContainsAll : NUnit.Framework.Constraints.Constraint
    {
        string[] expected;
        private IList<string> actualList;

        public ContainsAll(params string[] expected)
        {
            this.expected = expected;
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
        }

        public override void WriteMessageTo(MessageWriter writer)
        {
            string message = String.Format("Expected: {0}\n  Actual:   {1}",
                ListUtil.ToString(actualList), ListUtil.ToString(expected));
            writer.WriteMessageLine(message);
        }

        public override bool Matches(object actual)
        {
            actualList = (IList<string>)actual;
            if (actualList.Count != expected.Length)
                return false;
            for (int i = 0; i < expected.Length; i++)
                if (!actualList.Contains(expected[i]))
                    return false;
            return true;
        }
    }
}
