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
            CategoryController get_all_categories = new CategoryController();


        string results = get_all_categories.getAllCategories();

            string expected_results =
                "ID: 1003, Name: board games, Description: Two player games on the board\n";

            Assert.Equal(expected_results, results);
        }

        // Test 2: Get category by ID
        [Fact]
        public void test_CategoryGetById()
        {
            CategoryController get_category = new CategoryController();

            string results = get_category.getCategory(1003);

            string expected_results =
                "ID: 1003, Name: board games, Description: Two player games on the board";

            Assert.Equal(expected_results, results);
        }

        // Test 3: Add category
        [Fact]
        public void test_CategoryAdd()
        {
            CategoryController add_category = new CategoryController();

            string results = add_category.addCategory(
                "Clothing",
                "Clothing products"
            );

            Assert.Equal("Category added successfully.", results);
        }

        // Test 4: Get a category that does not exist
        [Fact]
        public void test_CategoryGetInvalidId()
        {
            CategoryController get_category = new CategoryController();

            string results = get_category.getCategory(-1);

            Assert.Equal("Category not found.", results);
        }

        // Test 5: Delete a category that does not exist
        [Fact]
        public void test_CategoryDeleteInvalidId()
        {
            CategoryController delete_category = new CategoryController();

            string results = delete_category.deleteCategory(-1);

            Assert.Equal("Category not found.", results);
        }
    }


}
