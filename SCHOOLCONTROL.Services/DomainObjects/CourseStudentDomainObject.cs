using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.DAO;
using SCHOOLCONTROL.Services.Mappings;

namespace SCHOOLCONTROL.Services.DomainObjects
{
    public class CourseStudentDomainObject
    {
        public List<CourseStudent> Get()
        {
            using (var dao = new CourseStudentDAO())
            {
                var query = dao.Query(e => true);
                var coursesstudents = query.ToArray();

                var cuenta3 = coursesstudents.Length;

                using (var courdao = new CourseDAO())
                {
                    var ids = coursesstudents.Select(e => e.IDCURSO).Distinct();
                    var courses = courdao.Query(e => ids.Contains(e.ID)).ToArray();
                    var cuenta2 = courses.Length;

                    using (var studendao = new StudentDAO())
                    {
                         var ids2 = coursesstudents.Select(e => e.IDESTUDIANTE).Distinct();
                         var students = studendao.Query(e => ids2.Contains(e.ID)).ToArray();
                         var cuenta = students.Length;


                        return coursesstudents.Select(e => e.Map(students, courses)).ToList();
                    }
                    
                }
            }
        }

        public object Register(CourseStudent info)
        {
            try
            {
                ValidateCourse(info);
                ValidateStudent(info);
                ValidateScore(info);
                var model = info.Map();
                using (var dao = new CourseStudentDAO())
                {
                    return dao.Register(model);
                }
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
                ValidateCourse(info);
                ValidateStudent(info);
                ValidateScore(info);
                var model = info.Map();
                using (var dao = new CourseStudentDAO())
                {
                    return dao.Modify(model);
                }
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
                var model = info.Map();
                using (var dao = new CourseStudentDAO())
                {
                    return dao.Delete(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ValidateCourse(CourseStudent info)
        {
            using (var dao = new CourseDAO())
            {
                var query = dao.Query(e => true);
                var qCourse = query.Where(e => e.ID == info.Course.ID);
                if (!qCourse.Any())
                {
                    throw new Exception(Common.Constants.Messages.CURSO_NO_ENCONTRADO);
                }
            }
        }
        public void ValidateStudent(CourseStudent info)
        {
            using (var dao = new StudentDAO())
            {
                var query = dao.Query(e => true);
                var qStudent = query.Where(e => e.ID == info.Student.ID);
                if (!qStudent.Any())
                {
                    throw new Exception(Common.Constants.Messages.ESTUDIANTE_NO_ENCONTRADO);
                }
            }
        }
        public void ValidateScore(CourseStudent info)
        {
            if(info.Score>0 || info.Score > 100)
            {
                throw new Exception(Common.Constants.Messages.CALIFICACION_ENTRE_0_Y_100);
            }
        }

    }
}
