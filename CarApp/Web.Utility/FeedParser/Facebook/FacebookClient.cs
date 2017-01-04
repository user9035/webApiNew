using System;
using System.Net.Http;
using Web.Utility.FeedParser.Core;

namespace Web.Utility.FeedParser.Facebook
{
    /// <summary>
    /// Provides the functionality to read data from Facebook feed.
    /// </summary>
    sealed class FacebookClient : ApiClient, IPhotoUriProvider
    {
        private FacebookFeedParser parser;

        private FacebookFeedParser Parser
        {
            get
            {
                if (this.parser == null)
                    this.parser = new FacebookFeedParser(this);
                return parser;
            }
        }

        /// <summary>
        /// Gets the API URI.
        /// </summary>
        protected override Uri GetApiUri()
        {
            return new Uri("https://graph.facebook.com/");
        }

        /// <summary>
        /// Create a new instance of the <see cref="Uri"/> to get data from Facebook RSS feed.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of Facebook RSS feed.</param>
        /// <returns>A new instance of the <see cref="Uri"/></returns>
        protected override HttpRequestMessage CreateRequest(Uri uri)
        {
            var userId = this.GetUserId(uri);
            var path = $"{userId}/posts/";
            var parameters = "fields=created_time,from,id,message,object_id,picture,type";
            var message = new HttpRequestMessage(HttpMethod.Get, this.CreateApiUri(path, parameters));
            this.AddAuthorizationHeader(message);
            return message;
        }

        /// <summary>
        /// Adds the authorization header to the passed request message.
        /// </summary>
        /// <param name="message">A request message.</param>
        private void AddAuthorizationHeader(HttpRequestMessage message)
        {
            message.Headers.Add("Authorization", "OAuth 886497794801411|NnXdVVVPG2OlqoTp6T4atG0u8kQ");
        }

        /// <summary>
        /// Gets an URI to post photo.
        /// </summary>
        /// <param name="photoId">A photo id.</param>
        /// <returns>An URI to post photo.</returns>
        string IPhotoUriProvider.GetPhotoUri(long photoId)
        {
            string path = $"{photoId}/picture";
            string parameters = "type=normal";
            var photoUri = this.CreateApiUri(path, parameters);
            using (var response = this.GetHttpResponseMessage(photoUri))
            {
                //
                // Since Facebook returns image in binary view and does additional request.
                // The URI of image is kept in request message.
                //
                return response.RequestMessage.RequestUri.AbsoluteUri;
            }
        }

        /// <summary>
        /// Gets an instance of RSS parser to handle a responce from Facebook RSS feed.
        /// </summary>
        /// <returns>An instance of RSS parser.</returns>
        protected override IFeedParser GetParser()
        {
            return this.Parser;
        }

        /// <summary>
        /// Gets an user id.
        /// </summary>
        /// <param name="uri">A URI of Facebook RSS feed.</param>
        /// <returns>An user id.</returns>
        private string GetUserId(Uri uri)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, this.CreateApiUri(uri.AbsoluteUri, string.Empty));
            this.AddAuthorizationHeader(message);
            var response = this.GetHttpResponseMessage(message);
            var userData = this.Parser.ParseUserData(response);
            return userData.UserId;
        }
    }
}
