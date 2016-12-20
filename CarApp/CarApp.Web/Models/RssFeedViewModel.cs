using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entity;

namespace CarApp.Web.Models
{
    public class RssFeedViewModel
    {
        public string Caption
        {
            get;
            set;
        }

        public int Id
        {
            get;
            private set;
        }

        public Uri Link
        {
            get;
            set;
        }

        public RssFeedViewModel()
        {
        }

        public RssFeedViewModel(RssFeedEntity entity)
        {
            this.Id = entity.Id;
            this.Caption = entity.Caption;
            this.Link = entity.Uri;
        }
    }
}