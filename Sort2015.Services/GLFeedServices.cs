using Sort2015.Data;
using Sort2015.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.Services
{
    public interface IGLFeedServices
    {
        List<DailyGem> GetDailyGems();
        List<DailyGem> GetDailyGems(int pageSize = 0, int page = 0);
    }
    public class GLFeedServices: IGLFeedServices
    {
        private IGLFeedRepository _repository;

        public GLFeedServices()
        {
            _repository = new GLFeedRepository();
        }

        public GLFeedServices(IGLFeedRepository repository)
        {
            _repository = repository;
        }

        public List<DailyGem> GetDailyGems()
        {
            return _repository.GetDailyGems().ToList();
        }

        public List<DailyGem> GetDailyGems(int pageSize=0, int page = 0)
        {
            if ((page == 0 && pageSize == 0) || page==0 )
            {
                return _repository.GetDailyGems().ToList();
            }
            else
            {
                return _repository.GetDailyGems().Skip(page * pageSize).Take(pageSize).ToList();
            }
        }

    }
}
