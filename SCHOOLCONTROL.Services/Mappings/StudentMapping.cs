using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Mappings
{
    public static class StudentMapping
    {
        public static Student Map(this ESTUDIANTE model)
        {
            return new Student
            {
                ID = model.ID,
                Name = model.NOMBRE,
                LastName = model.APELLIDOS,
                Address = model.DIRECCION,
                Score = model.CALIFICACION
            };
        }

        public static ESTUDIANTE Map(this Student info)
        {
            return new ESTUDIANTE
            {
                ID = info.ID,
                NOMBRE = info.Name,
                APELLIDOS = info.LastName,
                DIRECCION = info.Address,
                CALIFICACION = info.Score
            };
        }

    }
}
