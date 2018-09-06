using Drinkar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Drinkar
{
    public class App
    {
        DataAccess dataAccess = new DataAccess();
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

            ConsoleKey command = Console.ReadKey().Key;
            switch (command)
            {
                case ConsoleKey.A: ShowAllDrinks(); break;
                case ConsoleKey.B: ShowAllCategories(); break;
                case ConsoleKey.C: ShowAllDrinksThatMatchesIngredient()/*ShowAllMatchedDrinks()*/; break;
                default: RedCenterText("Du verkar redan ha druckit en hel del...Tryck valfri knapp för att göra ett nytt försök"); Console.ReadKey(); PageMainMenu(); break;
            }
        }

        private void ShowDrinksByCategory(int input)
        {
            Console.Clear();
            ShowAppLogo();
            List<Drink> listOfDrinksCategory = dataAccess.GetAllDrinksByCategoryId(input);

            Console.WriteLine(); //namn på kategori

            foreach (var x in listOfDrinksCategory)
            {
                Console.WriteLine(x.Id + " " + x.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("Välj nummer på den drink du vill se receptet på");
            ShowDrinkRecipe(int.Parse(Console.ReadLine()));
        }

        private void ShowAllDrinksThatMatchesIngredient()
        {
            Console.Clear();
            ShowAppLogo();
            CenterText("Låt oss rekommendera en drink utifrån vad du har hemma!");
            CenterTextWithoutNewLine("Skriv in ingredienser separerade med ett kommatecken(,): ");
            string line = Console.ReadLine();
            List<string> i = line.Split(',').ToList();

            List<Drink> allDrink = dataAccess.GetAllDrinksWithIngredient(i);
            Console.WriteLine();
            CenterText("Drinkar som du kan skapa är:");
            Console.WriteLine();
            Console.WriteLine(" ID".PadLeft(Console.WindowWidth / 2 - 10) + "".PadRight(10) + "Namn");
            Console.WriteLine();
            foreach (Drink bp in allDrink)
            {
                Console.WriteLine(string.Join(",".PadRight(10), bp.Id.ToString().PadLeft(Console.WindowWidth / 2 - 10), bp.Name.PadLeft(10)));
            }
            Console.WriteLine("");
            Console.WriteLine("Välj den drink du vill se recept på");
            ShowDrinkRecipe(int.Parse(Console.ReadLine()));
        }

        private void ShowDrinkRecipe(int input)
        {
            Console.Clear();
            Drink drink = dataAccess.GetDrinkRecipe(input);
            Console.WriteLine(drink.Name);

            for (int i = 0; i < drink.Ingredient.Count; i++)
            {
                Console.WriteLine($"{drink.Ingredient[i]}  {drink.MeasuresOfIngredients[i].ToString()} cl");
            }
            Console.WriteLine("");
            Console.WriteLine("Välj den drink du vill se recept på");
            
            ShowDrinkRecipe(int.Parse(Console.ReadLine()));
        }

        void ShowAllCategories()
        {
            Console.Clear();
            ShowAppLogo();
            List<Category> listOfCategories = dataAccess.GetAllCategories();
            foreach (var item in listOfCategories)
            {
                Console.WriteLine($"{item.Id.ToString()} {item.Name}");
            }
            Console.WriteLine("Skriv in siffran på den kategori där du vill se drinkarna");
            ShowDrinksByCategory(int.Parse(Console.ReadLine()));
        }

        void ShowAllDrinks()
        {
            Console.Clear();
            ShowAppLogo();
            WhiteCenterText("Alla drinkar\n");

            List<Drink> alladrinkar = dataAccess.GetAllDrinks();
            foreach (var item in alladrinkar)
            {
                CenterText($"{item.Id} {item.Name}");
            }
            Console.WriteLine("Välj den drink du vill se recept på");
            ShowDrinkRecipe(int.Parse(Console.ReadLine()));
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
