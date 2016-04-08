using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TypeResolver.Tests
{
    [TestClass]
    public class UnitTest1
    {

        interface ISimple { }

        class Simple : ISimple { }

        interface IAnother { }


        interface ISimpleGeneric<T> { }

        class SimpleGeneric<T> : ISimpleGeneric<T> { }

        class NestedGeneric<T> : ISimpleGeneric<List<T>> { }
        
        class FullySpecified : ISimpleGeneric<string> { }


        [TestMethod]
        public void AcceptsSimpleRelation() {
            var vec = Analyser.Analyse(typeof(Simple), typeof(ISimple));

            Assert.IsNotNull(vec);
        }

        [TestMethod]
        public void AcceptsSymmetricalGenericRelation() {
            var vec = Analyser.Analyse(typeof(SimpleGeneric<>), typeof(ISimpleGeneric<>));

            Assert.IsNotNull(vec);
        }

        [TestMethod]
        public void AcceptsFullySpecifiedGenericRelation() {
            var vec = Analyser.Analyse(typeof(SimpleGeneric<string>), typeof(ISimpleGeneric<string>));

            Assert.IsNotNull(vec);
        }

        [TestMethod]
        public void AcceptsNestedGenericArg() {
            var tNestedAbstract = typeof(ISimpleGeneric<>).MakeGenericType(typeof(List<>));

            var vec = Analyser.Analyse(typeof(NestedGeneric<>), tNestedAbstract);

            Assert.IsNotNull(vec);
        }


        [TestMethod]
        public void RejectsBadSimple() {
            var vec = Analyser.Analyse(typeof(Simple), typeof(IAnother));

            Assert.IsNull(vec);
        }













    }
}
