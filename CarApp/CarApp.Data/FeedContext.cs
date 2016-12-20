using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace DAL
{
    public class FeedContext : DbContext
    {
        private const string db = "feedDb";
        private volatile Type _dependency;

        public FeedContext() : base(db)
        {
            Database.SetInitializer<FeedContext>(new DropCreateDatabaseIfModelChanges<FeedContext>());
            _dependency = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var feedConfiguration = modelBuilder.Entity<RssFeedEntity>();
            feedConfiguration.ToTable("feed");
            feedConfiguration.Property(x => x.UriString).HasColumnName("uri");
            feedConfiguration.Ignore(x => x.Uri);
        }
    }
}
