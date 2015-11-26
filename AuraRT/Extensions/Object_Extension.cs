using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Extensions
{
    public static class Object_Extension
    {
        public static string ToStringNullSafe(this object value)
        {
            return (value ?? string.Empty).ToString();
        }
    }
}
