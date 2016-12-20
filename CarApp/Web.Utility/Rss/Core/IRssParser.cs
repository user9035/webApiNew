using System;
using System.Collections.Generic;
using Mikz.Feed.Contracts.Events;

namespace Web.Utility.Rss.Core
{
    internal interface IRssParser
    {
        IEnumerable<FeedItemContract> Parse(string httpResponseContent);
    }
}
