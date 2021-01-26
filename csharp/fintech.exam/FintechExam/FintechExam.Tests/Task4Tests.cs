using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FintechExam.Tests
{
    public class Task4Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RunsFirstExample()
        {
            var run = Task4.Run(3, ParseExample("1 2 3"));
            Assert.AreEqual("-1 -1", run);
        }   
        
        [Test]
        public void RunsSecondExample()
        {
            var run = Task4.Run(2, ParseExample("1 3 1"));
            Assert.AreEqual("1 2", run);
        }
        
        // [Test]
        // public void RunsThridExample()
        // {
        //     var run = ThirdTask.Run(2, ParseExample("2 1"));
        //     Assert.AreEqual("1 2", run);
        // }
        //
        // [Test]
        // public void Run1()
        // {
        //     var run = ThirdTask.Run(2, ParseExample("1 2 2 1"));
        //     Assert.AreEqual("3 4", run);
        // }

        private List<int> ParseExample(string s) => s
            .Split(" ",StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}