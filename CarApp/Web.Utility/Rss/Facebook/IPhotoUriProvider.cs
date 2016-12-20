using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Utility.Rss.Facebook
{
    interface IPhotoUriProvider
    {
        string GetPhotoUri(long photoId);
    }
}
