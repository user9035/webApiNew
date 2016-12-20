using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarApp.Web.Models;
using Domain.Entity;
using Domain.Interface;
using Newtonsoft.Json;

namespace CarApp.Web.Controllers
{
    public class FeedsController : ApiController
    {
        private IFeedRepository repository;

        [HttpPost]
        public void Add(RssFeedViewModel newFeed)
        {
            if (newFeed == null)
                throw new ArgumentNullException();
            var newEntity = this.repository.Create();
            newEntity.Caption = newFeed.Caption;
            newEntity.Uri = newFeed.Link;
            this.repository.Add(newEntity);
            this.repository.SaveChanges();
        }

        [HttpPut]
        public void Edit(RssFeedViewModel feed)
        {
            var entity = this.repository.GetById(feed.Id);
            if (entity != null)
            {
                entity.Caption = feed.Caption;
                entity.Uri = feed.Link;
                this.repository.SaveChanges();
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var entity = this.repository.GetById(id);
            if (entity != null)
            {
                this.repository.Delete(entity);
                this.repository.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.repository != null)
                {
                    this.repository.Dispose();
                    this.repository = null;
                }
            }
            base.Dispose(disposing);
        }

        public IEnumerable<RssFeedViewModel> GetFeeds()
        {
            return this.repository.GetAll().Select(x => new RssFeedViewModel(x));
        }

        public FeedsController(IFeedRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException();
            this.repository = repository;
        }
    }
}