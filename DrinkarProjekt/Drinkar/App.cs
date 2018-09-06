using Drinkar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drinkar
{
    public class App
    {
        DataAccess dataAcccess = new DataAccess();
        public void Run()
        {
            WelcomeText();
            PageMainMenu();
        }
        private void PageMainMenu()
        {
            Console.Clear();
            ShowAppLogo();
            WhiteCenterText("Vad kan vi göra för dig?\n");

            CenterText("A) Visa alla drinkar");
            CenterText("B) Visa alla kategorier");
            CenterText("C) Generera drinkar");
            CenterText("D) Visa alla drink-kategorier");

            ConsoleKey command = Console.ReadKey().Key;
            switch (command)
            {
                case ConsoleKey.A: /*ShowAllDrinks()*/; break;
                case ConsoleKey.B: /*ShowAllCategories()*/; break;
                case ConsoleKey.C: ShowAllDrinksThatMatchesIngredient()/*ShowAllMatchedDrinks()*/; break;
                default: RedCenterText("Du verkar redan ha druckit en hel del...Tryck valfri knapp för att göra ett nytt försök"); Console.ReadKey(); PageMainMenu(); break;
            }

            
            //CenterText("Låt oss rekommendera en drink utifrån vad du har hemma!");
            //CenterTextWithoutNewLine("Skriv in ingredienser separerade med ett kommatecken(,): ");
            //string line = Console.ReadLine();


            ////string line = "Ljus rom, Sockerlag";
            //var input = line.Split(','); //new string[] { "Ljus rom", "Sockerlag", "Kuku" };
            //int ss = input.Length;


            ////List<string> usersIngredients = new List<string>();
            //CenterText("Nice! Du kan göra följande drinkar : ");


            //List<Drink> matched = ShowAllMatchedDrinks(;
            //if (matched.Count != 0)
            //{
            //    ChooseDrink();
            //    Console.ReadKey();
            //    PageMainMenu();
            //}

            //else
            //    Console.WriteLine("Go to SystemBolaget..");

        }
        private void ShowAllDrinksThatMatchesIngredient()
        {
            Console.Clear();
            ShowAppLogo();
            CenterText("Låt oss rekommendera en drink utifrån vad du har hemma!");
            CenterTextWithoutNewLine("Skriv in ingredienser separerade med ett kommatecken(,): ");
            string line = Console.ReadLine();
            List<string> i = line.Split(',').ToList();

            List<Drink> allDrink = dataAcccess.GetAllDrinksWithIngredient(i);
            Console.WriteLine();
            CenterText("Drinkar som du kan skapa är:");
            Console.WriteLine();
            Console.WriteLine(" ID".PadLeft(Console.WindowWidth / 2 - 10) + "".PadRight(10) + "Namn");
            Console.WriteLine();
            foreach (Drink bp in allDrink)
            {
                Console.WriteLine(string.Join(",".PadRight(10), bp.Id.ToString().PadLeft(Console.WindowWidth / 2 - 10), bp.Name.PadLeft(10)));
            }
        }

        private void ShowDrinkRecipe()
        {
            Console.Clear();
            Drink drink = dataAcccess.GetDrinkRecipe(1);
            Console.WriteLine(drink.Name + "  " + drink.Measure);
            foreach (var item in drink.Ingredient)
            {
                Console.WriteLine(item);
            }
        }

        void ShowAllCategories()
        {
            List<Category> listOfCategories = dataAcccess.GetAllCategories();
            foreach (var item in listOfCategories)
            {
                Console.WriteLine(item);
            }
        }

        void ShowAllDrinks()
        {
            List<Drink> alladrinkar = dataAcccess.GetAllDrinks();
            foreach (var item in alladrinkar)
            {
                Console.WriteLine(item.Name);
            }
        }

        private void RedCenterText(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + s.Length / 2) + "}", s);
            Console.ResetColor();
        }

        private void WelcomeText()
        {
            ShowAppLogo();

            WhiteCenterText("Välkommen!\n\n");
            CenterText("Är du törstig?\n");
            CenterText("Klart du är!\n");
            Console.ReadKey();
        }

        private static void ShowAppLogo()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            CenterText("*********************************");
            Console.WriteLine();
            CenterText("DrinkMaker");
            Console.WriteLine();
            CenterText("*********************************");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
        }

        private void WhiteCenterText(string s)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + s.Length / 2) + "}", s);
            Console.ResetColor();
        }

        private static void CenterText(string s)
        {
            Console.WriteLine("{0," + ((Console.WindowWidth / 2) + s.Length / 2) + "}", s);
        }
        private static void CenterTextWithoutNewLine(string s)
        {
            Console.Write("{0," + ((Console.WindowWidth / 2) + s.Length / 2) + "}", s);
        }

        private void ChooseDrink()
        {
            Console.WriteLine("Can you decide now which cocktail do you want to make, if so press (a). Overwise, press (b) for getting some advice.");
            string response = Console.ReadLine();
            if (response == "a")
            {
                Console.WriteLine("Choose the number of a cocktail you want to make:");
                int answer = int.Parse(Console.ReadLine());

                //var dataAccess = new DataAccess();

                //Drink matchedDrinks = dataAccess.GetCocktailNameById(answer);

                //Console.WriteLine(dataAccess.GetCocktailNameById(description));
            }
            else if (response == "b")
            {
                Console.WriteLine("What kind of drink do you want? /n(a) Sweet /n(b) Sour /n(c) Extra alcohol ");
                string cocktailKind = Console.ReadLine();
                //if (cocktailKind == "a")
                //    Console.WriteLine(DataAccess.GetCocktailByKind(sweet));
                //else if (cocktailKind == "b")
                //    Console.WriteLine(DataAccess.GetCocktailByKind(sour));
                //else if (cocktailKind == "c")
                //    Console.WriteLine(DataAccess.GetCocktailByKind());extraAlcohol
                //else
                //{
                //    Console.Clear();
                //    ChooseDrink();
                //}
            }
            else
            Console.Clear();
            PageMainMenu();
        }

        //private List<Drink> ShowAllMatchedDrinks(string[] ingrediences)
        //{
        //    Console.Clear();

        //    var dataAccess = new DataAccess();
        //    List<Drink> matched = dataAccess.GetAllDrinksWithIngredient(ingrediences, ingrediences.Length);

        //    foreach (var drink in matched)
        //    {
        //        Console.WriteLine(drink.Id + " " + drink.Name + " " + drink.Description);
        //    }
        //    return matched;
        //}

    }
}
