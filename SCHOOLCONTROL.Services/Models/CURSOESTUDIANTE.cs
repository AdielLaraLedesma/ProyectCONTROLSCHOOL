using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Models
{
    public class CURSOESTUDIANTE
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int IDCURSO { get; set; }
        public int IDESTUDIANTE { get; set; }
        public int CALIFICACION { get; set; }

        public CURSO CURSO_OBJ { get; set; }
        public ESTUDIANTE ESTUDIANTE_OBJ { get; set; }
    }
}
