using AuraRT.Display;
using AuraRT.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.System.Profile;

namespace AuraRT.Utilities
{
    public enum InfoLicense { Trial, Full, NotAvailable }
    public class IAPHelper
    {
        /// <summary>
        /// Compra un IAP
        /// </summary>
        public static async Task<PurchaseResults> PurchaseIAP(string iap)
        {
            PurchaseResults ricezione = await CurrentApp.RequestProductPurchaseAsync(iap);

            return ricezione;
        }

        /// <summary>
        /// Verifica se un IAP è attivo
        /// </summary>
        public static bool CheckIAP(string iap)
        {
            if (CurrentApp.LicenseInformation.ProductLicenses[iap].IsActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// Controlla se l'app è in versione trial
        /// </summary>
        public static InfoLicense CheckLicense(bool force=false)
        {
            if(force) { return InfoLicense.Trial; }
            

            if(CurrentApp.LicenseInformation.IsActive)
            {
                if(CurrentApp.LicenseInformation.IsTrial)
                {
                    return InfoLicense.Trial;
                }
                else
                {
                    return InfoLicense.Full;
                }
            }
            else
            {
                return InfoLicense.NotAvailable;
            }
        }

        /// <summary>
        /// Compra un IAP dallo store con maschera di risultato
        /// </summary>
        public static async Task PurchaseFromStore(string iap, string success, string already, string failed, string notfullfill, string done, string warning)
        {
            var result = await PurchaseIAP(iap);

            switch(result.Status)
            {
                //Acquisto effettuato
                case ProductPurchaseStatus.Succeeded:
                    MessageDialogHelper.Show(success, done);
                    break;

                //Hai già acquistato questo prodotto
                case ProductPurchaseStatus.AlreadyPurchased:
                    MessageDialogHelper.Show(already, warning);
                    break;

                //Acquisto fallito
                case ProductPurchaseStatus.NotPurchased:
                    MessageDialogHelper.Show(failed, warning);
                    break;

                //L'acquisto non si è verificato perchè un acquisto precedente di questo prodotto a consumo non è andato a buon fine.
                case ProductPurchaseStatus.NotFulfilled:
                    MessageDialogHelper.Show(notfullfill, warning);
                    break;
            }
        }


        /// <summary>
        /// Compra un IAP dallo store tramite un codice promo con maschera di risultato
        /// </summary>
        public static async Task PurchaseFromPromo(string iap, string insertcode, string activatefromcode, string ok, string cancel, string loading, string post_progetto, string post_azione, string url, string purchase_success, string purchase_already, string purchase_failed, string purchase_notfullfill, string done, string warning, string codenotvalid, string error, string codesoldout, string nointernet, string report_body, string report_title, string report_button)
        {
            string codice=null;

            var dialog = await MessageDialogHelper.DialogTextBox(insertcode, activatefromcode, "", ok, cancel);
            if(dialog.result==true)
            {
                codice=dialog.output;

                await StatusBarHelper.ShowLoading(loading);

                try
                {
                    //Parametri da passare in POST
                    HttpContent postdata = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("progetto", post_progetto),
                        new KeyValuePair<string, string>("codice", codice),
                        new KeyValuePair<string, string>("azione", post_azione),
                        new KeyValuePair<string, string>("nocache", DateTime.Now.Ticks.ToString()),
                    });

                    //INIZIALIZZA RISULTATO
                    string result=null;

                    //AVVIO LA CONNESSIONE
                    HttpClient client = new HttpClient();
                    var response = await client.PostAsync(url, postdata);

                    StatusBarHelper.HideLoading();

                    if(response.IsSuccessStatusCode)
                    {
                        //leggi risultato
                        var responseStream = await response.Content.ReadAsStreamAsync();
                        using(StreamReader responseReader = new StreamReader(responseStream))
                        {
                            result = responseReader.ReadToEnd();
                        }

                        //deserializza
                        ResultPromoCode res = Json.Deserialize<ResultPromoCode>(result);

                        
                        if(res.stato)
                        {
                            if(res.promo==iap)
                            {
                                await PurchaseFromStore(iap, purchase_success, purchase_already, purchase_failed, purchase_notfullfill, done, warning);
                            }
                            else
                            {
                                MessageDialogHelper.Show(codenotvalid, error);
                            }
                        }
                        else
                        {
                            switch(res.messaggio)
                            {
                                case "Ex02":
                                    MessageDialogHelper.Show(codenotvalid, error);
                                    break;
                                case "Ex03":
                                    MessageDialogHelper.Show(codesoldout, error);
                                    break;
                                default:
                                    ReportHelper.SendEmail(res.messaggio, Utility.Appname(), report_body, report_title, report_button, cancel);
                                    break;
                            }
                        }

                    }
                    else
                    {
                        //ERRORE NELLA CONNESSIONE AL SERVER
                        MessageDialogHelper.Show(nointernet, error);
                    }

                }
                catch(Exception ex)
                {
                    var messaggio=ex.Message+"\n\n"+ex.StackTrace+"\n\n"+ex.HResult;
                    ReportHelper.SendEmail(messaggio, Utility.Appname(), report_body, report_title, report_button, cancel);
                }

                StatusBarHelper.HideLoading();

                
            }


        }
    }

    public class ResultPromoCode
    {
        public bool stato { get; set; }
        public string promo { get; set; }
        public string messaggio { get; set; }
    }
}
