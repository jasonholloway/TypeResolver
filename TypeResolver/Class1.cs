using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeResolver
{
    public static class Analyser
    {

        public static TypeVector Analyse(Type concreteType, Type abstractType) 
        {
            Debug.Assert(concreteType.IsSubclassOf(abstractType));

            throw new NotImplementedException();
        }


    }


    public class TypeVector
    {

    }


}
