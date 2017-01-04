using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Mikz.Feed.Contracts.Events;

namespace Web.Utility.FeedParser.Core
{
    /// <summary>
    ///     Provides base functionality to parse string from RSS feed.
    /// </summary>
    /// <owner>Vladimir Smolenskiy</owner>
    /// <typeparam name="TContainer">The type of data container.</typeparam>
    public abstract class FeedParserBase<TContainer> : IFeedParser where TContainer : class
    {
        /// <summary>
        ///     Creates a new instance of <see cref="FeedItemContract" />.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A new instance of <see cref="FeedItemContract" />.</returns>
        private FeedItemContract CreateFeedItemContract(TContainer container)
        {
            var id = GetId(container);
            var title = GetTitle(container);
            var description = GetDescription(container);
            var content = GetContent(container);
            var thumbnail = GetThumbnail(container);
            var link = GetLink(container);
            var publishDate = GetPublishDate(container);
            return new FeedItemContract(id, title, description, content, thumbnail, link, publishDate);
        }

        /// <summary>
        /// Gets a content.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A content.</returns>
        protected virtual string GetContent(TContainer container)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets a description.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A description.</returns>
        protected virtual string GetDescription(TContainer container)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets an id.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>An id.</returns>
        protected virtual string GetId(TContainer container)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets a link.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A link.</returns>
        protected virtual string GetLink(TContainer container)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets a publish date.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A publish date.</returns>
        protected virtual DateTime GetPublishDate(TContainer container)
        {
            return DateTime.Today;
        }

        /// <summary>
        /// Gets a thumbnail.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A thumbnail.</returns>
        protected virtual string GetThumbnail(TContainer container)
        {
            return string.Empty;
        }

        /// <summary>
        /// Gets a title.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A title.</returns>
        protected virtual string GetTitle(TContainer container)
        {
            return string.Empty;
        }

        /// <summary>
        ///     Parses passed string.
        /// </summary>
        /// <param name="response">An HTTP response content.</param>
        /// <returns>A list of <see cref="FeedItemContract" /></returns>
        public IEnumerable<FeedItemContract> Parse(HttpResponseMessage response)
        {
            ExceptionHelper.CheckArgumentNull(response, nameof(response));
            var data = this.GetResponseData(response);
            return data.Any() ? data.Select(CreateFeedItemContract).ToList() : null;
        }

        /// <summary>
        /// Gets data from the specified HTTP response.
        /// </summary>
        /// <param name="response">A HTTP response.</param>
        /// <returns>Data from the specified HTTP response.</returns>
        protected abstract IEnumerable<TContainer> GetResponseData(HttpResponseMessage response);
    }
}