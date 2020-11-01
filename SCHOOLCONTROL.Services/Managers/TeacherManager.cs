using SCHOOLCONTROL.Services.DomainObjects;
using SCHOOLCONTROL.Common.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Managers
{
    public class TeacherManager
    {

        public Teacher GetByID(int ID)
        {
            try
            {
                var dobj = new TeacherDomainObject();
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
                var dobj = new TeacherDomainObject();
                return dobj.Get(text);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public object Register(Teacher info)
        {
            try
            {
                var dobj = new TeacherDomainObject();
                return dobj.Register(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Delete(Teacher info)
        {
            try
            {
                var dobj = new TeacherDomainObject();
                return dobj.Delete(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Modify(Teacher info)
        {
            try
            {
                var dobj = new TeacherDomainObject();
                return dobj.Modify(info);
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
