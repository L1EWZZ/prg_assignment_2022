using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    class Adult : Ticket
    {
        public bool popcornOffer { get; set; }

        public Adult() { }

        public Adult(Screening screening, bool PopcornOffer) : base(screening)
        {
            popcornOffer = PopcornOffer;
        }

        //calculate what 
        public override double CalculatePrice()
        {
            double priceToPay = 0;
            DateTime todayDate = DateTime.Now;
            string typeOfScreening = screening.screeningType;  //2D or 3D screening type

            if (typeOfScreening == "3D")
            {
                if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                {
                    priceToPay = 11;
                }
                else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)
                {
                    priceToPay = 14;
                }
            }
            else
            {
                if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                {
                    priceToPay = 8.5;
                }
                else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)
                {
                    priceToPay = 12.5;
                }
            }
            return priceToPay;
        }

        public override string ToString()
        {
            return string.Format("{0,-20}", popcornOffer);
        }
    }
}
