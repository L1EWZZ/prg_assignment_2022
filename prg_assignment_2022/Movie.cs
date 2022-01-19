using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg_assignment
{
    //Movie does not inherit Screening. By inheriting, im saying that a Movie is a Screening itself which is false
    class Movie
    {
        public string title { get; set; }
        public int duration { get; set; }
        public string classification { get; set; }
        public DateTime openingDate { get; set; }

        public List<string> genreList { get; set; } = new List<string>();

        public List<Screening> screeningList { get; set; } = new List<Screening>();

        public Movie() { }

        public Movie(string Title, int Duration, string Classification, DateTime OpeningDate, List<string> GenreList)
        {
            title = Title;
            duration = Duration;
            classification = Classification;
            openingDate = OpeningDate;
            genreList = GenreList;
        }

        public void AddGenre(string genre)
        {
            genreList.Add(genre);
        }


        public void addScreening(Screening screeningToBeAdded)
        {
            screeningList.Add(screeningToBeAdded);
        }

        public override string ToString()
        {
            return string.Format("{0,-30} {1,-20} {2,-20} {3,-20}", title, duration, classification, openingDate.ToString("dd/MM/yyyy"));
        }
    }
}
