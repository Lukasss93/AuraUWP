using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Aura.Net
{
    public class MessageDialogHelper
    {
        /// <summary>
        /// Visualizza una finestra di dialogo asincrona
        /// </summary>
        public static async void Show(string content, string title="", string ok="ok")
        {
            MessageDialog messageDialog = new MessageDialog(content, title);
            messageDialog.Commands.Add(new UICommand(ok, null, true));

            await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Visualizza una finestra di dialogo sincrona
        /// </summary>
        public static async Task ShowSync(string content, string title="")
        {
            MessageDialog messageDialog = new MessageDialog(content, title);
            await messageDialog.ShowAsync();
        }

        /// <summary>
        /// Visualizza una finestra di conferma con ok, annulla
        /// </summary>
        public static async Task<bool> Confirm(string content, string title, string ok="ok", string cancel="annulla")
        {
            try
            {
                MessageDialog msgDialog = new MessageDialog(content, title);

                msgDialog.Commands.Add(new UICommand(ok, null, true));
                msgDialog.Commands.Add(new UICommand(cancel, null, false));
                msgDialog.CancelCommandIndex=1;

                var result= await msgDialog.ShowAsync();

                if((bool)result.Id == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message+"\n\n"+ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Visualizza una finestra di dialogo con TextBox
        /// </summary>
        public static async Task<DialogTextBoxResult> DialogTextBox(string content, string title, string box="", string ok="ok", string cancel="annulla", string sub=null, string header=null, InputScopeNameValue scopename=InputScopeNameValue.Default)
        {
            DialogTextBoxResult risposta = new DialogTextBoxResult();
            risposta.result=false;
            risposta.output=null;



            StackPanel stack = new StackPanel();

            TextBlock contenuto = new TextBlock();
            contenuto.Text=content;
            stack.Children.Add(contenuto);

            InputScope scope = new InputScope();
            scope.Names.Add(new InputScopeName(scopename));

            TextBox tb = new TextBox();
            tb.Text=box;
            tb.InputScope = scope;
            tb.Header = header;
            tb.Margin = new Thickness(0,5,0,5);
            stack.Children.Add(tb);

            if(sub!=null)
            {
                TextBlock subba = new TextBlock();
                subba.Text=sub;
                subba.TextWrapping = TextWrapping.Wrap;
                stack.Children.Add(subba);
            }


            ContentDialog cd = new ContentDialog();
            cd.Title = title;
            cd.Content = stack;


            cd.PrimaryButtonText = ok;
            cd.PrimaryButtonClick+= (s, ev) =>
            {
                risposta.result=true;
                risposta.output=tb.Text;
            };

            cd.SecondaryButtonText=cancel;
            cd.SecondaryButtonClick+= (s, ev) =>
            {
                risposta.result=false;
                risposta.output=null;
            };

            cd.Opened+=(s, ev) =>
            {
                tb.Focus(FocusState.Keyboard);
                tb.SelectAll();
            };

            await cd.ShowAsync();

            return risposta;
        }

    }

    public class DialogTextBoxResult
    {
        public bool result { get; set; }
        public string output { get; set; }
    }
}
