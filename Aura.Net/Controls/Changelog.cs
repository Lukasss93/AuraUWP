using Aura.Net.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace Aura.Net.Controls
{
    /// <summary>
    /// Classe che consente, dato in input una lista di Changelog, di stampare in uno stack in input tutti gli elementi del Changelog.
    /// </summary>
    public class Changelog
    {
        /// <summary>Versione della lista dei cambiamenti.</summary>
        private Version _version { get; set; }
        /// <summary>Lista di cambiamenti.</summary>
        private List<string> _changes { get; set; }

        /// <summary>Costruttore che inizializza un elemento Changelog.</summary>
        /// <param name="version">Versione della lista dei cambiamenti.</param>
        /// <param name="changes">Lista di cambiamenti.</param>
        public Changelog(Version version, List<string> changes)
        {
            _version = version;
            _changes = changes;
        }

        /// <summary>Restituisce la versione della lista dei cambiamenti.</summary>
        public Version GetVersion()
        {
            return _version;
        }

        /// <summary>Restituisce la lista dei cambiamenti.</summary>
        public List<string> GetChanges()
        {
            return _changes;
        }

        /// <summary>Stampa in uno StackPanel in input tutti gli elementi della lista della lista di Changelog.</summary>
        /// <param name="list">Lista di Changelog.</param>
        /// <param name="stackpanel">StackPanel in cui stampare gli elementi.</param>
        /// <param name="current">Stringa che appare nella prima versione del changelog. Default: "corrente".</param>
        /// <param name="versionforeground">Colore del testo della versione. Default: Colore principale del telefono.</param>
        public static void GenerateChangelog(List<Changelog> list, StackPanel stackpanel, string currentstring="corrente", SolidColorBrush versionforeground=null, SolidColorBrush textforeground=null)
        {
            stackpanel.Children.Clear();

            int i=0;

            foreach(Changelog item in list)
            {
                i++;

                StackPanel pannello = new StackPanel();
                pannello.HorizontalAlignment=HorizontalAlignment.Stretch;
                pannello.VerticalAlignment=VerticalAlignment.Top;
                pannello.Orientation = Orientation.Vertical;
                pannello.Margin = new Thickness(0, 0, 0, 10);

                TextBlock versione = new TextBlock();
                versione.FontFamily = new FontFamily("Segoe WP");
                versione.FontWeight= FontWeights.Thin;
                versione.FontSize= 34;
                versione.Foreground = versionforeground == null ? MyColors.PhoneAccentBrush : versionforeground;

                versione.Inlines.Add(new Run() { Text = item.GetVersion().ToString() });
                if(i == 1) { versione.Inlines.Add(new Run() { Text = " (" + currentstring + ")", FontSize=22 }); }



                pannello.Children.Add(versione);

                foreach(string cambiamento in item.GetChanges())
                {
                    TextBlock testo_cambiamento = new TextBlock();
                    testo_cambiamento.Foreground = textforeground == null ? Application.Current.Resources["PhoneForegroundBrush"] as Brush : textforeground;
                    testo_cambiamento.FontFamily = new FontFamily("Segoe WP");
                    testo_cambiamento.FontWeight= FontWeights.Normal;
                    testo_cambiamento.TextWrapping=TextWrapping.Wrap;
                    testo_cambiamento.FontSize= 18;
                    testo_cambiamento.Text = "• " + cambiamento;
                    pannello.Children.Add(testo_cambiamento);

                }
                stackpanel.Children.Add(pannello);
            }
        }
    }
}
