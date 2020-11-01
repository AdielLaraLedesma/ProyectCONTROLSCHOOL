using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.Models;
using SCHOOLCONTROL.Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Mappings
{
    public static class CourseStudentMapping
    {
        public static CourseStudent Map(this CURSOESTUDIANTE model, ESTUDIANTE[] student, params CURSO[] course) 
        {
             return new CourseStudent
             {
                ID = model.ID,
                Course = course?.First(f => f.ID == model.IDCURSO)?.Map(),
                Student = student.First(f => f.ID == model.IDESTUDIANTE).Map(),
                Score = model.CALIFICACION
             };
            
        }
        public static CURSOESTUDIANTE Map(this CourseStudent info)
        {
            return new CURSOESTUDIANTE
            {
                 
                ID = info.ID,
                IDCURSO = info.Course.ID,
                IDESTUDIANTE = info.Student.ID,
                CALIFICACION = info.Score
            };
        }
    }
}
