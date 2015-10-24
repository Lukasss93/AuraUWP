using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Net.Common
{
    public class Link
    {
        public Link()
        {

        }

        public Link(string header, string content, Uri navuri)
        {
            Header = header;
            Content = content;
            NavUri = navuri;
        }

        private string header;

        public string Header
        {
            get { return header; }
            set { header = value; }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        private Uri navuri;

        public Uri NavUri
        {
            get { return navuri; }
            set { navuri = value; }
        }

    }
}
