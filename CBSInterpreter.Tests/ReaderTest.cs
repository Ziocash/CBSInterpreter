using CBSInterpreter.Core;
using CBSInterpreter.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace CBSInterpreter.Tests
{
    public class ReaderTest
    {
        [Test]
        public void TestReader()
        {
            CBSReader reader = new();
            List<CBSEntry> entries = new();
            Assert.DoesNotThrow(() => { entries = reader.GetEntries(); });
            Assert.NotNull(entries);
            Assert.NotZero(entries.Count);
            Assert.IsFalse(entries[1] == entries[0]);
        }
    }
}