using Sort2015.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.Data
{
    public interface IGLFeedRepository
    {
        IQueryable<DailyGem> GetDailyGems();
        object AddRecord(object entity);
        object UpdateRecord(object entity);
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

        // All purpose Add Record method
        public object AddRecord(object entity)
        {
            var dbEntity = _context.Entry(entity);
            dbEntity.State = EntityState.Added;
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string errMessage = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validErr in validationErrors.ValidationErrors)
                    {
                        errMessage += string.Format("Property: {0} Error:{1}", validErr.PropertyName, validErr.ErrorMessage);
                    }

                }
                throw new Exception(errMessage);
            }

            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                dbEntity.State = EntityState.Detached;
            }
            return dbEntity.Entity;
        }

        // All purpose Update Record method
        public object UpdateRecord(object entity)
        {
            var dbEntity = _context.Entry(entity);
            dbEntity.State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string errMessage = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validErr in validationErrors.ValidationErrors)
                    {
                        errMessage += string.Format("Property: {0} Error:{1}", validErr.PropertyName, validErr.ErrorMessage);
                    }

                }
                throw new Exception(errMessage);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                dbEntity.State = EntityState.Detached;
            }
            return dbEntity.Entity;
        }

    }
}
