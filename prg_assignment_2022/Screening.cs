using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    class Screening
    {
        public int screeningNo { get; set; }
        public DateTime screeningDateTime { get; set; }
        public string screeningType { get; set; }
        public int seatsRemaining { get; set; }
        public Cinema cinema { get; set; }
        public Movie movie { get; set; }

        public Screening() { }

        //is it int ScreenigNo or int SeatsRemaining?
        public Screening(int ScreenigNo, DateTime ScreeningDateTime, string ScreeningType, Cinema Cinema, Movie Movie)
        {
            screeningNo = ScreenigNo;
            screeningDateTime = ScreeningDateTime;
            screeningType = ScreeningType;
            cinema = Cinema;
            movie = Movie;
        }

        public override string ToString()
        {
            var combinedTiming = string.Format("{0,4} {1,9}",screeningDateTime.ToString("dd/MM/yyyy"), screeningDateTime.ToShortTimeString());
            return string.Format("{0,-30} {1,-30} {2,-20} {3,-30} {4,-30} {5,-30}", screeningNo, cinema.name, cinema.hallNo, movie.title, combinedTiming, screeningType);
        }
    }
}
