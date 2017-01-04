using System;
using System.Net.Http;
using DotNetOpenAuth.OAuth;
using Web.Utility.FeedParser.Core;

namespace Web.Utility.FeedParser.Twitter
{
    /// <summary>
    /// Provides the functionality to get data from Twitter.
    /// </summary>
    class TwitterClient : ApiClient
    {
        /// <summary>
        /// Adds the authorization header to the passed request message.
        /// </summary>
        /// <param name="message"></param>
        private void AddAuthorizationHeader(HttpRequestMessage message)
        {
            using (var messageHandler = new OAuth1HmacSha1HttpMessageHandler())
            {
                messageHandler.AccessToken = "702871256249602048-1omG2JuXYyGksU33j0UFltVZgbd2Ak8";
                messageHandler.AccessTokenSecret = "fi6p2TtMkEmXABHOX3Ra8EEvtKXoKEKvwhK5SFwOyMDjD";
                messageHandler.ConsumerKey = "RPJTD9pc0AeCRlFuD4IN1n54w";
                messageHandler.ConsumerSecret = "wZIWRuLzd6gwc5EZGgJNZd4Hhjypqf7HSsYy0p03BI6H1rTesJ";
                messageHandler.ApplyAuthorization(message);
            }
        }

        /// <summary>
        /// Create a new instance of the <see cref="HttpRequestMessage"/> to get data from RSS feed.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of RSS feed.</param>
        /// <returns>A new instance of the <see cref="HttpRequestMessage"/></returns>
        protected override HttpRequestMessage CreateRequest(Uri uri)
        {
            var userId = this.GetUserId(uri);
            string query = $"screen_name={userId}&count=50&exclude_replies=false&include_rts=false&lang=en";
            var apiUri = this.CreateApiUri("1.1/statuses/user_timeline.json", query);
            var message = new HttpRequestMessage(HttpMethod.Get, apiUri);
            this.AddAuthorizationHeader(message);
            return message;
        }

        /// <summary>
        /// Gets the API URI of Twitter.
        /// </summary>
        protected override Uri GetApiUri()
        {
            return new Uri("https://api.twitter.com/");
        }

        /// <summary>
        /// Gets a parser to handle server response.
        /// </summary>
        /// <returns>A parser.</returns>
        protected override IFeedParser GetParser()
        {
            return new TwitterParser();
        }

        /// <summary>
        /// Gets a user id.
        /// </summary>
        /// <param name="uri">A URI from which a user id is retrieved.</param>
        /// <returns>A user id.</returns>
        private string GetUserId(Uri uri)
        {
            return uri.AbsolutePath.TrimStart('/');
        }
    }
}
