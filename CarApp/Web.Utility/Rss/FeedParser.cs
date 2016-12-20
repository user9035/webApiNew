using System;
using System.Collections.Generic;
using Mikz.Feed.Contracts.Events;
using Web.Utility.Rss.Core;
using Web.Utility.Rss.Facebook;
using Web.Utility.Rss.Instagram;
using Web.Utility.Rss.Twitter;
using Web.Utility.Rss.Youtube;

namespace Web.Utility.Rss
{
    public sealed class FeedParser
    {
        public IEnumerable<FeedItemContract> ParseFeed(Uri uri)
        {
            RssClientBase client;
            switch (uri.Host)
            {
                case DomainName.Facebook:
                    client = new FacebookClient();
                    break;

                case DomainName.Instagram:
                    client = new InstagramClient();
                    break;

                case DomainName.Twitter:
                    client = new TwitterClient();
                    break;

                case DomainName.Youtube:
                    client = new YoutubeClient();
                    break;

                default:
                    throw new NotImplementedException();
            }
            return client.GetData(uri);
        }
    }
}
