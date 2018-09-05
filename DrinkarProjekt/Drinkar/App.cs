﻿using Drinkar.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Drinkar
{
    public class App
    {
        public void Run()
        {
            PageMainMenu();
        }

        private void PageMainMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome!\nAre you thirsty?    Of course you are!\n");
            Console.WriteLine("Let us recommend you a cocktail depending on what you have!");
            Console.Write("Enter ingredients separated with a comma(,): ");
            string input = Console.ReadLine(); ////List<string> usersIngredients = new List<string>();
            Console.WriteLine("Nice! You can make following drinks : ");

            ShowAllMatchedDrinks(matched);
            if (matched != 0)
            {
                ChooseDrink();
                Console.ReadKey();
                PageMainMenu();
            }

            else
                Console.WriteLine("Go to SystemBolaget..");

        }

        private void ChooseDrink()
        {
            Console.WriteLine("Can you decide now which cocktail do you want to make, if so press (a). Overwise, press (b) for getting some advice.");
            string response = Console.ReadLine();
            if (response == "a")
            {
                Console.WriteLine("Choose the number of a cocktail you want to make:");
                int answer = int.Parse(Console.ReadLine());

                var dataAccess = new DataAccess();

                Drink matchedDrinks = dataAccess.GetCocktailNameById(answer);

                Console.WriteLine(dataAccess.GetCocktailNameById(description));
            }
            else if (response == "b")
            {
                Console.WriteLine("What kind of drink do you want? /n(a) Sweet /n(b) Sour /n(c) Sexy ");
                string cocktailKind = Console.ReadLine();
                if (cocktailKind == "a")
                    Console.WriteLine(DataAccess.GetCocktailByKind(sweet));
                else if (cocktailKind == "b")
                    Console.WriteLine(DataAccess.GetCocktailByKind(sour));
                else if (cocktailKind == "c")
                    Console.WriteLine(DataAccess.GetCocktailByKind(sexy));
                else
                {
                    Console.Clear();
                    ChooseDrink();
                }
            }
            else
            Console.Clear();
            PageMainMenu();
        }

        private void ShowAllMatchedDrinks()
        {
            Console.Clear();

            var dataAccess = new DataAccess();
            List<Drink> matched = dataAccess.GetAllMatchedCocktails();

            foreach (var drink in matched)
            {
                Console.WriteLine(drink.Id + " " + drink.Name + " " + drink.Description);
            }

        }

    }
}