using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    class SeniorCitizen : Ticket
    {
        public int yearOfBirth { get; set; }

        public SeniorCitizen() { }

        public SeniorCitizen(Screening screening, int YearOfBirth) : base(screening)
        {
            yearOfBirth = YearOfBirth;
        }

        public override double CalculatePrice()
        {
            double priceToPay = 0;
            DateTime todayDate = DateTime.Now;
            DateTime openingDateLimit = screening.movie.openingDate.AddDays(7);  //7 days after movie opening date
            string typeOfScreening = screening.screeningType;  //2D or 3D screening type

            if (typeOfScreening == "3D")  //if senior citizen buys 3D ticket,
            {
                if (todayDate > openingDateLimit)  //if senior citizen buys ticket 7 days after openingDateLimit,
                {
                    if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                    {
                        priceToPay = 6;
                    }
                    else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)  //from friday to sunday
                    {
                        priceToPay = 14;
                    }
                }
                else  //if senior citizen did not buy ticket 7 days after openingDateLimit, 
                {
                    if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                    {
                        priceToPay = 11;
                    }
                    else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)  //from friday to sunday
                    {
                        priceToPay = 14;
                    }
                }
            }
            else  //if senior citizen buys 2D ticket
            {
                if (todayDate > openingDateLimit)  //if senior citizen buys ticket 7 days after openingDateLimit,
                {
                    if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                    {
                        priceToPay = 5;
                    }
                    else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)  //from friday to sunday
                    {
                        priceToPay = 12.5;
                    }
                }
                else  //if senior citizen did not buy ticket 7 days after openingDateLimit, 
                {
                    if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                    {
                        priceToPay = 8.5;
                    }
                    else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)  //from friday to sunday
                    {
                        priceToPay = 12.5;
                    }
                }
            }
            return priceToPay;
        }

        //print what
        public override string ToString()
        {
            return string.Format("{0,-20}", yearOfBirth);
        }
    }
}
