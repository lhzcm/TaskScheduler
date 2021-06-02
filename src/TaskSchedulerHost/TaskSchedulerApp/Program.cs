using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null)
            {
                foreach (var item in args)
                {
                    Console.WriteLine(item);
                }
            }
            Console.Read();
        }
    }
}
