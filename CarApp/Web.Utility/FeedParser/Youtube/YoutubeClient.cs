using System;
using System.Net.Http;
using Web.Utility.FeedParser.Core;

namespace Web.Utility.FeedParser.Youtube
{
    /// <summary>
    /// Provides the functionality to get data from Youtube RSS feeds.
    /// </summary>
    class YoutubeClient : ApiClient
    {
        private YoutubeParser parser = new YoutubeParser();

        /// <summary>
        /// Create a new instance of the <see cref="HttpRequestMessage"/> to get data from RSS feed.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of RSS feed.</param>
        /// <returns>A new instance of the <see cref="HttpRequestMessage"/></returns>
        protected override HttpRequestMessage CreateRequest(Uri uri)
        {
            var uploadsId = this.GetUpploadsId(uri);
            var apiUri = this.CreateApiUri("youtube/v3/playlistItems", 
                $"key={this.GetAccessToken()}&part=snippet&maxResults=30&playlistId={uploadsId}");
            return new HttpRequestMessage(HttpMethod.Get, apiUri);
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns>The access token.</returns>
        private string GetAccessToken()
        {
            return "AIzaSyCyrvAvJdXBdyWKoDJYQAVgrITaPpw212s";
        }

        /// <summary>
        /// Gets the API URI.
        /// </summary>
        protected override Uri GetApiUri()
        {
            return new Uri("https://www.googleapis.com/");
        }

        /// <summary>
        /// Gets a channel id.
        /// </summary>
        /// <param name="uri">A URI which contains a channel id.</param>
        /// <returns>A channel id.</returns>
        private string GetChannelId(Uri uri)
        {
            var nodes = uri.AbsolutePath.Split('/');
            if (nodes.Length >= 3 && nodes[2].StartsWith("UC"))
                return nodes[2];
            throw new ArgumentException("The channel id is missing in the specified URI.");
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
        /// <param name="uri">A URI which contains a user id.</param>
        /// <returns>A user id.</returns>
        private string GetUserId(Uri uri)
        {
            var nodes = uri.AbsolutePath.Split('/');
            if (nodes.Length >= 2 && nodes[1].StartsWith("UU"))
                return nodes[1];
            throw new ArgumentException("The user id is missing in the specified URI.");
        }

        /// <summary>
        /// Gets uploads id.
        /// </summary>
        /// <param name="uri">A URI to get user or channel id.</param>
        /// <returns>Uploads id.</returns>
        private string GetUpploadsId(Uri uri)
        {
            var parameters = $"key={this.GetAccessToken()}&part=contentDetails" + (uri.AbsolutePath.StartsWith("user")
                ? $"&forUsername={this.GetUserId(uri)}"
                : $"&id={this.GetChannelId(uri)}");
            var uriApi = this.CreateApiUri("youtube/v3/channels", parameters);
            return this.parser.ParseUploadsId(this.GetString(uriApi));
        }
    }
}
