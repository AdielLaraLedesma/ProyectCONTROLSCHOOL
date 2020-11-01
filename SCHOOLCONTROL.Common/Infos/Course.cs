using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Common.Infos
{
    public class Course
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int Grade { get; set; }
        public Teacher Teacher { get; set; }
    }
}
