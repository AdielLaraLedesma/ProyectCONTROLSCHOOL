using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Common.Constants
{
    public class Messages
    {
        public const string PROFESOR_MAXIMO_MATERIAS = "El maximo de cursos que se le puede asignar a un procesor sin plaza son 6";
        public const string ALUMNO_CALIFICACION_BAJA = "El alumno para inscribirse a un curso debe tener promedio aprobatorio (>70)";
        public const string CALIFICACION_ENTRE_0_Y_100 = "El rango de calificación es entre 0 y 100";
        public const string ALUMNO_NOMBRE_REPETIDO = "El alumno ya esta registrado";
        public const string PROFESOR_NOMBRE_REPETIDO = "El profesor ya esta registrado";
        public const string PROFESOR_NO_ENCONTRADO = "El profesor no fue encontrado";
        public const string CURSO_NO_ENCONTRADO = "El curso no fue encontrado";
        public const string ESTUDIANTE_NO_ENCONTRADO = "El estudiante no fue encontrado";
        public const string ALUMNO_CON_CURSOS = "No se puede eliminar un alumno sí tiene cursos asignados";
        public const string PROFESOR_CON_CURSOS = "No se puede eliminar un profesor sí tiene cursos asignados";
        public const string CURSO_CON_CURSOSALUMNOS = "No se puede eliminar un curso sí esta dado de alta con algun alumno";

    }
}
