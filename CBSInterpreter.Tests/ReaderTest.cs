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
            Assert.That(entries, Is.Not.Null);
            Assert.That(entries, Has.Count.Not.EqualTo(0));
            Assert.That(entries[1] == entries[0], Is.False);
        }
    }
}