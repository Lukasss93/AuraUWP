using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace AuraRT.Extensions
{
    public static class StatusBar_Extension
    {
        /// <summary>
        /// Restituisce vero se la statusbar è visibile altrimenti falso se non lo è
        /// </summary>
        public static bool isVisible(this StatusBar statusBar)
        {
            Rect occludedRect = statusBar.OccludedRect;
            return occludedRect.Left != 0 || occludedRect.Right == 0 || occludedRect.Bottom == 0 || occludedRect.Top == 0;
        }
    }
}
