using System;
using System.Web.Mvc;
using DAL.Repository;
using Domain.Interface;
using Web.Utility.FeedParser;

namespace CarApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private IFeedRepository repository;

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ViewFeed(int id)
        {
            var feed = this.repository.GetById(id);
            if (feed == null)
                throw new NullReferenceException();
            var parser = new FeedReader();
            var data = parser.Read(feed.Uri);
            return this.View(data);
        }

        public HomeController() : this(new FeedRepository())
        {
        }

        public HomeController(IFeedRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException();
            this.repository = repository;
        }
    }
}