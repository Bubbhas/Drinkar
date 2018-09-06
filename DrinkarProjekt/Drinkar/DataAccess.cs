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

                List<string> fixedList = new List<string>();

                foreach (var item in i)
                {
                    var trimmed = item.Trim();
                    var newString = trimmed.First().ToString().ToUpper() + String.Join("", trimmed.Skip(1)).ToLower();

                    fixedList.Add(newString);
                }

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

                    fixedList.Intersect(ingredients).Count();

                    if (fixedList.Intersect(ingredients).Count() == ingredients.Count())
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

        internal List<Category> GetAllCategories()
        {
            string sql = @"select distinct Category.Name as Kategori
                            from DrinkToCategory
                            join Drink on Drink.Id=DrinkToCategory.DrinkId
                            join Category on Category.Id=DrinkToCategory.CategoryId";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Category> listOfCategories = new List<Category>();

                while (reader.Read())
                {
                    Category cat = new Category();
                    cat.Name = reader.GetSqlString(0).Value;
                    listOfCategories.Add(cat);
                }
                return listOfCategories;
            }
        }

        public List<Drink> GetAllDrinks()
        {
            string sql = @"SELECT Drink.Id, Drink.Name
                           from drink";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Drink> listOfDrinks = new List<Drink>();

                while (reader.Read())
                {
                    int id = reader.GetSqlInt32(0).Value;
                    string name = reader.GetSqlString(1).Value;

                    var drink = new Drink();
                    drink.Id = id;
                    drink.Name = name;

                    listOfDrinks.Add(drink);

                }
                return listOfDrinks;
            }
        }

        internal Drink GetDrinkRecipe(int IdOfDrink)
        {
            string sql = @"Select Drink.Id, Drink.Name, Drink.Description, Drink.Glass, Ingredient.Name, IngredientToDrink.MeasureOfDrink
                            From Drink
                            join IngredientToDrink on drink.id = IngredientToDrink.DrinkId
                            join Ingredient on IngredientToDrink.IngredientId = Ingredient.Id
                            Where Drink.Id = @Id
                            ";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", IdOfDrink));
                SqlDataReader reader = command.ExecuteReader();

                var drink = new Drink();

                while (reader.Read())
                {
                    int id = reader.GetSqlInt32(0).Value;

                    string name = reader.GetSqlString(1).Value;
                    string description = reader.GetSqlString(2).Value;
                    decimal measure = reader.GetSqlDecimal(5).Value;


                    drink.Id = id;
                    drink.Name = name;
                    drink.Description = description;
                    drink.Measure = measure;

                    if (!reader.GetSqlString(4).IsNull)
                    {
                        drink.Ingredient.Add(reader.GetSqlString(4).Value);
                    }
                }
                return drink;
            }
        }
    }
}
