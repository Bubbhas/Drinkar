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


        internal List<Drink> GetAllDrinksWithIngredient(List<int> ingredientId)
        {

            string s = string.Join(",", ingredientId);

            string sql = @"Select Drink.Id, Drink.Name, Drink.Description
                           From Drink
                           Left Join IngredientToDrink ON Drink.ID = IngredientToDrink.DrinkId
                           Left Join Ingredient ON Ingredient.Id = IngredientToDrink.IngredientId
                           Where ID IN (" + s + ")";

            var result = new List<Drink>();

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetSqlInt32(0).Value;
                    string name = reader.GetSqlString(1).Value;
                    string description = reader.GetSqlString(2).Value;

                    var drink = new Drink();
                    drink.Id = id;
                    drink.Name = name;
                    drink.Description = description;

                    result.Add(drink);
                }
                return result;
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
