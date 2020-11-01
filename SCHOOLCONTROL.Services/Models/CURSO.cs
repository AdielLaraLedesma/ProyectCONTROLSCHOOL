using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Models
{
    public class CURSO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(255)]
        public String NOMBRE { get; set; }
        public int GRADO { get; set; }
        public int IDPROFESOR { get; set; }

        public PROFESOR PROFESOR_OBJ { get; set; }
    }
}
