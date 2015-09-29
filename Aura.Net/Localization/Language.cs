using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Aura.Net.Localization
{
    [Bindable]
    public class Language
    {
        public string nome { get; set; }
        public string codice { get; set; }

        public Language(string n, string c)
        {
            nome=n;
            codice=c;
        }

        public string GetName()
        {
            return nome;
        }

        public string GetCode()
        {
            return codice;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is Language))
                return false;

            Language card =(Language)obj;
            return card.GetName().Equals(nome) && card.GetCode().Equals(codice);
        }

        public override int GetHashCode()
        {
            return (nome+codice).GetHashCode();
        }
    }
}
