using System;
using static System.Console;

namespace Deliverable3
{
    public class Passenger
    {
        private int[] _SeatNumber;
        private string[] _FullName;
        private int[] _SecurityNum;
        private string[] _DeparTime;

        public string[] fullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public int[] seatNumber
        {
            get { return _SeatNumber; }
            set { _SeatNumber = value; }
        }

        private int[] securityNum
        {
            get
            {
                randomNum();
                return _SecurityNum;
            }
        }

        public string[] deparTime
        {
            get { return _DeparTime; }
            set { _DeparTime = value; }
        }

        private int[] randomNum()
        {
            const int min = 30000;
            const int max = 999999;
            
            for (int i = 0; i < _FullName.Length; i++)
            {
                _SecurityNum[i] = new Random().Next(min, max);
            }
            return _SecurityNum;
        }

        public string trimName(int value)
        {
            if (_FullName[value].Length > 5)
            {
                _FullName[value] = _FullName[value].Remove(5);
            }
            return _FullName[value];
        }

        public void disAvaiSeat()
        {
            bool check = false;
            for (int i = 1; i <= 40; i++)
            {
                check = false;
                for (int j = 0; j < _SeatNumber.Length; j++)
                {
                    if (i == _SeatNumber[j])
                    {
                        check = true;
                    }
                }
                if (!check)
                {
                    Write($"{i,4}");
                }
            }
        }

        public void displayPassenger(int pasNum)
        { // display the final list of passengers
            Clear();
            WriteLine("Welcome to ARASH AIRLINES, Here's your boarding passes\n");
            for (int i = 0; i < pasNum; i++)
            {
                WriteLine("\nBoarding pass\tYour flight\tSecurity Number");
                WriteLine($"TW -> BNE\tQA77\t\t{securityNum[i]}\n");
                WriteLine($"********************** Passenger #{i + 1} **********************");
                WriteLine("Passenger\t\t\tDate");
                WriteLine($"{_FullName[i].ToUpper()}\t\t\t\t{_DeparTime[i]}");
                WriteLine("Flight Number\t\t\tDeparture Gate");
                WriteLine($"QA 77\t\t\t\tSFO/T2");
                WriteLine("Where's my seat?");
                WriteLine($"{_SeatNumber[i]}");
                WriteLine("********************* ARASH AIRLINES *********************\n");
            }
        }

        public Passenger(int ArraySize)
        {
            _FullName = new string[ArraySize];
            _SeatNumber = new int[ArraySize];
            _SecurityNum = new int[ArraySize];
            _DeparTime = new string[ArraySize];
        }
    }
}
