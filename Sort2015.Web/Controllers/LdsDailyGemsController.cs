using Sort2015.Data.Models;
using Sort2015.Services;
using Sort2015.Web.Extensions;
using Sort2015.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sort2015.Web.Controllers
{
    public class LdsDailyGemsController : ApiController
    {
        private IGLFeedServices _glFeedService;

        public LdsDailyGemsController()
        {
            _glFeedService = new GLFeedServices();
        }

        public LdsDailyGemsController(IGLFeedServices glFeedService)
        {
            _glFeedService = glFeedService;
        }

        // GET: DailyGems
        [HttpGet]
        [Route("api/dailygems")]
        [ResponseType(typeof(List<DailyGemVM>))]
        public IHttpActionResult GetDailyGems()
        {
            var gems = _glFeedService.GetDailyGems().Select(m => new DailyGemVM(m)).ToList();
            //return Ok(gems);
            return new CustomActionResult<List<DailyGemVM>>(HttpStatusCode.OK, gems);
        }

        [HttpPost]
        [Route("api/dailygems")]
        public DailyGem AddDailyGem(DailyGem dailyGem)
        {
            return _glFeedService.AddDailyGem(dailyGem);
        }
    }
}