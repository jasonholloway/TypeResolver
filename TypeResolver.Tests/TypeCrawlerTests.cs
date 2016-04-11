using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TypeResolver.Tests
{
    [TestClass]
    public class TypeInspectorTests
    {

        [TestMethod]
        public void InspectorReturnsNullIfBadMatch() 
        {
            var inspector = TypeInspectorComposer.CreateFor(typeof(int));

            var result = inspector(typeof(long));

            Assert.IsNull(result);
        }

        
        [TestMethod]
        public void InspectorReturnsEmptyEnumerationIfGoodSimpleMatch() 
        {
            var inspector = TypeInspectorComposer.CreateFor(typeof(int));

            var result = inspector(typeof(int));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(Enumerable.Empty<Type>()));
        }


        [TestMethod]
        public void InspectorReturnsInPlaceGenArgument() 
        {
            var inspector = TypeInspectorComposer.CreateFor(typeof(List<>));

            var result = inspector(typeof(List<int>));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(new[] { typeof(int) }));
        }


        [TestMethod]
        public void InspectorReturnsNestedInPlaceGenArgument() 
        {
            var inspector = TypeInspectorComposer.CreateFor(typeof(List<>).MakeGenericType(typeof(List<>)));

            var result = inspector(typeof(List<List<int>>));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(new[] { typeof(int) }));
        }







    }
}
