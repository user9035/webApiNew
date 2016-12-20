using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Interface;

namespace DAL.Repository
{
    public sealed class FeedRepository : RepositoryBase<RssFeedEntity>, IFeedRepository
    {
        public FeedRepository() : base(new FeedContext())
        {
        }

        public RssFeedEntity GetByCaption(string caption)
        {
            if (string.IsNullOrEmpty(caption))
                return null;
            return this.EntitySet.SingleOrDefault(entity => entity.Caption == caption);
        }

        public RssFeedEntity GetById(int id)
        {
            return this.EntitySet.SingleOrDefault(x => x.Id == id);
        }
    }
}
