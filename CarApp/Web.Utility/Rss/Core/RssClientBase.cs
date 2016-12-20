using System;
using System.Collections.Generic;
using System.Net.Http;
using Mikz.Feed.Contracts.Events;

namespace Web.Utility.Rss.Core
{
    /// <summary>
    /// Provides the functionality to read RSS feeds.
    /// </summary>
    internal abstract class RssClientBase
    {
        private HttpClient client;
        private bool disposed = false;

        /// <summary>
        /// Finilizes managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finilizes managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                if (this.client != null)
                {
                    this.client.Dispose();
                    this.client = null;
                }
            }

            this.disposed = true;
        }

        protected string GetString(HttpRequestMessage message)
        {
            using (var task = this.client.SendAsync(message))
            {
                task.Wait();
                return task.Result.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// Executes an http request towards the specified URI.
        /// </summary>
        /// <param name="uri">A URI.</param>
        /// <returns>An http response.</returns>
        protected string GetString(Uri uri)
        {
            using (var task = this.client.GetStringAsync(uri))
            {
                task.Wait();
                return task.Result;
            }
        }

        /// <summary>
        /// Executes an http request towards the specified URI.
        /// </summary>
        /// <param name="uri">A URI.</param>
        /// <returns>An http response.</returns>
        protected HttpResponseMessage GetHttpResponseMessage(Uri uri)
        {
            using (var task = this.client.GetAsync(uri))
            {
                task.Wait();
                return task.Result;
            }
        }

        /// <summary>
        /// Gets data from the specified RSS feed.
        /// </summary>
        /// <param name="feedUri">A feed to read the data.</param>
        /// <returns>The list of <see cref="FeedItemContract"/></returns>
        /// <exception cref="ArgumentNullException">if the passed URI is null.</exception>
        public virtual IEnumerable<FeedItemContract> GetData(Uri feedUri)
        {
            if (feedUri == null)
                throw new ArgumentNullException(nameof(feedUri));

            var message = this.CreateRequest(feedUri);
            var response = this.GetString(message);
            var parser = this.GetParser();

            if (parser == null)
                throw new NullReferenceException("Parser is not initialized.");

            return parser.Parse(response);
        }

        /// <summary>
        /// Create a new instance of the <see cref="HttpRequestMessage"/> to get data from RSS feed.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of RSS feed.</param>
        /// <returns>A new instance of the <see cref="HttpRequestMessage"/></returns>
        protected abstract HttpRequestMessage CreateRequest(Uri uri);

        /// <summary>
        /// Gets a parser to handle server response.
        /// </summary>
        /// <returns>A parser.</returns>
        protected abstract IRssParser GetParser();

        /// <summary>
        /// Initializes a new instance of the <see cref="RssClientBase"/>
        /// </summary>
        protected RssClientBase() 
        {
            this.client = new HttpClient();
        }
    }
}