using Drinkar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Drinkar
{
    public class App
    {
        DataAccess dataAccess = new DataAccess();
        public void Run()
        {
            //SetConsoleWindowToFullSize();
            WelcomeText();
           // ShowLogIn();
            //ShowCreateProfile();
            PageMainMenu();
        }

        private static void SetConsoleWindowToFullSize()
        {
            int largestWidth = Console.LargestWindowWidth;
            int largestHeight = Console.LargestWindowHeight;
            Console.SetWindowSize(largestWidth, largestHeight);
        }

        private void PageMainMenu()
        {
            Console.Clear();
            ShowAppLogo();
            WhiteCenterText("Vad kan vi göra för dig?\n");

            CenterText("A) Visa alla drinkar");
            CenterText("B) Visa alla kategorier");
            CenterText("C) Generera drinkar");
            CenterText("D) Slumpa fram en drink");

            ConsoleKey command = Console.ReadKey().Key;
            switch (command)
            {
                case ConsoleKey.A: ShowAllDrinks(); break;
                case ConsoleKey.B: ShowAllCategories(); break;
                case ConsoleKey.C: ShowAllDrinksThatMatchesIngredient()/*ShowAllMatchedDrinks()*/; break;
                case ConsoleKey.D: DrinkRandomizer(); break;
                default: RedCenterText("Du verkar redan ha druckit en hel del...Tryck valfri knapp för att göra ett nytt försök"); Console.ReadKey(); PageMainMenu(); break;
            }
        }
        private void ShowCreateProfile()
        {
            Console.WriteLine("Skapa ditt konto nu och få en cykel på köpet!");
            Console.WriteLine("Ange ditt önskade Användarnamn");
            string username = Console.ReadLine();
            string email = CheckValidationOnEmail();
            Console.WriteLine("Ange din adress");
            string address = Console.ReadLine();
            Console.WriteLine("Ange ditt lösenord");
            string password = Console.ReadLine();

            bool successfullCreation = dataAccess.CreateProfile(username, email, address, password);
            if (successfullCreation)
            {
                Console.Clear();
                Console.WriteLine("Ditt konto är skapat");
                Console.ReadKey();
                ShowLogIn();
            }
        }
        private string CheckValidationOnEmail()
        {
            Console.WriteLine("Ange din email");
            string email = Console.ReadLine();
            Regex regex = new Regex(@"^\w+@.+");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ange en giltligt email");
                Console.ForegroundColor = ConsoleColor.Gray;
                CheckValidationOnEmail();
            }
            return email;
        }
        private void DrinkRandomizer()
        {
            Console.Clear();
            ShowAppLogo();

            Random rnd = new Random();

            int drinkId = rnd.Next(1, 15);

            Drink drink = dataAccess.GetRandomDrink(drinkId);


            CenterText("Din drink blir.....\n");

            ShowDrinkRecipe(drink.Id);


            Console.ReadKey();
            PageMainMenu();
        }

        private void ShowDrinksByCategory(int input)
        {
            Console.Clear();
            ShowAppLogo();
            List<Drink> listOfDrinksCategory = dataAccess.GetAllDrinksByCategoryId(input);



            Console.WriteLine(); //namn på kategori

            foreach (var x in listOfDrinksCategory)
            {
                CenterText(x.Id + " " + x.Name);
            }
            Console.WriteLine("");
            WhiteCenterText("Välj nummer på den drink du vill se receptet på");
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
            Console.Clear();
            ShowDrinkRecipe(int.Parse(Console.ReadLine()));
        }

        private void ShowDrinkRecipe(int input)
        {
            
            Drink drink = dataAccess.GetDrinkRecipe(input);
            WhiteCenterText($"{drink.Name}\n");
            CenterText($"{drink.Description}\n");

            for (int i = 0; i < drink.Ingredient.Count; i++)
            {
                CenterText($"{drink.Ingredient[i]}  {drink.MeasuresOfIngredients[i].ToString()} cl");
            }
            Console.WriteLine();
            Console.ReadKey();
            //int sameInput = input;
            
            ShowDrinkInstructions(input);
            PageMainMenu();
            //return sameInput;
        }

        void ShowAllCategories()
        {
            Console.Clear();
            ShowAppLogo();
            List<Category> listOfCategories = dataAccess.GetAllCategories();

            WhiteCenterText("Kategorier:\n");

            foreach (var item in listOfCategories)
            {
                CenterText($"{item.Id.ToString()} {item.Name}");
            }
            Console.WriteLine();
            WhiteCenterTextWithoutNewLine("Skriv in siffran på den kategori där du vill se drinkarna: ");
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
            Console.WriteLine();
            WhiteCenterTextWithoutNewLine("Välj den drink du vill se recept på: ");
            int drinkId = int.Parse(Console.ReadLine());
            Console.Clear();
            ShowAppLogo();
            ShowDrinkRecipe(drinkId);
        }

        private void WhiteCenterTextWithoutNewLine(string s)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0," + ((Console.WindowWidth / 2) + s.Length / 2) + "}", s);
            Console.ResetColor();
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
            Console.CursorVisible = false;
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
        private void ShowDrinkInstructions(int input)
        {
            Console.WriteLine("Vill du sätta igång med drinkgörandet? (J)a eller (N)ej?");
            string aswer = Console.ReadLine().ToLower();

            if (aswer == "j")
            {
                Drink drink = dataAccess.GetDrinkInstructions(input);
                Console.WriteLine(drink.Instructions);

                Console.WriteLine("");
                Console.WriteLine("Nu du är guru på att göra drinkar!");
                PageMainMenu();
            }
            else

                PageMainMenu();
        }
        private void ShowLogIn()
        {
            Console.Clear();
            ShowAppLogo();
            WhiteCenterTextWithoutNewLine("Ange ditt användarnamn (email): ");
            string email = Console.ReadLine();
            string password = "";
            WhiteCenterTextWithoutNewLine("Ange ditt lösenord: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);
            bool access = dataAccess.TestUnderNameAndPassWord(email, password);
            if (access)
            {
                WhiteCenterText("Du är nu inloggad!");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                ShowAppLogo();
                RedCenterText("Felaktigt användarnamn eller lösenord");
                Console.ReadKey();
                ShowLogIn();
            }
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
