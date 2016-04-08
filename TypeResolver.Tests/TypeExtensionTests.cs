using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TypeResolver.Tests
{
    [TestClass]
    public class TypeExtensionTests
    {

        [TestMethod]
        public void IsOpenGenericWorks() 
        {
            Assert.IsTrue(typeof(List<>).IsOpenGeneric(), "Fails on List<T> -> true");
            Assert.IsFalse(typeof(int).IsOpenGeneric(), "Fails on int -> false");
            Assert.IsFalse(typeof(List<int>).IsOpenGeneric(), "Fails on List<int> -> false");
            Assert.IsTrue(typeof(List<>).MakeGenericType(typeof(List<>)).IsOpenGeneric(), "Fails on List<List<T>> -> true");
        }
        

    }
}
