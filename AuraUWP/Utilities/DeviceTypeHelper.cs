using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

namespace AuraUWP.Utilities
{
    public static class DeviceTypeHelper
    {
        public static DeviceFormFactorType GetDeviceFormFactorType()
        {
            switch(AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    return DeviceFormFactorType.Phone;
                case "Windows.Desktop":
                    return DeviceFormFactorType.Desktop;
                case "Windows.Universal":
                    return DeviceFormFactorType.IoT;
                case "Windows.Team":
                    return DeviceFormFactorType.SurfaceHub;
                default:
                    return DeviceFormFactorType.Other;
            }
        }

        public static bool IsTabletModeEnabled()
        {
            if(GetDeviceFormFactorType() == DeviceFormFactorType.Desktop)
            {
                return UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse ? false : true;
            }
            else
            {
                return false;
            }
        }
    }

    public enum DeviceFormFactorType
    {
        Phone,
        Desktop,
        IoT,
        SurfaceHub,
        Other
    }
}
