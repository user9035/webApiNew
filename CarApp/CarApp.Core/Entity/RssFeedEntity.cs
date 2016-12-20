using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public sealed class RssFeedEntity : EntityBase
    {
        public string Caption { get; set; }

        public string UriString
        {
            get;
            set;
        }

        public Uri Uri
        {
            get
            {
                return new Uri(this.UriString);
            }
            set
            {
                this.UriString = value?.AbsoluteUri;
            }
        }
    }
}
