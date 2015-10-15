using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Net.Cryptography
{
    public class RSA
    {
        /// <summary>
        /// Cripta una stringa in RSA
        /// </summary>
        public static string Cripta(string messaggio)
        {
            string risultato = "";

            int e = 79;
            int N = 3337;

            foreach(var carattere in messaggio)
            {
                int lettera= (int)carattere;

                var valore=lettera%N;
                for(int i=2; i<=e; i++)
                {
                    valore=(valore*lettera)%N;
                }

                risultato += valore + ";";
            }


            return risultato;
        }

        /// <summary>
        /// Decripta una stringa in RSA
        /// </summary>
        public static string Decripta(string messaggio)
        {
            string risultato="";

            int d=1019;
            int N=3337;

            string[] dato = messaggio.Split(';');
            for(int j=0; j<dato.Count()-1; j++)
            {
                var valore=Convert.ToInt32(dato[j])%N;
                for(int i=2; i<=d; i++)
                {
                    valore=(valore*Convert.ToInt32(dato[j]))%N;
                }
                risultato+=(char)valore;
            }

            return risultato;
        }
    }
}
