using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Common.Infos
{
    public class CourseStudent
    {
        public int ID { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int Score { get; set; }
    }
}
