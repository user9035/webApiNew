using System;
using System.Net.Http;
using Web.Utility.FeedParser.Core;

namespace Web.Utility.FeedParser.Instagram
{
    /// <summary>
    /// Provides the functionality to fetch data from Instagram.
    /// </summary>
    class InstagramClient : ApiClient
    {
        private InstagramFeedParser parser = new InstagramFeedParser();

        /// <summary>
        /// Create a new instance of the <see cref="HttpRequestMessage"/> to get data from RSS feed.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of RSS feed.</param>
        /// <returns>A new instance of the <see cref="HttpRequestMessage"/></returns>
        protected override HttpRequestMessage CreateRequest(Uri uri)
        {
            var userId = this.GetUserId(uri);
            var accesToken = this.GetAccessToken();
            return new HttpRequestMessage(HttpMethod.Get, this.CreateApiUri($"v1/users/{userId}/media/recent/", 
                $"access_token={accesToken}"));
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>The access token.</returns>
        private string GetAccessToken()
        {
            return "3154430999.3f8386e.86a77c0605264accbdfcdaaf477c8da2";
        }

        /// <summary>
        /// Gets the API URI of Instagram.
        /// </summary>
        protected override Uri GetApiUri()
        {
            return new Uri("https://api.instagram.com/");
        }

        /// <summary>
        /// Gets a parser to handle server response.
        /// </summary>
        /// <returns>A parser.</returns>
        protected override IFeedParser GetParser()
        {
            return this.parser;
        }

        /// <summary>
        /// Gets a user id.
        /// </summary>
        /// <param name="uri">A URI.</param>
        /// <returns>A user id.</returns>
        private string GetUserId(Uri uri)
        {
            var userName = uri.AbsolutePath.Trim('/');
            var requestUri = this.CreateApiUri("v1/users/search", $"q={userName}&access_token={this.GetAccessToken()}");
            return this.parser.GetUserId(this.GetString(requestUri));
        }
    }
}
