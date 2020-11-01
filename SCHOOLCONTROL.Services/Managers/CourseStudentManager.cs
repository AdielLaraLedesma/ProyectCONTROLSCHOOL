using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.DomainObjects;

namespace SCHOOLCONTROL.Services.Managers
{
    public class CourseStudentManager
    {
        public object Get()
        {
            try
            {
                var dobj = new CourseStudentDomainObject();
                var odbs = dobj.Get();
                
                return odbs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Register(CourseStudent info)
        {
            try
            {
                var dobj = new CourseStudentDomainObject();
                return dobj.Register(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Delete(CourseStudent info)
        {
            try
            {
                var dobj = new CourseStudentDomainObject();
                return dobj.Delete(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Modify(CourseStudent info)
        {
            try
            {
                var dobj = new CourseStudentDomainObject();
                return dobj.Modify(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
