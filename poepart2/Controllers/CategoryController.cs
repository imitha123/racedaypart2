using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // Get all Categories
        [HttpGet]
        public string getAllCategories()
        {
            string message = "";

            try
            {
                string connectionString = @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Category";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);

                    message += $"ID: {id}, Name: {name}, Description: {description}\n";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Get category by id
        [HttpGet("{id}")]
        public string getCategory(int id)
        {
            string message = "";

            try
            {
                string connectionString = @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Category WHERE categoryId = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int categoryId = reader.GetInt32(0);
                    string categoryName = reader.GetString(1);
                    string categoryDescription = reader.GetString(2);

                    message = $"ID: {categoryId}, Name: {categoryName}, Description: {categoryDescription}";
                }
                else
                {
                    message = "Category not found.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Add category
        [HttpPost]
        public string addCategory(string name, string description)
        {
            string message = "";

            try
            {
                string connectionString = @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO Category (categoryName, categoryDescription) VALUES (@name, @description)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Category added successfully.";
                }
                else
                {
                    message = "Failed to add category.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Delete category
        [HttpDelete("{id}")]
        public string deleteCategory(int id)
        {
            string message = "";

            try
            {
                string connectionString = @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM Category WHERE categoryId = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Category deleted successfully.";
                }
                else
                {
                    message = "Failed to delete category.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // Update category
        [HttpPut("{id}")]
        public string updateCategory(int id, string name, string description)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE Category " +
                               "SET categoryName = @name, categoryDescription = @description " +
                               "WHERE categoryID = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Category updated successfully.";
                }
                else
                {
                    message = "Category not found.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }

}
