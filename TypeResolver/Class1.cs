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

        public static TypeResolver Analyse(Type concreteType, Type abstractType) 
        {
            var inspector = TypeInspectorComposer.CreateFor(abstractType);

            //but now we have to relate results of inspection to concrete type's requirements...

            //inspector will always return args in particular order
            //we need to rearrange them into concrete type's intended order
            //we can get concrete gen params by running inspector on its prototype:

            var concreteGenParams = inspector(abstractType);

            //and use this to build rearranger
            //...

            return new TypeResolver();                    
        }


    }

        

    public static class TypeInspectorComposer
    {
        public static Func<Type, IEnumerable<Type>> CreateFor(Type protoType) 
        {
            if(protoType.IsGenericParameter) {
                //if prototype is gen param, then we return encountered subject, housed in array of one
                return (s) => new[] { s };
            }

            if(protoType.IsArray) {
                var subInspector = CreateFor(protoType.GetElementType());

                return (s) => s.IsArray
                                ? subInspector(s.GetElementType())
                                : null;
            }

            if(!protoType.IsGenericType) {
                //if prototype is simple, non-generic, then we expect subject to be an exact match
                return (s) => s == protoType
                                ? Enumerable.Empty<Type>()
                                : null;
            }
            else { 
                //if prototype has gen args (or params), then - via a guard ensuring that subject is itself generic - we must
                //match each of subject's gen args to the given prototypical ones.            
                var subInspectors = protoType.GetGenericArguments()
                                                .Select(tt => CreateFor(tt))
                                                .ToArray();

                var protoGenDef = protoType.AsGenericDefinition();

                return (subject) => { 
                    if(!subject.IsGenericType) {
                        return null; //bad subject!
                    }
                    
                    if(subject.AsGenericDefinition() != protoGenDef) {
                        return null; //also bad subject!
                    }
                    

                    var subjectGenArgs = subject.GetGenericArguments();

                    if(subjectGenArgs.Length != subInspectors.Length) { 
                        return null; //avoids failing messily on subsequent loop
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



    public class SimpleTypeResolver : TypeResolver 
    {

    }


    public class TypeResolver
    {

    }


}
