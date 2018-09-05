using System;
using System.Collections.Generic;
using System.Text;

namespace Drinkar
{
   public  class App
    {
        public void Run()
        {
            List<string> usersIngredients = new List<string>();
            Console.WriteLine("Welcome!\nAre you thirsty?    Of course you are!\nLet us recommend you a cocktail depending on what you have!");
            Console.Write("Enter ingredients separated with a comma(,): ");
            string input = Console.ReadLine();

            Console.WriteLine("Hello from Anastasia");
        }
        
    }
}
