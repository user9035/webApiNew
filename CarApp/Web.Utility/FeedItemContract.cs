using System;
using Mikz.Feed.Guard.Extensions;

namespace Mikz.Feed.Contracts.Events
{
    /// <summary>
    /// The entity, that represents feed item data transfer object.
    /// </summary>
    public sealed class FeedItemContract
    {
        private string _content;

        /// <summary>
        /// The identifier of the item.
        /// </summary>
        public string ItemId { get; private set; }

        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; private set; }

        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Content of the item.
        /// </summary>
        public string Content
        {
            get
            {
                return _content.Decode();
            }
            private set
            {
                if (value != null)
                {
                    _content = value.Decode();
                }
            }
        }

        /// <summary>
        /// Thumbnail of the item.
        /// </summary>
        public string Thumbnail { get; private set; }

        /// <summary>
        /// The link to item source.
        /// </summary>
        public string Link { get; private set; }

        public int PostCountLimit { get; set; }

        /// <summary>
        /// Creates a new item of the feed.
        /// </summary>
        /// <param name="itemId">The identifier of the item.</param>
        /// <param name="title">The title of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="content">Content of the item.</param>
        /// <param name="thumbnail">Thumbnail of the item.</param>
        /// <param name="link">The link to item source.</param>
        public FeedItemContract(string itemId, string title, string description, string content, string thumbnail, string link, DateTime publishDate)
        {
            ItemId = itemId;
            Title = title;
            Description = description;
            Content = content;
            Thumbnail = thumbnail;
            Link = link;
            PublishDate = publishDate;
        }

        public FeedItemContract()
        {
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("ItemId: {0}, Title: {1}, Description: {2}, Content: {3}, Thumbnail: {4}, Link: {5}",
                ItemId ?? "(null)", Title ?? "(null)",
                GetTruncated(Description, 100), GetTruncated(Content, 100),
                Thumbnail ?? "(null)", Link ?? "(null)");
        }

        private static string GetTruncated(string source, int limit)
        {
            var result = source == null
                ? "(null)"
                : source.Length > limit
                    ? source.Substring(0, limit - 3) + "..."
                    : source;
            return result;
        }
    }
}