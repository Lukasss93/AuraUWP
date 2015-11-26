using AuraRT.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Email;

namespace AuraRT.Utilities
{
    public class ReportHelper
    {
        /// <summary>
        /// Crea una finestra di dialogo per l'invio, tramite email, di un report
        /// </summary>
        public static async void SendEmail(string eccezione, string appname, string body, string title, string report, string cancel)
        {
            var risultato = await MessageDialogHelper.Confirm(body, title, report, cancel);
            if(risultato==true)
            {
                EmailMessage em = new EmailMessage();
                em.Subject="[BUG] "+appname;
                em.Body=eccezione;
                em.To.Add(new EmailRecipient("windowsphone@lucapatera.it"));

                await EmailManager.ShowComposeNewEmailAsync(em);
            }
        }
    }
}
