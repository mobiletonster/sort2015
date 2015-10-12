﻿using Sort2015.Data.Models;
using Sort2015.Services;
using Sort2015.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sort2015.Web.Controllers
{
    public class DailyGemsController : ApiController
    {
        private IGLFeedServices _glFeedService;

        public DailyGemsController()
        {
            _glFeedService = new GLFeedServices();
        }

        public DailyGemsController(IGLFeedServices glFeedService)
        {
            _glFeedService = glFeedService;
        }

        // GET: DailyGems
        [HttpGet]
        [Route("api/dailygems")]
        public List<DailyGem> GetDailyGems()
        {
            return _glFeedService.GetDailyGems();
        }

        [BasicAuth]
        [HttpPost]
        [Route("api/dailygems")]
        public DailyGem AddDailyGem(DailyGem dailyGem)
        {
            return _glFeedService.AddDailyGem(dailyGem);
        }
    }
}