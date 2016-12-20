using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Interface
{
    public interface IFeedRepository : IRepositoryBase<RssFeedEntity>
    {
        RssFeedEntity GetById(int id);
    }
}
