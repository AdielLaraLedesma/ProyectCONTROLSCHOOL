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
    public class CourseDomainObject
    {
        public List<Course> Get(string text)
        {
            using (var dao = new CourseDAO())
            {
                var query = dao.Query(e => true);

                if (!string.IsNullOrWhiteSpace(text))
                {
                    query = query.Where(e => e.NOMBRE.Contains(text));
                }
                var courses = query.ToArray();
                var coursesLe = courses.Length;
                using (var teadao = new TeacherDAO())
                {
                    var ids = courses.Select(e => e.IDPROFESOR).Distinct();
                    var teachers = teadao.Query(e => ids.Contains(e.ID)).ToArray();

                    return courses.Select(e => e.Map(teachers)).ToList();
                }
            }
        }

        public Course GetByID(int IDd)
        {
            using (var dao = new CourseDAO())
            {
                var query = dao.Query(e => true);
                var courses = query.ToArray();
                using (var teaDAO = new TeacherDAO())
                {
                    var ids = courses.Select(e => e.IDPROFESOR).Distinct();
                    var teachers = teaDAO.Query(e => e.ID == IDd).ToArray();

                    return courses.Select(e => e.Map(teachers)).First();
                }

                
            }
        }

        public object Register(Course info)
        {
            try
            {
                ValidateTeacher(info);
                ValidateMaxTeacherCourses(info);
                var model = info.Map();
                using (var dao = new CourseDAO())
                {
                    return dao.Register(model);
                }
                
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
                ValidateTeacher(info);
                ValidateMaxTeacherCourses(info);
                // ValidateNoDuplicates(info, false);

                var model = info.Map();
                using (var dao = new CourseDAO())
                {
                    return dao.Modify(model);
                }
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
                ValidateNoCourseStudentInstan(info);
                var model = info.Map();
                using (var dao = new CourseDAO())
                {
                    return dao.Delete(model);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void ValidateTeacher(Course info)
        {
            using (var dao = new TeacherDAO())
            {
                var query = dao.Query(e => true);
                var qTeacher = query.Where(e => e.ID == info.Teacher.ID);
                if (!qTeacher.Any())
                {
                    throw new Exception(Common.Constants.Messages.PROFESOR_NO_ENCONTRADO);
                }
            }
        }

        public void ValidateMaxTeacherCourses(Course info)
        {
            using (var dao = new TeacherDAO())
            {
                var query = dao.Query(e => true);
                var qTeacher = query.Where(e => e.ID == info.Teacher.ID).First().Map();
                if (!qTeacher.HasPlaza)
                {
                    using (var coursedao = new CourseDAO())
                    {
                        var coursequery = coursedao.Query(e => true);
                        var qCourse = coursequery.Where(e => e.IDPROFESOR == info.Teacher.ID).ToArray();
                        if (qCourse.Length >= Common.Constants.Constants.MAX_TEACHER_CURSOS)
                        {
                            throw new Exception(Common.Constants.Messages.PROFESOR_MAXIMO_MATERIAS);
                        }
                    }
                }
            }
        }
        public void ValidateNoCourseStudentInstan(Course info)
        {
            using (var dao = new CourseStudentDAO())
            {
                var query = dao.Query(e => true);
                var qCourseStudent = query.Where(e => e.IDCURSO == info.ID).ToList();
                if (qCourseStudent.Any())
                {
                    throw new Exception(Common.Constants.Messages.ALUMNO_CON_CURSOS);
                }
            }
        }

    }
}
