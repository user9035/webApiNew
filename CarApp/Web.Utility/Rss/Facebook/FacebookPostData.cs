using System;
using Newtonsoft.Json;

namespace Web.Utility.Rss.Facebook
{
    class FacebookPostData
    {
        [JsonProperty(PropertyName = "created_time")]
        public DateTime Created
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "object_id")]
        public long ObjectId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "picture")]
        public string PicturePath
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "type")]
        public PostObjectType Type
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "from")]
        public FacebookUserInfo User
        {
            get;
            set;
        }
    }
}