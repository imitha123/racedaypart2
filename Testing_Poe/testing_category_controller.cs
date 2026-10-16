using Xunit;
using Microsoft.AspNetCore.Mvc;
using poepart2.Controllers;
using System.Text.Json;

namespace Testing_Poe
{
    public class testing_category_controller
    {
        // Test 1: Get all categories
        [Fact]
        public void test_CategoryGetAll()
        {
            CategoryController controller = new CategoryController();


        IActionResult result = controller.GetAllCategories();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("board games", json);
            Assert.Contains("Clothing", json);
        }

        // Test 2: Get category by ID
        [Fact]
        public void test_CategoryGetById()
        {
            CategoryController controller = new CategoryController();

            IActionResult result = controller.GetCategory(1003);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("board games", json);
            Assert.Contains("Two player games on the board", json);
        }

        // Test 3: Get an existing Clothing category
        [Fact]
        public void test_CategoryGetClothing()
        {
            CategoryController controller = new CategoryController();

            IActionResult result = controller.GetCategory(2002);

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(200, okResult.StatusCode);

            string json = JsonSerializer.Serialize(okResult.Value);

            Assert.Contains("Clothing", json);
            Assert.Contains("Clothing products", json);
        }

        // Test 4: Get a category that does not exist
        [Fact]
        public void test_CategoryGetInvalidId()
        {
            CategoryController controller = new CategoryController();

            IActionResult result = controller.GetCategory(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 5: Delete a category with an invalid ID
        [Fact]
        public void test_CategoryDeleteInvalidId()
        {
            CategoryController controller = new CategoryController();

            IActionResult result = controller.DeleteCategory(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Test 6: Update a category with an invalid ID
        [Fact]
        public void test_CategoryUpdateInvalidId()
        {
            CategoryController controller = new CategoryController();

            CategoryRequest request = new CategoryRequest
            {
                Name = "Updated Test Category",
                Description = "Updated test description"
            };

            IActionResult result = controller.UpdateCategory(-1, request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }


}
