using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace prg_assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> listOfOrders = new List<Order>();
            var selectedMovie = new Movie();
            bool mainStatus = true;
            List<Screening> screeningList = new List<Screening>();
            List<Movie> movieList = new List<Movie>();
            List<Cinema> cinemaList = new List<Cinema>();

            methods.loadCinemaData(cinemaList);
            methods.loadMovieData(movieList);

            methods.loadScreeningDate(screeningList, cinemaList, movieList);


            while (mainStatus)
            {
                methods.printMenu();  //print menu

                #region get user's number option
                int numberOption = 0;
                bool status1 = true;
                while (status1)
                {
                    numberOption = methods.getNumberOption();  //return -1 if wrong number option
                    if (numberOption == -1)
                    {
                        Console.WriteLine("Please enter a correct number option.\n");
                    }
                    else
                    {
                        status1 = false;
                    }
                }
                Console.WriteLine("");
                #endregion

                if (numberOption == 1)
                {
                    methods.printMovieList(movieList);
                }

                else if (numberOption == 2)
                {
                    methods.printScreeningList(screeningList);
                }

                else if (numberOption == 3)
                {
                    while (true)  //validation while loop to ask for correct user input for movie title 
                    {
                        selectedMovie = methods.getMovieName(movieList);  //method to validate user string input and obtain Movie object or null
                        if (selectedMovie != null)  //if not null, means correct user input, so break out of validation for movie title loop
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter correct movie title.");
                            Console.WriteLine("");
                        }
                    }
                    methods.printMovieSession(selectedMovie, screeningList);  //print out all sessions for the selected movie name
                    Console.WriteLine("");
                    Console.WriteLine("");
                }

                else if (numberOption == 4)
                {
                    methods.printCinemaList(cinemaList);
                }

                else if (numberOption == 5)
                {
                    #region 2. asking for movie title and getting movie object (Movie selectedMovie)
                    while (true)  //validation while loop to ask for correct user input for movie title 
                    {
                        selectedMovie = methods.getMovieName(movieList);  //method to validate user string input and obtain Movie object or null
                        if (selectedMovie != null)  //if not null, means correct user input, so break out of validation for movie title loop
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a correct movie title.");
                            Console.WriteLine("");
                        }
                    }
                    #endregion

                    #region 3. asking for screening type of movie (string screeningType)
                    string screeningType;
                    while (true)  //validation while loop to ask for correct user input for screening type
                    {
                        screeningType = methods.askForScreeningType();  //method to validate user string input and obtain screening type string or null
                        if (screeningType != null)  //if not null, means correct user input, so break out of validation for screening type loop
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a correct screening type.");
                            Console.WriteLine("");
                        }
                    }
                    #endregion

                    #region 4. asking for screening date of selected movie (DateTime screeningDateTime)
                    var screeningDateTime = new DateTime();

                    while (true)  //validation while loop to ask for correct user input for screening date
                    {
                        screeningDateTime = methods.askForScreeningDateForSlectedMovie(selectedMovie);  //method to validate correct datetime input

                        if (screeningDateTime != default(DateTime))  //if not default DateTime, means correct user input, so break out of validation loop
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a screening date after the movie opening date.\n");
                        }
                    }
                    #endregion

                    #region 5. list out all cinema halls
                    Console.WriteLine("");
                    methods.printCinemaList(cinemaList);
                    #endregion

                    #region 6. get user input on cinema and hall number from list of cinemas (Cinema selectedCinema, int chosenHallNumber)
                    var selectedCinema = new Cinema();

                    //getting cinema hall name from user 
                    while (true)
                    {
                        selectedCinema = methods.selectCinemaHall(cinemaList, screeningList);

                        if (selectedCinema == null)
                        {
                            Console.WriteLine("Please enter a correct cinema hall.\n");
                        }
                        else
                        {
                            break;
                        }
                    }

                    //getting hall number of cinema from the user
                    int chosenHallNumber;
                    while (true)
                    {
                        chosenHallNumber = methods.getHallNoOfCinema(selectedCinema, cinemaList);

                        if (chosenHallNumber == 0)
                        {
                            Console.Write("Please only enter a hall number between the given numbers.\n");
                        }
                        else
                        {
                            break;
                        }
                    }

                    //checking if user entered DateTime collides with any of the timings and asking again if wrong DateTime input 
                    while (true)
                    {
                        bool timeCollided = methods.checkIfDateTimeCollides(screeningList, screeningDateTime, selectedCinema, chosenHallNumber, selectedMovie);

                        if (timeCollided == true)
                        {
                            Console.WriteLine("Your movie screening is unsuccessful becuase your timing cannot be accepted due to overlap with other screening timings. Please enter again.");
                            screeningDateTime = methods.askForScreeningDateForSlectedMovie(selectedMovie);
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion

                    methods.createScreeningObject(chosenHallNumber, selectedMovie, screeningDateTime, screeningType, screeningList, selectedCinema, cinemaList);
                    Console.WriteLine("Your movie screening is successful and has been added to the list!");
                }

                else if (numberOption == 6)
                {
                    methods.printScreeningList(screeningList);

                    int screeningNumber;
                    while (true)
                    {
                        screeningNumber = methods.getScreeningNumber(screeningList);

                        if (screeningNumber == 0)
                        {
                            Console.WriteLine("Your screening cannot be removed since the entered screening number is invalid. Enter a valid screening number.\n");
                        }
                        else
                        {
                            break;
                        }
                    }
                    methods.removeScreeningSession(screeningList, screeningNumber);
                }

                else if (numberOption == 7)
                {
                    //1. print the movie list
                    methods.printMovieList(movieList);
                    Console.WriteLine("");

                    //2. get movie name
                    while (true)  //validation while loop to ask for correct user input for movie title 
                    {
                        selectedMovie = methods.getMovieName(movieList);  //method to validate user string input and obtain Movie object or null
                        if (selectedMovie != null)  //if not null, means correct user input, so break out of validation for movie title loop
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter correct movie title.");
                            Console.WriteLine("");
                        }
                    }

                    while (true)
                    {
                        var movieExists = methods.checkIfHaveMovieScreening(selectedMovie, screeningList);
                        if (movieExists == true)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please choose a movie that is amongst the list of screening.\n");
                            selectedMovie = methods.getMovieName(movieList);
                        }
                    }

                    //3. list all movie screenings of selected movie
                    Console.WriteLine("");
                    Console.WriteLine("{0,-30} {1,-30} {2,-20} {3,-30} {4,-30} {5,-30}", "Screening Number", "Cinema Name", "HallNo", "Movie Name", "Screening Date Time", "Screening Type");
                    Console.WriteLine("{0,-30} {1,-30} {2,-20} {3,-30} {4,-30} {5,-30}", "----------------", "-----------", "------", "----------", "-------------------", "--------------");
                    methods.printMovieSession(selectedMovie, screeningList);
                    Console.WriteLine("");

                    //4. and 5. get screening from the list and retrieve it
                    var chosenScreening = methods.getScreeningNumberFromMovieScreening(screeningList, selectedMovie);

                    //6. get total number of tickets
                    int totalTickets;
                    while (true)
                    {
                        totalTickets = methods.getTicketsOrdered(chosenScreening);
                        bool isMoreThanCapacity = methods.checkIfOrderedTicketsMoreThanCapacity(chosenScreening, totalTickets);

                        if (isMoreThanCapacity == true)
                        {
                            Console.WriteLine("Please enter tickets smaller than " + chosenScreening.cinema.capacity);
                            Console.WriteLine("");
                        }
                        else
                        {
                            break;
                        }
                    }

                    //check if classification of movie is not "G". return true if not "G"
                    bool isRatedG = methods.checkMovieClassifications(selectedMovie);
                    int remainingTickets;
                    var newOrder = new Order();

                    //if it is not rated G, ask for age requirement of each ticket
                    if (isRatedG == false)
                    {
                        remainingTickets = methods.askForTicketAge(totalTickets, selectedMovie);  //asking for age requirement for each ticket

                        if (remainingTickets == 0)
                        {
                            Console.WriteLine("There is no tickets for payment.\n");
                        }
                        else
                        {
                            newOrder = methods.createNewTicktForOrder(remainingTickets, chosenScreening, cinemaList, selectedMovie, listOfOrders);  //returns an order object with all ticket order details
                            listOfOrders.Add(newOrder);  //add the new order to a list of orders for deletion of order later on
                            methods.checkUserAmountPaid(newOrder);
                        }
                    }
                    else  //else just create order object, do not ask for age requirements since G is universal age
                    {
                        newOrder = methods.createNewTicktForOrder(totalTickets, chosenScreening, cinemaList, selectedMovie, listOfOrders);
                        listOfOrders.Add(newOrder);
                        methods.checkUserAmountPaid(newOrder);
                    }

                }

                else if (numberOption == 8)
                {
                    bool isListOfOrderEmpty;
                    isListOfOrderEmpty = methods.isListOfOrderEmpty(listOfOrders);

                    if (isListOfOrderEmpty == true)  //if there are no orders in list of orders, 
                    {
                        Console.WriteLine("There is no order to be removed.");
                    }

                    else
                    {
                        methods.printListOfOrders(listOfOrders);

                        //validation to  get correct order number from list of orders
                        var searchedOrder = new Order();
                        while (true)
                        {
                            searchedOrder = methods.getOrderNumber(listOfOrders);
                            if (searchedOrder == null)
                            {
                                Console.WriteLine("Please enter a valid order number from the list of orders.\n");
                            }
                            else
                            {
                                break;
                            }
                        }

                        var searchedCinema = new Cinema();
                        bool screeningExists;
                        screeningExists = methods.checkIfScreeningStillOn(searchedOrder, screeningList);  //returns true if screening to remove exists in list of screening
                        if (screeningExists == false)
                        {
                            Console.WriteLine("Your order removal is unsuccessful as the screening number does not exist in the list of screenings.");
                        }
                        else
                        {
                            searchedCinema = methods.searchForCinemaToUpdate(cinemaList, searchedOrder, listOfOrders);  //return the cinema with its capacity updated
                        }
                    }
                }

                else if (numberOption == 9)
                {
                    bool emptyList;
                    emptyList = methods.isListOfOrderEmpty(listOfOrders);  //returns true if list of order is empty
                    if (emptyList == true)
                    {
                        Console.WriteLine("There are no orders to be displayed.");
                    }
                    else
                    {
                        methods.printListOfOrders(listOfOrders);
                    }
                }

                else
                {
                    Console.WriteLine("Thank you for using Golden Movies.");
                    mainStatus = false;
                }
            }
        }
    }
}
