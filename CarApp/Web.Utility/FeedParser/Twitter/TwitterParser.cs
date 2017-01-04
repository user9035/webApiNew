using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Web.Utility.FeedParser.Core;

namespace Web.Utility.FeedParser.Twitter
{
    /// <summary>
    /// Provides the functionality to parse data from Twitter RSS feeds.
    /// </summary>
    class TwitterParser : FeedParserBase<JToken>
    {
        private readonly IHttpContentConverter converter;

        /// <summary>
        /// Gets an id.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>An id.</returns>
        protected override string GetId(JToken container)
        {
            return container["id_str"].Value<string>();
        }

        /// <summary>
        /// Gets a title.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A title.</returns>
        protected override string GetTitle(JToken container)
        {
            var text = container["text"].Value<string>();
            if (string.IsNullOrEmpty(text))
                return base.GetTitle(container);
            return ParserHelper.GetTitleDescription(text).Item1;
        }

        /// <summary>
        /// Gets a description.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A description.</returns>
        protected override string GetDescription(JToken container)
        {
            var text = container["text"].Value<string>();
            if (string.IsNullOrEmpty(text))
                return base.GetDescription(container);
            return ParserHelper.GetTitleDescription(text).Item2;
        }

        /// <summary>
        /// Gets a publish date.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A publish date.</returns>
        protected override DateTime GetPublishDate(JToken container)
        {
            var value = container["created_at"].Value<string>();
            return DateTime.ParseExact(value, "ddd MMM dd HH:mm:ss +ffff yyyy",
                new CultureInfo("en-US"));
        }

        /// <summary>
        /// Gets a link.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A link.</returns>
        protected override string GetLink(JToken container)
        {
            return $"http://www.twitter.com/screen_name/status/{this.GetId(container)}";
        }

        /// <summary>
        /// Parses the specified HTTP response content.
        /// </summary>
        /// <param name="response">An HTTP response content.</param>
        /// <returns>A list of <see cref="JToken" /></returns>
        protected override IEnumerable<JToken> GetResponseData(HttpResponseMessage response)
        {
            return this.converter.Convert<JArray>(response);
        }

        /// <summary>
        /// Gets a thumbnail.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A thumbnail.</returns>
        protected override string GetThumbnail(JToken container)
        {
            var token = container.SelectToken("entities.media[0].media_url");
            return token == null ? string.Empty : token.Value<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterParser"/> class.
        /// </summary>
        internal TwitterParser() : this(new JsonConverter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwitterParser"/> class.
        /// </summary>
        /// <param name="converter">A Json converter.</param>
        internal TwitterParser(IHttpContentConverter converter)
        {
            ExceptionHelper.CheckArgumentNull(converter, nameof(converter));
            this.converter = converter;
        }
    }
}
