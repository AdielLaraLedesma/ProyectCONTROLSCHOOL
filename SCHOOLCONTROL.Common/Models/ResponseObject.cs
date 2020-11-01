using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOLCONTROL.Common.Models
{
    public class ResponseObject
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public object Result { get; set; }
    }
}
