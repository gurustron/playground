using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FintechExam.Tests
{
    public class FirstTaskTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RunsFirstExample()
        {
            var run = FirstTask.Run(5,2, new List<string>{"1","2", "1", "3", "5"});
            Assert.AreEqual(16, run);
        }   
        
        [Test]
        public void RunsSecondExample()
        {
            var run = FirstTask.Run(3, 1, ParseExample("99 5 85"));
            Assert.AreEqual(10, run);
        }
        
        [Test]
        public void RunsThridExample()
        {
            var run = FirstTask.Run(1, 11, ParseExample("9999"));
            Assert.AreEqual(0, run);
        }

        [Test]
        public void Run1()
        {
            var run = FirstTask.Run(2, 2, ParseExample("189 19"));
            Assert.AreEqual(800 +80, run);
        }
        
        [Test]
        public void Run2()
        {
            var run = FirstTask.Run(2, 4, ParseExample("105 19"));
            Assert.AreEqual(800 +90 + 80 + 4, run);
        }
        private List<string> ParseExample(string s) => s.Split(" ",StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}