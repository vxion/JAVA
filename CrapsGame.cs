using System;
using System.Collections.Generic;

namespace gameofCrap
{
    class Program
    {
        //class variable to ensure random numbers appear when finding random dice values
        static Random randomNumbers = new Random();

        //declaring class variables for end game statistics
      
        static int highestNoOfRolls = 1;
        static int lowestNoOfRolls = 0;
        static int mostCommonRoll = 0;
        static int noOfWins = 0;
        static int noOfLosses = 0;

        static void Main(string[] args)
        {
            DisplayIntro();
        }

        private static void DisplayIntro()
        {
            Console.WriteLine("\nThe Game Craps\n");

            Console.WriteLine("How to play Craps:\n");

            Console.WriteLine("You enter the number of games you want to play.");
            Console.WriteLine("In each game, the shooter rolls two dice.");
            Console.WriteLine("If the numbers on the dice add up to 2, 3 or 12, the shooter looses.");
            Console.WriteLine("If the numbers on the dice add up to 7 or 11, the shooter wins.");
            Console.WriteLine("If the numbers on the dice add up to 4, 5, 6, 8, 9 or 10, that sets the points.");
            Console.WriteLine("In the latter case, the shooter continues to roll until the numbers match the points, then the shooter wins.");
            Console.WriteLine("Or, if a 7 is rolled, the shooter looses.\n");

            //call on method to commence asking the number of games a player would like to play
            ProcessNumberofGames();
        }

        private static void ProcessNumberofGames()
        {
            int numberOfGames = 0;
            string continueGame = "";
            Console.WriteLine("How many games of Craps do you want to play?");

            try
            {
                numberOfGames = Int32.Parse(Console.ReadLine());

                if (numberOfGames <= 0)
                {
                    NumberofGamesEntryError();
                }
                else
                {
                    PlayGame(numberOfGames);
                    Console.WriteLine();
                    Console.WriteLine("Press 'y' to continue playing");
                    continueGame = Console.ReadLine();
                    if (continueGame == "y" || continueGame == "Y")
                    {
                        ClearVariableValuesForNewGame();
                        DisplayIntro();
                    }
                    else
                    {
                        Console.WriteLine("Exiting.");
                        Console.ReadKey();
                    }
                }
            }
            catch
            {
                NumberofGamesEntryError();
            }
        }

        private static void NumberofGamesEntryError()
        {
            string agreeToContinue = "";
            Console.WriteLine("That entry was not recognized. Press 'y' if you wish to continue, or press any other key to exit.");
            agreeToContinue = Console.ReadLine();
            if (agreeToContinue == "y" || agreeToContinue == "Y")
                DisplayIntro();
            else
            {
                Console.WriteLine("Exiting");
                Console.ReadKey();
            }
        }

        //commence the actual game
        private static void PlayGame(int numberOfGames)
        {
            int dice1 = 0;
            int dice2 = 0;
            int sum = 0;

            for (int i = 0; i < numberOfGames; i++)
            {
                //a blank line for display purposes
                Console.WriteLine();

                dice1 = RollDice();
                dice2 = RollDice();

                //sum of dice1 and dice2
                sum = dice1 + dice2;
              
                Console.WriteLine("For round " + (i + 1) + ", the first dice has the value: " + dice1 + " + " + dice2 + " : "+sum);

                //process the dice results
                ProcessDiceResults(sum);

                //display newline
                Console.WriteLine();
            }
            DisplayEndGameStatistics();
        }

        //process dice results by calling on relevant method using a switch
        private static void ProcessDiceResults(int sum)
        {
            switch (sum)
            {
                //if the sum of the dice1 and dice2 is 2,3,12 shooter looses
                case 2:
                case 3:
                case 12:
                    ShooterLoses();
                    break;
                //if the sum of the dice1 and dice2 is 7,11 shooter wins
                case 7:
                case 11:
                    ShooterWins();
                    break;
                //if the sum of the dice1 and dice2 is 4,5,6,8,9,10 pointround
                case 4:
                case 5:
                case 6:
                case 8:
                case 9:
                case 10:
                    PointsRound(sum);
                    break;
                default:
                    break;
            }
        }

        //shooter loses method
        private static void ShooterLoses()
        {
            Console.WriteLine("Sorry, shooter looses this round.");
            //calculation for end of game statistic
            NoOfLosses();
        }
        //shooter wins method
        private static void ShooterWins()
        {
            Console.WriteLine("Shooter wins this round!");
            //calculation for end of game statistic
            NoOfWins();
        }

        //point round method
        private static void PointsRound(int sum)
        {
            //set the pointsum to zero dice1 and dice is zero, round counter to zero
            int pointSum = 0;
            int dice1 = 0;
            int dice2 = 0;
            int roundsCounter = 1;
            Boolean matchfound = false;

            Console.WriteLine("Commencing points round: Rolling again Shooter wins the round if the dice sum matches the points value " + sum + " before a 7 is rolled. Otherwise, the shooter loses.");
            Console.WriteLine("");

            //match found loop untill matchfound set to true
            while (matchfound == false)
            {
                Console.WriteLine();
                dice1 = RollDice(); //set the dice1 value
                dice2 = RollDice(); //set the dice2 value
                Console.WriteLine("Points round:");
                Console.WriteLine("The first dice has the value: " + dice1 + " + " + dice2 + ".");

                //adding the dice1 and dice2
                pointSum = dice1 + dice2;
                //chek if the point is 7 shooter loses
                if (pointSum == 7)
                {
                    Console.WriteLine("This gives the sum of: " + pointSum);
                    ShooterLoses();
                    matchfound = true;
                }
                //check if the point is sum value shooter wins
                else if (pointSum == sum)
                {
                    Console.WriteLine("This gives the sum of: " + pointSum);
                    ShooterWins();
                    matchfound = true;
                }
                else
                    Console.WriteLine("This gives the sum of: " + pointSum + ". This does not match the points value " + sum + " or a 7. Rolling again.");
                Console.ReadLine();

                //collect and manipulate data for end of game statistics
                roundsCounter++;
            }

          
        }

        //set the all varaible to 0 or default value initialize to the game
        public static void ClearVariableValuesForNewGame()
        {
            highestNoOfRolls = 1;
            lowestNoOfRolls = 0;
            mostCommonRoll = 0;
            noOfWins = 0;
            noOfLosses = 0;

        }
        //roll dice
        static int RollDice()
        {
            int dice = 0;
            dice = randomNumbers.Next(1, 7); //random number from 1 to 7
            return dice;
        }

        //no of wins method
        static void NoOfWins()
        {
            //count the number of wins
            noOfWins = noOfWins + 1;
        }

        //no of losses
        static void NoOfLosses()
        {
            //counts the no of loses
            noOfLosses = noOfLosses + 1;
        }

        //game statisrics method
        static void DisplayEndGameStatistics()
        {
            //prints the game statistics
            Console.WriteLine("The highest number of rolls was: " + highestNoOfRolls);
            Console.WriteLine("The lowest number of rolls was: " + lowestNoOfRolls);
            Console.WriteLine("The most common roll was: " + mostCommonRoll);
            Console.WriteLine("Total games you won: " + noOfWins);
            Console.WriteLine("Total games you lost: " + noOfLosses);
        }
    }
}

