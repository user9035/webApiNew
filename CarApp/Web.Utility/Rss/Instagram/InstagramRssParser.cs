using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Web.Utility.Rss.Core;

namespace Web.Utility.Rss.Instagram
{
    /// <summary>
    /// Provides the functionality to parse response from Instagram RSS feeds.
    /// </summary>
    class InstagramRssParser : RssParserBase<JToken>
    {
        /// <summary>
        /// Gets a description.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A description.</returns>
        protected override string GetDescription(JToken token)
        {
            var text = token.SelectToken("caption.text");
            if (text != null)
            {
                var splittedText = ParserHelper.GetTitleDescription(text.Value<string>());
                return splittedText.Item2;
            }
            return this.GetPublishDate(token).ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets an id.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>An id.</returns>
        protected override string GetId(JToken token)
        {
            return token.SelectToken("user.id").Value<string>();
        }

        /// <summary>
        /// Gets a link.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A link.</returns>
        protected override string GetLink(JToken token)
        {
            return token["link"].Value<string>();
        }

        /// <summary>
        /// Gets a publish date.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A publish date.</returns>
        protected override DateTime GetPublishDate(JToken token)
        {
            var createdTime = token.SelectToken("created_time");
            return new DateTime(1970,1,1).AddSeconds(createdTime.Value<double>());
        }

        /// <summary>
        /// Gets a thumbnail.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A thumbnail.</returns>
        protected override string GetThumbnail(JToken token)
        {
            return token.SelectToken("images.standard_resolution.url").Value<string>();
        }

        /// <summary>
        /// Gets a title.
        /// </summary>
        /// <param name="token">A data container from RSS feed.</param>
        /// <returns>A title.</returns>
        protected override string GetTitle(JToken token)
        {
            var text = token.SelectToken("caption.text");
            if (text != null)
            {
                var splittedText = ParserHelper.GetTitleDescription(text.Value<string>());
                return splittedText.Item1;
            }
            var userName = token.SelectToken("user.full_name").Value<string>().Replace(" ", string.Empty);
            return $"http://instagram.com/{userName}";
        }

        /// <summary>
        /// Gets a user id.
        /// </summary>
        /// <param name="httpResponseContent">An HTTP response.</param>
        /// <returns>A user id.</returns>
        public string GetUserId(string httpResponseContent)
        {
            JObject jsonObject = JObject.Parse(httpResponseContent);
            return jsonObject.SelectToken("data[0].id").Value<string>();
        }

        /// <summary>
        /// Parses the specified HTTP response content.
        /// </summary>
        /// <param name="httpResponseContent">An HTTP response content.</param>
        /// <returns>A list of <see cref="JToken" /></returns>
        protected override IEnumerable<JToken> ParseInternal(string httpResponseContent)
        {
            return JObject.Parse(httpResponseContent)["data"];
        }
    }
}
