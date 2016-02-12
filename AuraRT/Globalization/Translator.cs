using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Globalization
{
    public class Translator
    {
        public string lingua { get; set; }
        public string nome { get; set; }

        public Translator(string l, string n)
        {
            lingua=l;
            nome=n;
        }
    }
}
