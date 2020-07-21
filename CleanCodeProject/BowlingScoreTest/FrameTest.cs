using System;
using BowlingScore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace BowlingScoreTest
{
    [TestFixture]
    public class FrameTest
    {
        [Test]
        public void TestScoreThrows()
        {
            var f = new Frame();
            Assert.That(f.Score, Is.Zero);
        }

        [Test]
        public void TestAddOnThrow()
        {
            var f = new Frame();
            f.Add(5);
            Assert.That(f.Score, Is.EqualTo(5));
        }
    }
}
