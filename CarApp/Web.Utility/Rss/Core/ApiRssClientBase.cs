using System;

namespace Web.Utility.Rss.Core
{
    /// <summary>
    /// Represents base client to work with API of web resource.
    /// </summary>
    abstract class ApiRssClientBase : RssClientBase
    {
        private Uri api;

        /// <summary>
        /// Gets the API URI.
        /// </summary>
        protected Uri Api => this.api ?? (this.api = this.GetApiUri());

        /// <summary>
        /// Creates an URI to which the data should be fetched from.
        /// </summary>
        /// <param name="path">A path of URI.</param>
        /// <param name="parameters">A list of URI parameters.</param>
        /// <returns>An URI.</returns>
        protected Uri CreateApiUri(string path, string parameters)
        {
            var uriBuilder = new UriBuilder(this.Api);
            uriBuilder.Port = -1;
            uriBuilder.Path = path;
            uriBuilder.Query = parameters;
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Gets the API URI.
        /// </summary>
        protected abstract Uri GetApiUri();
    }
}
