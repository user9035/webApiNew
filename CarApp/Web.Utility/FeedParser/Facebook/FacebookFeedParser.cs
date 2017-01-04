using System;
using System.Collections.Generic;
using System.Net.Http;
using Web.Utility.FeedParser.Core;

namespace Web.Utility.FeedParser.Facebook
{
    /// <summary>
    /// Provides the functionality to parse responces from Facebook feeds.
    /// </summary>
    internal sealed class FacebookFeedParser : FeedParserBase<FacebookPostData>
    {
        private readonly IHttpContentConverter converter;
        private readonly IPhotoUriProvider photoUriProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookFeedParser"/> class.
        /// </summary>
        internal FacebookFeedParser(IPhotoUriProvider provider) : this(new JsonConverter(), provider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookFeedParser"/> class.
        /// </summary>
        /// <param name="parser">A JSON parser.</param>
        internal FacebookFeedParser(IHttpContentConverter parser, IPhotoUriProvider provider)
        {
            ExceptionHelper.CheckArgumentNull(parser, nameof(parser));
            ExceptionHelper.CheckArgumentNull(provider, nameof(provider));
            this.converter = parser;
            this.photoUriProvider = provider;
        }

        /// <summary>
        /// Gets a content.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A content.</returns>
        protected override string GetContent(FacebookPostData container)
        {
            return container.Message;
        }

        /// <summary>
        /// Gets a description.
        /// </summary>
        /// <param name="post">A data container from RSS feed.</param>
        /// <returns>A description.</returns>
        protected override string GetDescription(FacebookPostData post)
        {
            var splittedPost = this.SplitPostMessage(post.Message);
            return splittedPost == null ? string.Empty : splittedPost.Item2;
        }

        /// <summary>
        /// Gets an id.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>An id.</returns>
        protected override string GetId(FacebookPostData container)
        {
            return container.Id;
        }

        /// <summary>
        /// Gets an URI to Facebbok post.
        /// </summary>
        /// <param name="post">A data container with Facebook post data.</param>
        /// <returns>An URI to Facebook post.</returns>
        protected override string GetLink(FacebookPostData post)
        {
            return $"https://www.facebook.com/{post.Id}";
        }

        /// <summary>
        /// Gets a publish date.
        /// </summary>
        /// <param name="container">A data container from RSS feed.</param>
        /// <returns>A publish date.</returns>
        protected override DateTime GetPublishDate(FacebookPostData container)
        {
            return container.Created;
        }

        /// <summary>
        /// Gets a thumbnail.
        /// </summary>
        /// <param name="post">A data container from RSS feed.</param>
        /// <returns>A thumbnail.</returns>
        protected override string GetThumbnail(FacebookPostData post)
        {
            string imgPath = string.Empty;
            if (!string.IsNullOrEmpty(post.PicturePath) && post.Type == PostObjectType.Video)
                imgPath = post.PicturePath;

            else if (post.ObjectId != 0 && post.Type == PostObjectType.Photo)
                return this.photoUriProvider.GetPhotoUri(post.ObjectId);

            return imgPath;
        }

        /// <summary>
        /// Gets a title.
        /// </summary>
        /// <param name="post">A data container from RSS feed.</param>
        /// <returns>A title.</returns>
        protected override string GetTitle(FacebookPostData post)
        {
            if (string.IsNullOrEmpty(post.Message))
                return base.GetTitle(post);

            var splittedPost = this.SplitPostMessage(post.Message);
            return splittedPost == null ? string.Empty : splittedPost.Item1;
        }

        /// <summary>
        /// Parses the specified HTTP response content.
        /// </summary>
        /// <param name="response">An HTTP response content.</param>
        /// <returns>A list of <see cref="FacebookPostData" /></returns>
        protected override IEnumerable<FacebookPostData> GetResponseData(HttpResponseMessage response)
        {
            var fbObject = this.converter.Convert<FbRoot>(response);
            return fbObject.Posts;
        }

        /// <summary>
        /// Parses json with user info.
        /// </summary>
        /// <param name="response">A HTTP response.</param>
        /// <returns>A user info.</returns>
        internal FacebookUserInfo ParseUserData(HttpResponseMessage response)
        {
            return this.converter.Convert<FacebookUserInfo>(response);
        }

        /// <summary>
        /// Splits a post message to extract text for title and description.
        /// </summary>
        /// <param name="message">A post message.</param>
        /// <returns>A post message.</returns>
        private Tuple<string, string> SplitPostMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return null;
            return ParserHelper.GetTitleDescription(message);
        }
    }
}
