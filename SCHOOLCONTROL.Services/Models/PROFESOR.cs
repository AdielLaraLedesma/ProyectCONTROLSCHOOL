using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Services.Models
{
    public class PROFESOR
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(255)]
        public String NOMBRE { get; set; }
        [MaxLength(255)]
        public String APELLIDOS { get; set; }
        [MaxLength(255)]
        public String DIRECCION { get; set; }
        public int SALARIO { get; set; }
        public bool TIENEPLAZA { get; set; }
    }
}
