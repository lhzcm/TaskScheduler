using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerTest.Dmm
{
    public class fc3dlottery
    {
        public int state { get; set; }
        public string message { get; set; }
        public int pageCount { get; set; }
        public int countNum { get; set; }
        public int Tflag { get; set; }
        public List<fc3dlotteryResult> result { get; set; }
    }

    public class fc3dlotteryResult
    {
        public string name { get; set; }
        public string code { get; set; }
        public string date { get; set; }
        public string red { get; set; }
    }
}
