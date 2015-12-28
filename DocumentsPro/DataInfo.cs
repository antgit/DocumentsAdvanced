using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Documents2012
{
    internal class DataInfo
    {
        public DataInfo()
        {

        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Provider;

        public string Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }
        private string _ConnectionString;

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }



    }
}
