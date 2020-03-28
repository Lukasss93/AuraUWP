using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AuraUWP.Display
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
            DialogTextBoxResult risposta = new DialogTextBoxResult
            {
                Result = false,
                Output = null
            };



            StackPanel stack = new StackPanel();

            if(content != null)
            {
                TextBlock contenuto = new TextBlock
                {
                    Text = content
                };
                stack.Children.Add(contenuto);
            }

            InputScope scope = new InputScope();
            scope.Names.Add(new InputScopeName(scopename));

            TextBox tb = new TextBox
            {
                Text = box,
                InputScope = scope,
                Header = header,
                Margin = new Thickness(0, 5, 0, 5)
            };
            stack.Children.Add(tb);

            if(sub!=null)
            {
                TextBlock subba = new TextBlock
                {
                    Text = sub,
                    TextWrapping = TextWrapping.Wrap
                };
                stack.Children.Add(subba);
            }


            ContentDialog cd = new ContentDialog
            {
                Title = title,
                Content = stack,
                PrimaryButtonText = ok
            };

            cd.PrimaryButtonClick+= (s, ev) =>
            {
                risposta.Result=true;
                risposta.Output=tb.Text;
            };

            cd.SecondaryButtonText=cancel;
            cd.SecondaryButtonClick+= (s, ev) =>
            {
                risposta.Result=false;
                risposta.Output=null;
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
        public bool Result { get; set; }
        public string Output { get; set; }
    }
}
