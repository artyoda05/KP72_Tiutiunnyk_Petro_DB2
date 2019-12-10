using System;
using System.Collections.Generic;
using System.Text;

namespace lab2.Database
{
    class SearchResult
    {
        private long id;
        private string attr;
        private string ts_headline;

        public SearchResult(long id, string attr, string ts_headline)
        {
            Id = id;
            this.Attr = attr;
            this.Ts_headline = ts_headline;
        }

        public long Id { get => id; set => id = value; }
        public string Attr { get => attr; set => attr = value; }
        public string Ts_headline { get => ts_headline; set => ts_headline = value; }
    }
}
