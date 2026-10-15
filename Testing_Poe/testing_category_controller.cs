
using Xunit;
using poepart2.Controllers;

namespace Testing_Poe
{
    public class testing_category_controller
    {
        // Test 1: Get all categories
        [Fact]
        public void test_CategoryGetAll()
        {
            CategoryController controller = new CategoryController();

            string results = controller.getAllCategories();

            Assert.Contains(
                "ID: 1003, Name: board games, Description: Two player games on the board",
                results
            );

            Assert.Contains(
                "ID: 2002, Name: Clothing, Description: Clothing products",
                results
            );
        }

        // Test 2: Get category by ID
        [Fact]
        public void test_CategoryGetById()
        {
            CategoryController controller = new CategoryController();

            string results = controller.getCategory(1003);

            Assert.Equal(
                "ID: 1003, Name: board games, Description: Two player games on the board",
                results
            );
        }

        // Test 3: Get an existing Clothing category
        [Fact]
        public void test_CategoryGetClothing()
        {
            CategoryController controller = new CategoryController();

            string results = controller.getCategory(2002);

            Assert.Equal(
                "ID: 2002, Name: Clothing, Description: Clothing products",
                results
            );
        }

        // Test 4: Get a category that does not exist
        [Fact]
        public void test_CategoryGetInvalidId()
        {
            CategoryController controller = new CategoryController();

            string results = controller.getCategory(-1);

            Assert.Equal("Category not found.", results);
        }

        // Test 5: Delete a category that does not exist
        [Fact]
        public void test_CategoryDeleteInvalidId()
        {
            CategoryController controller = new CategoryController();

            string results = controller.deleteCategory(-1);

            Assert.Equal("Category not found.", results);
        }

        // Test 6: Update a category that does not exist
        [Fact]
        public void test_CategoryUpdateInvalidId()
        {
            CategoryController controller = new CategoryController();

            string results = controller.updateCategory(
                -1,
                "Updated Test Category",
                "Updated test description"
            );

            Assert.Equal("Category not found.", results);
        }
    }
}

