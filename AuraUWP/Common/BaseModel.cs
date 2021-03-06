using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel;

namespace AuraUWP.Common
{
    public class BaseModel : INotifyPropertyChanged
    {
        public static bool IsInDesignMode
        {
            get
            {
                return DesignMode.DesignModeEnabled;
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
