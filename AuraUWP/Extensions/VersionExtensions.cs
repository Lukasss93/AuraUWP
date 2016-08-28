using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraUWP.Extensions
{
    public static class VersionExtensions
    {
        public static string ToStringRelevance(this Version v)
        {
            if(v.Revision == 0 && v.Build != 0)
            {
                return v.Major + "." + v.Minor + "." + v.Build;
            }
            else if(v.Revision == 0 && v.Build == 0)
            {
                return v.Major + "." + v.Minor;
            }
            else
            {
                return v.ToString();
            }
        }
    }
}
