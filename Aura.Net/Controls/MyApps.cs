using System;

namespace Aura.Net.Controls
{
    public class MyApps
    {
        private Guid _id { get; set; }
        private Uri _url { get; set; }
        private string _name { get; set; }

        public MyApps(Guid id, Uri uri, string name)
        {
            _id=id;
            _url=uri;
            _name=name;
        }

        public string getID()
        {
            return _id.ToString();
        }

        public Uri GetURI()
        {
            return _url;
        }

        public string getName()
        {
            return _name;
        }

    }
}
