using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly string connectionString =
        @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";


    // GET: api/Category
    // Get all categories
    [HttpGet]
        public string getAllCategories()
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM Category";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);

                    message +=
                        $"ID: {id}, Name: {name}, Description: {description}\n";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // GET: api/Category/1
        // Get category by ID
        [HttpGet("{id}")]
        public string getCategory(int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "SELECT * FROM Category WHERE categoryId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int categoryId = reader.GetInt32(0);
                    string categoryName = reader.GetString(1);
                    string categoryDescription = reader.GetString(2);

                    message =
                        $"ID: {categoryId}, Name: {categoryName}, Description: {categoryDescription}";
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

        // POST: api/Category
        // Add category
        [HttpPost]
        public string addCategory(string name, string description)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "INSERT INTO Category (categoryName, categoryDescription) " +
                    "VALUES (@name, @description)";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Category added successfully."
                    : "Failed to add category.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // DELETE: api/Category/1
        // Delete category
        [HttpDelete("{id}")]
        public string deleteCategory(int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "DELETE FROM Category WHERE categoryId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Category deleted successfully."
                    : "Category not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // PUT: api/Category/1
        // Update category
        [HttpPut("{id}")]
        public string updateCategory(
            int id,
            string name,
            string description)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "UPDATE Category " +
                    "SET categoryName = @name, " +
                    "categoryDescription = @description " +
                    "WHERE categoryID = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Category updated successfully."
                    : "Category not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }


}
