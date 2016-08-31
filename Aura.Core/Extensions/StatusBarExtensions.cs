using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace Aura.Extensions
{
    public static class StatusBarExtensions
    {
        /// <summary>
        /// Restituisce vero se la statusbar è visibile altrimenti falso se non lo è
        /// </summary>
        public static bool IsVisible(this StatusBar statusBar)
        {
            Rect occludedRect = statusBar.OccludedRect;
            return occludedRect.Left != 0 || occludedRect.Right == 0 || occludedRect.Bottom == 0 || occludedRect.Top == 0;
        }
    }
}
