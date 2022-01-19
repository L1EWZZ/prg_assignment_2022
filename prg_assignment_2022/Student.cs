using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    class Student : Ticket
    {
        public string levelOfStudy { get; set; }

        public Student() { }

        public Student(Screening screening, string LevelOfStudy) : base(screening)
        {
            levelOfStudy = LevelOfStudy;
        }

        //if student buys 3D movie on Monday to Thrusday, pay 8. Else if bought on Friday to Sunday 14
        public override double CalculatePrice()
        {
            double priceToPay = 0;
            DateTime todayDate = DateTime.Now;
            DateTime openingDateLimit = screening.movie.openingDate.AddDays(7);  //7 days after movie opening date
            string typeOfScreening = screening.screeningType;  //2D or 3D screening type

            if (typeOfScreening == "3D")  //if student buys 3D ticket,
            {
                if (todayDate > screening.movie.openingDate && todayDate < openingDateLimit)  //if student buys ticket 7 days after openingDateLimit,
                {
                    if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                    {
                        priceToPay = 8;
                    }
                    else if (todayDate.DayOfWeek >= DayOfWeek.Friday && todayDate.DayOfWeek <= DayOfWeek.Sunday)  //from friday to sunday
                    {
                        priceToPay = 14;
                    }
                }
                else  //if student did not buy ticket 7 days after openingDateLimit, 
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
            else  //if student buys 2D ticket
            {
                if (todayDate > screening.movie.openingDate && todayDate < openingDateLimit)  //if student buys ticket in the 7 days limit
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
                else  //if student did not buy ticket 7 days after openingDateLimit, 
                {
                    if (todayDate.DayOfWeek >= DayOfWeek.Monday && todayDate.DayOfWeek <= DayOfWeek.Thursday)  //from monday to thursday
                    {
                        priceToPay = 7;
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
            return string.Format("{0,-20}", levelOfStudy);
        }
    }
}
