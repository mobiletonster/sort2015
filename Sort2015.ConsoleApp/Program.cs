using Sort2015.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new GLFeedContext();
            var gems = context.DailyGems.ToList();
            foreach(var g in gems)
            {
                Console.WriteLine(g.Quote);
            }
            Console.ReadKey();
        }
    }
}
