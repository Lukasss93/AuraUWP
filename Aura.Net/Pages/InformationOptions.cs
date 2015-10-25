using Aura.Net.Common;
using Aura.Net.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Net.Pages
{
    public class InformationOptions
    {
        public enum Pages { ABOUTME, CHANGELOG, MYAPPS, PRO }

        public InformationOptions()
        {
            Page = Pages.ABOUTME;
            AboutMePage = new AboutMePageOptions();
            ChangelogPage = new ChangelogPageOptions();
            MyAppsPage = new MyAppsPageOptions();
            ProPage = new ProPageOptions();
        }

        #region campi
        private Pages page;

        private AboutMePageOptions aboutmepage;
        private ChangelogPageOptions changelogpage;
        private MyAppsPageOptions myappspage;
        private ProPageOptions propage;
        
        #endregion

        #region proprietà
        public Pages Page
        {
            get { return page; }
            set { page = value; }
        }

        public AboutMePageOptions AboutMePage
        {
            get { return aboutmepage; }
            set { aboutmepage = value; }
        }

        public ChangelogPageOptions ChangelogPage
        {
            get { return changelogpage; }
            set { changelogpage = value; }
        }

        public MyAppsPageOptions MyAppsPage
        {
            get { return myappspage; }
            set { myappspage = value; }
        }
        
        public ProPageOptions ProPage
        {
            get { return propage; }
            set { propage = value; }
        }
        #endregion

        public class AboutMePageOptions
        {
            public AboutMePageOptions()
            {
                Header = "";
                Avatar = null;
                FullName = "";
                Nickname = "";
                Links = new List<Link>();
            }

            private string header;

            public string Header
            {
                get { return header; }
                set { header = value; }
            }


            private Uri avatar;

            public Uri Avatar
            {
                get { return avatar; }
                set { avatar = value; }
            }

            private string fullname;

            public string FullName
            {
                get { return fullname; }
                set { fullname = value; }
            }

            private string nickname;

            public string Nickname
            {
                get { return nickname; }
                set { nickname = value; }
            }

            private List<Link> links;

            public List<Link> Links
            {
                get { return links; }
                set { links = value; }
            }


        }

        public class ChangelogPageOptions
        {
            public ChangelogPageOptions()
            {
                Header = "";
                AppLogo = null;
                AppName = "";
                Changes = new List<Changelog>();
            }

            private string header;

            public string Header
            {
                get { return header; }
                set { header = value; }
            }

            private Uri applogo;

            public Uri AppLogo
            {
                get { return applogo; }
                set { applogo = value; }
            }


            private string appname;

            public string AppName
            {
                get { return appname; }
                set { appname = value; }
            }

            private List<Changelog> changes;

            public List<Changelog> Changes
            {
                get { return changes; }
                set { changes = value; }
            }

            private string current;

            public string Current
            {
                get { return current; }
                set { current = value; }
            }

            private string rate;

            public string Rate
            {
                get { return rate; }
                set { rate = value; }
            }



        }

        public class MyAppsPageOptions
        {
            public MyAppsPageOptions()
            {
                Header = "";
                MyAppsList = new List<MyApps>();
            }

            private string header;

            public string Header
            {
                get { return header; }
                set { header = value; }
            }

            private List<MyApps> myappslist;

            public List<MyApps> MyAppsList
            {
                get { return myappslist; }
                set { myappslist = value; }
            }
        }

        public class ProPageOptions
        {
            public ProPageOptions()
            {
                Header = "";
                ProEnabled = true;
            }

            private string header;

            public string Header
            {
                get { return header; }
                set { header = value; }
            }

            private bool proenabled;

            public bool ProEnabled
            {
                get { return proenabled; }
                set { proenabled = value; }
            }

        }

        
    }
}
