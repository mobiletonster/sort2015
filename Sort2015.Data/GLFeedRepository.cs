using Sort2015.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.Data
{
    public interface IGLFeedRepository
    {
        IQueryable<DailyGem> GetDailyGems();
    }
    public class GLFeedRepository: IGLFeedRepository
    {
        private GLFeedContext _context;
        public GLFeedRepository()
        {
            _context = new GLFeedContext();
        }

        public GLFeedRepository(GLFeedContext context)
        {
            _context = context;
        }

        public IQueryable<DailyGem> GetDailyGems()
        {
            return _context.DailyGems;
        }
    }
}
