using SCHOOLCONTROL.Common.Infos;
using SCHOOLCONTROL.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Mappings
{
    public static class TeacherMapping
    {
        public static Teacher Map(this PROFESOR model)
        {
            return new Teacher
            {
                ID = model.ID,
                Name = model.NOMBRE,
                LastName = model.APELLIDOS,
                Address = model.DIRECCION,
                Wage = model.SALARIO,
                HasPlaza = model.TIENEPLAZA
            };
        }

        public static PROFESOR Map(this Teacher info)
        {
            return new PROFESOR
            {
                ID = info.ID,
                NOMBRE = info.Name,
                APELLIDOS = info.LastName,
                DIRECCION = info.Address,
                SALARIO = info.Wage,
                TIENEPLAZA = info.HasPlaza
            };
        }
    }
}
