using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraRT.Extensions
{
    public static class String_Extension
    {
        /// <summary>
        /// Aggiunge uno 0 a sinistra di una stringa se essa ha solo 1 carattere
        /// </summary>
        public static string AddZero(this String stringa)
        {
            return (stringa.Count()==1)?"0"+stringa:stringa;
        }

        /// <summary>
        /// Restituisce la stringa con la prima lettera in maiuscolo.
        /// </summary>
        public static string ToUpperFirst(this String str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Restituisce la stringa con la prima lettera di ogni parola in maiuscolo.
        /// </summary>
        public static string ToUpperWords(this String str)
        {
            string output="";

            string[] stringhe = str.Split(' ');

            int i=0;
            foreach(string s in stringhe)
            {
                i++;

                output+=s.ToUpperFirst();

                if(i==stringhe.Count())
                {
                    output+=" ";
                }
            }

            return output;
        }


        /// <summary>
        /// Restituisce la stringa senza estensione
        /// </summary>
        public static string RemoveExtension(this String str)
        {
            string output = "";

            string[] stringhe = str.Split('.');

            for(int i=0; i<stringhe.Count()-1; i++)
            {
                output += stringhe[i];
            }

            return output;
        }

        public static bool areNumbers(this String str)
        {
            foreach(char c in str)
            {
                if(!Char.IsDigit(c))
                {
                    return false;
                }                
            }
            return true;
        }

        public static bool CheckLetters(this String str, string check)
        {
            int err = 0;


            foreach(char c in check)
            {
                if(!(str.IndexOf(c)!=-1))
                {
                    err++;
                }
            }
            
            return err==0?true:false;
        }
    }
}
