using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AuraUWP.Extensions
{
    public static class CommandBarExtensions
    {
        /// <summary>
        /// Pulisce i pulsanti primari dalla commandbar
        /// </summary>
        public static void ClearPrimaryCommands(this CommandBar appbar)
        {
            while(appbar.PrimaryCommands.Count > 0)
            {
                appbar.PrimaryCommands.RemoveAt(0);
            }
        }

        /// <summary>
        /// Pulisce i pulsanti secondari dalla commandbar
        /// </summary>
        public static void ClearSecondaryCommands(this CommandBar appbar)
        {
            while(appbar.SecondaryCommands.Count > 0)
            {
                appbar.SecondaryCommands.RemoveAt(0);
            }
        }

        /// <summary>
        /// Pulisce tutti i pulsanti dalla commandbar
        /// </summary>
        public static void ClearAll(this CommandBar appbar)
        {
            ClearPrimaryCommands(appbar);
            ClearSecondaryCommands(appbar);
        }
    }
}
