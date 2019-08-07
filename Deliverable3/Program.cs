using System;
using static System.Console;

namespace Deliverable3
{
    class Program
    {
        static void Main(string[] args)
        {
            const string EXIT = "999";
            const string EXIT2 = "000";
            const int seatAmount = 40;

            Passenger passenger = new Passenger(seatAmount);
            int pasNum = 0; // number of passenger
            int cusNum = 0; // number in a checkin list
            int invoiceNum = 0;

            do
            {
                do
                {
                    Clear();
                    do
                    { // check if full name is longer than 5 characters
                        passenger.fullName[pasNum] = getInput("full name", cusNum + 1);

                        if (passenger.fullName[pasNum].Length > 5)
                        { //number of characters greater than 5 then display message
                            WriteLine("\nWarning! Passenger names shouldn't longer than 5 characters," +
                                        "\nit will be trimmed automatically by system.");
                            Write("\npress any key to continue or 999 to re-enter >>");
                            passenger.trimName(pasNum); // trim the string which is greater than 5 character
                        }
                        else
                        {
                            while (passenger.fullName[pasNum] == "")
                            { // if the string has no value
                                WriteLine("Plz enter a 5 characters name\n");
                                passenger.fullName[pasNum] = getInput("full name", cusNum + 1);
                            }
                            break; // if the number of characters is less than 5
                        }
                    } while (ReadLine() == EXIT);

                    Clear();

                    if (isFull(passenger, pasNum)) break; // check if all the seats have been occupied

                    do
                    { 
                        if (checkSeat(passenger, ref pasNum, ref cusNum) != true) continue;
                        else break;
                    } while (checkSeat(passenger, ref pasNum, ref cusNum) != true);

                    Write("\nContinue to key-in: Any key\n" +
                        $"Finish your invoice: {EXIT}\n" +
                            "See the available seats: 000 \n>>");

                    if (ReadLine() == EXIT2)
                    {
                        Write("Available:");
                        passenger.disAvaiSeat(); // display the empty seats
                        WriteLine("\nTo continue book seats press any key\n" +
                            $"To finish, press {EXIT} >>");
                    }

                } while (ReadLine() != EXIT);

                if (isFull(passenger, pasNum)) break; // check if all the seats have been occupied

                Clear();
                displayInvoice(passenger, ref cusNum, ref pasNum, invoiceNum); // display a list of customers
                invoiceNum = pasNum;

                Write("\n\nPress any key to change consumer\n" +
                    $"or press {EXIT} to finish purchasing >>");
            } while (ReadLine() != EXIT);

            passenger.displayPassenger(pasNum); // display all the passengers
        }

        public static string getInput(string value, int i)
        { // accept the input value then give the output string
            Write($"Passenger #{i} , Plz enter your {value} >>");
            return ReadLine();
        }

        public static bool checkSeat(Passenger passenger, ref int seatNum, ref int cusNum)
        { // check if all of the seats have been taken
            const int min = 1;
            const int max = 40;
            bool check = true; // if the format of the value is not correct, return a FALSE value
            string message = "";

            bool seatType = int.TryParse(getInput("seat number", cusNum + 1), out passenger.seatNumber[seatNum]);

            if (!seatType)
            { // check type
                message = "Invalid input type";
                check = false;
            }
            else if (passenger.seatNumber[seatNum] < min || passenger.seatNumber[seatNum] > max)
            { // check the value is in the correct range
                message = "Invalid input value";
                check = false;
            }

            if (message != "")
            { // display the message only when there's an error
                WriteLine($"{message}.\n" +
                    $"Seat number should be a number\n" +
                        $"and should not greater than {max}\n");
            }
            else
            {
                passenger.seatNumber[seatNum] = isSeatExist(passenger, seatNum, cusNum);
                passenger.deparTime[seatNum] = DateTime.Now.ToString("dddd., dd MMM yyyy");
                seatNum++;
                cusNum++;
            }
            return check;
        }

        public static bool isFull(Passenger passenger, int seatNum)
        { // check if all the seats have been occupied
            bool full = false;
            if (seatNum == passenger.fullName.Length)
            {
                WriteLine("Sorry, all seats have been taken");
                full = true;
            }
            return full;
        }

        public static int isSeatExist(Passenger passenger, int pasNum, int cusNum)
        { // check if the seat has been chosen
            for (int i = 0; i < pasNum; i++)
            {
                while (passenger.seatNumber[pasNum] == passenger.seatNumber[i])
                {
                    WriteLine("This seat has been taken, plz try another seat number");
                    checkSeat(passenger, ref passenger.seatNumber[pasNum], ref cusNum);
                }
            }
            return passenger.seatNumber[pasNum];
        }

        public static void displayInvoice(Passenger passenger, ref int cusNum, ref int pasNum, int invoiceNum)
        { // display the invoice of a customer
            Clear();
            WriteLine("------ INVOICE START ------\n");
            for (int i = invoiceNum; i < pasNum; i++)
            {
                WriteLine($"Passenger #{i + 1}\n" +
                    $"\tFull Name: {passenger.fullName[i].ToUpper()}\n" +
                    $"\tSeat Number: {passenger.seatNumber[i]}\n");
            }
            WriteLine("------ INVOICE FINISH ------\n");
            cusNum = 0; // the number should be zero after print out the invoice
        }

    }
}