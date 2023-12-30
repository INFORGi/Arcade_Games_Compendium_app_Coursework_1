using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WpfApp1;
using static WpfApp1.Space_invaders;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Проверка скорости противник в игре Space Invaders на нормальной сложности
        /// </summary>
        [TestMethod]
        public void EnemySpeedCheckLevel0()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.StartGame(0);
            Assert.AreEqual(6, spaceInvaders.enemySpeed);
        }
        /// <summary>
        /// Проверка скорости обновления кода отвечающего за скорость противника в игре Space Invaders
        /// </summary>
        [TestMethod]
        public void BulletTimerLimitLevel0()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.StartGame(0);
            Assert.AreEqual(90, spaceInvaders.bulletTimerLimit);
        }
        /// <summary>
        /// Проверка скорости противник в игре Space Invaders на высокой сложности
        /// </summary>
        [TestMethod]
        public void EnemySpeedCheckLevel1()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.StartGame(1);
            Assert.AreEqual(10, spaceInvaders.enemySpeed);
        }


        /// <summary>
        /// Проверка ввода рейтинга при условии что новый рейтинг больше предыдущего
        /// </summary>
        [TestMethod]
        public void RatingMoreThereLevel0()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.Reyting(0,"DS",100);
            Assert.AreEqual("DS - 100", spaceInvaders.k[0]);
        }
        /// <summary>
        /// Проверка ввода рейтинга при условии что новый рейтинг меньше предыдущего
        /// </summary>
        [TestMethod]
        public void RatingLesserIncludedLevel0()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.Reyting(0, "DS", 10);
            Assert.AreEqual("DS - 34", spaceInvaders.k[0]);
        }
        /// <summary>
        /// Проверка ввода рейтинга при условии что новый рейтинг равен предыдущего
        /// </summary>
        [TestMethod]
        public void RatingRankingRavenImeaningfulLevel0()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.Reyting(0, "DS", 34);
            Assert.AreEqual("DS - 34", spaceInvaders.k[0]);
        }

        [TestMethod]
        public void RatingMoreThereLevel1()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.Reyting(1, "a", 1000);
            Assert.AreEqual("a - 1000", spaceInvaders.k[0]);
        }
        [TestMethod]
        public void RatingLesserIncludedLevel1()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.Reyting(1, "a", 10);
            Assert.AreEqual("a - 218", spaceInvaders.k[0]);
        }
        [TestMethod]
        public void RatingRankingRavenImeaningfulLevel1()
        {
            var spaceInvaders = new SpaceInvaders();
            spaceInvaders.Reyting(1, "a", 218);
            Assert.AreEqual("a - 218", spaceInvaders.k[0]);
        }
    }
}
