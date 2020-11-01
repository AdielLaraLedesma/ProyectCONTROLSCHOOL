using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.DomainObjects;

namespace SCHOOLCONTROL.Services.Managers
{
    public class CourseManager
    {
        public Course GetByID(int ID)
        {
            try
            {
                var dobj = new CourseDomainObject();
                return dobj.GetByID(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }




        public object Get(string text)
        {
            try
            {
                var dobj = new CourseDomainObject();
                return dobj.Get(text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Register(Course info)
        {
            try
            {
                var dobj = new CourseDomainObject();
                return dobj.Register(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public object Modify(Course info)
        {
            try
            {
                var dobj = new CourseDomainObject();
                return dobj.Modify(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Delete(Course info)
        {
            try
            {
                var dobj = new CourseDomainObject();
                return dobj.Delete(info);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
