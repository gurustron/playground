using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FintechExam.Tests
{
    public class ThirdTaskTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RunsFirstExample()
        {
            var run = ThirdTask.Run(4, ParseExample("2 1 4 6"));
            Assert.AreEqual("-1 -1", run);
        }   
        
        [Test]
        public void RunsSecondExample()
        {
            var run = ThirdTask.Run(2, ParseExample("1 2"));
            Assert.AreEqual("-1 -1", run);
        }
        
        [Test]
        public void RunsThridExample()
        {
            var run = ThirdTask.Run(2, ParseExample("2 1"));
            Assert.AreEqual("1 2", run);
        }

        [Test]
        public void Run1()
        {
            var run = ThirdTask.Run(2, ParseExample("1 2 2 1"));
            Assert.AreEqual("3 4", run);
        }

        private List<long> ParseExample(string s) => s
            .Split(" ",StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
    }
}