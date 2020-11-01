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
    public class StudentDomainObject
    {
        public List<Student> Get(string text)
        {
            using(var dao = new StudentDAO())
            {
                var query = dao.Query(e => true);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    query = query.Where(e => e.NOMBRE.Contains(text));
                }
                return query.ToArray().Select(e => e.Map()).ToList();
            }
        }

        public Student GetByID(int IDd)
        {
            using (var dao = new StudentDAO())
            {
                var query = dao.Query(e => true);
                return query.Where(e => e.ID == IDd).First().Map();
            }
        }

        public object Register(Student info)
        {
            try
            {
                ValidateScore(info);
                ValidateNoDuplicates(info);
                var model = info.Map();
                using (var dao = new StudentDAO())
                {
                    return dao.Register(model);
                }
                
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
                ValidateScore(info);
                ValidateNoDuplicates(info, false);
                var model = info.Map();
                using (var dao = new StudentDAO())
                {
                    return dao.Modify(model);
                }
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
                var model = info.Map();
                using (var dao = new StudentDAO())
                {
                    return dao.Delete(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void ValidateNoDuplicates(Student info, bool isNew = true)
        {
            using (var dao = new StudentDAO())
            {
                var query = isNew ? dao.Query(e => true) : dao.Query(e => e.ID != info.ID);
                var qName = query.Where(e => e.NOMBRE == info.Name);
                if (qName.Any())
                {
                    var qLastName = query.Where(e => e.APELLIDOS == info.LastName);
                    if (qLastName.Any())
                    {
                        throw new Exception(Common.Constants.Messages.ALUMNO_NOMBRE_REPETIDO);
                    }
                }
            }
        }

        public void ValidateScore(Student info)
        {
            if (info.Score > 0 || info.Score > 100)
            {
                throw new Exception(Common.Constants.Messages.CALIFICACION_ENTRE_0_Y_100);
            }
        }

        public void ValidateNoStudentInstan(Student info)
        {
            using (var dao = new CourseStudentDAO())
            {
                var query = dao.Query(e => true);
                var qCourseStudent = query.Where(e => e.IDESTUDIANTE == info.ID).ToList();
                if (qCourseStudent.Any())
                {
                    throw new Exception(Common.Constants.Messages.PROFESOR_CON_CURSOS);
                }
            }
        }


    }
}
