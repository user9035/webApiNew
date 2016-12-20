using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Web.Utility.Rss.Core;

namespace Web.Utility.Rss.Youtube
{
    /// <summary>
    /// Provides the functionality to parse response from Youtube RSS feeds.
    /// </summary>
    class YoutubeParser : RssParserBase<JToken>
    {
        /// <summary>
        /// Gets a description.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A description.</returns>
        protected override string GetDescription(JToken token)
        {
            var description = token.SelectToken("snippet.description");
            return description.Value<string>();
        }

        /// <summary>
        /// Gets an id.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>An id.</returns>
        protected override string GetId(JToken token)
        {
            return token.SelectToken("snippet.resourceId.videoId").Value<string>();
        }

        /// <summary>
        /// Gets a link.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A link.</returns>
        protected override string GetLink(JToken token)
        {
            var videoId = this.GetId(token);
            return $"http://www.youtube.com/watch?v={videoId}";
        }

        /// <summary>
        /// Gets a publish date.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A publish date.</returns>
        protected override DateTime GetPublishDate(JToken token)
        {
            var publishDate = token.SelectToken("snippet.publishedAt");
            return publishDate.Value<DateTime>();
        }

        /// <summary>
        /// Gets a thumbnail.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A thumbnail.</returns>
        protected override string GetThumbnail(JToken token)
        {
            var thumbnail = token.SelectToken("snippet.thumbnails.medium.url");
            if (thumbnail == null)
                return string.Empty;

            var value = thumbnail.Value<string>();
            var uri = new Uri(value);
            return uri.Scheme == "https" ? $"http://{uri.Host}{uri.AbsolutePath}" : value;
        }

        /// <summary>
        /// Gets a title.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A title.</returns>
        protected override string GetTitle(JToken token)
        {
            return token.SelectToken("snippet.title").Value<string>();
        }

        /// <summary>
        /// Parses the specified HTTP response content.
        /// </summary>
        /// <param name="httpResponseContent">An HTTP response content.</param>
        /// <returns>A list of <see cref="JObject" /></returns>
        protected override IEnumerable<JToken> ParseInternal(string httpResponseContent)
        {
            return JObject.Parse(httpResponseContent)["items"];
        }

        /// <summary>
        /// Parses a response with uploads id.
        /// </summary>
        /// <param name="httpResponseContent">An HTTP response.</param>
        /// <returns>Uploads id.</returns>
        internal string ParseUploadsId(string httpResponseContent)
        {
            JObject jsonObject = JObject.Parse(httpResponseContent);
            return jsonObject.SelectToken("items[0].contentDetails.relatedPlaylists.uploads").Value<string>();
        }
    }
}
