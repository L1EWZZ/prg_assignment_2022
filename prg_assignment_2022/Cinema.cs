using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    class Cinema
    {
        public string name { get; set; }
        public int hallNo { get; set; }
        public int capacity { get; set; }

        public Cinema()
        {
        }

        public Cinema(string Name, int HallNo, int Capacity)
        {
            name = Name;
            hallNo = HallNo;
            capacity = Capacity;
        }

        public override string ToString()
        {
            return string.Format("{0,-20} {1,-20} {2,-20}", name, hallNo, capacity);
        }
    }
}
