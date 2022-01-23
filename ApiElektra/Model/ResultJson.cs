using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiElektra.Model
{
    public class ResultJson
    {
        public bool Status { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
    }
}
