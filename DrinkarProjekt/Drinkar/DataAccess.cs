using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Drinkar.Domain;

namespace Drinkar
{
    public class DataAccess
    {
        string conString = @"Server=(localdb)\mssqllocaldb;Database=Drinks";


        internal List<Drink> GetAllDrinksWithIngredient(List<string> i)
        {

            string sql = @"SELECT Drink.Id, Drink.Name, Ingredient.Name
                           from drink
                            Left join IngredientToDrink on drink.id = IngredientToDrink.DrinkId
                            Left join Ingredient on IngredientToDrink.IngredientId = Ingredient.Id ";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var dic = new Dictionary<int, Drink>();
                List<Drink> matchingDrinks = new List<Drink>();
                while (reader.Read())
                {
                    int id = reader.GetSqlInt32(0).Value;
                    if (!dic.ContainsKey(id))
                    {
                        string name = reader.GetSqlString(1).Value;

                        var drink = new Drink();
                        drink.Id = id;
                        drink.Name = name;

                        dic.Add(id, drink);
                    }
                    if (!reader.GetSqlString(2).IsNull)
                    {
                        dic[id].Ingredient.Add(reader.GetSqlString(2).Value);
                    }
                }
                foreach (Drink drink in dic.Values)
                {
                    List<string> ingredients = drink.Ingredient;

                    i.Intersect(ingredients).Count();

                    if (i.Intersect(ingredients).Count() == ingredients.Count())
                    {
                        matchingDrinks.Add(drink);
                    }
                }
                return matchingDrinks;
            }
        }

        public void CreateDrink(string name, string description)
        {
            string sql = @"INSERT INTO Drink(Name, Description)
                            Values(@Name, @Description)";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("@Name", name));
                command.Parameters.Add(new SqlParameter("@Description", description));
                command.ExecuteNonQuery();
            }
        }

        internal void CreateIngredient(string name)
        {
            string sql = @"INSERT INTO Ingredient(Name)
                            Values(@Name)";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("@Name", name));
                command.ExecuteNonQuery();
            }
        }
    }
}
