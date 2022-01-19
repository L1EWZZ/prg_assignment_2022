using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace prg_assignment
{
    class methods
    {
        public static void printMenu()
        {
            Console.WriteLine("\n-----Welcome to Golden Movies-----");
            Console.WriteLine("1. Display all movies.");
            Console.WriteLine("2. Display all movie screenings.");
            Console.WriteLine("3. Display a specific movie screenings.");
            Console.WriteLine("4. Display all cinemas.");
            Console.WriteLine("5. Add a movie screening session.");
            Console.WriteLine("6. Delete a movie screening session.");
            Console.WriteLine("7. Order movie tickets.");
            Console.WriteLine("8. Cancel order of ticket.");
            Console.WriteLine("9. Print list of orders.");
            Console.WriteLine("0. Exit.");
            Console.WriteLine("-----------------------------------\n");
        }

        //return -1 if invalid number option. return numberOption if valid number option from user 
        public static int getNumberOption()
        {
            int numberOption;

            Console.Write("Please enter your option: ");
            while (!int.TryParse(Console.ReadLine(), out numberOption))
            {
                Console.Write("Please enter a valid number: ");
            }
            for (int validNumber = 0; validNumber < 10; validNumber++)
            {
                if (numberOption == validNumber)
                {
                    return numberOption;
                }
            }
            return -1;
        }

        public static List<Screening> loadScreeningDate(List<Screening> screeningDataList, List<Cinema> cinemaDataList, List<Movie> movieDataList)
        {
            int screeningNo = 1001;
            //using StreamReader to read each line of the csv file from desktop
            using (StreamReader eachLines = new StreamReader("C:/Users/zheng/Desktop/Screening.csv"))
            {
                string eachRow;

                _ = eachLines.ReadLine();   //remove header since we already printing header seperately in another method 

                while ((eachRow = eachLines.ReadLine()) != null)   //read each row of string data in csv file until it reach null
                {
                    List<string> stringListOfDetails = eachRow.Split(',').ToList();
                    screeningNo++;
                    foreach (var cinema in cinemaDataList)
                    {
                        foreach (var movie in movieDataList)
                        {
                            //if Screening Hall number of csv file is same as cinema object hallNo AND if cinema name of csv file is same as cinema object Name AND movie title of csv file same as movie object title,
                            if (stringListOfDetails[3] == cinema.hallNo.ToString() && stringListOfDetails[2] == cinema.name && stringListOfDetails[4] == movie.title)
                            {
                                //add the specific cinema and movie object
                                var dateTime = DateTime.ParseExact(stringListOfDetails[0], "dd/MM/yyyy h:mmtt", CultureInfo.InvariantCulture);

                                Screening newScreeningData = new Screening(screeningNo, dateTime, stringListOfDetails[1], cinema, movie);
                                screeningDataList.Add(newScreeningData);
                            }
                        }
                    }
                }
            }
            return screeningDataList;
        }

        public static List<Movie> loadMovieData(List<Movie> movieDataList)
        {
            var callMovieMethods = new Movie();

            //using StreamReader to read each line of the csv file from desktop
            using (StreamReader eachLines = new StreamReader("C:/Users/zheng/Desktop/Movie.csv"))
            {
                string eachRow;
                //DateTime openingDate;

                _ = eachLines.ReadLine();   //remove header of csv file

                while ((eachRow = eachLines.ReadLine()) != null)   //read each row of string data in csv file until it reach null
                {

                    List<string> stringListOfDetails = eachRow.Split(',').ToList();

                    if (!(stringListOfDetails.Any(string.IsNullOrWhiteSpace)))
                    {
                        List<string> genreList = new List<string>();
                        genreList.Add(stringListOfDetails[2]);  //list of genres from csv file

                        DateTime openingDate = DateTime.ParseExact(stringListOfDetails[4], "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        var movieData = new Movie(stringListOfDetails[0], int.Parse(stringListOfDetails[1]), stringListOfDetails[3], openingDate, genreList);
                        movieDataList.Add(movieData);
                    }
                }
            }
            return movieDataList;
        }

        //populate cinema list
        public static List<Cinema> loadCinemaData(List<Cinema> cinemaDetailsList)
        {
            //using StreamReader to read each line of the csv file from desktop
            using (StreamReader eachLines = new StreamReader("C:/Users/zheng/Desktop/Cinema.csv"))
            {
                string eachRow;

                _ = eachLines.ReadLine();   //remove header since we already printing header seperately in another method 

                while ((eachRow = eachLines.ReadLine()) != null)   //read each row of string data in csv file until it reach null
                {
                    List<string> stringListOfDetails = eachRow.Split(',').ToList();

                    var cinemaDetail = new Cinema(stringListOfDetails[0], int.Parse(stringListOfDetails[1]), int.Parse(stringListOfDetails[2]));
                    cinemaDetailsList.Add(cinemaDetail);
                }
            }
            return cinemaDetailsList;
        }

        public static void printCinemaList(List<Cinema> cinemaList)
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "Name", "HallNo", "Capacity");
            Console.WriteLine("{0,-20} {1,-20} {2,-20}", "----", "------", "--------");
            foreach (var cinemaDetail in cinemaList)
            {
                Console.WriteLine(cinemaDetail.ToString());
            }
        }

        public static void printMovieList(List<Movie> movieList)
        {
            Console.WriteLine("{0,-30} {1,-20} {2,-20} {3,-20}", "Title", "Duration", "Classification", "Opening Date");
            Console.WriteLine("{0,-30} {1,-20} {2,-20} {3,-20}", "-----", "--------", "--------------", "------------");
            foreach (var movieDetail in movieList)
            {
                Console.WriteLine(movieDetail.ToString());
            }
        }

        public static void printScreeningList(List<Screening> screeningList)
        {
            Console.WriteLine("{0,-30} {1,-30} {2,-20} {3,-30} {4,-30} {5,-30}", "Screening Number", "Cinema Name", "HallNo", "Movie Name", "Screening Date Time", "Screening Type");
            Console.WriteLine("{0,-30} {1,-30} {2,-20} {3,-30} {4,-30} {5,-30}", "----------------", "-----------", "------", "----------", "-------------------", "--------------");
            foreach (var screeningDetail in screeningList)
            {
                Console.WriteLine(screeningDetail.ToString());
            }
        }

        public static Movie getMovieName(List<Movie> movieList)  //etither NULL or correct Movie name 
        {
            string movieTitle = "";

            bool status = true;
            //get user input on movie
            while (status)
            {
                //if user enters anything but a string word, print message and ask for user input again
                Console.Write("Enter a movie title: ");
                movieTitle = Console.ReadLine().Trim();  //read the input, trim out any whitespace before or after the string input 

                if (string.IsNullOrWhiteSpace(movieTitle))  //if entered input is a whitespace 
                {
                    Console.WriteLine("Please do not enter a white space.");  //tell the user not to enter a white space 
                    Console.WriteLine("");
                }
                else  //break out of validation loop for white space 
                {
                    status = false;
                }
            }

            foreach (var movie in movieList)
            {
                if (movie.title == movieTitle)  //if entered movieTitle from user is correct, return the Movie object
                {
                    return movie;
                }
            }
            return null;  //if wrong user input for movieTitle return null
        }

        public static void printMovieSession(Movie selectedMovie, List<Screening> screeningList)
        {
            foreach (var screening in screeningList)  ///for each screening in screeningList
            {
                if (screening.movie.title == selectedMovie.title)  //if they have the same title, 
                {
                    Console.WriteLine(screening.ToString());  //print out all the screening data
                }
            }
        }

        public static string askForScreeningType()
        {
            string screeningType = "";
            bool status = true;

            //get user input on screening type
            while (status)
            {
                //if user enters anything but aa string word, print message and ask for user input again
                Console.Write("Enter a screening type: ");
                screeningType = Console.ReadLine().Trim();

                if (screeningType.Any(char.IsWhiteSpace))  //if entered input is not a whitespace 
                {
                    Console.WriteLine("Please do not enter a white space.\n");  //tell user not to enter white space
                }
                else  //break out of validation loop
                {
                    status = false;
                }
            }

            if (screeningType == "2D" || screeningType == "3D")
            {
                return screeningType;
            }
            return null;
        }

        public static List<Screening> checkForAvailableScreening(List<Screening> screeningList, string screeningType, Movie selectedMovie)
        {
            List<Screening> availableScreeningList = new List<Screening>();
            foreach (var screening in screeningList)
            {
                if (screening.movie.title == selectedMovie.title && screening.screeningType == screeningType)
                {
                    availableScreeningList.Add(screening);
                }
            }
            if (availableScreeningList.Count == 0)
            {
                return null;
            }
            else
            {
                return availableScreeningList;
            }
        }

        public static DateTime askForScreeningDateForSlectedMovie(Movie selectedMovie)
        {
            var screeningDate = new DateTime();
            string stringInputForScreeningDate;
            bool status = true;

            while (status)
            {
                Console.Write("Enter a screening date for the movie: ");
                stringInputForScreeningDate = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(stringInputForScreeningDate))
                {
                    Console.WriteLine("Please do not enter a white space.");
                    Console.WriteLine("");
                }

                //keep asking user for exact datetime format day/month/year hour:minute AM/PM
                else if (!DateTime.TryParseExact(stringInputForScreeningDate, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out screeningDate))
                {
                    Console.WriteLine("Please enter screening date in date time format day/month/year hour:minute AM/PM. day and month have to be double digits.\n");
                }

                else
                {
                    status = false;
                }
            }

            if (screeningDate > selectedMovie.openingDate)
            {
                return screeningDate;
            }

            else
            {
                screeningDate = default(DateTime);
            }
            return screeningDate;
        }

        public static Cinema selectCinemaHall(List<Cinema> CinemasList, List<Screening> screeningList)
        {
            string selectedCinema = "";
            bool status = true;

            while (status)
            {
                Console.Write("\nPlease select a cinema hall: ");
                selectedCinema = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(selectedCinema))
                {
                    Console.WriteLine("Please do not enter a white space.");
                    Console.WriteLine("");
                }
                else
                {
                    status = false;
                }
            }

            foreach (var cinema in CinemasList)  //get the cinema from user 
            {
                if (cinema.name == selectedCinema)
                {
                    return cinema;
                }
            }
            return null;
        }

        public static int getHallNoOfCinema(Cinema selectedCinema, List<Cinema> cinemaList)
        {
            List<int> hallNumbersList = new List<int>();

            foreach (var cinema in cinemaList)
            {
                if (selectedCinema.name == cinema.name)
                {
                    hallNumbersList.Add(cinema.hallNo);
                }
            }

            int chosenHallNumber;

            Console.Write("Enter a hall number from " + hallNumbersList[0] + " to " + hallNumbersList.Last() + ": ");
            while (!int.TryParse(Console.ReadLine(), out chosenHallNumber))
            {
                Console.Write("Please enter a correct integer hall number from " + hallNumbersList[0] + " to " + hallNumbersList.Last() + ": ");
            }

            foreach (var hallNum in hallNumbersList)
            {
                if (hallNum == chosenHallNumber)
                {
                    return hallNum;
                }
            }

            return 0;
        }

        public static bool checkIfDateTimeCollides(List<Screening> screeningList, DateTime userScreeningDateTime, Cinema selectedCinema, int chosenHallNumber, Movie selectedMovie)
        {
            List<Screening> selectedScreeningList = new List<Screening>();  //list of screening if the name and hall number of selected Cinema from user is same as screening from screeningList
            List<DateTime> newScreeningDateTimeList = new List<DateTime>();  //list of new DateTime after adding 30 mins cleaning time and duration of movie to the old screening DateTime 

            foreach (var selectedScreening in screeningList)
            {
                if (selectedScreening.cinema.name == selectedCinema.name && selectedScreening.cinema.hallNo == chosenHallNumber)
                {
                    selectedScreeningList.Add(selectedScreening);
                }
            }

            int durationOfMovieAndCleaning = selectedMovie.duration + 30;  //total mins taken for cleaning cinema and watching movie 

            foreach (var screening in selectedScreeningList)
            {
                //adding cleaning and movie duration to old DateTime of each screening from specific screening list
                DateTime afterMovieAndCleaningDateTime = screening.screeningDateTime.AddMinutes(durationOfMovieAndCleaning);
                newScreeningDateTimeList.Add(afterMovieAndCleaningDateTime);
            }

            bool status = true;
            foreach (var screening in selectedScreeningList)  //foreach screening from the specific screening 
            {
                DateTime startTime = screening.screeningDateTime;  //starting date time will be screening's old datetime before adding movie duration and cleaning 
                foreach (var endDateTime in newScreeningDateTimeList)
                {
                    //if user inputs a timing which overlaps from movie starting time to end of movie + cleaning time, status becomes false
                    if (userScreeningDateTime >= startTime && userScreeningDateTime < endDateTime)
                    {
                        status = false;
                    }
                }
            }

            //if status is false, means user did not enter right timing
            if (status == false)
            {
                return true;
            }
            //else correct timing 
            return false;
        }

        public static List<Screening> createScreeningObject(int chosenhallNumber, Movie selectedMovie, DateTime userScreeningDateTime, string screeningType, List<Screening> screeningList, Cinema selectedCinema, List<Cinema> cinemaList)
        {
            int newScreeningNo = screeningList.Last().screeningNo + 1;  //add 1 to screening number to increase by one
            selectedCinema.hallNo = chosenhallNumber;  //change to user selected hall number 
            var newScreening = new Screening(newScreeningNo, userScreeningDateTime, screeningType, selectedCinema, selectedMovie);

            //if added new screening under the cinema, minus 1 from old capacity of cinema and populate seats remaining for new screening object
            foreach (var cinema in cinemaList)
            {
                if (cinema.name == newScreening.cinema.name && cinema.hallNo == newScreening.cinema.hallNo)
                {
                    newScreening.seatsRemaining = cinema.capacity;
                }
            }

            screeningList.Add(newScreening);
            return screeningList;
        }

        public static void populateSeatsRemaining(List<Cinema> cinemaList, List<Screening> screeningList)
        {
            foreach (var cinema in cinemaList)
            {
                foreach (var screening in screeningList)
                {
                    if (cinema.name == screening.cinema.name && cinema.hallNo == screening.cinema.hallNo)
                    {
                        screening.seatsRemaining = cinema.capacity;
                    }
                }
            }

            foreach (var screeningObject in screeningList)
            {
                foreach (var checkScreening in screeningList)
                {
                    if (screeningObject.cinema.name == checkScreening.cinema.name && screeningObject.cinema.hallNo == checkScreening.cinema.hallNo)
                    {
                        screeningObject.seatsRemaining = screeningObject.seatsRemaining - 1;
                    }
                }

            }
        }



        public static int getScreeningNumber(List<Screening> screeningList)
        {
            int screeningNo;
            Console.Write("Enter a screening number of a screening session: ");
            while (!int.TryParse(Console.ReadLine().Trim(), out screeningNo))
            {
                Console.Write("Please enter a correct number: ");
            }

            foreach (var screening in screeningList)
            {
                if (screening.screeningNo == screeningNo)
                {
                    return screeningNo;
                }
            }
            return 0;
        }

        public static void removeScreeningSession(List<Screening> screeningList, int screeningNo)
        {
            var screeningToRemove = new Screening();
            foreach (var screening in screeningList)
            {
                if (screeningNo == screening.screeningNo)
                {
                    screeningToRemove = screening;
                    break;
                }
            }
            screeningList.Remove(screeningToRemove);
            Console.WriteLine("Your selected screening has been removed from the list of screening.");
        }

        public static bool checkIfHaveMovieScreening(Movie chosenMovie, List<Screening> screeningList)
        {
            foreach (var screening in screeningList)
            {
                if (chosenMovie.title == screening.movie.title)
                {
                    return true;  //return the true if movie is amongst the list of screening
                }
            }
            return false;  //return false if chosen movie dont exist amongst screening list
        }

        public static Screening getScreeningNumberFromMovieScreening(List<Screening> screeningList, Movie selectedMovie)
        {
            List<Screening> selectedMovieScreeningList = new List<Screening>();
            foreach (var screening in screeningList)
            {
                if (selectedMovie.title == screening.movie.title)
                {
                    selectedMovieScreeningList.Add(screening);
                }
            }

            int chosenScreeningNumber;
            while (true)
            {
                chosenScreeningNumber = methods.getScreeningNumber(selectedMovieScreeningList);  //get screening number from screening list of movie

                if (chosenScreeningNumber == 0)
                {
                    Console.WriteLine("Please enter a correct screening number.\n");
                }
                else
                {
                    break;
                }
            }

            var screeningToBeReturned = new Screening();
            foreach (var screening in screeningList)
            {
                if (screening.screeningNo == chosenScreeningNumber)
                {
                    screeningToBeReturned = screening;
                }
            }
            return screeningToBeReturned;
        }

        public static int getTicketsOrdered(Screening screeningToBeReturned)  //returns ordered number of tickets
        {
            int totalTickets;
            Console.Write("Enter total number of tickets below " + screeningToBeReturned.cinema.capacity + " to order: ");

            while (!int.TryParse(Console.ReadLine(), out totalTickets))
            {
                Console.Write("Please enter a correct number: ");
            }

            return totalTickets;
        }

        //checking if the ordered number of tickets is less than cinema capacity
        public static bool checkIfOrderedTicketsMoreThanCapacity(Screening screeningToBeReturned, int totalOrderedTickets)
        {
            if (screeningToBeReturned.cinema.capacity < totalOrderedTickets)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //check if movie classification is anything but "G"
        public static bool checkMovieClassifications(Movie selectedMovie)
        {
            if (selectedMovie.classification == "G")  //if it is not "G", return true
            {
                return true;
            }
            return false;
        }

        //if it is not "G" rating, have to ask staff for each ticket, do they go above the age requirements
        //if one of the ticket does not reach age requirement, remove the ticket
        public static int askForTicketAge(int totalTickets, Movie selectedMovie)
        {
            string confirmation;
            int ticketRemoved = 0;
            for (int i = 0; i < totalTickets; i++)
            {
                int counter = i + 1;
                Console.Write("Does ticket holder " + counter + " go above the age for classification " + selectedMovie.classification + "? ");
                confirmation = Console.ReadLine();
                if (confirmation == "N")
                {
                    Console.WriteLine("This ticket has been removed as the ticket holder is not above age requirement.\n");
                    ticketRemoved = ticketRemoved + 1;
                }
                else if (confirmation == "Y")
                {
                    Console.WriteLine("Valid ticket!\n");
                    continue;
                }
                else
                {
                    Console.WriteLine("Please enter 'Y' or 'N'.\n");
                }
            }
            int remainingTickets = totalTickets - ticketRemoved;
            return remainingTickets;
        }

        public static Order createNewTicktForOrder(int numberOfTickets, Screening chosenScreening, List<Cinema> cinemaList, Movie selectedMovie, List<Order> listOfOrders)
        {
            //brand new order
            double adultTotalPriceToPay = 0;
            double studentPrice = 0;
            double seniorCitizenPrice = 0;
            int seniorCitizenAge;
            string ageGroup;
            string finalStudyLevel = "";
            int rejectedTickets = 0;
            string levelOfStudy;
            string popcornDealConfirmation;
            int popcornDealTotalPrice = 0;
            var newOrder = new Order();
            var newAdultOrder = new Adult();

            newOrder = new Order((listOfOrders.Count()) + 1, DateTime.Now);
            newOrder.status = "Unpaid";

            for (int i = 0; i < numberOfTickets; i++)
            {
                //validation to check if age group is Student, Adult, Senior Citizen
                while (true)
                {
                    int count = i + 1;
                    Console.Write("Please enter age group (Student, Adult, Senior Citizen) for ticket number " + count + ": ");
                    ageGroup = Console.ReadLine().Trim();

                    if (ageGroup == "Student" || ageGroup == "Adult" || ageGroup == "Senior Citizen")
                    {
                        break;
                    }
                    else if (string.IsNullOrWhiteSpace(ageGroup))
                    {
                        Console.WriteLine("Please do not enter a white space.\n");
                    }
                    else
                    {
                        Console.WriteLine("Please enter a correct age group.\n");
                    }
                }

                //if ticket holder is a student, check if student level of study is Primary, Secondary, Tertiarys
                if (ageGroup == "Student")
                {
                    while (true)
                    {
                        Console.Write("Please enter student's level of study (Primary, Secondary, Tertiary): ");
                        levelOfStudy = Console.ReadLine().Trim();

                        if (levelOfStudy == "Primary" || levelOfStudy == "Secondary" || levelOfStudy == "Tertiary")
                        {
                            //Primary students are not allowed to watch NNC16 shows etc
                            if (levelOfStudy == "Primary" && selectedMovie.classification == "NC16" || levelOfStudy == "Primary" && selectedMovie.classification == "M18" || levelOfStudy == "Primary" && selectedMovie.classification == "R21" || levelOfStudy == "Secondary" && selectedMovie.classification == "M18" || levelOfStudy == "Secondary" && selectedMovie.classification == "R21")
                            {
                                Console.WriteLine(levelOfStudy + " level of study is not permitted for " + selectedMovie.classification + ". Please select again.\n");
                            }
                            else
                            {
                                Console.WriteLine("The student ticket has been validated.\n");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a correct level of study.\n");
                        }
                    }

                    var newStudentOrder = new Student(chosenScreening, finalStudyLevel);
                    studentPrice = newStudentOrder.CalculatePrice();
                    newOrder.amount += studentPrice;
                    newOrder.ticketList.Add(newStudentOrder);
                }

                //else if ticket holder is a senior citizen
                else if (ageGroup == "Senior Citizen")
                {
                    Console.Write("PLease enter senior citizen's age: ");
                    while (!int.TryParse(Console.ReadLine().Trim(), out seniorCitizenAge))
                    {
                        Console.Write("Please enter a valid age: ");
                    }

                    if (seniorCitizenAge < 55)  //if senior citizen is belowe aged 55, reject the ticket and remove it 
                    {
                        Console.WriteLine("This senior citizen ticket has been rejected as he or she is not above 55.\n");
                        rejectedTickets = rejectedTickets + 1;
                    }

                    //if senior citizen aged 55 and above, approve it
                    else
                    {
                        Console.WriteLine("Senior Citizen tickte has been validaed!\n");
                        var newSeniorCitizenOrder = new SeniorCitizen(chosenScreening, seniorCitizenAge);
                        seniorCitizenPrice = newSeniorCitizenOrder.CalculatePrice();
                        newOrder.amount += seniorCitizenPrice;
                        newOrder.AddTicket(newSeniorCitizenOrder);
                    }
                }

                //else if ticket holder is an adult
                else if (ageGroup == "Adult")
                {
                    while (true)
                    {
                        Console.Write("Does the ticket holder wish to have the popcorn deal for $3? ");
                        popcornDealConfirmation = Console.ReadLine();

                        if (popcornDealConfirmation == "Y")
                        {
                            Console.WriteLine("Deal has been added!\n");
                            popcornDealTotalPrice += 3;
                            break;
                        }
                        else if (popcornDealConfirmation == "N")
                        {
                            Console.WriteLine("Deal has been called off.\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter only 'Y' or 'N'.\n");
                        }
                    }

                    bool popcornDealBool;
                    //if ticket holder chooses not to have popcorn deal, create adult order but do not add $3
                    if (popcornDealConfirmation == "N")
                    {
                        popcornDealBool = false;
                        newAdultOrder = new Adult(chosenScreening, popcornDealBool);
                        adultTotalPriceToPay = newAdultOrder.CalculatePrice();
                    }
                    //else add $3
                    else
                    {
                        popcornDealBool = true;
                        newAdultOrder = new Adult(chosenScreening, popcornDealBool);
                        adultTotalPriceToPay = newAdultOrder.CalculatePrice() + 3;
                    }
                    newOrder.amount += adultTotalPriceToPay;
                    newOrder.AddTicket(newAdultOrder);
                }
            }

            //remove the number of booked tickets from each cinema's seating capacity
            foreach (var cinema in cinemaList)
            {
                if (chosenScreening.cinema.name == cinema.name && chosenScreening.cinema.hallNo == cinema.hallNo)
                {
                    cinema.capacity -= numberOfTickets;
                }
            }
            return newOrder;
        }

        //asking user to press any key, type in amount, pay and change order status to paid
        public static double askUserToPayAndReturnMoneyGiven(Order newOrder)
        {
            double amountToPay;
            Console.WriteLine("");
            Console.WriteLine("The amount user has to pay is $" + newOrder.amount);
            Console.Write("Please press any key to pay. ");  //press anything to continue
            Console.ReadKey();

            Console.WriteLine("");
            Console.Write("Please enter amount to pay: ");
            while (!double.TryParse(Console.ReadLine(), out amountToPay))
            {
                Console.Write("Please enter correct amount to pay: ");
            }
            return amountToPay;
        }

        //loop to make sure user gives correct amount. if given amount is more than payable amount, return change 
        public static double checkUserAmountPaid(Order newOrder)
        {
            double amountPaid;
            double changeGiven;
            while (true)
            {
                amountPaid = methods.askUserToPayAndReturnMoneyGiven(newOrder);
                if (amountPaid < newOrder.amount)
                {
                    Console.WriteLine("Please enter an amount greater than $" + newOrder.amount);
                }
                else
                {
                    newOrder.status = "Paid";
                    changeGiven = amountPaid - newOrder.amount;
                    Console.WriteLine("The order has been successfully paid for. The change is $" + changeGiven);
                    break;
                }
            }
            return changeGiven;
        }

        //return true if list of orders is empty
        public static bool isListOfOrderEmpty(List<Order> listOfOrders)
        {
            if (listOfOrders.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static Order getOrderNumber(List<Order> listOfOrders)  //return true if order number is a valid number from list of orders 
        {
            double orderNumber;

            Console.Write("\nPlease enter order number: ");
            while (!double.TryParse(Console.ReadLine(), out orderNumber))
            {
                Console.Write("Please enter a correct order number: ");
            }

            foreach (var order in listOfOrders)
            {
                if (order.orderNo == orderNumber)
                {
                    return order;
                }
            }
            return null;
        }

        public static void printListOfOrders(List<Order> listOfOrders)
        {
            Console.WriteLine("{0,-20} {1,-30} {2,-20} {3,-20}", "Order No", "Order Date Time", "Amount", "Status");
            Console.WriteLine("{0,-20} {1,-30} {2,-20} {3,-20}", "--------", "---------------", "------", "------");
            foreach (var order in listOfOrders)
            {
                Console.WriteLine(order.ToString());
            }
        }

        public static bool checkIfScreeningStillOn(Order selectedOrder, List<Screening> listOfScreenings)
        {
            //return true if screening is still on
            foreach (var ticket in selectedOrder.ticketList)
            {
                foreach (var screening in listOfScreenings)
                {
                    if (ticket.screening.screeningNo == screening.screeningNo)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //search for the cinema that needs to have seats updated 
        public static Cinema searchForCinemaToUpdate(List<Cinema> cinemaList, Order selectedOrder, List<Order> listOfOrders)
        {
            int numberOfTickets;
            var cinemaToBeReturned = new Cinema();
            foreach (var cinema in cinemaList)
            {
                foreach (var ticket in selectedOrder.ticketList)
                {
                    if (cinema.name == ticket.screening.cinema.name && cinema.hallNo == ticket.screening.cinema.hallNo)
                    {
                        cinemaToBeReturned = cinema;
                    }
                }
            }
            numberOfTickets = selectedOrder.ticketList.Count();
            cinemaToBeReturned.capacity += numberOfTickets;
            selectedOrder.status = "Cancelled";
            listOfOrders.Remove(selectedOrder);
            Console.WriteLine("This order has been refunded.\n");
            return cinemaToBeReturned;
        }
    }
}