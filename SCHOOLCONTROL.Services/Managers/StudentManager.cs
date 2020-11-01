using SCHOOLCONTROL.Services.DomainObjects;
using SCHOOLCONTROL.Common.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Managers
{
    public class StudentManager
    {

        public Student GetByID(int ID)
        {
            try
            {
                var dobj = new StudentDomainObject();
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
                var dobj = new StudentDomainObject();
                return dobj.Get(text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Register(Student info)
        {

            try
            {
                var dobj = new StudentDomainObject();
                return dobj.Register(info);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public object Modify(Student info)
        {
            try
            {
                var dobj = new StudentDomainObject();
                return dobj.Modify(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object Delete(Student info)
        {
            try
            {
                var dobj = new StudentDomainObject();
                return dobj.Delete(info);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
