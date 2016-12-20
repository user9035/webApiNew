using Newtonsoft.Json;

namespace Web.Utility.Rss.Facebook
{
    class FacebookUserInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string UserId
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "name")]
        public string UserName
        {
            get;
            set;
        }
    }
}
