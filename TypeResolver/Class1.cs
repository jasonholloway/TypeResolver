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


    public static class TypeInspectorComposer
    {
        public static Func<Type, IEnumerable<Type>> CreateFor(Type protoType) 
        {
            if(protoType.IsGenericParameter) {
                //if prototype is gen param, then we return encountered subject, housed in array of one
                return (s) => new[] { s };
            }

            if(!protoType.IsGenericType) {
                //if prototype is simple, non-generic, then we expect subject to be an exact match
                return (s) => s == protoType
                                ? Type.EmptyTypes
                                : null;
            }
            else { 
                //if prototype has gen args (or params), then - via a guard ensuring that subject is itself generic - we must
                //match each of subject's gen args to the given prototypical ones.            
                var subInspectors = protoType.GetGenericArguments()
                                                .Select(tt => CreateFor(tt))
                                                .ToArray();

                return (subject) => {
                    if(!subject.IsGenericType) { //simple but effective guard
                        return null;
                    }

                    //************************************
                    //need to check gendef is correct!
                    //************************************

                    var subjectGenArgs = subject.GetGenericArguments();

                    if(subjectGenArgs.Length != subInspectors.Length) { //avoids failing messily on subsequent loop
                        return null;
                    }
                    
                    //try to match gen args one by one, ensuring clean, immediate quit if discrepency found
                    var matched = Enumerable.Empty<Type>();

                    for(int i = 0; i < subInspectors.Length; i++) 
                    {
                        var subInspector = subInspectors[i];

                        var result = subInspector(subjectGenArgs[i]);

                        if(result == null) {
                            return null;
                        }

                        matched = matched.Concat(result);
                    }

                    return matched;
                };
            }
        }
    }



    public class SimpleTypeVector : TypeVector 
    {

    }


    public class TypeVector
    {

    }


}
