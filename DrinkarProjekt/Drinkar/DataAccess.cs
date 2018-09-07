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
        internal bool TestUnderNameAndPassWord(string email, string password)
        {
            bool existOrNot = false;
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand cmd = new SqlCommand(@"select count(*) 
                        From User1 
                        Where Email=@email AND Password=@password", connection))
            {
                connection.Open();

                cmd.Parameters.Add(new SqlParameter("@email", email));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    existOrNot = true;
                }

            }
            return existOrNot;
        }
        internal Drink GetDrinkInstructions(int sameInput)
        {
            string sql = @"Select Drink.Id, Drink.Name, Drink.Instructions
                            From Drink                            
                            Where Drink.Id = @Id
                            ";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Id", sameInput));
                SqlDataReader reader = command.ExecuteReader();

                var drink = new Drink();

                reader.Read();

                int id = reader.GetSqlInt32(0).Value;

                string name = reader.GetSqlString(1).Value;
                string instructions = reader.GetSqlString(2).Value;


                //drink.Id = id;
                //drink.Name = name;
                drink.Instructions = instructions;

                return drink;
            }
        }
        internal Drink GetRandomDrink(int drinkId)
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
                command.Parameters.Add(new SqlParameter("Id", drinkId));
                SqlDataReader reader = command.ExecuteReader();

                var drink = new Drink();

                reader.Read();
                int id = reader.GetSqlInt32(0).Value;

                string name = reader.GetSqlString(1).Value;
                string description = reader.GetSqlString(2).Value;


                drink.Id = id;
                drink.Name = name;
                drink.Description = description;


                return drink;
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
            string sql = @"select distinct Id, Name as Kategori
                            from Category";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                List<Category> listOfCategories = new List<Category>();

                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = reader.GetSqlInt32(0).Value;
                    category.Name = reader.GetSqlString(1).Value;
                    listOfCategories.Add(category);
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

        public List<Drink> GetAllDrinksByCategoryId(int categoryId)
        {
            string sql = @"select Drink.Id, Drink.Name
            from DrinkToCategory
            join Drink on Drink.Id = DrinkToCategory.DrinkId
            join Category on Category.Id = DrinkToCategory.CategoryId
            where Category.Id = @Id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("@Id", categoryId));

                SqlDataReader reader = command.ExecuteReader();
                var result = new List<Drink>();

                while (reader.Read())
                {

                    int drinkId = reader.GetSqlInt32(0).Value;
                    string drinkName = reader.GetSqlString(1).Value;

                    var newDrink = new Drink();

                    newDrink.Id = drinkId;
                    newDrink.Name = drinkName;

                    result.Add(newDrink);
                }
                return result;
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


                    drink.Id = id;
                    drink.Name = name;
                    drink.Description = description;

                    if (!reader.GetSqlString(4).IsNull)
                    {
                        drink.Ingredient.Add(reader.GetSqlString(4).Value);
                        drink.MeasuresOfIngredients.Add(reader.GetSqlDecimal(5).Value);
                    }
                }
                return drink;
            }
        }
    }
}
