using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.DAO;
using SCHOOLCONTROL.Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.DomainObjects
{
    public class TeacherDomainObject
    {

        public List<Teacher> Get(string text)
        {
            using (var dao = new TeacherDAO())
            {
                var query = dao.Query(e => true);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    query = query.Where(e => e.NOMBRE.Contains(text));
                }
                return query.ToArray().Select(e => e.Map()).ToList();
            }
        }

        public Teacher GetByID(int IDd)
        {
            using (var dao = new TeacherDAO())
            {
                var query = dao.Query(e => true);
                return query.Where(e => e.ID == IDd).First().Map();
            }
        }

        public object Register(Teacher info)
        {
            try
            {
                ValidateNoDuplicates(info);
                var model = info.Map();
                using (var dao = new TeacherDAO())
                {
                    return dao.Register(model);
                }
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
                ValidateNoDuplicates(info, false);
                var model = info.Map();
                using (var dao = new TeacherDAO())
                {
                    return dao.Modify(model);
                }
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

                ValidateNoTeacherInstan(info);
                var model = info.Map();
                using (var dao = new TeacherDAO())
                {
                    return dao.Delete(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void ValidateNoDuplicates(Teacher info, bool isNew = true)
        {
            using (var dao = new TeacherDAO())
            {
                var query = isNew ? dao.Query(e => true) : dao.Query(e => e.ID != info.ID);
                var qName = query.Where(e => e.NOMBRE == info.Name);
                if (qName.Any())
                {
                    var qLastName = query.Where(e => e.APELLIDOS == info.LastName);
                    if (qLastName.Any())
                    {
                        throw new Exception(Common.Constants.Messages.PROFESOR_NOMBRE_REPETIDO);
                    }
                }
            }
        }

        public void ValidateNoTeacherInstan(Teacher info)
        {
            using (var dao = new CourseDAO())
            {
                var query = dao.Query(e => true);
                var qCourse = query.Where(e => e.IDPROFESOR == info.ID).ToList();
                if (qCourse.Any())
                {
                    throw new Exception(Common.Constants.Messages.PROFESOR_CON_CURSOS);
                }
            }
        }

    }
}
