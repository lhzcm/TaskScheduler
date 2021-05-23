using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedulerHost.Models
{
    public class Result
    {
        public object Data { get; set; }
        public Code Code { get; set; }
        public string Msg { get; set; }
    }
}
