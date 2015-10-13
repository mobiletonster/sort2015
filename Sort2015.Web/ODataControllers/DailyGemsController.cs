using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.OData;
using Sort2015.Data;
using Sort2015.Data.Models;
using System.Web.Http;

namespace Sort2015.Web.ODataControllers
{
    public class DailyGemsController: ODataController
    {
        private GLFeedContext context;
        public DailyGemsController()
        {
            context = new GLFeedContext();
        }

        [EnableQuery]
        public IQueryable<DailyGem> GetDailyGems()
        {
            return context.DailyGems;
        }

        [EnableQuery]
        public SingleResult<DailyGem> GetDailyGem([FromODataUri] int id)
        {
            return SingleResult.Create(context.DailyGems.Where(m => m.Id == id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyGemsExists(int key)
        {
            return context.DailyGems.Count(e => e.Id == key) > 0;
        }
    }
}
