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
    public static class CourseMapping
    {
        public static Course Map(this CURSO model, params PROFESOR[] profesor)
        {
            return new Course
            {
                ID = model.ID,
                Name = model.NOMBRE,
                Grade = model.GRADO,
                Teacher = profesor?.FirstOrDefault(f => f.ID == model.IDPROFESOR)?.Map()
            };
        }
        public static CURSO Map(this Course info)
        {
            return new CURSO
            {
                ID = info.ID,
                NOMBRE = info.Name,
                GRADO = info.Grade,
                IDPROFESOR = info.Teacher.ID
            };
        }
    }
}
