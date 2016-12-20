using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Web.Utility.Rss.Core;

namespace Web.Utility.Rss.Facebook
{
    /// <summary>
    /// Provides the functionality to parse responces from Facebook RSS feed.
    /// </summary>
    sealed class FacebookRssParser : RssParserBase<FacebookPostData>
    {
        private IPhotoUriProvider photoUriProvider;

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
        /// Initializes the current instance.
        /// </summary>
        /// <param name="provider">A helper to get photo URI.</param>
        internal void Init(IPhotoUriProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            this.photoUriProvider = provider;
        }

        /// <summary>
        /// Parses the passed response from Facebook RSS feed.
        /// </summary>
        /// <param name="httpResponseContent">An HTTP response content from Facebook RSS feed.</param>
        /// <returns>A list filled in by Facebook post data.</returns>
        protected override IEnumerable<FacebookPostData> ParseInternal(string httpResponseContent)
        {
            var data = JsonConvert.DeserializeObject<FbRoot>(httpResponseContent);
            return data.Posts;
        }

        /// <summary>
        /// Parses json with user info.
        /// </summary>
        /// <param name="responce">A HTTP response.</param>
        /// <returns>A user info.</returns>
        internal FacebookUserInfo ParseUserData(string responce)
        {
            return JsonConvert.DeserializeObject<FacebookUserInfo>(responce);
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

    class FbRoot
    {
        [JsonProperty(PropertyName = "data")]
        internal IEnumerable<FacebookPostData> Posts
        {
            get;
            set;
        }
    }
}
