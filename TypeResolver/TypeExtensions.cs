using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeResolver
{
    public static class TypeExtensions
    {
        
        public static bool IsOpenGeneric(this Type @this) 
        {
            if(@this.IsGenericTypeDefinition) {
                return true;
            }

            if(!@this.IsGenericType) {
                return false;
            }
            
            return @this.GetGenericArguments()
                        .Any(a => a.IsOpenGeneric());
        }


        public static Type AsGenericDefinition(this Type @this) 
        {
            return @this.IsGenericTypeDefinition ? @this : @this.GetGenericTypeDefinition();
        }


    }
}
