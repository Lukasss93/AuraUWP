using System;
using System.Threading.Tasks;
using AuraUWP.Display;
using Windows.ApplicationModel.Store;

namespace AuraUWP.Utilities
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
    }
}
