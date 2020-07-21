using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using BowlingScore;
using NUnit.Framework;

namespace BowlingScoreTest
{
    [TestFixture]
    public class GameTest
    {
        private Game game;
        [SetUp]
        public void SetUp()
        {
            game = new Game();
        }


        #region 初始測試用。不完整的一局，不會呼叫到Score。

        // [Test]
        // public void TestOneThrow()
        // {
        //     game.Add(5);
        //     Assert.That(game.Score, Is.EqualTo(5));
        //     Assert.That(game.CurrentFrame, Is.EqualTo(1));
        // }

        #endregion


        [Test]
        public void TestTwoThrowsNoMark()
        {
            game.Add(5);
            game.Add(4);
            Assert.That(game.Score, Is.EqualTo(9));
            Assert.That(game.CurrentFrame, Is.EqualTo(2));
        }

        [Test]
        public void TestFourThorwsNoMark()
        {
            game.Add(5);
            game.Add(4);
            game.Add(7);
            game.Add(2);
            Assert.That(game.Score, Is.EqualTo(18));
            Assert.That(game.ScoreForFrame(1), Is.EqualTo(9));
            Assert.That(game.ScoreForFrame(2), Is.EqualTo(18));
            Assert.That(game.CurrentFrame, Is.EqualTo(3));
        }

        [Test]
        public void TestSimpleSpare()
        {
            game.Add(3);
            game.Add(7);
            game.Add(3);
            Assert.That(13, Is.EqualTo(game.ScoreForFrame(1)));
            Assert.That(game.CurrentFrame, Is.EqualTo(2));
        }

        [Test]
        public void TestSipleFrameAfterSpare()
        {
            game.Add(3);
            game.Add(7);
            game.Add(3);
            game.Add(2);
            Assert.That(game.ScoreForFrame(1), Is.EqualTo(13));
            Assert.That(game.ScoreForFrame(2), Is.EqualTo(18));
            Assert.That(game.Score, Is.EqualTo(18));
            Assert.That(game.CurrentFrame, Is.EqualTo(3));
        }

        [Test]
        public void TestSimpleStrike()
        {
            game.Add(10);
            game.Add(3);
            game.Add(6);
            Assert.That(game.ScoreForFrame(1), Is.EqualTo(19));
            Assert.That(game.Score(), Is.EqualTo(28));
            Assert.That(game.CurrentFrame, Is.EqualTo(3));
        }

        [Test]
        public void TestPerfectGame()
        {
            for (var i = 0; i < 12; i++)
            {
                game.Add(10);
            }
            Assert.That(game.Score(), Is.EqualTo(300));
            Assert.That(game.CurrentFrame, Is.EqualTo(11));
        }

        [Test]
        public void TestEndOfArray()
        {
            for (var i = 0; i < 9; i++)
            {
                game.Add(0);
                game.Add(0);
            }
            game.Add(2);
            game.Add(8);//第十局spare
            game.Add(10);//最後一投strike
            Assert.That(game.Score(), Is.EqualTo(20));
        }

        [Test]
        public void TestSampleGame()
        {
            game.Add(1);
            game.Add(4);
            game.Add(4);
            game.Add(5);
            game.Add(6);
            game.Add(4);
            game.Add(5);
            game.Add(5);
            game.Add(10);
            game.Add(0);
            game.Add(1);
            game.Add(7);
            game.Add(3);
            game.Add(6);
            game.Add(4);
            game.Add(10);
            game.Add(2);
            game.Add(8);
            game.Add(6);
            Assert.That(game.Score(), Is.EqualTo(133));
        }

        [Test]
        public void TestHeartBreak()
        {
            for (var i = 0; i < 11; i++)
                game.Add(10);
            game.Add(9);
            Assert.That(game.Score(), Is.EqualTo(299));
        }

        [Test]
        public void TestTenthFrameSpare()
        {
            for (var i = 0; i < 9; i++)
                game.Add(10);
            game.Add(9);
            game.Add(1);
            game.Add(1);
            Assert.That(game.Score(), Is.EqualTo(270));
        }
    }
}
