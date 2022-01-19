using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    abstract class Ticket
    {
        public Screening screening { get; set; }

        public Ticket() { }

        public Ticket(Screening Screening)
        {
            screening = Screening;
        }

        abstract public double CalculatePrice();

        public override string ToString()
        {
            return string.Format("{0,-20}", screening.screeningNo);
        }
    }
}
