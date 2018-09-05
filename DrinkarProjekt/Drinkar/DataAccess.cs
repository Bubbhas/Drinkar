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

        internal void CreateDrink(string name, string description)
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
