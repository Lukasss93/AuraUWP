using Aura.Net.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Net.Pages
{
    public class InformationParameter
    {
        public InformationParameter(bool pro, Pages page, InformationParameterNavbarIcons navbaricons, InformationParameterInfo info, List<Changelog> changelog)
        {
            this.Pro = pro;
            this.Page = page;
            this.NavbarIcons = navbaricons;
            this.Info = info;
            this.Changelog = changelog;
        }

        private bool pro;
        public bool Pro
        {
            get { return pro; }
            set { pro = value; }
        }
        
        private Pages page;
        public Pages Page
        {
            get { return page; }
            set 
            {
                if(this.Pro == false && value == Pages.PRO)
                {
                    throw new Exception("Non puoi impostare la proprietà Page a 'PRO' se la proprietà Pro è false.");
                }
                else
                {
                    page = value;
                }
            }
        }

        private InformationParameterNavbarIcons navbaricons;
        public InformationParameterNavbarIcons NavbarIcons
        {
            get { return navbaricons; }
            set { navbaricons = value; }
        }

        private InformationParameterInfo info;
        public InformationParameterInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        private List<Changelog> changelog;
        public List<Changelog> Changelog
        {
            get { return changelog; }
            set { changelog = value; }
        }

                

        

        public enum Pages
        {
            INFO,
            VERSIONS,
            APPS,
            PRO
        }
        
    }

    public class InformationParameterNavbarIcons
    {
        public InformationParameterNavbarIcons(Uri uri_info, Uri uri_versions, Uri uri_apps, Uri uri_pro)
        {
            this.UriInfo = uri_info;
            this.UriVersions = uri_versions;
            this.UriApps = uri_apps;
            this.UriPro = uri_pro;
        }

        private Uri uri_info;
        public Uri UriInfo
        {
            get { return uri_info; }
            set { uri_info = value; }
        }

        private Uri uri_versions;
        public Uri UriVersions
        {
            get { return uri_versions; }
            set { uri_versions = value; }
        }

        private Uri uri_apps;
        public Uri UriApps
        {
            get { return uri_apps; }
            set { uri_apps = value; }
        }

        private Uri uri_pro;
        public Uri UriPro
        {
            get { return uri_pro; }
            set { uri_pro = value; }
        }
    }

    public class InformationParameterInfo
    {
        public InformationParameterInfo(Uri uri_avatar)
        {
            this.UriAvatar = uri_avatar;
        }

        private Uri uri_avatar;
        public Uri UriAvatar
        {
            get { return uri_avatar; }
            set { uri_avatar = value; }
        }
        
    }
}
