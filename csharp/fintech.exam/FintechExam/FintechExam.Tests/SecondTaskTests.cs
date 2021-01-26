using System.Collections.Generic;
using NUnit.Framework;

namespace FintechExam.Tests
{
    public class SecondTaskTests
    {
        [Test]
        public void RunsFirstExample()
        {
            var run = SecondTask.Run("4 7");
            Assert.AreEqual(4, run);
        }           
        
        [Test]
        public void RunsSecondExample()
        {
            var run = SecondTask.Run("10 100");
            Assert.AreEqual(9, run);
        }   
        
        [Test]
        public void Run1()
        {
            var run = SecondTask.Run("88 88");
            Assert.AreEqual(1, run);
        }   

        [Test]
        public void Run2()
        {
            var run = SecondTask.Run("88 99");
            Assert.AreEqual(2, run);
        }   
    }
}