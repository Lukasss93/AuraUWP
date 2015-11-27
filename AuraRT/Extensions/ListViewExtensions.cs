using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AuraRT.Extensions
{
    public static class ListViewExtensions
    {
        /// <summary>
        /// Restituisce true, se la modalità seleziona (multiple o extended) è attiva
        /// </summary>
        public static bool IsSelectionEnabled(this ListView lista)
        {
            if(lista.SelectionMode==ListViewSelectionMode.Multiple || lista.SelectionMode==ListViewSelectionMode.Extended)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Restituisce una Lista di T degli elementi selezionati dalla ListView
        /// </summary>
        public static List<T> GetSelectedItems<T>(this ListView c)
        {
            List<T> selezioni = new List<T>();
            foreach(T valori in c.SelectedItems)
            {
                selezioni.Add(valori);
            }
            return selezioni;
        }
    }
}
