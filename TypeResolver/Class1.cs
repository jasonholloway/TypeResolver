using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeResolver
{
    public static class Analyser
    {

        public static TypeVector Analyse(Type subType, Type superType) 
        {
            //IsGenericTypeDefinition only cares about the immediate type
            //a nested generic is invisible to it

            //ALL WE NEED TO DO IS TO SATISFY THE CONCRETE-TYPE'S GENARG NEEDS
            //it's already assumed that the two types relate to each other.

            //each supertype, implicit also, should be inspected suchly

            //IS IT SUFFICIENT? - that is the question

            //and its only gen args we care about in this...


            if(subType.IsGenericTypeDefinition ^ superType.IsGenericTypeDefinition) {
                return null;
            }

            if(superType.IsAssignableFrom(subType)) {
                return new SimpleTypeVector();
            }

            //either clean wrong or both generic 
            //...
            
            if(!subType.IsGenericTypeDefinition) {
                return null;
            }


            return new TypeVector(); ;                        
        }


    }



    //need to crawl through superType till we have enough genArgs discovered
    //some relations will not include all genargs needed by concreteType

    //so: we only need to crawl one type at a time, but we must be laying down a trail of lambdas as we go...

    //but each leg of the crawl must half-expect to fail: 
    //is its subject type what it was expecting? If not, return null.


    public static class TypeMatcherFactory
    {
        public static Func<Type, IEnumerable<Type>> BuildFor(Type t) 
        {            
            //if prototype is simple, then we just return empty type arg (signifying success), via a guard ensuring subject is type expected
            if(!t.IsGenericType) {
                return (s) => s == t
                              ? Type.EmptyTypes
                              : null;
            }

            //if prototype is gen param, then we return array of one, guarding against bad subject - which would be an open generic type
            if(t.IsGenericParameter) {
                return (s) => !s.IsOpenGeneric() 
                                ? new[] { s }
                                : null;
            }

            //if prototype has gen args (or params), then we delegate downwards, via a guard comparing that gendef is as expected

            //need to prebuild genarg inspectors here, injecting them into below lambda
            //these will always need to be pre-built! Cos we're building once for every possible inspection

            //instead of list of types, need to relate each match to prototype's gen param, which it should share with concrete impl.

            return s => {
                if(!s.IsGenericType) {
                    return null;
                }

                //try to match all gen args of prototype
                //one by one, ensuring clean, immediate quit if bad match found
                var matched = Enumerable.Empty<Type>();

                foreach(var protoGenArg in t.GetGenericArguments()) {

                }

                throw new NotImplementedException();


                //subject should be gen type itself
                //otherwise bad


            };
        }
    }



    public class SimpleTypeVector : TypeVector 
    {

    }


    public class TypeVector
    {

    }


}
